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
    public static DataSet DsCashBook(DateTime d1, DateTime d2, string acCode, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Currency");
        mast.Columns.Add("DbAmt");
        mast.Columns.Add("CrAmt");
        mast.Columns.Add("BalAmt");
        mast.Columns.Add("OpenAmt");
        mast.Columns.Add("CloseAmt");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("DocDate");
        det.Columns.Add("DocType");
        det.Columns.Add("DocNo");
        det.Columns.Add("ChqNo");
        det.Columns.Add("Rmk");
        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");
        det.Columns.Add("BalAmt");




        return set;
    }
    public static DataSet DsBankReceipt(DateTime d1, DateTime d2, string acCode, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("DocDate");
        det.Columns.Add("DocType");
        det.Columns.Add("DocNo");
        det.Columns.Add("ChqNo");
        det.Columns.Add("ChqDate");
        det.Columns.Add("DocAmt");
        det.Columns.Add("LocAmt");
        det.Columns.Add("Status");
        det.Columns.Add("ClearDate");

        string sql = string.Format(@"SELECT DocType, DocNo, DocDate, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, BankRec, BankDate
FROM XAArReceipt where DocType='RE' and DocDate>='{0}' and DocDate<'{1}' and AcCode='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            string docDate = SafeValue.SafeDateStr(reader["DocDate"]);
            string chqNo = reader["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            string status = SafeValue.SafeString(reader["BankRec"], "N");
            if (chqNo != "CASH1")//chqNo.Length > 0 && 
            {
                string clearDate = SafeValue.SafeDateStr(reader["BankDate"]);
                if (status == "Y")
                    status = "CLEARED";
                else
                {
                    status = "UNCLEARED";
                    clearDate = "";
                }
                DataRow row = det.NewRow();
                row["AcCode"] = acCode;
                row["DocDate"] = docDate;
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["ChqNo"] = chqNo;
                row["ChqDate"] = chqDate;
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                row["LocAmt"] = docAmt.ToString("#,##0.00");
                row["Status"] = status;
                row["ClearDate"] = clearDate;
                det.Rows.Add(row);
            }
        }
        reader.Close();
        reader.Dispose();

        sql = string.Format(@"SELECT DocType, DocNo, DocDate, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, BankRec, BankDate
FROM XAApPayment where DocType='SR' and DocDate>='{0}' and DocDate<'{1}' and AcCode='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);
        //ps
        SqlConnection con1 = new SqlConnection(conStr);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand(sql, con1);
        SqlDataReader reader1 = cmd1.ExecuteReader();
        // Call Read before accessing data.
        while (reader1.Read())
        {
            string docNo = reader1["DocNo"].ToString();
            string docType = reader1["DocType"].ToString();
            string docDate = SafeValue.SafeDateStr(reader1["DocDate"]);
            string chqNo = reader1["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader1["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader1["DocAmt"], 0);
            decimal locAmt = SafeValue.SafeDecimal(reader1["LocAmt"], 0);
            string status = SafeValue.SafeString(reader1["BankRec"], "N");
            if (chqNo != "CASH")//chqNo.Length > 0 && 
            {
                string clearDate = SafeValue.SafeDateStr(reader1["BankDate"]);
                if (status == "Y")
                    status = "CLEARED";
                else
                {
                    status = "UNCLEARED";
                    clearDate = "";
                }
                DataRow row = det.NewRow();
                row["AcCode"] = acCode;
                row["DocDate"] = docDate;
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["ChqNo"] = chqNo;
                row["ChqDate"] = chqDate;
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                row["LocAmt"] = docAmt.ToString("#,##0.00");
                row["Status"] = status;
                row["ClearDate"] = clearDate;
                det.Rows.Add(row);
            }
        }
        DataRow rowMast = mast.NewRow();
        rowMast["AcCode"] = acCode;
        rowMast["AcDesc"] = GetObj("SELECT AcDesc FROM XXChartAcc WHERE Code ='" + acCode + "'");
        rowMast["DatePeriod"] = string.Format("From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        rowMast["UserId"] = userId;
        mast.Rows.Add(rowMast);

        set.Tables.Add(mast);
        set.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["AcCode"], det.Columns["AcCode"]);
        set.Relations.Add(r);

        return set;
    }
    public static DataSet DsBankPayment(DateTime d1, DateTime d2, string acCode, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("DocDate");
        det.Columns.Add("DocType");
        det.Columns.Add("DocNo");
        det.Columns.Add("ChqNo");
        det.Columns.Add("ChqDate");
        det.Columns.Add("DocAmt");
        det.Columns.Add("LocAmt");
        det.Columns.Add("Status");
        det.Columns.Add("ClearDate");
        //pc
        string sql = string.Format(@"SELECT DocType, DocNo, DocDate, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, BankRec, BankDate
FROM XAArReceipt where DocType='PC' and DocDate>='{0}' and DocDate<'{1}' and AcCode='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            string docDate = SafeValue.SafeDateStr(reader["DocDate"]);
            string chqNo = reader["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            string status = SafeValue.SafeString(reader["BankRec"], "N");
            if (chqNo != "CASH")//chqNo.Length > 0 && 
            {
                string clearDate = SafeValue.SafeDateStr(reader["BankDate"]);
                if (status == "Y")
                    status = "CLEARED";
                else
                {
                    status = "UNCLEARED";
                    clearDate = "";
                }
                DataRow row = det.NewRow();
                row["AcCode"] = acCode;
                row["DocDate"] = docDate;
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["ChqNo"] = chqNo;
                row["ChqDate"] = chqDate;
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                row["LocAmt"] = docAmt.ToString("#,##0.00");
                row["Status"] = status;
                row["ClearDate"] = clearDate;
                det.Rows.Add(row);
            }
        }
        reader.Close();
        reader.Dispose();

        sql = string.Format(@"SELECT DocType, DocNo, DocDate, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, BankRec, BankDate
FROM XAApPayment where DocType='PS' and DocDate>='{0}' and DocDate<'{1}' and AcCode='{2}'", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), acCode);
        //ps
        SqlConnection con1 = new SqlConnection(conStr);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand(sql, con1);
        SqlDataReader reader1 = cmd1.ExecuteReader();
        // Call Read before accessing data.
        while (reader1.Read())
        {
            string docNo = reader1["DocNo"].ToString();
            string docType = reader1["DocType"].ToString();
            string docDate = SafeValue.SafeDateStr(reader1["DocDate"]);
            string chqNo = reader1["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader1["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader1["DocAmt"], 0);
            decimal locAmt = SafeValue.SafeDecimal(reader1["LocAmt"], 0);
            string status = SafeValue.SafeString(reader1["BankRec"], "N");
            if (chqNo != "CASH")//chqNo.Length > 0 && 
            {
                string clearDate = SafeValue.SafeDateStr(reader1["BankDate"]);
                if (status == "Y")
                    status = "CLEARED";
                else
                {
                    status = "UNCLEARED";
                    clearDate = "";
                }
                DataRow row = det.NewRow();
                row["AcCode"] = acCode;
                row["DocDate"] = docDate;
                row["DocType"] = docType;
                row["DocNo"] = docNo;
                row["ChqNo"] = chqNo;
                row["ChqDate"] = chqDate;
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                row["LocAmt"] = docAmt.ToString("#,##0.00");
                row["Status"] = status;
                row["ClearDate"] = clearDate;
                det.Rows.Add(row);
            }
        }
        DataRow rowMast = mast.NewRow();
        rowMast["AcCode"] = acCode;
        rowMast["AcDesc"] = GetObj("SELECT AcDesc FROM XXChartAcc WHERE Code ='" + acCode + "'");
        rowMast["DatePeriod"] = string.Format("From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        rowMast["UserId"] = userId;
        mast.Rows.Add(rowMast);

        set.Tables.Add(mast);
        set.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["AcCode"], det.Columns["AcCode"]);
        set.Relations.Add(r);

        return set;
    }

    public static DataSet DsBankRecon(DateTime d1, string balAmt, string acCode, string userId)
    {
        DataSet set = new DataSet();

        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("BalAmt");
        mast.Columns.Add("TotAmt");
        DataTable det = new DataTable("Detail");
        det.Columns.Add("AcCode");
        det.Columns.Add("ChqNo");
        det.Columns.Add("ChqDate");
        det.Columns.Add("ChqType");
        det.Columns.Add("ChqInfo");
        det.Columns.Add("DocAmt");

        decimal totAmt = SafeValue.SafeDecimal(balAmt, 0);
        //pc
        string sql = string.Format(@"SELECT DocType, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate
FROM XAArReceipt where DocDate<'{0}' and AcCode='{1}' and (BankRec='' OR BankRec='N')", d1.AddDays(1).ToString("yyyy-MM-dd"), acCode);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string chqNo = reader["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader["DocAmt"], 0);
            string chqType = reader["DocType"].ToString();
            //decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);
            //if (chqNo != "CASH" && docAmt > 0)//chqNo.Length > 0 && 
            //{
            DataRow row = det.NewRow();
            row["AcCode"] = acCode;
            row["ChqNo"] = chqNo;
            row["ChqDate"] = chqDate;
            if (chqType == "RE")
            {
                row["ChqType"] = "RE";
                row["ChqInfo"] = "Add : Uncredited Deposit";
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                totAmt += docAmt;
            }
            else
            {
                row["ChqType"] = "PC"; ;
                row["ChqInfo"] = "Less: UnPresented Cheque";
                row["DocAmt"] = (-docAmt).ToString("#,##0.00");
                totAmt -= docAmt;
            }
            det.Rows.Add(row);
            //}
        }
        reader.Close();
        reader.Dispose();

        sql = string.Format(@"SELECT DocType, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, BankRec, BankDate
FROM XAApPayment where DocType='PS' and DocDate<='{0}' and AcCode='{1}' and (BankRec='' OR BankRec='N')", d1.AddDays(1).ToString("yyyy-MM-dd"), acCode);
        //ps
		//throw new Exception(sql);
        SqlConnection con1 = new SqlConnection(conStr);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand(sql, con1);
        SqlDataReader reader1 = cmd1.ExecuteReader();
        // Call Read before accessing data.
        while (reader1.Read())
        {
            string chqNo = reader1["ChqNo"].ToString();
            string chqDate = SafeValue.SafeDateStr(reader1["ChqDate"]);
            decimal docAmt = SafeValue.SafeDecimal(reader1["DocAmt"], 0);
            //decimal locAmt = SafeValue.SafeDecimal(reader1["LocAmt"], 0);
            string chqType = reader1["DocType"].ToString();
            //if (chqNo != "CASH" && docAmt > 0)//chqNo.Length > 0 && 
            //{
            DataRow row = det.NewRow();
            row["AcCode"] = acCode;
            row["ChqNo"] = chqNo;
            row["ChqDate"] = chqDate;
            // row["LocAmt"] = docAmt.ToString("#,##0.00");

            if (chqType == "SR")
            {
                row["ChqType"] = "RE";
                row["ChqInfo"] = "Add : Uncredited Deposit";
                row["DocAmt"] = docAmt.ToString("#,##0.00");
                totAmt += docAmt;
            }
            else
            {
                row["ChqType"] = "PC"; ;
                row["ChqInfo"] = "Less: UnPresented Cheque";
                row["DocAmt"] = (-docAmt).ToString("#,##0.00");
                totAmt -= docAmt;
            };
            det.Rows.Add(row);
            //}
        }
        DataRow rowMast = mast.NewRow();
        rowMast["AcCode"] = acCode;
        rowMast["AcDesc"] = GetObj("SELECT AcDesc FROM XXChartAcc WHERE Code ='" + acCode + "'");
        rowMast["DatePeriod"] = string.Format("Up to {0}", d1.ToString("dd/MM/yyyy"));
        rowMast["UserId"] = userId;
        rowMast["BalAmt"] = SafeValue.SafeDecimal(balAmt, 0).ToString("0.00");
        rowMast["TotAmt"] = totAmt.ToString("0.00");
        mast.Rows.Add(rowMast);

        set.Tables.Add(mast);
        set.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["AcCode"], det.Columns["AcCode"]);
        set.Relations.Add(r);

        return set;
    }

  
    #endregion
}
