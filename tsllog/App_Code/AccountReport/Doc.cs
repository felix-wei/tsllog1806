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
    public AccountPrint()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region document
    public static DataSet DsArReceipt(string docId)
    {
       string sqlMast = string.Format(@"SELECT SequenceId,DocType, DocNo, DocDate, DocCurrency, DocExRate, PartyTo, DocAmt, LocAmt, AcCode, ChqNo, ExportInd, BankName, Remark, 
 UserId FROM XAArReceipt WHERE (SequenceId = '{0}')", docId);
        string sqlDet = string.Format(@"SELECT AcCode,AcSource, DocId,DocType,DocAmt, LocAmt, Currency, ExRate, Remark1
FROM XAArReceiptDet WHERE (RepId = '{0}')", docId);
        DataTable mast = new DataTable("mast");
        mast.Columns.Add("Oid");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("BankName");
        mast.Columns.Add("DocAmt");
        mast.Columns.Add("LocAmt");
        mast.Columns.Add("TotDocAmt");
        mast.Columns.Add("TotLocAmt");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Remark");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("Oid");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcSource");
        det.Columns.Add("Rmk");
        det.Columns.Add("Due");
        det.Columns.Add("Applied");
        det.Columns.Add("ExRate");
        det.Columns.Add("Local");
        try
        {
            DataTable tab_mast = Helper.Sql.List(sqlMast);
            DataTable tab_det = Helper.Sql.List(sqlDet);
            if (tab_mast.Rows.Count > 0)
            {
                decimal totDocAmt = 0;
                decimal totLocAmt = 0;
                string docType = tab_mast.Rows[0]["DocType"].ToString();
                decimal exRate = SafeValue.SafeDecimal(tab_mast.Rows[0]["DocExRate"], 1);
                string currency = tab_mast.Rows[0]["DocCurrency"].ToString().ToUpper();
                for (int i = 0; i < tab_det.Rows.Count; i++)
                {
                    string acSource = tab_det.Rows[i]["AcSource"].ToString();
                    DataRow rowDet = det.NewRow();
                    rowDet["Oid"] = docId;
                    rowDet["AcCode"] = tab_det.Rows[i]["AcCode"];
                    rowDet["AcSource"] = acSource;
                    rowDet["Rmk"] = tab_det.Rows[i]["Remark1"];
                    decimal lineDocAmt = SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0);
                    decimal lineLocAmt = SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                    string lineCurrency = SafeValue.SafeString(tab_det.Rows[i]["Currency"], "").ToUpper();
                    rowDet["Due"] = lineDocAmt.ToString("#,##0.00");
                    rowDet["Applied"] = lineDocAmt.ToString("#,##0.00");
                    rowDet["ExRate"] = Helper.Safe.AccountEx(tab_det.Rows[i]["ExRate"]);
                    rowDet["Local"] = lineLocAmt.ToString("#,##0.00");
                    if (docType == "RE")
                    {
                        if (acSource == "CR")
                        {
                            totLocAmt += lineLocAmt;
                            if (currency == lineCurrency)
                                totDocAmt += lineDocAmt;
                        }
                        else
                        {
                            totLocAmt -= lineLocAmt;
                            if (currency == lineCurrency)
                                totDocAmt -= lineDocAmt;
                        }
                    }
                    else
                    {
                        if (acSource == "DB")
                        {
                            totLocAmt += lineLocAmt;
                            if (currency == lineCurrency)
                                totDocAmt += lineDocAmt;
                        }
                        else
                        {
                            totLocAmt -= lineLocAmt;
                            if (currency == lineCurrency)
                                totDocAmt -= lineDocAmt;
                        }
                    }
                    det.Rows.Add(rowDet);
                }


                DataRow row = mast.NewRow();
                row["Oid"] = docId;
                row["DocNo"] = tab_mast.Rows[0]["DocNo"].ToString();
                row["DocType"] = docType;
                row["ChqNo"] = tab_mast.Rows[0]["ChqNo"];
                row["AcCode"] = tab_mast.Rows[0]["AcCode"];
                row["Remark"] = tab_mast.Rows[0]["Remark"];
                row["DocDate"] = SafeValue.SafeDateStr(tab_mast.Rows[0]["DocDate"]);
                string partyTo = tab_mast.Rows[0]["PartyTo"].ToString();
                if (partyTo.Length > 1)
                {
                    row["PartyTo"] = EzshipHelper.GetPartyName(partyTo);
                }

                row["Currency"] = currency;
                row["ExRate"] = Helper.Safe.AccountEx(exRate);
                row["BankName"] = tab_mast.Rows[0]["BankName"];
                row["DocAmt"] = Helper.Safe.AccountNz(tab_mast.Rows[0]["DocAmt"]); //totDocAmt.ToString("#,##0.00");
                row["LocAmt"] = Helper.Safe.AccountNz(tab_mast.Rows[0]["LocAmt"]); //totLocAmt.ToString("#,##0.00");
                row["TotDocAmt"] = Helper.Safe.AccountNz(totDocAmt); //totDocAmt.ToString("#,##0.00");
                row["TotLocAmt"] = Helper.Safe.AccountNz(totLocAmt); //totLocAmt.ToString("#,##0.00");
                row["UserId"] = tab_mast.Rows[0]["UserId"];
                mast.Rows.Add(row);
            }
        }
        catch
        {
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Oid"], det.Columns["Oid"]);
        ds.Relations.Add(r);
        return ds;
    }

    public static DataSet DsApVoucher(string docId)
    {
        string sqlMast = string.Format(@"SELECT  SequenceId, AcCode, DocType, DocNo, DocDate,SupplierBillNo, ChqNo, ChqDate, PartyTo, OtherPartyName, CurrencyId, ExRate, Description, DocAmt, LocAmt,UserId,Term
FROM XAApPayable WHERE(SequenceId = '{0}')", docId);
        string sqlDet = string.Format(@"SELECT     AcCode,AcSource, ChgCode, ChgDes1, ChgDes2, ChgDes3, ChgDes4, Currency, ExRate, DocAmt, LocAmt
FROM XAApPayableDet WHERE (DocId = '{0}')", docId);
        DataTable mast = new DataTable("mast");
        mast.Columns.Add("Oid");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("SupplierBillNo");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        // mast.Columns.Add("BankName");
        mast.Columns.Add("DocAmt");
        mast.Columns.Add("LocAmt");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Remark");
        mast.Columns.Add("CompanyName");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("Oid");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcSource");
        det.Columns.Add("Rmk");
        det.Columns.Add("Currency");
        det.Columns.Add("ExRate");
        det.Columns.Add("DocAmt");
        det.Columns.Add("LocAmt");
        try
        {
            DataTable tab_mast = Helper.Sql.List(sqlMast);
            DataTable tab_det = Helper.Sql.List(sqlDet);
            if (tab_mast.Rows.Count > 0)
            {
                decimal totAmt = 0;
                for (int i = 0; i < tab_det.Rows.Count; i++)
                {
                    string acSource = tab_det.Rows[i]["AcSource"].ToString();
                    DataRow rowDet = det.NewRow();
                    rowDet["Oid"] = docId;
                    rowDet["AcCode"] = tab_det.Rows[i]["AcCode"];
                    rowDet["AcSource"] = acSource;
                    rowDet["Rmk"] = tab_det.Rows[i]["ChgDes1"];
                    rowDet["Currency"] = tab_det.Rows[i]["Currency"];
                    rowDet["ExRate"] = Helper.Safe.AccountEx(tab_det.Rows[i]["ExRate"]);
                    rowDet["DocAmt"] = Helper.Safe.AccountNz(tab_det.Rows[i]["DocAmt"]); //, 0).ToString("#,##0.00");
					
					decimal local_ = SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                    if (acSource == "DB") {
						rowDet["LocAmt"] = Helper.Safe.AccountNz(local_);
                        totAmt += local_;
					}
                    else {
						rowDet["LocAmt"] = Helper.Safe.AccountNz(local_*-1);
                        totAmt -= local_;
					}	
                    det.Rows.Add(rowDet);
                }


                DataRow row = mast.NewRow();
                row["Oid"] = docId;
                row["DocNo"] = tab_mast.Rows[0]["DocNo"].ToString();
                row["SupplierBillNo"] = tab_mast.Rows[0]["SupplierBillNo"].ToString();//tab_mast.Rows[0]["DocNo"].ToString();
                row["DocType"] = tab_mast.Rows[0]["DocType"];
                row["ChqNo"] = tab_mast.Rows[0]["ChqNo"];
                row["AcCode"] = tab_mast.Rows[0]["AcCode"];
                row["Remark"] = tab_mast.Rows[0]["Description"];
                row["DocDate"] = SafeValue.SafeDateStr(tab_mast.Rows[0]["DocDate"]); ;//SafeValue.SafeDateStr(tab_mast.Rows[0]["DocDate"]);
                string partyTo = tab_mast.Rows[0]["PartyTo"].ToString();
                if (partyTo.Length > 3)
                {
                    row["PartyTo"] = EzshipHelper.GetPartyName(partyTo);
                }
                else
                {
                    row["PartyTo"] = tab_mast.Rows[0]["OtherPartyName"];
                }
                decimal exRate = SafeValue.SafeDecimal(tab_mast.Rows[0]["ExRate"], 1);
                string currency = tab_mast.Rows[0]["CurrencyId"].ToString();
                row["Currency"] = currency;
                row["ExRate"] = Helper.Safe.AccountEx(exRate); 
                row["DocAmt"] = totAmt.ToString("#,##0.00");
                row["LocAmt"] = totAmt.ToString("#,##0.00");
                row["UserId"] = tab_mast.Rows[0]["UserId"];
                row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                mast.Rows.Add(row);
            }
        }
        catch
        {
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Oid"], det.Columns["Oid"]);
        ds.Relations.Add(r);
        return ds;
    }
    public static DataSet DsApPayment(string docId)
    {
        string sqlMast = string.Format(@"SELECT SequenceId, AcCode, DocType, DocNo, DocDate, ChqNo, ChqDate, PartyTo, OtherPartyName, CurrencyId, ExRate, DocAmt, LocAmt, UserId, Remark
FROM  XAApPayment WHERE (SequenceId = '{0}')", docId);
        string sqlDet = string.Format(@"SELECT     AcCode,AcSource, Remark1, Remark2, Remark3, Currency, ExRate, DocAmt, LocAmt
FROM XAApPaymentDet WHERE (PayId = '{0}')", docId);
        DataTable mast = new DataTable("mast");
        mast.Columns.Add("Oid");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("PartyTo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        // mast.Columns.Add("BankName");
        mast.Columns.Add("DocAmt");
        mast.Columns.Add("LocAmt");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Remark");
        mast.Columns.Add("CompanyName");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("Oid");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcSource");
        det.Columns.Add("Rmk");
        det.Columns.Add("Currency");
        det.Columns.Add("ExRate");
        det.Columns.Add("DocAmt");
        det.Columns.Add("LocAmt");
        try
        {
            DataTable tab_mast = Helper.Sql.List(sqlMast);
            DataTable tab_det = Helper.Sql.List(sqlDet);
            if (tab_mast.Rows.Count > 0)
            {
                string docType = tab_mast.Rows[0]["DocType"].ToString();
                string currency = tab_mast.Rows[0]["CurrencyId"].ToString().ToUpper();
                decimal exRate = SafeValue.SafeDecimal(tab_mast.Rows[0]["ExRate"], 1);
                decimal totLocAmt = 0;
                decimal totDocAmt = 0;
                if (tab_det.Rows.Count == 0)
                {
                    DataRow rowDet = det.NewRow();
                    rowDet["Oid"] = docId;
                    rowDet["AcCode"] = "";
                    rowDet["Rmk"] = "";
                    rowDet["Currency"] = "";
                    rowDet["ExRate"] = "";
                    rowDet["DocAmt"] = "0.00";
                    rowDet["LocAmt"] = "0.00";
                    det.Rows.Add(rowDet);
                }
                else
                {
                    for (int i = 0; i < tab_det.Rows.Count; i++)
                    {
                        string acSource = tab_det.Rows[i]["AcSource"].ToString();
                        DataRow rowDet = det.NewRow();
                        rowDet["Oid"] = docId;
                        rowDet["AcCode"] = tab_det.Rows[i]["AcCode"];
                        rowDet["AcSource"] = tab_det.Rows[i]["AcSource"];
                        rowDet["Rmk"] = tab_det.Rows[i]["Remark1"];
                        string lineCurrency = tab_det.Rows[i]["Currency"].ToString().ToUpper();
                        rowDet["Currency"] = lineCurrency;
                        rowDet["ExRate"] = tab_det.Rows[i]["ExRate"];
                        rowDet["DocAmt"] = SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0).ToString("#,##0.00");
                        rowDet["LocAmt"] = SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0).ToString("#,##0.00");
                        if (docType == "PS")
                        {
                            if (acSource == "DB")
                            {
                                totLocAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                                if (currency == lineCurrency)
                                    totDocAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0);
                            }
                            else
                            {
                                totLocAmt -= SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                                if (currency == lineCurrency)
                                    totDocAmt -= SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0);
                            }
                        }
                        else
                        {
                            if (acSource == "CR")
                            {
                                totLocAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                                if (currency == lineCurrency)
                                    totDocAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0);
                            }
                            else
                            {
                                totLocAmt -= SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
                                if (currency == lineCurrency)
                                    totDocAmt -= SafeValue.SafeDecimal(tab_det.Rows[i]["DocAmt"], 0);
                            }
                        }
                        det.Rows.Add(rowDet);
                    }
                }


                DataRow row = mast.NewRow();
                row["Oid"] = docId;
                row["DocNo"] = tab_mast.Rows[0]["DocNo"].ToString();
                row["DocType"] = docType;
                row["ChqNo"] = tab_mast.Rows[0]["ChqNo"];
                row["AcCode"] = tab_mast.Rows[0]["AcCode"];
                row["Remark"] = tab_mast.Rows[0]["Remark"];
                row["DocDate"] = SafeValue.SafeDateStr(tab_mast.Rows[0]["DocDate"]);
                string partyTo = tab_mast.Rows[0]["PartyTo"].ToString();
                if (partyTo.Length > 3)
                {
                    row["PartyTo"] = EzshipHelper.GetPartyName(partyTo);
                }
                else
                {
                    row["PartyTo"] = tab_mast.Rows[0]["OtherPartyName"];
                }
                row["Currency"] = currency;
                row["ExRate"] = exRate.ToString("#,##0.00");
                //row["BankName"] = tab_mast.Rows[0]["BankName"];
                row["DocAmt"] = totDocAmt.ToString("#,##0.00");
                row["LocAmt"] = totLocAmt.ToString("#,##0.00");
                row["UserId"] = tab_mast.Rows[0]["UserId"];
                row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                mast.Rows.Add(row);
            }
        }
        catch
        {
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Oid"], det.Columns["Oid"]);
        ds.Relations.Add(r);
        return ds;
    }

    public static DataSet DsJournalEntry(string docId)
    {
        string sqlMast = string.Format(@"SELECT SequenceId, DocNo, DocType, DocDate, CurrencyId, ExRate, CrAmt, DbAmt, CurrencyCrAmt, CurrencyDbAmt, Remark, UserId, EntryDate
FROM XAGlEntry WHERE (SequenceId = '{0}')", docId);
        string sqlDet = string.Format(@"SELECT GlLineNo, AcCode, AcSource, CurrencyId, ExRate, CrAmt, DbAmt, CurrencyCrAmt, CurrencyDbAmt, Remark
FROM XAGlEntryDet WHERE (GlNo = '{0}')", docId);
        DataTable mast = new DataTable("mast");
        mast.Columns.Add("Oid");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("DocAmt");
        mast.Columns.Add("LocAmt");
        mast.Columns.Add("Remark");
        mast.Columns.Add("UserId");
        mast.Columns.Add("CustName");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("DbAmt");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("Oid");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcDes");
        det.Columns.Add("AcSource");
        det.Columns.Add("Rmk");
        det.Columns.Add("Currency");
        det.Columns.Add("ExRate");
        det.Columns.Add("CrAmt");
        det.Columns.Add("DbAmt");
        try
        {
            DataTable tab_mast = Helper.Sql.List(sqlMast);
            DataTable tab_det = Helper.Sql.List(sqlDet);
            if (tab_mast.Rows.Count > 0)
            {
                decimal crAmt = 0;
                decimal dbAmt = 0;
                for (int i = 0; i < tab_det.Rows.Count; i++)
                {
                    string acSource = tab_det.Rows[i]["AcSource"].ToString();
                    DataRow rowDet = det.NewRow();
                    rowDet["Oid"] = docId;
                    rowDet["AcCode"] = tab_det.Rows[i]["AcCode"];
                    rowDet["AcDes"] = ConnectSql.ExecuteScalar(string.Format("SELECT AcDesc FROM XXChartAcc where Code='{0}'", tab_det.Rows[i]["AcCode"]));

                    rowDet["AcSource"] = tab_det.Rows[i]["AcSource"];
                    rowDet["Rmk"] = tab_det.Rows[i]["Remark"];
                    rowDet["Currency"] = tab_det.Rows[i]["CurrencyId"];
                    rowDet["ExRate"] = SafeValue.SafeDecimal(tab_det.Rows[i]["ExRate"], 1).ToString("0.000");
                    if (acSource == "DB")
                    {
                        rowDet["DbAmt"] = SafeValue.SafeDecimal(tab_det.Rows[i]["CurrencyDbAmt"], 0).ToString("#,##0.00");
                        dbAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["CurrencyDbAmt"], 0);
                    }
                    else
                    {
                        rowDet["CrAmt"] = SafeValue.SafeDecimal(tab_det.Rows[i]["CurrencyCrAmt"], 0).ToString("#,##0.00");
                        crAmt += SafeValue.SafeDecimal(tab_det.Rows[i]["CurrencyCrAmt"], 0);
                    }
                    det.Rows.Add(rowDet);
                }


                DataRow row = mast.NewRow();
                row["Oid"] = docId;
                row["DocNo"] = tab_mast.Rows[0]["DocNo"].ToString();
                row["DocType"] = tab_mast.Rows[0]["DocType"];
                row["Remark"] = tab_mast.Rows[0]["Remark"];
                row["DocDate"] = SafeValue.SafeDateStr(tab_mast.Rows[0]["DocDate"]);
                decimal exRate = SafeValue.SafeDecimal(tab_mast.Rows[0]["ExRate"], 1);
                string currency = tab_mast.Rows[0]["CurrencyId"].ToString();
                row["Currency"] = currency;
                row["ExRate"] = exRate.ToString("#,##0.000");
                row["DocAmt"] = SafeValue.SafeDecimal(tab_mast.Rows[0]["CrAmt"], 1).ToString("#,##0.00");
                row["LocAmt"] = SafeValue.SafeDecimal(tab_mast.Rows[0]["CurrencyCrAmt"], 1).ToString("#,##0.00");
                row["UserId"] = tab_mast.Rows[0]["UserId"];
                row["CustName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                row["CrAmt"] = crAmt.ToString("#,##0.00");
                row["DbAmt"] = dbAmt.ToString("#,##0.00");
                mast.Rows.Add(row);
            }
        }
        catch
        {
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Oid"], det.Columns["Oid"]);
        ds.Relations.Add(r);
        return ds;
    }

    #endregion

    private static int DiffDays(DateTime docDate, DateTime printDate, string diffType)
    {//diffType=D or M
        TimeSpan ts = printDate.Subtract(docDate);
        int days = ts.Days;
        if (diffType == "M")
        {
            DateTime startDate_current = new DateTime(printDate.Year, printDate.Month, 1);
            DateTime endDate_current = startDate_current.AddMonths(1);
            DateTime startDate_30 = startDate_current.AddMonths(-1);
            DateTime startDate_60 = startDate_current.AddMonths(-2);
            DateTime startDate_90 = startDate_current.AddMonths(-3);
            DateTime startDate_120 = startDate_current.AddMonths(-4);
            if (docDate >= startDate_current)
                days = 0;
            else if (docDate >= startDate_30 && docDate < startDate_current)
                days = 30;
            else if (docDate >= startDate_60 && docDate < startDate_30)
                days = 60;
            else if (docDate >= startDate_90 && docDate < startDate_60)
                days = 90;
            else if (docDate >= startDate_120 && docDate < startDate_90)
                days = 120;
            else
                days = 150;
        }
        return days;
    }
 
    private static string GetObj(string sql)
    {
        string s = "";
        try
        {
            s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
        }
        catch
        { }
        return s;
    }

}
