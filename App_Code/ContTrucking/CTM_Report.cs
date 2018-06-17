using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using C2;

/// <summary>
/// CTM_Report 的摘要说明
/// </summary>
public class CTMDocPrint
{
    public CTMDocPrint()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}


    #region P&L
    public static DataSet PrintCTMJob(string refN, string userId)
    {
        DataSet set = new DataSet();
        DataTable tab_mast = Pl_Mast(refN, userId);
        string refType = "CTM";
        DataTable tab_Inv = Pl_Inv(refN, refType);
        DataTable tab_Dn = Pl_Dn(refN, refType);
        DataTable tab_Cn = Pl_Cn(refN, refType);
        DataTable tab_Pl = Pl_Pl(refN, refType);
        DataTable tab_Vo = Pl_Vo(refN, refType);
        DataTable tab_Cost = Pl_Cost(refN, refType);

        tab_mast.TableName = "Mast";
        tab_Inv.TableName = "IV";
        tab_Cn.TableName = "CN";
        tab_Dn.TableName = "DN";
        tab_Pl.TableName = "PL";
        tab_Vo.TableName = "VO";
        tab_Cost.TableName = "COST";
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }

    private static DataTable Pl_Mast(string refN, string userId)
    {
        string sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,job.CreateBy as UserId,'SGD' as Currency,1 as ExRate,JobType as JobType
,dbo.fun_GetPartyName(job.ClientId) as LocalCust,dbo.fun_GetPartyName(job.ClientId) as Agent,'{1}' as Company,JOB.CarrierBkgNo as Obl
,job.Vessel as Ves,convert(nvarchar(10),job.EtaDate,103) as Eta,dbo.fun_GetPortName(job.Pol) as Pol, dbo.fun_GetPortName(job.Pod) as Pod,'' ContN
,det1.Qty,det1.PackageType as Pack,det1.wT as Wt,det1.M3 as M3,0 as TsM3,0 as LocalM3
,job.Remark as Rmk
,isnull(inv.IV,0) Rev1
,0 as Rev2
,isnull(inv.DN,0) Rev3
,isnull(inv.CN,0) Rev4
,isnull(inv.IV+inv.DN-inv.CN,0) as Rev
,ISNULL(pay.Cost1,0) as Cost1
,0 as Cost2
,0 Cost3
,ISNULL(pay.Cost1,0) as Cost
,isnull(inv.IV+inv.DN-inv.CN,0)-ISNULL(pay.Cost1,0) as Profit
FROM CTM_Job as job  
left outer join (select jobNo,sum(Qty) as Qty,sum(Weight) as Wt,sum(volume) as M3,MAX(PackageType) as PackageType from CTM_JobDet1 group by JobNo) as det1 on job.JobNo=det1.JobNo
left outer join (SELECT MastRefNo as RefNo,sum(case when DocType='IV' then LineLocAmt else 0 end) as IV,SUM(case when DocType='DN' then lineLocAmt else 0 end) as DN,SUM(case when DocType='CN' then lineLocAmt else 0 end) as CN
FROM XaArInvoiceDet WHERE MastRefNo = '{0}' and MastType = 'CTM' group by MastRefNo) as inv on inv.RefNo=job.JobNo
left outer join (select MastRefNo as RefNo,SUM(case DocType when 'PL' then LineLocAmt when 'SD' then LineLocAmt when 'VO' then LineLocAmt when 'SC' then -LineLocAmt else 0 end ) as Cost1 
from XAApPayableDet where MastRefNo='{0}' and MastType = 'CTM' group by MastRefNo) as pay on pay.RefNo=job.JobNo
--left outer join (SELECT refNo as refNo,sum(CostLocAmt) as Cost3 FROM CTM_Costing WHERE RefNo='{0}' and JobType ='CTM' group by RefNo) as costing on costing.refNo=job.JobNo
where job.JobNo='{0}'", refN, System.Configuration.ConfigurationManager.AppSettings["CompanyName"]);
        DataSet ds = ConnectSql.GetDataSet(sql);
        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0].Copy();
        }
        return new DataTable();
    }
    private static DataTable Pl_Inv(string refN, string refType)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(MAX(mast.PartyTo)) AS CustName, SUM(det.LineLocAmt) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = '{1}') and mast.DocType='IV' GROUP BY mast.DocNo", refN, refType);

        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        tab.TableName = "DN";
        return tab;
    }
    private static DataTable Pl_Dn(string refN, string refType)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(MAX(mast.PartyTo)) AS CustName, SUM(det.LineLocAmt) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = '{1}') and mast.DocType='DN' GROUP BY mast.DocNo", refN, refType);

        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        tab.TableName = "DN";
        return tab;
    }
    private static DataTable Pl_Cn(string refN, string refType)
    {
        DataTable tab = new DataTable("Cn");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(MAX(mast.PartyTo)) AS CustName, SUM(det.LineLocAmt) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = '{1}') and mast.DocType='CN' GROUP BY mast.DocNo", refN, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Pl(string refN, string refType)
    {
        string sql = string.Format(@"SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, 
case when Mast.DocType='SC' then -det.LineLocAmt else det.LineLocAmt end AS Amount
FROM XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.DocType = 'PL' or mast.DocType = 'SC' or mast.DocType = 'SD') AND (mast.MastType = '{1}')", refN, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Vo(string refN, string refType)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, 
det.LineLocAmt AS Amount
FROM XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'VO') AND (mast.MastType = '{1}')", refN, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        tab.TableName = "VO";
        return tab;
    }
    private static DataTable Pl_Cost(string refN, string refType)
    {
        return new DataTable();
    }


    #endregion

}