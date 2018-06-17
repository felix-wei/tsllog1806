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




    #region other report
   
    public static DataSet DsArDocListing_Sum(DateTime d1, DateTime d2, string partyTo, string docType, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("Index");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("DueDate");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("LocAmt");

        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");
        det.Columns.Add("LocDbAmt");
        det.Columns.Add("LocCrAmt");
        string sql = "";
        if (docType == "IV" || docType == "CN" || docType == "DN")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource,DocNo,DocType, DocDate,DocDueDate, PartyTo,CurrencyId,ExRate,'' as ChqNo,DocAmt, LocAmt
FROM XAArInvoice where DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo='" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        else if (docType == "RE" || docType == "PC")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource,DocNo,DocType, DocDate, PartyTo, DocCurrency as CurrencyId, DocExRate as ExRate,ChqNo,DocAmt, LocAmt
FROM XAArReceipt where  DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo='" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        int index = 1;
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string sql_det = "";
            string title = "AR INVOICE LISTING";
            if (docType == "IV")
            {
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "DN")
            {
                title = "AR DEBIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "CN")
            {
                title = "AR CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "RE")
            {
                title = "AR RECEIPT LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArReceiptDet WHERE RepId = '{0}'", sequenceId);
            }
            else if (docType == "PC")
            {
                title = "AR PAYMENT CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArReceiptDet WHERE RepId = '{0}'", sequenceId);
            }
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            DataRow row = mast.NewRow();
            row["Title"] = title;
            row["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            row["UserId"] = userId;
            row["Index"] = index;
            row["DocNo"] = reader["DocNo"].ToString();
            row["DocType"] = docType;
            row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
            row["DueDate"] = "";
            if (docType == "IV" || docType == "CN" || docType == "DN")
                row["DueDate"] = SafeValue.SafeDateStr(reader["DocDueDate"]);
            string partyId = reader["PartyTo"].ToString();
            string partyName = EzshipHelper.GetPartyName(partyId);
            row["PartyId"] = partyId;
            row["PartyName"] = partyName;
            row["ChqNo"] = reader["ChqNo"].ToString();
            row["Currency"] = reader["CurrencyId"].ToString();
            row["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
            row["LocAmt"] = locAmt.ToString("#,##0.00");
            index++;
            mast.Rows.Add(row);


            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (i == 0)
                {
                    DataRow detRow = det.NewRow();
                    detRow["AcCode"] = reader["AcCode"].ToString();
                    detRow["DbAmt"] = 0;
                    detRow["CrAmt"] = 0;
                    detRow["LocDbAmt"] = 0;
                    detRow["LocCrAmt"] = 0;
                    string acDorc = reader["AcSource"].ToString();
                    if (acDorc == "DB")
                    {
                        detRow["DbAmt"] = docAmt;
                        detRow["LocDbAmt"] = locAmt;
                    }
                    else
                    {
                        detRow["CrAmt"] = docAmt;
                        detRow["LocCrAmt"] = locAmt;
                    }
                    det.Rows.Add(detRow);
                }

                DataRow detRow1 = det.NewRow();
                detRow1["AcCode"] = tab.Rows[i]["AcCode"].ToString();
                detRow1["DbAmt"] = 0;
                detRow1["CrAmt"] = 0;
                detRow1["LocDbAmt"] = 0;
                detRow1["LocCrAmt"] = 0;
                string acDorc1 = tab.Rows[i]["AcSource"].ToString();
                if (acDorc1 == "DB")
                {
                    detRow1["DbAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    detRow1["LocDbAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                }
                else
                {
                    detRow1["CrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    detRow1["LocCrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                }
                det.Rows.Add(detRow1);
            }
        }
        reader.Close();
        reader.Dispose();
        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("table1", det, "AcCode,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt,sum(LocDbAmt) LocDbAmt,sum(LocCrAmt) LocCrAmt", "", "AcCode");

        DataTable rptDet = new DataTable("Detail");
        rptDet.Columns.Add("AcCode");
        rptDet.Columns.Add("AcDesc");
        rptDet.Columns.Add("DbAmt");
        rptDet.Columns.Add("CrAmt");
        rptDet.Columns.Add("LocDbAmt");
        rptDet.Columns.Add("LocCrAmt");

        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow rptRow = rptDet.NewRow();
            rptRow["AcCode"] = tab1.Rows[i]["AcCode"];
            rptRow["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab1.Rows[i]["AcCode"] + "'");
            rptRow["DbAmt"] = tab1.Rows[i]["DbAmt"];
            rptRow["CrAmt"] = tab1.Rows[i]["CrAmt"];
            rptRow["LocDbAmt"] = tab1.Rows[i]["LocDbAmt"];
            rptRow["LocCrAmt"] = tab1.Rows[i]["LocCrAmt"];
            rptDet.Rows.Add(rptRow);
        }
        set.Tables.Add(mast);
        set.Tables.Add(rptDet);

        return set;
    }
    public static DataSet DsArDocListing_Detail(DateTime d1, DateTime d2, string partyTo, string docType, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("Index");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("DueDate");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("LocAmt");

        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("LocDbAmt");
        mast.Columns.Add("LocCrAmt");

        mast.Columns.Add("DbAmt1");
        mast.Columns.Add("CrAmt1");
        mast.Columns.Add("LocDbAmt1");
        mast.Columns.Add("LocCrAmt1");

        //DataTable det = new DataTable("Detail");
        //det.Columns.Add("AcCode");
        //mast.Columns.Add("AcDesc");
        //det.Columns.Add("DbAmt");
        //det.Columns.Add("CrAmt");
        //det.Columns.Add("LocDbAmt");
        //det.Columns.Add("LocCrAmt");
        string sql = "";
        if (docType == "IV" || docType == "CN" || docType == "DN")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource,DocNo,DocType, DocDate,DocDueDate, PartyTo,CurrencyId,ExRate,'' as ChqNo,DocAmt, LocAmt
FROM XAArInvoice where DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo='" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        else if (docType == "RE" || docType == "PC")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource,DocNo,DocType, DocDate, PartyTo, DocCurrency as CurrencyId, DocExRate as ExRate,ChqNo,DocAmt, LocAmt
FROM XAArReceipt where  DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo='" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        int index = 1;
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string sql_det = "";
            string title = "AR INVOICE LISTING";
            if (docType == "IV")
            {
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "DN")
            {
                title = "AR DEBIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "CN")
            {
                title = "AR CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArInvoiceDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "RE")
            {
                title = "AR RECEIPT LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArReceiptDet WHERE RepId = '{0}'", sequenceId);
            }
            else if (docType == "PC")
            {
                title = "AR PAYMENT CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAArReceiptDet WHERE RepId = '{0}'", sequenceId);
            }
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);



            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {

                DataRow row1 = mast.NewRow();
                row1["Title"] = title;
                row1["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                row1["UserId"] = userId;
                row1["Index"] = index;
                row1["DocNo"] = reader["DocNo"].ToString();
                row1["DocType"] = docType;
                row1["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                row1["DueDate"] = "";
                if (docType == "IV" || docType == "CN" || docType == "DN")
                    row1["DueDate"] = SafeValue.SafeDateStr(reader["DocDueDate"]);
                string partyId1 = reader["PartyTo"].ToString();
                string partyName1 = EzshipHelper.GetPartyName(partyId1);
                row1["PartyId"] = partyId1;
                row1["PartyName"] = partyName1;
                row1["ChqNo"] = reader["ChqNo"].ToString();
                row1["Currency"] = reader["CurrencyId"].ToString();
                row1["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
                row1["LocAmt"] = 0;

                row1["AcCode"] = tab.Rows[i]["AcCode"].ToString();
                row1["DbAmt"] = 0;
                row1["CrAmt"] = 0;
                row1["LocDbAmt"] = 0;
                row1["LocCrAmt"] = 0;
                string acDorc1 = tab.Rows[i]["AcSource"].ToString();
                decimal detDocAmt = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                decimal detLocAmt = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                if (acDorc1 == "DB")
                {
                    row1["DbAmt"] = detDocAmt;
                    row1["LocDbAmt"] = detLocAmt;
                    if (docAmt > 0)
                    {
                        row1["DbAmt1"] = detDocAmt.ToString("#,##0.00");
                        row1["LocDbAmt1"] = detLocAmt.ToString("#,##0.00");
                    }
                }
                else
                {
                    row1["CrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    row1["LocCrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                    if (docAmt > 0)
                    {
                        row1["CrAmt1"] = detDocAmt.ToString("#,##0.00");
                        row1["LocCrAmt1"] = detLocAmt.ToString("#,##0.00");
                    }
                }
                mast.Rows.Add(row1);
                if (i == 0)
                {
                    DataRow row = mast.NewRow();
                    row["Title"] = title;
                    row["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                    row["UserId"] = userId;
                    row["Index"] = index;
                    row["DocNo"] = reader["DocNo"].ToString();
                    row["DocType"] = docType;
                    row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                    row["DueDate"] = "";
                    if (docType == "IV" || docType == "CN" || docType == "DN")
                        row["DueDate"] = SafeValue.SafeDateStr(reader["DocDueDate"]);
                    string partyId = reader["PartyTo"].ToString();
                    string partyName = EzshipHelper.GetPartyName(partyId);
                    row["PartyId"] = partyId;
                    row["PartyName"] = partyName;
                    row["ChqNo"] = reader["ChqNo"].ToString();
                    row["Currency"] = reader["CurrencyId"].ToString();
                    row["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
                    row["LocAmt"] = locAmt.ToString("#,##0.00");



                    row["AcCode"] = reader["AcCode"].ToString();
                    row["DbAmt"] = 0;
                    row["CrAmt"] = 0;
                    row["LocDbAmt"] = 0;
                    row["LocCrAmt"] = 0;
                    string acDorc = reader["AcSource"].ToString();
                    if (acDorc == "DB")
                    {
                        row["DbAmt"] = docAmt;
                        row["LocDbAmt"] = locAmt;
                        if (docAmt > 0)
                        {
                            row["DbAmt1"] = docAmt.ToString("#,##0.00");
                            row["LocDbAmt1"] = locAmt.ToString("#,##0.00");
                        }
                    }
                    else
                    {
                        row["CrAmt"] = docAmt;
                        row["LocCrAmt"] = locAmt;
                        if (docAmt > 0)
                        {
                            row["CrAmt1"] = docAmt.ToString("#,##0.00");
                            row["LocCrAmt1"] = locAmt.ToString("#,##0.00");
                        }
                    }
                    mast.Rows.Add(row);
                }
                else if (i == tab.Rows.Count - 1)
                    index++;

            }
        }
        reader.Close();
        reader.Dispose();
        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("table1", mast, "AcCode,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt,sum(LocDbAmt) LocDbAmt,sum(LocCrAmt) LocCrAmt", "", "AcCode");

        DataTable rptDet = new DataTable("Detail");
        rptDet.Columns.Add("AcCode");
        rptDet.Columns.Add("AcDesc");
        rptDet.Columns.Add("DbAmt");
        rptDet.Columns.Add("CrAmt");
        rptDet.Columns.Add("LocDbAmt");
        rptDet.Columns.Add("LocCrAmt");

        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow rptRow = rptDet.NewRow();
            rptRow["AcCode"] = tab1.Rows[i]["AcCode"];
            rptRow["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab1.Rows[i]["AcCode"] + "'");
            rptRow["DbAmt"] = tab1.Rows[i]["DbAmt"];
            rptRow["CrAmt"] = tab1.Rows[i]["CrAmt"];
            rptRow["LocDbAmt"] = tab1.Rows[i]["LocDbAmt"];
            rptRow["LocCrAmt"] = tab1.Rows[i]["LocCrAmt"];
            rptDet.Rows.Add(rptRow);
        }
        set.Tables.Add(mast);
        set.Tables.Add(rptDet);

        return set;
    }

    public static DataSet DsApDocListing_Sum(DateTime d1, DateTime d2, string partyTo, string docType, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("Index");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("DueDate");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("LocAmt");

        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");
        det.Columns.Add("LocDbAmt");
        det.Columns.Add("LocCrAmt");
        string sql = "";
        if (docType == "PL" || docType == "SC" || docType == "SD")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource, SupplierBillNo AS DocNo, DocType, DocDate AS DocDate, PartyTo, CurrencyId, ExRate, '' AS ChqNo, DocAmt, LocAmt
FROM XAApPayable where DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo'" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        else if (docType == "PS" || docType == "SR")
        {
            sql = string.Format(@"SELECT SequenceId, AcCode, AcSource, DocNo, DocType, DocDate, PartyTo, CurrencyId, ExRate, ChqNo, DocAmt, LocAmt
FROM XAApPayment where  DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo'" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        int index = 1;
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string sql_det = "";
            string title = "AP SUPPLIER INVOICE LISTING";
            if (docType == "PL")
            {
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "SD")
            {
                title = "AP SUPPLIER DEBIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "SC")
            {
                title = "AP SUPPLIER CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "PS")
            {
                title = "AP SUPPLIER PAYMENT LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPaymentDet WHERE PayId = '{0}'", sequenceId);
            }
            else if (docType == "PC")
            {
                title = "AP SUPPLIER REFUND LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPaymentDet WHERE PayId = '{0}'", sequenceId);
            }
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            DataRow row = mast.NewRow();
            row["Title"] = title;
            row["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            row["UserId"] = userId;
            row["Index"] = index;
            row["DocNo"] = reader["DocNo"].ToString();
            row["DocType"] = docType;
            row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
            row["DueDate"] = "";
            string partyId = reader["PartyTo"].ToString();
            string partyName = EzshipHelper.GetPartyName(partyId);
            row["PartyId"] = partyId;
            row["PartyName"] = partyName;
            row["ChqNo"] = reader["ChqNo"].ToString();
            row["Currency"] = reader["CurrencyId"].ToString();
            row["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
            row["LocAmt"] = locAmt.ToString("#,##0.00");
            index++;
            mast.Rows.Add(row);


            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                if (i == 0)
                {
                    DataRow detRow = det.NewRow();
                    detRow["AcCode"] = reader["AcCode"].ToString();
                    detRow["DbAmt"] = 0;
                    detRow["CrAmt"] = 0;
                    detRow["LocDbAmt"] = 0;
                    detRow["LocCrAmt"] = 0;
                    string acDorc = reader["AcSource"].ToString();
                    if (acDorc == "DB")
                    {
                        detRow["DbAmt"] = docAmt;
                        detRow["LocDbAmt"] = locAmt;
                    }
                    else
                    {
                        detRow["CrAmt"] = docAmt;
                        detRow["LocCrAmt"] = locAmt;
                    }
                    det.Rows.Add(detRow);
                }

                DataRow detRow1 = det.NewRow();
                detRow1["AcCode"] = tab.Rows[i]["AcCode"].ToString();
                detRow1["DbAmt"] = 0;
                detRow1["CrAmt"] = 0;
                detRow1["LocDbAmt"] = 0;
                detRow1["LocCrAmt"] = 0;
                string acDorc1 = tab.Rows[i]["AcSource"].ToString();
                if (acDorc1 == "DB")
                {
                    detRow1["DbAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    detRow1["LocDbAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                }
                else
                {
                    detRow1["CrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    detRow1["LocCrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                }
                det.Rows.Add(detRow1);
            }
        }
        reader.Close();
        reader.Dispose();
        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("table1", det, "AcCode,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt,sum(LocDbAmt) LocDbAmt,sum(LocCrAmt) LocCrAmt", "", "AcCode");

        DataTable rptDet = new DataTable("Detail");
        rptDet.Columns.Add("AcCode");
        rptDet.Columns.Add("AcDesc");
        rptDet.Columns.Add("DbAmt");
        rptDet.Columns.Add("CrAmt");
        rptDet.Columns.Add("LocDbAmt");
        rptDet.Columns.Add("LocCrAmt");

        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow rptRow = rptDet.NewRow();
            rptRow["AcCode"] = tab1.Rows[i]["AcCode"];
            rptRow["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab1.Rows[i]["AcCode"] + "'");
            rptRow["DbAmt"] = tab1.Rows[i]["DbAmt"];
            rptRow["CrAmt"] = tab1.Rows[i]["CrAmt"];
            rptRow["LocDbAmt"] = tab1.Rows[i]["LocDbAmt"];
            rptRow["LocCrAmt"] = tab1.Rows[i]["LocCrAmt"];
            rptDet.Rows.Add(rptRow);
        }
        set.Tables.Add(mast);
        set.Tables.Add(rptDet);

        return set;
    }
    public static DataSet DsApDocListing_Detail(DateTime d1, DateTime d2, string partyTo, string docType, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("Index");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("DueDate");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("PartyName");
        mast.Columns.Add("ChqNo");
        mast.Columns.Add("Currency");
        mast.Columns.Add("ExRate");
        mast.Columns.Add("LocAmt");

        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("LocDbAmt");
        mast.Columns.Add("LocCrAmt");

        mast.Columns.Add("DbAmt1");
        mast.Columns.Add("CrAmt1");
        mast.Columns.Add("LocDbAmt1");
        mast.Columns.Add("LocCrAmt1");

        string sql = "";
        if (docType == "PL" || docType == "SC" || docType == "SD")
        {
            sql = string.Format(@"SELECT  SequenceId, AcCode, AcSource, SupplierBillNo AS DocNo, DocType, DocDate AS DocDate, PartyTo, CurrencyId, ExRate, '' AS ChqNo, DocAmt, LocAmt
FROM XAApPayable where DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo'" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        else if (docType == "PS" || docType == "SR")
        {
            sql = string.Format(@"SELECT SequenceId, AcCode, AcSource, DocNo, DocType, DocDate, PartyTo, CurrencyId, ExRate, ChqNo, DocAmt, LocAmt
FROM XAApPayment where  DocDate>='{0}' and DocDate<'{1}' and DocType='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);
            if (partyTo.Length > 0 && "null" != partyTo)
            {
                sql += " and PartyTo'" + partyTo + "'";
            }
            sql += " order by PartyTo,DocType,DocNo";
        }
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        int index = 1;
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string sql_det = "";
            string title = "AP SUPPLIER INVOICE LISTING";
            if (docType == "PL")
            {
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "SD")
            {
                title = "AP SUPPLIER DEBIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "SC")
            {
                title = "AP SUPPLIER CREDIT NOTE LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPayableDet where DocId='{0}'", sequenceId);
            }
            else if (docType == "PS")
            {
                title = "AP SUPPLIER PAYMENT LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPaymentDet WHERE PayId = '{0}'", sequenceId);
            }
            else if (docType == "PC")
            {
                title = "AP SUPPLIER REFUND LISTING";
                sql_det = string.Format("SELECT AcCode, AcSource, DocAmt, LocAmt FROM XAApPaymentDet WHERE PayId = '{0}'", sequenceId);
            }
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);



            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {

                DataRow row1 = mast.NewRow();
                row1["Title"] = title;
                row1["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                row1["UserId"] = userId;
                row1["Index"] = index;
                row1["DocNo"] = reader["DocNo"].ToString();
                row1["DocType"] = docType;
                row1["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                row1["DueDate"] = "";
                if (docType == "IV" || docType == "CN" || docType == "DN")
                    row1["DueDate"] = SafeValue.SafeDateStr(reader["DocDueDate"]);
                string partyId1 = reader["PartyTo"].ToString();
                string partyName1 = EzshipHelper.GetPartyName(partyId1);
                row1["PartyId"] = partyId1;
                row1["PartyName"] = partyName1;
                row1["ChqNo"] = reader["ChqNo"].ToString();
                row1["Currency"] = reader["CurrencyId"].ToString();
                row1["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
                row1["LocAmt"] = 0;

                row1["AcCode"] = tab.Rows[i]["AcCode"].ToString();
                row1["DbAmt"] = 0;
                row1["CrAmt"] = 0;
                row1["LocDbAmt"] = 0;
                row1["LocCrAmt"] = 0;
                string acDorc1 = tab.Rows[i]["AcSource"].ToString();
                decimal detDocAmt = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                decimal detLocAmt = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                if (acDorc1 == "DB")
                {
                    row1["DbAmt"] = detDocAmt;
                    row1["LocDbAmt"] = detLocAmt;
                    if (docAmt > 0)
                    {
                        row1["DbAmt1"] = detDocAmt.ToString("#,##0.00");
                        row1["LocDbAmt1"] = detLocAmt.ToString("#,##0.00");
                    }
                }
                else
                {
                    row1["CrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                    row1["LocCrAmt"] = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                    if (docAmt > 0)
                    {
                        row1["CrAmt1"] = detDocAmt.ToString("#,##0.00");
                        row1["LocCrAmt1"] = detLocAmt.ToString("#,##0.00");
                    }
                }
                mast.Rows.Add(row1);
                if (i == 0)
                {
                    DataRow row = mast.NewRow();
                    row["Title"] = title;
                    row["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                    row["UserId"] = userId;
                    row["Index"] = index;
                    row["DocNo"] = reader["DocNo"].ToString();
                    row["DocType"] = docType;
                    row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                    row["DueDate"] = "";
                    if (docType == "IV" || docType == "CN" || docType == "DN")
                        row["DueDate"] = SafeValue.SafeDateStr(reader["DocDueDate"]);
                    string partyId = reader["PartyTo"].ToString();
                    string partyName = EzshipHelper.GetPartyName(partyId);
                    row["PartyId"] = partyId;
                    row["PartyName"] = partyName;
                    row["ChqNo"] = reader["ChqNo"].ToString();
                    row["Currency"] = reader["CurrencyId"].ToString();
                    row["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
                    row["LocAmt"] = locAmt.ToString("#,##0.00");



                    row["AcCode"] = reader["AcCode"].ToString();
                    row["DbAmt"] = 0;
                    row["CrAmt"] = 0;
                    row["LocDbAmt"] = 0;
                    row["LocCrAmt"] = 0;
                    string acDorc = reader["AcSource"].ToString();
                    if (acDorc == "DB")
                    {
                        row["DbAmt"] = docAmt;
                        row["LocDbAmt"] = locAmt;
                        if (docAmt > 0)
                        {
                            row["DbAmt1"] = docAmt.ToString("#,##0.00");
                            row["LocDbAmt1"] = locAmt.ToString("#,##0.00");
                        }
                    }
                    else
                    {
                        row["CrAmt"] = docAmt;
                        row["LocCrAmt"] = locAmt;
                        if (docAmt > 0)
                        {
                            row["CrAmt1"] = docAmt.ToString("#,##0.00");
                            row["LocCrAmt1"] = locAmt.ToString("#,##0.00");
                        }
                    }
                    mast.Rows.Add(row);
                }
                else if (i == tab.Rows.Count - 1)
                    index++;

            }
        }
        reader.Close();
        reader.Dispose();
        DataSetHelper help = new DataSetHelper();
        DataTable tab1 = help.SelectGroupByInto("table1", mast, "AcCode,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt,sum(LocDbAmt) LocDbAmt,sum(LocCrAmt) LocCrAmt", "", "AcCode");

        DataTable rptDet = new DataTable("Detail");
        rptDet.Columns.Add("AcCode");
        rptDet.Columns.Add("AcDesc");
        rptDet.Columns.Add("DbAmt");
        rptDet.Columns.Add("CrAmt");
        rptDet.Columns.Add("LocDbAmt");
        rptDet.Columns.Add("LocCrAmt");

        for (int i = 0; i < tab1.Rows.Count; i++)
        {
            DataRow rptRow = rptDet.NewRow();
            rptRow["AcCode"] = tab1.Rows[i]["AcCode"];
            rptRow["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab1.Rows[i]["AcCode"] + "'");
            rptRow["DbAmt"] = tab1.Rows[i]["DbAmt"];
            rptRow["CrAmt"] = tab1.Rows[i]["CrAmt"];
            rptRow["LocDbAmt"] = tab1.Rows[i]["LocDbAmt"];
            rptRow["LocCrAmt"] = tab1.Rows[i]["LocCrAmt"];
            rptDet.Rows.Add(rptRow);
        }
        set.Tables.Add(mast);
        set.Tables.Add(rptDet);

        return set;
    }


    public static DataSet DsAccruedIncome(DateTime d1, DateTime d2, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("Eta");
        mast.Columns.Add("JobType");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("MastAcCode");
        mast.Columns.Add("MastAcDesc");
        mast.Columns.Add("PartyName");

        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("LineAmt");

        string sql = "";
        sql = string.Format(@"SELECT SequenceId, AcCode, AcSource, DocNo, DocType, DocDate, MastClass, MastType, Eta, PartyTo, DocAmt, LocAmt
FROM XAArInvoice where Eta>='{0}' and Eta<'{1}' and DocDate>='{1}' ", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));

        sql += " order by DocType,DocNo";
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string title = "ACCRUED INCOME REPORT";
            string sql_det = string.Format("SELECT  AcCode, Qty, Price, ExRate, Gst, GstAmt, DocAmt, LocAmt FROM XAArInvoiceDet WHERE (DocId = '{0}')", sequenceId);
            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                string acCode = tab.Rows[i]["AcCode"].ToString();
                DataRow row1 = mast.NewRow();
                row1["Title"] = title;
                row1["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                row1["UserId"] = userId;
                row1["DocNo"] = reader["DocNo"].ToString();
                row1["DocType"] = reader["DocType"].ToString();
                row1["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                row1["Eta"] = SafeValue.SafeDateStr(reader["Eta"]);
                string partyId1 = reader["PartyTo"].ToString();
                string partyName1 = EzshipHelper.GetPartyName(partyId1);
                row1["PartyId"] = partyId1;
                row1["PartyName"] = partyName1;
                row1["MastAcCode"] = reader["AcCode"].ToString();
                row1["MastAcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + reader["AcCode"] + "'");

                row1["AcCode"] = acCode;
                row1["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + acCode + "'");
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["Qty"], 0) * SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0) * SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 1), 2);
                row1["LineAmt"] = amt.ToString("#,##.00");
                mast.Rows.Add(row1);
            }
        }
        reader.Close();
        reader.Dispose();
        //DataSetHelper help = new DataSetHelper();
        //DataTable tab1 = help.SelectGroupByInto("table1", mast, "AcCode,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt,sum(LocDbAmt) LocDbAmt,sum(LocCrAmt) LocCrAmt", "", "AcCode");

        //DataTable rptDet = new DataTable("Detail");
        //rptDet.Columns.Add("AcCode");
        //rptDet.Columns.Add("AcDesc");
        //rptDet.Columns.Add("DbAmt");
        //rptDet.Columns.Add("CrAmt");
        //rptDet.Columns.Add("LocDbAmt");
        //rptDet.Columns.Add("LocCrAmt");

        //for (int i = 0; i < tab1.Rows.Count; i++)
        //{
        //    DataRow rptRow = rptDet.NewRow();
        //    rptRow["AcCode"] = tab1.Rows[i]["AcCode"];
        //    rptRow["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab1.Rows[i]["AcCode"] + "'");
        //    rptRow["DbAmt"] = tab1.Rows[i]["DbAmt"];
        //    rptRow["CrAmt"] = tab1.Rows[i]["CrAmt"];
        //    rptRow["LocDbAmt"] = tab1.Rows[i]["LocDbAmt"];
        //    rptRow["LocCrAmt"] = tab1.Rows[i]["LocCrAmt"];
        //    rptDet.Rows.Add(rptRow);
        //}
        set.Tables.Add(mast);
        // set.Tables.Add(rptDet);

        return set;
    }
    public static DataSet DsDeferredIncome(DateTime d1, DateTime d2, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Title");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");

        mast.Columns.Add("Index");
        mast.Columns.Add("DocNo");
        mast.Columns.Add("DocType");
        mast.Columns.Add("DocDate");
        mast.Columns.Add("Eta");
        mast.Columns.Add("JobType");
        mast.Columns.Add("PartyId");
        mast.Columns.Add("MastAcCode");
        mast.Columns.Add("MastAcDesc");
        mast.Columns.Add("PartyName");

        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("LineAmt");

        string sql = "";
        sql = string.Format(@"SELECT SequenceId, AcCode, AcSource, DocNo, DocType, DocDate, MastClass, MastType, Eta, PartyTo, DocAmt, LocAmt
FROM XAArInvoice where Eta>='{1}' AND DocDate>='{0}' and DocDate<'{1}' ", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"));

        sql += " order by DocType,DocNo";
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        int index = 1;
        while (reader.Read())
        {
            string sequenceId = reader["SequenceId"].ToString();
            string title = "DEFERRED INCOME REPORT";
            string sql_det = string.Format("SELECT  AcCode, Qty, Price, ExRate, Gst, GstAmt, DocAmt, LocAmt FROM XAArInvoiceDet WHERE (DocId = '{0}')", sequenceId);
            DataTable tab = Helper.Sql.List(sql_det);
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                DataRow row1 = mast.NewRow();
                row1["Title"] = title;
                row1["DatePeriod"] = string.Format("Doc Date :{0} To :{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
                row1["UserId"] = userId;
                row1["Index"] = index;
                row1["DocNo"] = reader["DocNo"].ToString();
                row1["DocType"] = reader["DocType"].ToString();
                row1["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                row1["Eta"] = SafeValue.SafeDateStr(reader["Eta"]);
                string partyId1 = reader["PartyTo"].ToString();
                string partyName1 = EzshipHelper.GetPartyName(partyId1);
                row1["PartyId"] = partyId1;
                row1["PartyName"] = partyName1;
                row1["MastAcCode"] = reader["AcCode"].ToString();
                row1["MastAcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + reader["AcCode"] + "'");

                row1["AcCode"] = tab.Rows[i]["AcCode"].ToString();
                row1["AcDesc"] = GetObj("Select AcDesc from XXChartAcc where Code='" + tab.Rows[i]["AcCode"] + "'");
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(tab.Rows[i]["Qty"], 0) * SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0) * SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 1), 2);
                row1["LineAmt"] = amt.ToString("#,##.00");
                mast.Rows.Add(row1);
                index++;
            }
        }
        reader.Close();
        reader.Dispose();
        set.Tables.Add(mast);
        return set;
    }
    #endregion
}
