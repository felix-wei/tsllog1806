using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using C2;

public static class TptDocPrint
{
    #region import dn/joborder
    public static DataTable  PrintDN(string refN, string refType,string userName)
    {
        DataTable ds = new DataTable();
        try
        {
            string sqlstr = string.Format(@"exec proc_PrintDN '{0}','{1}','{2}','{3}','{4}'", refN, "", refType, "", "");
            ds = ConnectSql.GetTab(sqlstr);
            // ds.TableName = "Mast";
        }
        catch (Exception ex)
        {
        }
        return ds;
    }
    public static DataSet PrintJobOrder(string exportN, string jobType)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintJobOrder1 '{0}','{1}','{2}','{3}','{4}'", exportN, "", jobType, "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["JobNo"], det.Columns["JobNo"]);
        }
        catch (Exception ex) { }
        return set1;
    } 

    #endregion
    #region P&L
    public static DataSet PrintPlImport(string refN, string userId)
    {
        DataSet set = new DataSet();
        DataTable tab_mast = PlImport_Mast(refN, userId);
        string refType = "Tpt";
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
    private static DataTable PlImport_Mast(string refN, string userId)
    {
        DataTable tab = new DataTable("PlMast");
        tab.Columns.Add("RefN");
        tab.Columns.Add("NowD");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("JobType");
        tab.Columns.Add("TsM3");
        tab.Columns.Add("LocalM3");

        tab.Columns.Add("LocalCust");
        tab.Columns.Add("Agent");
        tab.Columns.Add("Company");
        tab.Columns.Add("Obl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Pack");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("ContN");
        tab.Columns.Add("Rmk");

        tab.Columns.Add("Rev1");
        tab.Columns.Add("Rev2");
        tab.Columns.Add("Rev3");
        tab.Columns.Add("Rev4");
        tab.Columns.Add("Rev");

        tab.Columns.Add("Cost1");
        tab.Columns.Add("Cost2");
        tab.Columns.Add("Cost3");
        tab.Columns.Add("Cost");
        tab.Columns.Add("Profit");
        string sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,job.CreateBy as UserId,'SGD' as Currency,1 as ExRate,JobType JobType
,dbo.fun_GetPartyName(job.Cust) as LocalCust,dbo.fun_GetPartyName(job.Cust) as Agent,'' as Company,JOB.BkgRef as Obl
,job.Vessel as Ves,convert(nvarchar(10),job.Eta,103) as Eta,dbo.fun_GetPortName(job.Pol) as Pol, dbo.fun_GetPortName(job.Pod) as Pod,'' ContN
,job.Qty,job.PkgType as Pack,job.wT as Wt,job.M3 as M3
,job.JobRmk as Rmk
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = job.JobNo and MastType = 'TPT' and DocType='IV') Rev1
,0 as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = job.JobNo and MastType = 'TPT' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = job.JobNo and MastType = 'TPT' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = job.JobNo and MastType = 'TPT' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = job.JobNo  and MastType = 'TPT' and DocType='SC')  as Cost1
,0 as Cost2
,( SELECT sum(CostLocAmt) FROM TPT_Costing WHERE RefNo = job.JobNo and JobType ='TPT') Cost3
FROM tpt_job job  where job.JobNo='{0}'", refN);

        DataTable dt = ConnectSql.GetTab(sql);
        DataRow row1 = tab.NewRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string colName = dt.Columns[i].ColumnName;
            row1[colName] = dt.Rows[0][i];
        }
        tab.Rows.Add(row1);
        if (tab.Rows.Count > 0)
        {
            DataRow row = tab.Rows[0];
            decimal allM3 = SafeValue.SafeDecimal(tab.Rows[0]["M3"], 0);
            decimal transM3 = SafeValue.SafeDecimal(tab.Rows[0]["TsM3"], 0);

                row["TsM3"] = transM3.ToString("0.00") ;
                row["LocalM3"] = transM3.ToString("0.00");


            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string contN = "";
            row["ContN"] = contN;


            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //invocie
            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts rate
            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn

            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 + rev3 - rev4;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //ap
            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //
            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing
            

            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        return tab;
    }

    #region old
//    private static DataTable PlImport_Inv(string refN)
//    {
//        DataTable tab = new DataTable("Invoice");
//        tab.Columns.Add("BillNo");
//        tab.Columns.Add("CustName");
//        tab.Columns.Add("Amount");
//        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
//FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
//WHERE (mast.MastRefNo = '{0}') AND (mast.MastClass = 'Tpt') and mast.DocType='IV' GROUP BY mast.DocNo", refN);


//        decimal gstA = 0;
//        DataTable dt = ConnectSql.GetTab(sql);
//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow row = tab.NewRow();
//            row["BillNo"] = dt.Rows[i]["RefNo"];
//            row["CustName"] = ConnectSql.ExecuteScalar(string.Format("Select Name from XFCustomer where CustId='{0}'", dt.Rows[i]["CustId"]));

//            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
//            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
//            tab.Rows.Add(row);
//        }
//        return tab;
//    }
//    private static DataTable PlImport_Dn(string refN)
//    {
//        DataTable tab = new DataTable("DN");
//        tab.Columns.Add("BillNo");
//        tab.Columns.Add("CustName");
//        tab.Columns.Add("Amount");
//        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
//FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
//WHERE (mast.MastRefNo = '{0}') AND (mast.MastClass = 'Tpt') and mast.DocType='DN' GROUP BY mast.DocNo", refN);


//        decimal gstA = 0;
//        DataTable dt = ConnectSql.GetTab(sql);
//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow row = tab.NewRow();
//            row["BillNo"] = dt.Rows[i]["RefNo"];
//            row["CustName"] = ConnectSql.ExecuteScalar(string.Format("Select Name from XFCustomer where CustId='{0}'", dt.Rows[i]["CustId"]));

//            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
//            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
//            tab.Rows.Add(row);
//        }
//        return tab;
//    }

//    private static DataTable PlImport_Cn(string refN)
//    {
//        DataTable tab = new DataTable("Cn");
//        tab.Columns.Add("BillNo");
//        tab.Columns.Add("CustName");
//        tab.Columns.Add("Amount");
//        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
//FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
//WHERE (mast.MastRefNo = '{0}') AND (mast.MastClass = 'Tpt') and mast.DocType='CN' GROUP BY mast.DocNo", refN);
//        decimal gstA = 0;
//        DataTable dt = ConnectSql.GetTab(sql);
//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow row = tab.NewRow();
//            row["BillNo"] = dt.Rows[i]["RefNo"];
//            row["CustName"] = ConnectSql.ExecuteScalar(string.Format("Select Name from XFCustomer where CustId='{0}'", dt.Rows[i]["CustId"]));

//            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
//            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
//            tab.Rows.Add(row);
//        }
//        return tab;
//    }
//    private static DataTable PlImport_Pl(string refN)
//    {
//        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, mast.DocType,det.LocAmt*mast.ExRate AS Amount
//FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
//WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'PL' or mast.DocType = 'SC' or mast.DocType = 'SD') AND (mast.MastClass = 'Tpt')", refN);
//        DataTable dt = ConnectSql.GetTab(sql);

//        decimal gstA = 0;
//        DataTable tab = new DataTable("Payable");
//        tab.Columns.Add("Gd");
//        tab.Columns.Add("Vn");
//        tab.Columns.Add("DocN");
//        tab.Columns.Add("Amount");
//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow row = tab.NewRow();
//            row["Gd"] = dt.Rows[i]["Gd"];
//            string docType = dt.Rows[i]["DocType"].ToString();
//            row["Vn"] = dt.Rows[i]["Vn"].ToString() + "(" + docType + ")";
//            row["DocN"] = dt.Rows[i]["DocN"].ToString();
//            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);

//            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
//            if (docType == "SC")
//                row["Amount"] = (-SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0)).ToString("0.00");
//            tab.Rows.Add(row);
//        }
//        return tab;
//    }
//    private static DataTable PlImport_Vo(string refN)
//    {
//        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, det.LocAmt*mast.ExRate AS Amount
//FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
//WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'VO') AND (mast.MastType = 'I') AND (mast.MastClass = 'Tpt')", refN);
//        DataTable dt = ConnectSql.GetTab(sql);

//        decimal gstA = 0;
//        DataTable tab = new DataTable("Voucher");
//        tab.Columns.Add("Gd");
//        tab.Columns.Add("Vn");
//        tab.Columns.Add("DocN");
//        tab.Columns.Add("Amount");
//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow row = tab.NewRow();
//            row["Gd"] = dt.Rows[i]["Gd"];
//            row["Vn"] = dt.Rows[i]["Vn"];
//            row["DocN"] = dt.Rows[i]["DocN"];
//            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
//            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
//            tab.Rows.Add(row);
//        }
//        return tab;
//    } 
    #endregion

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
        string sql = string.Format(@"SELECT ChgCodeDes+Remark as Des, CostLocAmt as Amount FROM Tpt_Costing Where RefNo='{0}' and JobType='{1}'", refN, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        return tab;
    }

    #endregion

}
