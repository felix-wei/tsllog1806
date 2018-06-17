using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using C2;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ImportPrint
/// </summary>
public partial class AccountPrint
{

 
       

    #region ap report
    public static DataTable DsApCustomerTrans(DateTime d1, DateTime d2, string partyTo, string curyType, string userId)
    {
        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("Rmk");
        mast.Columns.Add("DocAmt");
        mast.Columns.Add("LocAmt");

        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        //all,iv,cn,dn,receipt,pc
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        string where = "";
        if (curyType == "0")
        {
            where = string.Format("DocDate>='{0}' and DocDate<'{1}' and (DocType='PL' or DocType='SC' or DocType='SD') AND CurrencyId='{2}' and ExportInd='Y' and CancelInd='N'", dateFrm, dateTo, System.Configuration.ConfigurationManager.AppSettings["Currency"]);
            if (partyTo.Length > 0 && partyTo != "null")
                where += " and PartyTo='" + partyTo + "'";
        }
        else if (curyType == "1")
        {
            where = string.Format("DocDate>='{0}' and DocDate<'{1}' and (DocType='PL' or DocType='SC' or DocType='SD') AND CurrencyId!='{2}' and ExportInd='Y' and CancelInd='N'", dateFrm, dateTo, System.Configuration.ConfigurationManager.AppSettings["Currency"]);
            if (partyTo.Length > 0 && partyTo != "null")
                where += " and PartyTo='" + partyTo + "'";
        }
        string sql = "SELECT SequenceId,DocType, DocNo,SupplierBillNo,AcSource, DocDate, PartyTo, CurrencyId,ExRate,DocAmt, LocAmt,Description FROM XAApPayable";
        sql += " where " + where;
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string docType = reader["DocType"].ToString();
            string docNo = reader["SupplierBillNo"].ToString();// reader["DocNo"].ToString();

            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string billNo = reader["DocNo"].ToString();

            partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string exRate = reader["ExRate"].ToString();
            string acSource = reader["AcSource"].ToString();
            string rmk = reader["Description"].ToString();
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            if (docType == "SC")
            {
            }
            else
            {
                docAmt = -docAmt;
                locAmt = -locAmt;
            }
            DataRow row = mast.NewRow();
            row["PartyTo"] = partyTo;
            row["Currency"] = currency;
            row["ExRate"] = exRate;
            row["PartyName"] = EzshipHelper.GetPartyName(partyTo);
            row["AcCode"] = SafeValue.SafeString(GetObj(string.Format("select  ApCode from XXPartyacc where PartyId='{0}' and CurrencyId='{1}'", partyTo, currency)));

            row["AcDesc"] = GetObj("select AcDesc from XXChartAcc where Code='" + row["AcCode"] + "'");
            row["DocDate"] = docDate.ToString("dd/MM/yyyy");
            row["DocType"] = docType;
            row["DocNo"] = docNo;
            row["Rmk"] = rmk;
            row["DocAmt"] = docAmt.ToString("#,##0.00");
            row["LocAmt"] = locAmt.ToString("#,##0.00");
            row["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
            row["UserId"] = userId;
            mast.Rows.Add(row);
        }

        return mast;
    }

    public static DataTable DsApStatement(DateTime d1, DateTime d2, string partyTo, string curyType, string userId)
    {
        // DateTime d1 = new DateTime(2000, 1, 1);
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");


        mast.Columns.Add("DocDate");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("Rmk");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("BalAmt");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        //all,iv,cn,dn,receipt,pc
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
		
        string where1 = "";
        if (partyTo.Length > 0 && partyTo != "null")
            where1 += " and PartyTo='" + partyTo + "'";
		

        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description, 
                     case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                     case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.CurrencyId='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' ) {3}
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType, where1);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string lastPartTo = "";


        decimal current = 0;
        decimal day30 = 0;
        decimal day60 = 0;
        decimal day90 = 0;
        decimal day120 = 0;
        decimal over120 = 0;
        decimal totAmt = 0;
        int i = 0;
        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        while (reader.Read())
        {
            string docType = reader["DocType"].ToString();
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string acSource = reader["AcSource"].ToString();
            string rmk = reader["Description"].ToString();


            if (i > 0 && lastPartTo != partyTo)
            {
                current = 0;
                day30 = 0;
                day60 = 0;
                day90 = 0;
                day120 = 0;
                over120 = 0;
                totAmt = 0;
            }
            lastPartTo = partyTo;
            i++;
            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);
            if (balanceAmt != 0)
            {
                decimal dbAmt = 0;
                decimal crAmt = 0;
                if (acSource == "DB")//sc
                {
                    dbAmt = -balanceAmt;
                    totAmt += dbAmt;
                }
                else
                {
                    crAmt = balanceAmt;
                    totAmt -= crAmt;
                }
                int days = DiffDays(docDate, d2, "M");

                if (days <= 0)
                    current += dbAmt - crAmt;
                else if (days <= 30)
                    day30 += dbAmt - crAmt;
                else if (days <= 60)
                    day60 += dbAmt - crAmt;
                else if (days <= 90)
                    day90 += dbAmt - crAmt;
                else if (days <= 120)
                    day120 += dbAmt - crAmt;
                else
                    over120 += dbAmt - crAmt;

                DataRow row = mast.NewRow();
                row["PartyTo"] = lastPartTo;
                row["Currency"] = currency;
                DataTable tab_Party = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["PartyName"] += "\n" + tab_Party.Rows[0]["Address"];
                    string tel1 = SafeValue.SafeString(tab_Party.Rows[0]["Tel1"], "");
                    string fax1 = SafeValue.SafeString(tab_Party.Rows[0]["Fax1"], "");
                    if (tel1.Length > 0)
                    {
                        row["PartyName"] += "\nTel:" + tel1;
                    }
                    if (fax1.Length > 0)
                    {
                        if (tel1.Length > 0)
                            row["PartyName"] += "  Fax:" + fax1;
                        else
                            row["PartyName"] += "\nFax:" + fax1;
                    }

                }
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = totAmt.ToString("#,##0.00");

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["Rmk"] = rmk;
                row["DbAmt"] = dbAmt.ToString("#,##0.00");
                row["CrAmt"] = crAmt.ToString("#,##0.00");
                row["BalAmt"] = totAmt.ToString("#,##0.00");
                row["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
                row["UserId"] = userId;
                mast.Rows.Add(row);
            }
        }

        if (mast.Rows.Count > 1)
        {
            DataRow rowLast = mast.Rows[mast.Rows.Count - 1];
            rowLast["PartyTo"] = lastPartTo;
            rowLast["Currency"] = curyType;
            DataTable tab_Party1 = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
            if (tab_Party1.Rows.Count == 1)
            {
                rowLast["PartyName"] = tab_Party1.Rows[0]["Name"];
                rowLast["PartyName"] += "\n" + tab_Party1.Rows[0]["Address"];
                string tel1 = SafeValue.SafeString(tab_Party1.Rows[0]["Tel1"], "");
                string fax1 = SafeValue.SafeString(tab_Party1.Rows[0]["Fax1"], "");
                if (tel1.Length > 0)
                {
                    rowLast["PartyName"] += "\nTel:" + tel1;
                }
                if (fax1.Length > 0)
                {
                    if (tel1.Length > 0)
                        rowLast["PartyName"] += "  Fax:" + fax1;
                    else
                        rowLast["PartyName"] += "\nFax:" + fax1;
                }

            }
            rowLast["Current"] = current.ToString("#,##0.00");
            rowLast["30Days"] = day30.ToString("#,##0.00");
            rowLast["60Days"] = day60.ToString("#,##0.00");
            rowLast["90Days"] = day90.ToString("#,##0.00");
            rowLast["120Days"] = day120.ToString("#,##0.00");
            rowLast["OverDue"] = over120.ToString("#,##0.00");
            rowLast["TotAmt"] = totAmt.ToString("#,##0.00");
            rowLast["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
            rowLast["UserId"] = userId;
        }
        return mast;
    }
    public static DataTable DsApStatement_ForeignLocal(DateTime d1, DateTime d2, string partyTo, string curyType, string userId)
    {
        // DateTime d1 = new DateTime(2000, 1, 1);
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");


        mast.Columns.Add("DocDate");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("Rmk");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("BalAmt");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        //all,iv,cn,dn,receipt,pc
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");

        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description, 
                     case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                     case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.ExportInd='Y' AND bill.CurrencyId!='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' )
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string lastPartTo = "";


        decimal current = 0;
        decimal day30 = 0;
        decimal day60 = 0;
        decimal day90 = 0;
        decimal day120 = 0;
        decimal over120 = 0;
        decimal totAmt = 0;
        int i = 0;
        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        while (reader.Read())
        {
            string docType = reader["DocType"].ToString();
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string acSource = reader["AcSource"].ToString();
            string rmk = reader["Description"].ToString();


            if (i > 0 && lastPartTo != partyTo)
            {
                current = 0;
                day30 = 0;
                day60 = 0;
                day90 = 0;
                day120 = 0;
                over120 = 0;
                totAmt = 0;
            }
            lastPartTo = partyTo;
            i++;

            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal loceAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 1);
            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);
            if (balanceAmt == docAmt)
                balanceAmt = loceAmt;
            else if (balanceAmt != 0)
            {
                balanceAmt = SafeValue.ChinaRound(balanceAmt * exRate, 2);
            }
            if (balanceAmt != 0)
            {
                decimal dbAmt = 0;
                decimal crAmt = 0;
                if (acSource == "DB")//sc
                {
                    dbAmt = -balanceAmt;
                    totAmt += dbAmt;
                }
                else
                {
                    crAmt = balanceAmt;
                    totAmt -= crAmt;
                }
                int days = DiffDays(docDate, d2, "M");

                if (days <= 0)
                    current += dbAmt - crAmt;
                else if (days <= 30)
                    day30 += dbAmt - crAmt;
                else if (days <= 60)
                    day60 += dbAmt - crAmt;
                else if (days <= 90)
                    day90 += dbAmt - crAmt;
                else if (days <= 120)
                    day120 += dbAmt - crAmt;
                else
                    over120 += dbAmt - crAmt;

                DataRow row = mast.NewRow();
                row["PartyTo"] = lastPartTo;
                row["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                DataTable tab_Party = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["PartyName"] += "\n" + tab_Party.Rows[0]["Address"];
                    string tel1 = SafeValue.SafeString(tab_Party.Rows[0]["Tel1"], "");
                    string fax1 = SafeValue.SafeString(tab_Party.Rows[0]["Fax1"], "");
                    if (tel1.Length > 0)
                    {
                        row["PartyName"] += "\nTel:" + tel1;
                    }
                    if (fax1.Length > 0)
                    {
                        if (tel1.Length > 0)
                            row["PartyName"] += "  Fax:" + fax1;
                        else
                            row["PartyName"] += "\nFax:" + fax1;
                    }

                }
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = totAmt.ToString("#,##0.00");

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["Rmk"] = rmk;
                row["DbAmt"] = dbAmt.ToString("#,##0.00");
                row["CrAmt"] = crAmt.ToString("#,##0.00");
                row["BalAmt"] = totAmt.ToString("#,##0.00");
                row["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
                row["UserId"] = userId;
                mast.Rows.Add(row);
            }
        }

        if (mast.Rows.Count > 1)
        {
            DataRow rowLast = mast.Rows[mast.Rows.Count - 1];
            rowLast["PartyTo"] = lastPartTo;
            rowLast["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            DataTable tab_Party1 = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
            if (tab_Party1.Rows.Count == 1)
            {
                rowLast["PartyName"] = tab_Party1.Rows[0]["Name"];
                rowLast["PartyName"] += "\n" + tab_Party1.Rows[0]["Address"];
                string tel1 = SafeValue.SafeString(tab_Party1.Rows[0]["Tel1"], "");
                string fax1 = SafeValue.SafeString(tab_Party1.Rows[0]["Fax1"], "");
                if (tel1.Length > 0)
                {
                    rowLast["PartyName"] += "\nTel:" + tel1;
                }
                if (fax1.Length > 0)
                {
                    if (tel1.Length > 0)
                        rowLast["PartyName"] += "  Fax:" + fax1;
                    else
                        rowLast["PartyName"] += "\nFax:" + fax1;
                }

            }
            rowLast["Current"] = current.ToString("#,##0.00");
            rowLast["30Days"] = day30.ToString("#,##0.00");
            rowLast["60Days"] = day60.ToString("#,##0.00");
            rowLast["90Days"] = day90.ToString("#,##0.00");
            rowLast["120Days"] = day120.ToString("#,##0.00");
            rowLast["OverDue"] = over120.ToString("#,##0.00");
            rowLast["TotAmt"] = totAmt.ToString("#,##0.00");
            rowLast["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
            rowLast["UserId"] = userId;
        }
        return mast;
    }
    public static DataTable DsApStatement_ForeignForeign(DateTime d1, DateTime d2, string partyTo, string curyType, string userId)
    {
        // DateTime d1 = new DateTime(2000, 1, 1);
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");


        mast.Columns.Add("DocDate");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("Rmk");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("BalAmt");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        //all,iv,cn,dn,receipt,pc
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");

        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description,
                     case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                     case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.ExportInd='Y' AND bill.CurrencyId!='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' )
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string lastPartTo = "";
        string lastCurrency = "";


        decimal current = 0;
        decimal day30 = 0;
        decimal day60 = 0;
        decimal day90 = 0;
        decimal day120 = 0;
        decimal over120 = 0;
        decimal totAmt = 0;
        int i = 0;
        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        while (reader.Read())
        {
            string docType = reader["DocType"].ToString();
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string acSource = reader["AcSource"].ToString();
            string rmk = reader["Description"].ToString();
            lastCurrency = currency;

            if (i > 0 && lastPartTo != partyTo)
            {
                current = 0;
                day30 = 0;
                day60 = 0;
                day90 = 0;
                day120 = 0;
                over120 = 0;
                totAmt = 0;
            }
            lastPartTo = partyTo;
            i++;

            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal loceAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 1);
            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);
            //if (balanceAmt == docAmt)
            //    balanceAmt = loceAmt;
            //else if (balanceAmt != 0)
            //{
            //    balanceAmt = SafeValue.ChinaRound(balanceAmt * exRate, 2);
            //}
            if (balanceAmt != 0)
            {
                decimal dbAmt = 0;
                decimal crAmt = 0;
                if (acSource == "DB")//sc
                {
                    dbAmt = -balanceAmt;
                    totAmt += dbAmt;
                }
                else
                {
                    crAmt = balanceAmt;
                    totAmt -= crAmt;
                }
                int days = DiffDays(docDate, d2, "M");

                if (days <= 0)
                    current += dbAmt - crAmt;
                else if (days <= 30)
                    day30 += dbAmt - crAmt;
                else if (days <= 60)
                    day60 += dbAmt - crAmt;
                else if (days <= 90)
                    day90 += dbAmt - crAmt;
                else if (days <= 120)
                    day120 += dbAmt - crAmt;
                else
                    over120 += dbAmt - crAmt;

                DataRow row = mast.NewRow();
                row["PartyTo"] = lastPartTo;
                row["Currency"] = currency;
                DataTable tab_Party = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["PartyName"] += "\n" + tab_Party.Rows[0]["Address"];
                    string tel1 = SafeValue.SafeString(tab_Party.Rows[0]["Tel1"], "");
                    string fax1 = SafeValue.SafeString(tab_Party.Rows[0]["Fax1"], "");
                    if (tel1.Length > 0)
                    {
                        row["PartyName"] += "\nTel:" + tel1;
                    }
                    if (fax1.Length > 0)
                    {
                        if (tel1.Length > 0)
                            row["PartyName"] += "  Fax:" + fax1;
                        else
                            row["PartyName"] += "\nFax:" + fax1;
                    }

                }
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = totAmt.ToString("#,##0.00");

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["Rmk"] = rmk;
                row["DbAmt"] = dbAmt.ToString("#,##0.00");
                row["CrAmt"] = crAmt.ToString("#,##0.00");
                row["BalAmt"] = totAmt.ToString("#,##0.00");
                row["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
                row["UserId"] = userId;
                mast.Rows.Add(row);
            }
        }

        if (mast.Rows.Count > 1)
        {
            DataRow rowLast = mast.Rows[mast.Rows.Count - 1];
            rowLast["PartyTo"] = lastPartTo;
            rowLast["Currency"] = lastCurrency;
            DataTable tab_Party1 = Helper.Sql.List("select  Name, GroupId, Address, Tel1, Tel2, Fax1, Fax2, Email1, Email2, Contact1, Contact2 from XXParty where PartyId='" + lastPartTo + "'");
            if (tab_Party1.Rows.Count == 1)
            {
                rowLast["PartyName"] = tab_Party1.Rows[0]["Name"];
                rowLast["PartyName"] += "\n" + tab_Party1.Rows[0]["Address"];
                string tel1 = SafeValue.SafeString(tab_Party1.Rows[0]["Tel1"], "");
                string fax1 = SafeValue.SafeString(tab_Party1.Rows[0]["Fax1"], "");
                if (tel1.Length > 0)
                {
                    rowLast["PartyName"] += "\nTel:" + tel1;
                }
                if (fax1.Length > 0)
                {
                    if (tel1.Length > 0)
                        rowLast["PartyName"] += "  Fax:" + fax1;
                    else
                        rowLast["PartyName"] += "\nFax:" + fax1;
                }

            }
            rowLast["Current"] = current.ToString("#,##0.00");
            rowLast["30Days"] = day30.ToString("#,##0.00");
            rowLast["60Days"] = day60.ToString("#,##0.00");
            rowLast["90Days"] = day90.ToString("#,##0.00");
            rowLast["120Days"] = day120.ToString("#,##0.00");
            rowLast["OverDue"] = over120.ToString("#,##0.00");
            rowLast["TotAmt"] = totAmt.ToString("#,##0.00");
            rowLast["DatePeriod"] = "To " + d2.ToString("dd/MM/yyyy");
            rowLast["UserId"] = userId;
        }
        return mast;
    }

    public static DataTable DsApAgingSummary(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsApAgingDetail(d1, d2, curyType, userId);
        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("tab1", tab, "PartyCode,sum(Current) Current,sum(30Days) 30Days,sum(60Days) 60Days,sum(90Days) 90Days,sum(120Days) 120Days,sum(OverDue) OverDue", "", "PartyCode");

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("PartyCode");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("PartyGroup");
        mast.Columns.Add("Currency");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");
        mast.Columns.Add("UserId");
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow row = tab1.Rows[i];
            DataRow rowMast = mast.NewRow();
            rowMast["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
            rowMast["PartyCode"] = row["PartyCode"];
            string partyName = EzshipHelper.GetPartyName(row["PartyCode"]);
            string partyGroup = GetObj("select groupid from xxparty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            string groupName = partyGroup;
            rowMast["PartyGroup"] = groupName;
            rowMast["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            rowMast["Current"] = SafeValue.SafeDecimal(row["Current"], 0).ToString("#,##0.00");
            rowMast["30Days"] = SafeValue.SafeDecimal(row["30Days"], 0).ToString("#,##0.00");
            rowMast["60Days"] = SafeValue.SafeDecimal(row["60Days"], 0).ToString("#,##0.00");
            rowMast["90Days"] = SafeValue.SafeDecimal(row["90Days"], 0).ToString("#,##0.00");
            rowMast["120Days"] = SafeValue.SafeDecimal(row["120Days"], 0).ToString("#,##0.00");
            rowMast["OverDue"] = SafeValue.SafeDecimal(row["OverDue"], 0).ToString("#,##0.00");
            rowMast["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
            rowMast["UserId"] = userId;

            mast.Rows.Add(rowMast);
        }

        return mast;
    }
    public static DataTable DsApAgingSummary_ForeignLocal(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsApAgingDetail_ForeignLocal(d1, d2, curyType, userId);

        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("tab1", tab, "PartyCode,sum(Current) Current,sum(30Days) 30Days,sum(60Days) 60Days,sum(90Days) 90Days,sum(120Days) 120Days,sum(OverDue) OverDue", "", "PartyCode");

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("PartyCode");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("PartyGroup");
        mast.Columns.Add("Currency");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");
        mast.Columns.Add("UserId");
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow row = tab1.Rows[i];
            DataRow rowMast = mast.NewRow();
            rowMast["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
            rowMast["PartyCode"] = row["PartyCode"];
            string partyName = EzshipHelper.GetPartyName(row["PartyCode"]);
            string partyGroup = GetObj("select groupid from xxparty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            string groupName = partyGroup;
            rowMast["PartyGroup"] = groupName;
            rowMast["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            rowMast["Current"] = SafeValue.SafeDecimal(row["Current"], 0).ToString("#,##0.00");
            rowMast["30Days"] = SafeValue.SafeDecimal(row["30Days"], 0).ToString("#,##0.00");
            rowMast["60Days"] = SafeValue.SafeDecimal(row["60Days"], 0).ToString("#,##0.00");
            rowMast["90Days"] = SafeValue.SafeDecimal(row["90Days"], 0).ToString("#,##0.00");
            rowMast["120Days"] = SafeValue.SafeDecimal(row["120Days"], 0).ToString("#,##0.00");
            rowMast["OverDue"] = SafeValue.SafeDecimal(row["OverDue"], 0).ToString("#,##0.00");
            rowMast["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
            rowMast["UserId"] = userId;

            mast.Rows.Add(rowMast);
        }

        return mast;
    }
    public static DataTable DsApAgingSummary_ForeignForeign(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsApAgingDetail_ForeignForeign(d1, d2, curyType, userId);

        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("tab1", tab, "PartyCode,Currency,sum(Current) Current,sum(30Days) 30Days,sum(60Days) 60Days,sum(90Days) 90Days,sum(120Days) 120Days,sum(OverDue) OverDue", "", "PartyCode,Currency");

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("PartyCode");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("PartyGroup");
        mast.Columns.Add("Currency");
        mast.Columns.Add("Current");
        mast.Columns.Add("30Days");
        mast.Columns.Add("60Days");
        mast.Columns.Add("90Days");
        mast.Columns.Add("120Days");
        mast.Columns.Add("OverDue");
        mast.Columns.Add("TotAmt");
        mast.Columns.Add("UserId");
        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow row = tab1.Rows[i];
            DataRow rowMast = mast.NewRow();
            rowMast["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
            rowMast["PartyCode"] = row["PartyCode"];
            string partyName = EzshipHelper.GetPartyName(row["PartyCode"]);
            string partyGroup = GetObj("select groupid from xxparty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            string groupName = partyGroup;
            rowMast["PartyGroup"] = groupName;
            rowMast["Currency"] = row["Currency"];
            rowMast["Current"] = SafeValue.SafeDecimal(row["Current"], 0).ToString("#,##0.00");
            rowMast["30Days"] = SafeValue.SafeDecimal(row["30Days"], 0).ToString("#,##0.00");
            rowMast["60Days"] = SafeValue.SafeDecimal(row["60Days"], 0).ToString("#,##0.00");
            rowMast["90Days"] = SafeValue.SafeDecimal(row["90Days"], 0).ToString("#,##0.00");
            rowMast["120Days"] = SafeValue.SafeDecimal(row["120Days"], 0).ToString("#,##0.00");
            rowMast["OverDue"] = SafeValue.SafeDecimal(row["OverDue"], 0).ToString("#,##0.00");
            rowMast["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
            rowMast["UserId"] = userId;

            mast.Rows.Add(rowMast);
        }

        return mast;
    }

    public static DataTable DsApAgingDetail(DateTime d1, DateTime d2, string curyType, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        string acCode = System.Configuration.ConfigurationManager.AppSettings["VendorLocalAccCode"];
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable tab = new DataTable();
        tab.Columns.Add("DatePeriod");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyGroup");
        tab.Columns.Add("Term");
        tab.Columns.Add("Tel");

        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("Rmk");
        tab.Columns.Add("Current");
        tab.Columns.Add("30Days");
        tab.Columns.Add("60Days");
        tab.Columns.Add("90Days");
        tab.Columns.Add("120Days");
        tab.Columns.Add("OverDue");
        tab.Columns.Add("TotAmt");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description, 
                      case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                    case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.ExportInd='Y' AND bill.CurrencyId='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' )
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        string payDocNo = "";
        string unPayDocNo = "";
        while (reader.Read())
        {
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();//
            string docType = reader["DocType"].ToString();
            string rmk = reader["Description"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string billNo = reader["DocNo"].ToString();
            docNo = docNo + "(" + billNo + ")";
            if (docType.ToUpper() == "PL")
            {
                if (excetpPl.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }
            else if (docType.ToUpper() == "VO")
            {
                if (excetpVo.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }



            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 1);

            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);
            if (balanceAmt != 0)
            {
                payDocNo += "'" + billNo + "',";
                decimal current = 0;
                decimal day30 = 0;
                decimal day60 = 0;
                decimal day90 = 0;
                decimal day120 = 0;
                decimal over120 = 0;


                int days = DiffDays(docDate, d2, "M");

                if (days <= 0)
                    current += balanceAmt;
                else if (days <= 30)
                    day30 += balanceAmt;
                else if (days <= 60)
                    day60 += balanceAmt;
                else if (days <= 90)
                    day90 += balanceAmt;
                else if (days <= 120)
                    day120 += balanceAmt;
                else
                    over120 += balanceAmt;


                DataRow row = tab.NewRow();
                row["PartyCode"] = partyTo;
                DataTable tab_Party = Helper.Sql.List("SELECT Name, GroupId, Address, Tel1, Fax1, Email1, Contact1, TermId FROM XXParty WHERE PartyId = '" + row["PartyCode"] + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["Term"] = tab_Party.Rows[0]["TermId"];
                    row["Tel"] = tab_Party.Rows[0]["Tel1"];
                    string groupName = SafeValue.SafeString(tab_Party.Rows[0]["GroupId"]);
                    row["PartyGroup"] = groupName;
                }

                row["DocNo"] = docNo;
                row["DocType"] = docType;

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
                row["Rmk"] = rmk;


                row["Currency"] = currency;
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
                row["UserId"] = userId;
                tab.Rows.Add(row);
            }
            else
            {
                unPayDocNo += "'" + billNo + "',";
            }
        }

        return tab;
    }
    public static DataTable DsApAgingDetail_ForeignLocal(DateTime d1, DateTime d2, string curyType, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable tab = new DataTable();
        tab.Columns.Add("DatePeriod");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyGroup");
        tab.Columns.Add("Term");
        tab.Columns.Add("Tel");

        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("Rmk");
        tab.Columns.Add("Current");
        tab.Columns.Add("30Days");
        tab.Columns.Add("60Days");
        tab.Columns.Add("90Days");
        tab.Columns.Add("120Days");
        tab.Columns.Add("OverDue");
        tab.Columns.Add("TotAmt");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description,
                     case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                     case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.ExportInd='Y' AND bill.CurrencyId!='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' )
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        string payDocNo = "";
        string unPayDocNo = "";
        while (reader.Read())
        {
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();//
            string docType = reader["DocType"].ToString();
            string rmk = reader["Description"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();

            string billNo = reader["DocNo"].ToString();
            docNo = docNo + "(" + billNo + ")";
            if (docType.ToUpper() == "PL")
            {
                if (excetpPl.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }
            else if (docType.ToUpper() == "VO")
            {
                if (excetpVo.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }



            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 1);

            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);
            if (balanceAmt == docAmt)
                balanceAmt = locAmt;
            else
                balanceAmt = SafeValue.ChinaRound(balanceAmt * exRate, 2);
            if (balanceAmt != 0)
            {
                payDocNo += "'" + billNo + "',";
                decimal current = 0;
                decimal day30 = 0;
                decimal day60 = 0;
                decimal day90 = 0;
                decimal day120 = 0;
                decimal over120 = 0;


                int days = DiffDays(docDate, d2, "M");
                if (days <= 0)
                    current += balanceAmt;
                else if (days <= 30)
                    day30 += balanceAmt;
                else if (days <= 60)
                    day60 += balanceAmt;
                else if (days <= 90)
                    day90 += balanceAmt;
                else if (days <= 120)
                    day120 += balanceAmt;
                else
                    over120 += balanceAmt;


                DataRow row = tab.NewRow();
                row["PartyCode"] = partyTo;
                DataTable tab_Party = Helper.Sql.List("SELECT Name, GroupId, Address, Tel1, Fax1, Email1, Contact1, TermId FROM XXParty WHERE PartyId = '" + row["PartyCode"] + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["Term"] = tab_Party.Rows[0]["TermId"];
                    row["Tel"] = tab_Party.Rows[0]["Tel1"];
                    string groupName = SafeValue.SafeString(tab_Party.Rows[0]["GroupId"]);
                    row["PartyGroup"] = groupName;
                }

                row["DocNo"] = docNo;
                row["DocType"] = docType;

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
                row["Rmk"] = rmk;


                row["Currency"] = currency;
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
                row["UserId"] = userId;
                tab.Rows.Add(row);
            }
            else
            {
                unPayDocNo += "'" + billNo + "',";
            }
        }

        return tab;
    }
    public static DataTable DsApAgingDetail_ForeignForeign(DateTime d1, DateTime d2, string curyType, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        curyType = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        DataTable tab = new DataTable();
        tab.Columns.Add("DatePeriod");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyGroup");
        tab.Columns.Add("Term");
        tab.Columns.Add("Tel");

        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("Rmk");
        tab.Columns.Add("Current");
        tab.Columns.Add("30Days");
        tab.Columns.Add("60Days");
        tab.Columns.Add("90Days");
        tab.Columns.Add("120Days");
        tab.Columns.Add("OverDue");
        tab.Columns.Add("TotAmt");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        string sql = string.Format(@"select  *,(DocAmt+RepAmt+PayAmt) as BalAmt from (
SELECT  bill.DocType, bill.DocNo, bill.SupplierBillNo,bill.AcSource, bill.DocDate, bill.PartyTo, bill.CurrencyId, bill.ExRate,bill.Description,
                     case when DocType='SC' then -bill.LocAmt else bill.LocAmt end as LocAmt,
                     case when DocType='SC' then -bill.DocAmt else bill.DocAmt end as DocAmt,
                     case when DocType='SC' then isnull(rep.RepAmt,0) else -isnull(rep.RepAmt,0) end  as RepAmt,
                     case when DocType='SC' then isnull(PayAmt,0) else -isnull(PayAmt,0) end  as PayAmt
FROM         xaappayable AS bill left join 
                          (SELECT det.DocId, SUM(det.DocAmt) AS RepAmt
                            FROM          XAArReceiptDet AS det INNER JOIN
                                                   XAArReceipt AS mast ON det.RepId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS rep ON rep.DocId = bill.SequenceId
                            left join 
                          (SELECT     det.DocId, SUM(det.DocAmt) AS PayAmt
                            FROM          XAAppaymentDet AS det INNER JOIN
                                                   XAAppayment AS mast ON det.PayId = mast.SequenceId
                            WHERE    (det.DocType = 'PL' OR
                                                   det.DocType = 'SC' or det.DocType = 'SD') AND (mast.CancelInd = 'N') AND (mast.ExportInd = 'Y')
                                                   and mast.DocDate>='{0}' and mast.DocDate<'{1}'
                            GROUP BY det.DocId) AS pay ON pay.DocId = bill.SequenceId
WHERE bill.DocDate>='{0}' and bill.DocDate<'{1}' 
and bill.ExportInd='Y' AND bill.CurrencyId!='{2}' and bill.CancelInd='N' 
and (bill.doctype='PL' or bill.doctype='SC' or bill.doctype='SD' )
) as aa where (DocAmt+RepAmt+PayAmt)>0 or (DocAmt+RepAmt+PayAmt)<0", dateFrm, dateTo, curyType);
        sql += " order by PartyTo,DocDate,SupplierBillNo,DocType,DocNo";



        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.

        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        string payDocNo = "";
        string unPayDocNo = "";
        while (reader.Read())
        {
            string docNo = reader["SupplierBillNo"].ToString();//reader["DocNo"].ToString();//
            string docType = reader["DocType"].ToString();
            string rmk = reader["Description"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));//SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string partyTo = reader["PartyTo"].ToString();
            string currency = reader["CurrencyId"].ToString();
            string billNo = reader["DocNo"].ToString();
            if (billNo == "5595")
            {
            }
            docNo = docNo + "(" + billNo + ")";
            if (docType.ToUpper() == "PL")
            {
                if (excetpPl.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }
            else if (docType.ToUpper() == "VO")
            {
                if (excetpVo.IndexOf(billNo + ",") != -1)
                {
                    continue;
                }
            }



            //decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            //decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            //decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 1);

            decimal balanceAmt = SafeValue.SafeDecimal(reader["BalAmt"], 0);

            if (balanceAmt != 0)
            {
                payDocNo += "'" + billNo + "',";
                decimal current = 0;
                decimal day30 = 0;
                decimal day60 = 0;
                decimal day90 = 0;
                decimal day120 = 0;
                decimal over120 = 0;


                int days = DiffDays(docDate, d2, "M");
                if (days <= 0)
                    current += balanceAmt;
                else if (days <= 30)
                    day30 += balanceAmt;
                else if (days <= 60)
                    day60 += balanceAmt;
                else if (days <= 90)
                    day90 += balanceAmt;
                else if (days <= 120)
                    day120 += balanceAmt;
                else
                    over120 += balanceAmt;


                DataRow row = tab.NewRow();
                row["PartyCode"] = partyTo;
                DataTable tab_Party = Helper.Sql.List("SELECT Name, GroupId, Address, Tel1, Fax1, Email1, Contact1, TermId FROM XXParty WHERE PartyId = '" + row["PartyCode"] + "'");
                if (tab_Party.Rows.Count == 1)
                {
                    row["PartyName"] = tab_Party.Rows[0]["Name"];
                    row["Term"] = tab_Party.Rows[0]["TermId"];
                    row["Tel"] = tab_Party.Rows[0]["Tel1"];
                    string groupName = SafeValue.SafeString(tab_Party.Rows[0]["GroupId"]);
                    row["PartyGroup"] = groupName;
                }

                row["DocNo"] = docNo;
                row["DocType"] = docType;

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["DatePeriod"] = "From " + d1.ToString("dd/MM/yyyy") + " To " + d2.ToString("dd/MM/yyyy");
                row["Rmk"] = rmk;


                row["Currency"] = currency;
                row["Current"] = current.ToString("#,##0.00");
                row["30Days"] = day30.ToString("#,##0.00");
                row["60Days"] = day60.ToString("#,##0.00");
                row["90Days"] = day90.ToString("#,##0.00");
                row["120Days"] = day120.ToString("#,##0.00");
                row["OverDue"] = over120.ToString("#,##0.00");
                row["TotAmt"] = (SafeValue.SafeDecimal(row["Current"], 0) + SafeValue.SafeDecimal(row["30Days"], 0) + SafeValue.SafeDecimal(row["60Days"], 0) + SafeValue.SafeDecimal(row["90Days"], 0) + SafeValue.SafeDecimal(row["120Days"], 0) + SafeValue.SafeDecimal(row["OverDue"], 0)).ToString("#,##0.00");
                row["UserId"] = userId;
                tab.Rows.Add(row);
            }
            else
            {
                unPayDocNo += "'" + billNo + "',";
            }
        }

        return tab;
    }

    #endregion
}