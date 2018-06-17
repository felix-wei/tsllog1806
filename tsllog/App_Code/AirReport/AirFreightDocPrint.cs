using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

public static class AirFreightDocPrint
{
    #region Air P&L
    public static DataSet PrintPl_AirRef(string refN, string userId)
    {
        string refType = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select refType from air_ref where RefNo='{0}'", refN)));
        DataSet set = new DataSet();
       DataTable tab_mast = Pl_Mast(refN, refType, userId);
        DataTable tab_Inv = Pl_Inv(refN, refType);
        DataTable tab_Dn =  Pl_Dn(refN, refType);
        DataTable tab_Ts = Pl_Ts(refN, refType);
        DataTable tab_Cn =  Pl_Cn(refN, refType);
        DataTable tab_Pl = Pl_Pl(refN, refType);
        DataTable tab_Vo = Pl_Vo(refN, refType);
        DataTable tab_Cost = Pl_Cost(refN, refType);
        tab_mast.TableName = "Mast";
        tab_Inv.TableName = "IV";
        tab_Ts.TableName = "TS";
        tab_Cn.TableName = "CN";
        tab_Dn.TableName = "DN";
        tab_Pl.TableName = "PL";
        tab_Vo.TableName = "VO";
        tab_Cost.TableName = "COST";

        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable Pl_Mast(string refN, string refType, string userId)
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

        tab.Columns.Add("Agent");
        tab.Columns.Add("LocalCust");
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

        string sql = string.Format(@"SELECT mast.RefNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,CurrencyId as Currency,ExRate,mast.RefType as JobType
        ,dbo.fun_GetPartyName(mast.LocalCust) as LocalCust,dbo.fun_GetPartyName(mast.AgentId) as Agent,'' as Company,mast.Mawb as Obl,mast.FlightNo1 as Ves,convert(nvarchar(10),mast.FlightDate0,103) as Eta,AirportName0 as Pol, AirportName1 as Pod,'' ContN
        ,mast.Qty,mast.PackageType as Pack,mast.Weight as Wt,mast.Volume as M3
        ,mast.Remarks as Rmk
        ,isnull((select sum(volume) from air_job where refNo=mast.RefNo and TsInd='Y'),0) as TsM3
        ,isnull((select sum(volume) from air_job where refNo=mast.RefNo and TsInd='N'),0) as LocalM3
        ,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = '{1}' and DocType='IV') Rev1
        ,0 as Rev2
        ,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = '{1}' and DocType='DN') Rev3
        ,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = '{1}' and DocType='CN') Rev4
        ,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = '{1}' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = '{1}' and DocType='SC')  as Cost1
        ,0 as Cost2
        ,( SELECT sum(CostLocAmt) FROM Air_Costing WHERE RefNo = mast.RefNo and JobType ='{1}') Cost3
        FROM air_ref mast
        where mast.RefNo='{0}'", refN, refType); 
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

            if (allM3 == 0)
            {
                row["TsM3"] = "";
                row["LocalM3"] = "";
            }
            else
            {
                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
            }

            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string contN = "";
            row["ContN"] = contN;


            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //iv
            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts
            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 + rev3 - rev4;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //pl/sc/sc
            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //vo
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
    private static DataTable Pl_Ts(string refN, string refType)
    {
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");
//        if (refType == "SI")
//        {
//            string sql = string.Format(@"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,dbo.fun_GetPortName(TsPod) as Pod,Volume AS M3, Weight AS WT, TsAgtRate as AgtRate
//,cast((Case when Weight/1000>Volume then Weight/1000 else Volume end)*TsAgtRate as numeric(10,2)) as Amount
//FROM SeaImport WHERE (RefNo = '{0}') AND (TsAgtRate > 0)  and TsInd='Y'", refN, refType);
//            DataTable dt = ConnectSql.GetTab(sql);
//            tab = dt.Copy();
//        }
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
        string sql = string.Format(@"SELECT ChgCodeDes+Remark as Des, CostLocAmt as Amount FROM Air_Costing Where RefNo='{0}' and JobType='{1}'", refN, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        return tab;
    }
    #endregion


    #region import/Export HOUSE P&L
    public static DataSet PrintPl_house(string refN, string jobNo, string userId)
    {
        int jobCnt = 1;
        decimal m3Percent = 0;
        string refType = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select refType from air_Job where RefNo='{0}'", refN)));

        jobCnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(jobNo) from air_Job where RefNo='{0}'", refN)), 1);
        m3Percent = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(string.Format(@"select (case when job.Weight/1000>job.volume then job.Weight/1000 else job.volume end)/(case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end)
 from air_Job as job inner join air_ref mast on mast.RefNo=job.RefNo where job.RefNo='{0}' and Job.JobNo='{1}'", refN, jobNo)), 1);

        if (jobCnt == 0)
            jobCnt = 1;

        DataSet set = new DataSet();
        DataTable tab_mast = Pl_Mast_house(refN, jobNo, refType, jobCnt, m3Percent, userId);
        DataTable tab_Inv = Pl_Inv_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Dn = Pl_Dn_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Ts = Pl_Ts_house(refN, jobNo, refType);
        DataTable tab_Cn = Pl_Cn_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Pl = Pl_Pl_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Vo = Pl_Vo_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Cost = Pl_Cost_house(refN, jobNo, refType, jobCnt, m3Percent);
        tab_mast.TableName = "Mast";
        tab_Inv.TableName = "IV";
        tab_Ts.TableName = "TS";
        tab_Cn.TableName = "CN";
        tab_Dn.TableName = "DN";
        tab_Pl.TableName = "PL";
        tab_Vo.TableName = "VO";
        tab_Cost.TableName = "COST";
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable Pl_Mast_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent, string userId)
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
        string sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,job.CreateBy as UserId,CurrencyId as Currency,ExRate,'' JobType
,dbo.fun_GetPartyName(mast.LocalCust) as LocalCust,dbo.fun_GetPartyName(job.customerid) as Agent,'' as Company,mast.Mawb as Obl,mast.FlightNo1 as Ves,convert(nvarchar(10),mast.FlightDate0,103) as Eta,mast.AirportName0 as Pol, mast.AirportName1 as Pod,'' ContN
,job.Qty,job.PackageType as Pack,job.Weight as Wt,job.Volume as M3
,job.Remark as Rmk
,(Case when Job.TsInd='Y' then job.Volume else 0 end) as TsM3
,(Case when Job.TsInd='N' then job.Volume else 0 end) as LocalM3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = '{1}' and DocType='IV') Rev1
,0 as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = '{1}' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = '{1}' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = '{1}' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo  and MastType = '{1}' and DocType='SC')  as Cost1
,0 as Cost2
,( SELECT sum(CostLocAmt) FROM Air_Costing WHERE RefNo = mast.RefNo and JobNo=job.JobNo and JobType ='{1}') Cost3
FROM air_job job inner join air_ref mast on job.RefNo=mast.RefNo
where mast.RefNo='{0}' and job.JobNo='{2}'", refN, refType,jobNo);


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

            if (allM3 == 0)
            {
                row["TsM3"] = "";
                row["LocalM3"] = "";
            }
            else
            {
                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
            }

            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string contN = "";
            row["ContN"] = contN;


            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //invocie
            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts rate
            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
            string sql_rev = string.Format(@"SELECT DocType
,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
 FROM XaArInvoiceDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN, jobNo, jobCnt, m3Percent, refType);
            DataTable tab_rev = ConnectSql.GetTab(sql_rev);
            for (int j = 0; j < tab_rev.Rows.Count; j++)
            {
                string docType = SafeValue.SafeString(tab_rev.Rows[j]["DocType"]).ToUpper();
                decimal amt = SafeValue.SafeDecimal(tab_rev.Rows[j]["Amt"], 0);

                if (docType == "IV")
                    rev1 += amt;
                else if (docType == "DN")
                    rev3 += amt;
                else if (docType == "CN")
                    rev4 += amt;
            }
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 + rev3 - rev4;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //ap
            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //
            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing
            string sql_ap = string.Format(@"SELECT DocType
,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
 FROM XaApPayableDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN, jobNo, jobCnt, m3Percent, refType);
            DataTable tab_ap = ConnectSql.GetTab(sql_ap);
            for (int j = 0; j < tab_ap.Rows.Count; j++)
            {
                string docType = SafeValue.SafeString(tab_ap.Rows[j]["DocType"]).ToUpper();
                decimal amt = SafeValue.SafeDecimal(tab_ap.Rows[j]["Amt"], 0);
                if (docType == "SC")
                    cost1 -= amt;
                else
                    cost1 += amt;
            }
            string sql_cost = string.Format(@"SELECT sum(Cast(case when SplitType='Set' then CostLocAmt/{2}
	       else CostLocAmt*{3}  end as numeric(38,2))) as Amount FROM Air_Costing  where RefNo='{0}' and (JobNo='0' or JobNo='') and JobType='{4}'", refN, jobNo, jobCnt, m3Percent, refType);
            cost3 += SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_cost), 0);
            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        return tab;
    }

    private static DataTable Pl_Inv_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo,  dbo.fun_GetPartyName(mast.PartyTo) AS CustName, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='IV' order by mast.DocNo "
            , refN, jobNo, jobCnt, m3Percent, refType);

        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Dn_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(mast.PartyTo) AS CustName, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='DN'  order by mast.DocNo"
            , refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Ts_house(string refN, string jobNo, string refType)
    {
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");

//        string sql = "";
//        if (refType == "SI")
//        {
//            sql = string.Format(@"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,dbo.fun_GetPortName(TsPod) as Pod,Volume AS M3, Weight AS WT
//,cast((Case when Weight/1000>Volume then Weight/1000 else Volume end)*TsAgtRate as numeric(38,2)) as Amount
//,(Select CurrencyId from SeaImportRef where RefNo=SeaImport.RefNo) as Currency 
//FROM SeaImport  WHERE (RefNo = '{0}') and JobNo='{1}' AND (TsAgtRate > 0) and TsInd='Y' ", refN, jobNo);
//            DataTable dt = ConnectSql.GetTab(sql);
//            tab = dt.Copy();
//        }
        return tab;
    }

    private static DataTable Pl_Cn_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(mast.PartyTo) AS CustName, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='CN' order by mast.DocNo"
            , refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Pl_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS Vn, mast.SupplierBillNo AS DocN,  dbo.fun_GetPartyName(mast.PartyTo) AS CustName, det.ChgCode ,det.ChgDes1 as Gd
,Cast(case when mast.DocType='SC' and det.JobRefNo='{1}' then -det.LineLocAmt
	       when mast.DocType='SC' and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then -det.LineLocAmt/{2}
	       when mast.DocType='SC' and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then -det.LineLocAmt*{3}
	       
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.JobRefNo='{1}' then det.LineLocAmt
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt/{2}
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM XAApPayableDet AS det INNER JOIN XAApPayable AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0')and (mast.DocType='PL' or mast.DocType='SC' OR mast.DocType='SD') AND (mast.MastType = '{4}') 
 order by mast.DocNo", refN, jobNo, jobCnt, m3Percent, refType);

        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Vo_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS Vn, mast.SupplierBillNo AS DocN,  dbo.fun_GetPartyName(mast.PartyTo) AS CustName, det.ChgCode ,det.ChgDes1 as Gd
,Cast(case when mast.DocType='VO' and det.JobRefNo='{1}' then det.LineLocAmt
	       when mast.DocType='VO' and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt/{2}
	       when mast.DocType='VO' and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM XAApPayableDet AS det INNER JOIN XAApPayable AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0')and (mast.DocType='VO') AND (mast.MastType = '{4}') 
 order by mast.DocNo", refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Cost_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des
,Cast(case when JobNo='{1}' then CostLocAmt
	       when SplitType='Set' and (JobNo='' or JobNo='0') then CostLocAmt/{2}
	       when SplitType!='Set' and (JobNo='' or JobNo='0') then CostLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM Air_Costing  Where RefNo='{0}' and (JobNo='{1}' OR JobNo='' or JobNo='0') and JobType='{4}'", refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    #endregion

    #region Air Awb
    public static DataTable PrintAWB(string refN, string jobN)
    {
        string sql = string.Format(@"exec proc_PrintAir_AWB '{0}','{1}','','',''", refN, jobN);
        //string sql = "";
        //if (jobN.Length>2)
        //{
        //    sql = string.Format("select ref.MAWB,job.* from air_job as job inner join air_ref as ref on job.RefNo=ref.RefNo where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobN);
        //}
        //else
        //{
        //    sql = string.Format("select * from air_ref  where RefNo='{0}'", refN);
        //}
        DataTable tab_mast = ConnectSql.GetTab(sql);
        tab_mast.TableName = "Mast";
        return tab_mast;
    }

    #endregion


    #region do/dn/so/bookingconfirmation/manifest/
    public static DataSet dsDo(string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintAir_import_DO '{0}','{1}','{2}','{3}','{4}'", "", jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["JobNo"], Detail.Columns["JobNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;
    }
    public static DataTable dsSo(string jobN)
    {
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpShippingOrder '{0}','{1}','{2}','{3}','{4}'", "", jobN, "AE", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            return ds_temp.Tables[0];
            //DataTable Detail = ds_temp.Tables[0].Copy();
            //Detail.TableName = "Detail";
            //ds.Tables.Add(Detail);
        }
        catch (Exception ex) { }
        return null;
    }

    public static DataTable PrintBookingConfirm(string jobN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintAir_export_BookingConfirm '{0}','{1}','{2}','{3}','{4}'", "", jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
        }
        catch (Exception ex) { }
        return dt;
    }

    public static DataSet PrintManifest(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintAir_export_Manifest '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", "");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefNo"], Detail.Columns["RefNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;
    }
    public static DataTable PrintDN(string refN, string jobN)
    {
        DataTable ds = new DataTable();
        try
        {
            string sqlstr = string.Format(@"exec proc_PrintDN '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "AI", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            ds = ConnectSql.GetTab(sqlstr);
        }
        catch (Exception ex)
        {

        }
        return ds;
    } 
    #endregion


}
