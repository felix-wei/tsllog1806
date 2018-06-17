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

    #region GL   aging report
    public static DataTable DsGlAgingSummary(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
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
            string partyName = GetObj("select Name from XXParty where PartyId='" + row["PartyCode"] + "'");
            string partyGroup = GetObj("select GroupId from XXParty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            rowMast["PartyGroup"] = partyGroup;
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
    public static DataTable DsGlAgingSummary_ForeignLocal(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail_ForeignLocal(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail_ForeignLocal(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
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
            string partyName = GetObj("select Name from XXParty where PartyId='" + row["PartyCode"] + "'");
            string partyGroup = GetObj("select GroupId from XXParty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            rowMast["PartyGroup"] = partyGroup;
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
    public static DataTable DsGlAgingSummary_ForeignForeign(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail_ForeignForeign(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail_ForeignForeign(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
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
            string partyName = GetObj("select Name from XXParty where PartyId='" + row["PartyCode"] + "'");
            string partyGroup = GetObj("select GroupId from XXParty where PartyId='" + row["PartyCode"] + "'");
            rowMast["PartyName"] = partyName;
            rowMast["PartyGroup"] = partyGroup;
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


    public static DataTable DsGlAgingDetail(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
        return tab;
    }
    public static DataTable DsGlAgingDetail_ForeignLocal(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail_ForeignLocal(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail_ForeignLocal(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
        return tab;
    }
    public static DataTable DsGlAgingDetail_ForeignForeign(DateTime d1, DateTime d2, string curyType, string userId)
    {
        DataTable tab = DsArAgingDetail_ForeignForeign(d1, d2, curyType, "", userId);
        DataTable tab_ap = DsApAgingDetail_ForeignForeign(d1, d2, curyType, userId);
        for (int i = 0; i < tab_ap.Rows.Count; i++)
        {
            DataRow row1 = tab.NewRow();
            DataRow row2 = tab_ap.Rows[i];
            row1["DatePeriod"] = row2["DatePeriod"];
            row1["PartyCode"] = row2["PartyCode"];
            row1["PartyName"] = row2["PartyName"];
            row1["PartyGroup"] = row2["PartyGroup"];
            row1["Term"] = row2["Term"];
            row1["Tel"] = row2["Tel"];
            row1["DocNo"] = row2["DocNo"];
            row1["DocType"] = row2["DocType"];
            row1["DocDate"] = row2["DocDate"];
            row1["Rmk"] = row2["Rmk"];
            row1["Current"] = -SafeValue.SafeDecimal(row2["Current"], 0);
            row1["30Days"] = -SafeValue.SafeDecimal(row2["30Days"], 0);
            row1["60Days"] = -SafeValue.SafeDecimal(row2["60Days"], 0);
            row1["90Days"] = -SafeValue.SafeDecimal(row2["90Days"], 0);
            row1["120Days"] = -SafeValue.SafeDecimal(row2["120Days"], 0);
            row1["OverDue"] = -SafeValue.SafeDecimal(row2["OverDue"], 0);
            row1["TotAmt"] = -SafeValue.SafeDecimal(row2["TotAmt"], 0);
            row1["UserId"] = row2["UserId"];
            row1["Currency"] = row2["Currency"];
            tab.Rows.Add(row1);
        }
        return tab;
    }
    #endregion
    #region GL
    public static DataTable DsGlTrialBalance(string year, string period, string userId)
    {
        DataTable tab = new DataTable();

        tab.Columns.Add("AcCode");
        tab.Columns.Add("AcName");
        tab.Columns.Add("DbAmt_Open");
        tab.Columns.Add("CrAmt_Open");
        tab.Columns.Add("DbAmt_Current");
        tab.Columns.Add("CrAmt_Current");
        tab.Columns.Add("DbAmt_Close");
        tab.Columns.Add("CrAmt_Close");
        tab.Columns.Add("DatePeriod");
        tab.Columns.Add("UserId");

        string where = string.Format("AcYear='{0}' and AcPeriod='{1}'", year, period);
        string sql = "SELECT  AcCode,AcSource, SUM(CurrencyCrAmt) AS CrAmt, SUM(CurrencyDbAmt) AS DbAmt FROM  XAGlEntryDet ";
        sql += " where " + where;
        sql += " GROUP BY AcCode,AcSource";

        DataTable current = Helper.Sql.List(sql);


        string sqlAcc = "SELECT Code, AcDesc,AcDorc FROM XXChartAcc ORDER BY Code ";
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sqlAcc, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {

            string acCode = reader["Code"].ToString();
            string acCodeName = reader["AcDesc"].ToString();
            string acSource = reader["AcDorc"].ToString();
            if (acCode == "2034")
            {
            }
            decimal open = SafeValue.SafeDecimal(GetObj(string.Format("SELECT OpenBal FROM XXAccStatus where Year='{0}' and AcPeriod='{1}' and AcCode='{2}'", year, period, acCode)), 0);
            decimal dbOpen = 0;
            decimal crOpen = 0;
            decimal dbCurrent = 0;
            decimal crCurrent = 0;
            if (acSource == "DB")
            {
                dbOpen = open;
            }
            else if (acSource == "CR")
            {
                crOpen = open;
            }

            DataRow[] row_Current = current.Select(string.Format("AcCode='{0}'", acCode));
            for (int i = 0; i < row_Current.Length; i++)
            {
                string curAcSource = row_Current[i]["AcSource"].ToString();
                if (curAcSource == "DB")
                {
                    dbCurrent += SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0) - SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0);
                }
                else //if (acSource == "CR")
                {
                    crCurrent += SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0) - SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0);
                }
            }


            decimal dbClose = dbOpen + dbCurrent;
            decimal crClose = crOpen + crCurrent;

            //if (dbClose == 0 && crClose == 0)
            //    continue;
            if (acSource == "DB")
            {
                dbClose = dbClose - crClose;
                crClose = 0;
            }
            else if (acSource == "CR")
            {
                crOpen = open;
                crClose = crClose - dbClose;
                dbClose = 0;
            }
            DataRow row = tab.NewRow();
            row["AcCode"] = acCode;
            row["AcName"] = acCodeName;
            row["DbAmt_Open"] = dbOpen.ToString("#,##0.00");
            row["CrAmt_Open"] = crOpen.ToString("#,##0.00");
            row["DbAmt_Current"] = dbCurrent.ToString("#,##0.00");
            row["CrAmt_Current"] = crCurrent.ToString("#,##0.00");
            row["DbAmt_Close"] = dbClose.ToString("#,##0.00");
            row["CrAmt_Close"] = crClose.ToString("#,##0.00");
            string sql_accperiod = string.Format("SELECT StartDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
            string from = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
            sql_accperiod = string.Format("SELECT EndDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
            string end = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
            row["DatePeriod"] = string.Format("Year :{0}  Period :{1}   ({2}-{3})", year, period, from, end);
            row["UserId"] = userId;

            tab.Rows.Add(row);
        }

        return tab;
    }

    public static DataSet DsGlBalanceSheet(string year, string period, string userId)
    {
        DataTable mast = new DataTable("Mast");
        mast.Columns.Add("Year");
        mast.Columns.Add("NetAmt");
        mast.Columns.Add("ShareAmt");
        mast.Columns.Add("PlAmt");
        mast.Columns.Add("EarnAmt");
        mast.Columns.Add("BsAmt");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        DataTable det = new DataTable("Detail");

        det.Columns.Add("Year");
        det.Columns.Add("Index");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcSubType");
        det.Columns.Add("AcName");
        det.Columns.Add("Amt");


        decimal shareAmt = 0;
        decimal plAmt = 0;
        decimal earnAmt = 0;
        decimal bsAmt = 0;
        //string where = string.Format("(AcYear='{0}' and AcPeriod<='{1}') or AcYear<'{0}' ", year, period);
        string where = string.Format("(AcYear='{0}' and AcPeriod='{1}') ", year, period);
        string sql = "SELECT AcCode, sum(CurrencyCrAmt) CrAmt, sum(CurrencyDbAmt) DbAmt FROM XAGlEntryDet";
        sql += " where " + where;
        sql += " group BY AcCode";
        DataTable current = Helper.Sql.List(sql);

        string sql_chart = "SELECT Code,AcDesc, AcType, AcDorc, AcBank, AcCurrency, AcSubType, GNo FROM XXChartAcc ";
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql_chart, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {

            string acCode = reader["Code"].ToString();
            if (acCode == "2033")
            {
            }
            string acCodeName = reader["AcDesc"].ToString();
            string acSource = reader["AcDorc"].ToString();

            string acCodeType = reader["AcType"].ToString();
            string acSubType = reader["AcSubType"].ToString();

            decimal dbAmt = 0;
            decimal crAmt = 0;
            decimal openAmt = SafeValue.SafeDecimal(GetObj(string.Format("SELECT OpenBal, CurrBal, CloseBal, AcDesc FROM XXAccStatus WHERE (Year = '{0}') AND (AcPeriod = '{1}') AND (AcCode = '{2}') ", year, period, acCode)), 0);
            if (acSource == "DB")
                dbAmt = openAmt;
            else
                crAmt = openAmt;
            DataRow[] row_Current = current.Select(string.Format("AcCode='{0}'", acCode));
            for (int i = 0; i < row_Current.Length; i++)
            {

                if (acSource == "DB")
                {
                    dbAmt += SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0) - SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0);
                }
                else if (acSource == "CR")
                {
                    crAmt += -SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0) + SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0);
                }
            }
            decimal amt = dbAmt - crAmt;
            if (acCodeType == "B")
            {
                DataRow row = det.NewRow();
                row["Year"] = year;

                row["AcCode"] = acCode;
                row["AcName"] = acCodeName;
                row["Amt"] = amt.ToString("#,##0.00");

                if (acSubType == "A")
                {
                    row["Index"] = 0;
                    row["AcSubType"] = "ASSETS";
                    det.Rows.Add(row);
                    bsAmt += amt;
                }
                else if (acSubType == "CA")
                {
                    row["Index"] = 1;
                    row["AcSubType"] = "CURRENT ASSETS";
                    det.Rows.Add(row);
                    bsAmt += amt;
                }
                else if (acSubType == "L")
                {
                    row["Index"] = 2;
                    row["AcSubType"] = "LIABILITIES";
                    det.Rows.Add(row);
                    bsAmt += amt;
                }
                else if (acSubType == "CL")
                {
                    row["Index"] = 3;
                    row["AcSubType"] = "CURRENT LIABILITIES";
                    det.Rows.Add(row);
                    bsAmt += amt;
                }
                else if (acSubType == "SC" )
                {
                    row["Index"] = 4;
                    row["AcSubType"] = "SHARE CAPITAL";
                    shareAmt += crAmt - dbAmt;
                }
            }
            else if (acCodeType == "P")
            {
                plAmt += crAmt - dbAmt;
            }
            else if (acCodeType == "R")
            {
                earnAmt += crAmt - dbAmt;
            }
        }
        //string shareAcCode = "3000";// System.Configuration.ConfigurationManager.AppSettings["ShareAcCode"];
        //string earnAcCode = System.Configuration.ConfigurationManager.AppSettings["EarnAcCode"];

        //shareAmt = SafeValue.SafeDecimal(GetObj(string.Format("SELECT OpenBal, CurrBal, CloseBal, AcDesc FROM XXAccStatus WHERE (Year = '{0}') AND (AcPeriod = '{1}') AND (AcCode = '{2}') ", year, period,shareAcCode)), 0);
        //earnAmt = SafeValue.SafeDecimal(GetObj(string.Format("SELECT OpenBal, CurrBal, CloseBal, AcDesc FROM XXAccStatus WHERE (Year = '{0}') AND (AcPeriod = '{1}') AND (AcCode = '{2}') ", year, period,earnAcCode)), 0);

        DataRow rowMast = mast.NewRow();
        rowMast["Year"] = year;
        rowMast["ShareAmt"] = shareAmt.ToString("#,##0.00");
        rowMast["PlAmt"] = plAmt.ToString("#,##0.00");
        rowMast["EarnAmt"] = earnAmt.ToString("#,##0.00");
        rowMast["BsAmt"] = bsAmt.ToString("#,##0.00");
        rowMast["NetAmt"] = (shareAmt + plAmt + earnAmt).ToString("#,##0.00");
        string sql_accperiod = string.Format("SELECT StartDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
        string from = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        sql_accperiod = string.Format("SELECT EndDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
        string end = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        rowMast["DatePeriod"] = string.Format("Year :{0}  Period :{1}   ({2}-{3})", year, period, from, end);
        //rowMast["DatePeriod"] = string.Format("Year :{0}  Period :{1}", year, period);
        rowMast["UserId"] = userId;
        mast.Rows.Add(rowMast);

        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Year"], det.Columns["Year"]);
        ds.Relations.Add(r);
        return ds;
    }
    public static DataSet DsGlPlStatement_1(string year, string period, string userId)
    {
        DataSet set = new DataSet();
        DataTable mast = new DataTable();
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Amt_Open");
        mast.Columns.Add("Amt_Current");
        mast.Columns.Add("Amt_Close");

        DataTable tab_Pl = new DataTable();
        tab_Pl.Columns.Add("AcCode");
        tab_Pl.Columns.Add("AcName");
        tab_Pl.Columns.Add("SortIndex");
        tab_Pl.Columns.Add("AcSubType");
        tab_Pl.Columns.Add("Amt_Open");
        tab_Pl.Columns.Add("Amt_Current");
        tab_Pl.Columns.Add("Amt_Close");
        tab_Pl.Columns.Add("Amt_Open1");
        tab_Pl.Columns.Add("Amt_Current1");
        tab_Pl.Columns.Add("Amt_Close1");
        DataTable tab_Revenue = new DataTable();
        tab_Revenue.Columns.Add("Amt_Open");
        tab_Revenue.Columns.Add("Amt_Current");
        tab_Revenue.Columns.Add("Amt_Close");
        DataTable tab_Other = new DataTable();
        tab_Other.Columns.Add("AcCode");
        tab_Other.Columns.Add("AcName");
        tab_Other.Columns.Add("SortIndex");
        tab_Other.Columns.Add("AcSubType");
        tab_Other.Columns.Add("Amt_Open");
        tab_Other.Columns.Add("Amt_Current");
        tab_Other.Columns.Add("Amt_Close");
        tab_Other.Columns.Add("Amt_Open1");
        tab_Other.Columns.Add("Amt_Current1");
        tab_Other.Columns.Add("Amt_Close1");
        DataTable tab_Expense = new DataTable();
        tab_Expense.Columns.Add("AcCode");
        tab_Expense.Columns.Add("AcName");
        tab_Expense.Columns.Add("SortIndex");
        tab_Expense.Columns.Add("AcSubType");
        tab_Expense.Columns.Add("Amt_Open");
        tab_Expense.Columns.Add("Amt_Current");
        tab_Expense.Columns.Add("Amt_Close");
        tab_Expense.Columns.Add("Amt_Open1");
        tab_Expense.Columns.Add("Amt_Current1");
        tab_Expense.Columns.Add("Amt_Close1");

        decimal amt_Open = 0;
        decimal amt_Current = 0;
        decimal amt_Close = 0;
        decimal amt_Open_Revenue = 0;
        decimal amt_Current_Revenue = 0;
        decimal amt_Close_Revenue = 0;

        string where = string.Format("AcYear='{0}' and AcPeriod='{1}'", year, period);
        string sql = "SELECT AcCode, sum(CurrencyCrAmt) CrAmt, sum(CurrencyDbAmt) DbAmt FROM XAGlEntryDet";
        sql += " where " + where;
        sql += " group BY AcCode";
        DataTable current = Helper.Sql.List(sql);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        string sql_chart = "SELECT Code,AcDesc, AcType, AcDorc, AcBank, AcCurrency, AcSubType, GNo FROM XXChartAcc ";
        SqlCommand cmd = new SqlCommand(sql_chart, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {

            string acCode = reader["Code"].ToString();
            string acCodeName = reader["AcDesc"].ToString();
            string acSource = reader["AcDorc"].ToString();
            string acCodeType = reader["AcType"].ToString(); ;
            string acSubType = reader["AcSubType"].ToString();
            if (acCodeType == "P")
            {
                string sql_open = string.Format("SELECT OpenBal FROM XXAccStatus where Year='{0}' and AcPeriod='{1}' AND AcCode='{2}'", year, period, acCode); ;
                decimal amtOpen = SafeValue.SafeDecimal(GetObj(sql_open), 0);
                decimal amtCurrent = 0;
                decimal amtClose = 0;

                decimal dbAmt = 0;
                decimal crAmt = 0;
                DataRow[] row_Current = current.Select(string.Format("AcCode='{0}'", acCode));
                for (int i = 0; i < row_Current.Length; i++)
                {

                    if (acSource == "DB")
                    {
                        dbAmt += SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0) - SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0);
                    }
                    else if (acSource == "CR")
                    {
                        crAmt = -SafeValue.SafeDecimal(row_Current[i]["DbAmt"], 0) + SafeValue.SafeDecimal(row_Current[i]["CrAmt"], 0);
                    }
                }

                if (acSource == "DB")
                {
                    amtCurrent = amtCurrent + dbAmt;
                }
                else if (acSource == "CR")
                {
                    amtCurrent = amtCurrent + crAmt;
                }
                amtClose = amtOpen + amtCurrent;
                if (acSubType == "S")
                {
                    DataRow row = tab_Pl.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["Amt_Open"] = amtOpen.ToString("#,##0.00");
                    row["Amt_Current"] = amtCurrent.ToString("#,##0.00");
                    row["Amt_Close"] = amtClose.ToString("#,##0.00");
                    row["Amt_Open1"] = amtOpen;
                    row["Amt_Current1"] = amtCurrent;
                    row["Amt_Close1"] = amtClose;


                    row["SortIndex"] = 0;
                    row["AcSubType"] = "Sales";

                    amt_Open += amtOpen;
                    amt_Current += amtCurrent;
                    amt_Close += amtClose;

                    amt_Open_Revenue += amtOpen;
                    amt_Current_Revenue += amtCurrent;
                    amt_Close_Revenue += amtClose;
					if(amtOpen!=0 || amtCurrent!=0)
						tab_Pl.Rows.Add(row);
                }
                else if (acSubType == "C")
                {
                    DataRow row = tab_Pl.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["Amt_Open"] = amtOpen.ToString("#,##0.00");
                    row["Amt_Current"] = amtCurrent.ToString("#,##0.00");
                    row["Amt_Close"] = amtClose.ToString("#,##0.00");
                    row["Amt_Open1"] = -amtOpen;
                    row["Amt_Current1"] = -amtCurrent;
                    row["Amt_Close1"] = -amtClose;

                    row["SortIndex"] = 1;
                    row["AcSubType"] = "Cost of Sales";

                    amt_Open -= amtOpen;
                    amt_Current -= amtCurrent;
                    amt_Close -= amtClose;

                    amt_Open_Revenue -= amtOpen;
                    amt_Current_Revenue -= amtCurrent;
                    amt_Close_Revenue -= amtClose;

					if(amtOpen!=0 || amtCurrent!=0)
						tab_Pl.Rows.Add(row);
                }
                else if (acSubType == "E")
                {
                    DataRow row = tab_Expense.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["Amt_Open"] = amtOpen.ToString("#,##0.00");
                    row["Amt_Current"] = amtCurrent.ToString("#,##0.00");
                    row["Amt_Close"] = amtClose.ToString("#,##0.00");
                    row["Amt_Open1"] = -amtOpen;
                    row["Amt_Current1"] = -amtCurrent;
                    row["Amt_Close1"] = -amtClose;

                    row["SortIndex"] = 2;
                    row["AcSubType"] = "Operating Expense";

                    amt_Open -= amtOpen;
                    amt_Current -= amtCurrent;
                    amt_Close -= amtClose;
					if(amtOpen!=0 || amtCurrent!=0)
						tab_Expense.Rows.Add(row);
                }
                else
                {
                    DataRow row = tab_Other.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["Amt_Open"] = amtOpen.ToString("#,##0.00");
                    row["Amt_Current"] = amtCurrent.ToString("#,##0.00");
                    row["Amt_Close"] = amtClose.ToString("#,##0.00");
                    row["Amt_Open1"] = amtOpen;
                    row["Amt_Current1"] = amtCurrent;
                    row["Amt_Close1"] = amtClose;

                    row["SortIndex"] = 0;
                    row["AcSubType"] = "Other Income";

                    amt_Open += amtOpen;
                    amt_Current += amtCurrent;
                    amt_Close += amtClose;

                    amt_Open_Revenue += amtOpen;
                    amt_Current_Revenue += amtCurrent;
                    amt_Close_Revenue += amtClose;
					if(amtOpen!=0 || amtCurrent!=0)
						tab_Other.Rows.Add(row);
                }
            }
        }
        DataRow row_mast = mast.NewRow();
        string sql_accperiod = string.Format("SELECT StartDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
        string from = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        sql_accperiod = string.Format("SELECT EndDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
        string end = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        row_mast["DatePeriod"] = string.Format("Year :{0}  Period :{1}   ({2}-{3})", year, period, from, end);
        row_mast["UserId"] = userId;

        row_mast["Amt_Open"] = amt_Open.ToString("#,##0.00");
        row_mast["Amt_Current"] = amt_Current.ToString("#,##0.00");
        row_mast["Amt_Close"] = amt_Close.ToString("#,##0.00");
        mast.Rows.Add(row_mast);
        DataRow row_Revenue = tab_Revenue.NewRow();
        row_Revenue["Amt_Open"] = amt_Open_Revenue.ToString("#,##0.00");
        row_Revenue["Amt_Current"] = amt_Current_Revenue.ToString("#,##0.00");
        row_Revenue["Amt_Close"] = amt_Close_Revenue.ToString("#,##0.00");
        tab_Revenue.Rows.Add(row_Revenue);


        set.Tables.Add(mast);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Other);
        set.Tables.Add(tab_Revenue);
        set.Tables.Add(tab_Expense);
        return set;
    }

    public static DataSet DsGlPlStatement_year(string year, string userId)
    {
        DataSet set = new DataSet();
        #region init tabs
        DataTable mast = new DataTable();
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("Mth1");
        mast.Columns.Add("Mth2");
        mast.Columns.Add("Mth3");
        mast.Columns.Add("Mth4");
        mast.Columns.Add("Mth5");
        mast.Columns.Add("Mth6");
        mast.Columns.Add("Mth7");
        mast.Columns.Add("Mth8");
        mast.Columns.Add("Mth9");
        mast.Columns.Add("Mth10");
        mast.Columns.Add("Mth11");
        mast.Columns.Add("Mth12");
        mast.Columns.Add("Mth1_V");
        mast.Columns.Add("Mth2_V");
        mast.Columns.Add("Mth3_V");
        mast.Columns.Add("Mth4_V");
        mast.Columns.Add("Mth5_V");
        mast.Columns.Add("Mth6_V");
        mast.Columns.Add("Mth7_V");
        mast.Columns.Add("Mth8_V");
        mast.Columns.Add("Mth9_V");
        mast.Columns.Add("Mth10_V");
        mast.Columns.Add("Mth11_V");
        mast.Columns.Add("Mth12_V");
        mast.Columns.Add("Tot");

        DataTable tab_Pl = new DataTable();
        tab_Pl.Columns.Add("AcCode");
        tab_Pl.Columns.Add("AcName");
        tab_Pl.Columns.Add("SortIndex");
        tab_Pl.Columns.Add("AcSubType");
        tab_Pl.Columns.Add("Mth1_V");
        tab_Pl.Columns.Add("Mth2_V");
        tab_Pl.Columns.Add("Mth3_V");
        tab_Pl.Columns.Add("Mth4_V");
        tab_Pl.Columns.Add("Mth5_V");
        tab_Pl.Columns.Add("Mth6_V");
        tab_Pl.Columns.Add("Mth7_V");
        tab_Pl.Columns.Add("Mth8_V");
        tab_Pl.Columns.Add("Mth9_V");
        tab_Pl.Columns.Add("Mth10_V");
        tab_Pl.Columns.Add("Mth11_V");
        tab_Pl.Columns.Add("Mth12_V");
        tab_Pl.Columns.Add("Tot");
        tab_Pl.Columns.Add("Mth1_V_1");
        tab_Pl.Columns.Add("Mth2_V_1");
        tab_Pl.Columns.Add("Mth3_V_1");
        tab_Pl.Columns.Add("Mth4_V_1");
        tab_Pl.Columns.Add("Mth5_V_1");
        tab_Pl.Columns.Add("Mth6_V_1");
        tab_Pl.Columns.Add("Mth7_V_1");
        tab_Pl.Columns.Add("Mth8_V_1");
        tab_Pl.Columns.Add("Mth9_V_1");
        tab_Pl.Columns.Add("Mth10_V_1");
        tab_Pl.Columns.Add("Mth11_V_1");
        tab_Pl.Columns.Add("Mth12_V_1");
        tab_Pl.Columns.Add("Tot_1");
        DataTable tab_Revenue = new DataTable();
        tab_Revenue.Columns.Add("Mth1_V");
        tab_Revenue.Columns.Add("Mth2_V");
        tab_Revenue.Columns.Add("Mth3_V");
        tab_Revenue.Columns.Add("Mth4_V");
        tab_Revenue.Columns.Add("Mth5_V");
        tab_Revenue.Columns.Add("Mth6_V");
        tab_Revenue.Columns.Add("Mth7_V");
        tab_Revenue.Columns.Add("Mth8_V");
        tab_Revenue.Columns.Add("Mth9_V");
        tab_Revenue.Columns.Add("Mth10_V");
        tab_Revenue.Columns.Add("Mth11_V");
        tab_Revenue.Columns.Add("Mth12_V");
        tab_Revenue.Columns.Add("Tot");
        tab_Revenue.Columns.Add("Mth1_V_1");
        tab_Revenue.Columns.Add("Mth2_V_1");
        tab_Revenue.Columns.Add("Mth3_V_1");
        tab_Revenue.Columns.Add("Mth4_V_1");
        tab_Revenue.Columns.Add("Mth5_V_1");
        tab_Revenue.Columns.Add("Mth6_V_1");
        tab_Revenue.Columns.Add("Mth7_V_1");
        tab_Revenue.Columns.Add("Mth8_V_1");
        tab_Revenue.Columns.Add("Mth9_V_1");
        tab_Revenue.Columns.Add("Mth10_V_1");
        tab_Revenue.Columns.Add("Mth11_V_1");
        tab_Revenue.Columns.Add("Mth12_V_1");
        tab_Revenue.Columns.Add("Tot_1");

        DataTable tab_Other = new DataTable();
        tab_Other.Columns.Add("AcCode");
        tab_Other.Columns.Add("AcName");
        tab_Other.Columns.Add("SortIndex");
        tab_Other.Columns.Add("AcSubType");
        tab_Other.Columns.Add("Mth1_V");
        tab_Other.Columns.Add("Mth2_V");
        tab_Other.Columns.Add("Mth3_V");
        tab_Other.Columns.Add("Mth4_V");
        tab_Other.Columns.Add("Mth5_V");
        tab_Other.Columns.Add("Mth6_V");
        tab_Other.Columns.Add("Mth7_V");
        tab_Other.Columns.Add("Mth8_V");
        tab_Other.Columns.Add("Mth9_V");
        tab_Other.Columns.Add("Mth10_V");
        tab_Other.Columns.Add("Mth11_V");
        tab_Other.Columns.Add("Mth12_V");
        tab_Other.Columns.Add("Tot");
        tab_Other.Columns.Add("Mth1_V_1");
        tab_Other.Columns.Add("Mth2_V_1");
        tab_Other.Columns.Add("Mth3_V_1");
        tab_Other.Columns.Add("Mth4_V_1");
        tab_Other.Columns.Add("Mth5_V_1");
        tab_Other.Columns.Add("Mth6_V_1");
        tab_Other.Columns.Add("Mth7_V_1");
        tab_Other.Columns.Add("Mth8_V_1");
        tab_Other.Columns.Add("Mth9_V_1");
        tab_Other.Columns.Add("Mth10_V_1");
        tab_Other.Columns.Add("Mth11_V_1");
        tab_Other.Columns.Add("Mth12_V_1");
        tab_Other.Columns.Add("Tot_1");

        DataTable tab_Expense = new DataTable();
        tab_Expense.Columns.Add("AcCode");
        tab_Expense.Columns.Add("AcName");
        tab_Expense.Columns.Add("SortIndex");
        tab_Expense.Columns.Add("AcSubType");
        tab_Expense.Columns.Add("Mth1_V");
        tab_Expense.Columns.Add("Mth2_V");
        tab_Expense.Columns.Add("Mth3_V");
        tab_Expense.Columns.Add("Mth4_V");
        tab_Expense.Columns.Add("Mth5_V");
        tab_Expense.Columns.Add("Mth6_V");
        tab_Expense.Columns.Add("Mth7_V");
        tab_Expense.Columns.Add("Mth8_V");
        tab_Expense.Columns.Add("Mth9_V");
        tab_Expense.Columns.Add("Mth10_V");
        tab_Expense.Columns.Add("Mth11_V");
        tab_Expense.Columns.Add("Mth12_V");
        tab_Expense.Columns.Add("Tot");
        tab_Expense.Columns.Add("Mth1_V_1");
        tab_Expense.Columns.Add("Mth2_V_1");
        tab_Expense.Columns.Add("Mth3_V_1");
        tab_Expense.Columns.Add("Mth4_V_1");
        tab_Expense.Columns.Add("Mth5_V_1");
        tab_Expense.Columns.Add("Mth6_V_1");
        tab_Expense.Columns.Add("Mth7_V_1");
        tab_Expense.Columns.Add("Mth8_V_1");
        tab_Expense.Columns.Add("Mth9_V_1");
        tab_Expense.Columns.Add("Mth10_V_1");
        tab_Expense.Columns.Add("Mth11_V_1");
        tab_Expense.Columns.Add("Mth12_V_1");
        tab_Expense.Columns.Add("Tot_1");

        #endregion
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        string sql_chart = "SELECT Code,AcDesc, AcType, AcDorc, AcBank, AcCurrency, AcSubType, GNo FROM XXChartAcc where AcType='P'";
        SqlCommand cmd = new SqlCommand(sql_chart, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        decimal v_mth1 = 0;
        decimal v_mth2 = 0;
        decimal v_mth3 = 0;
        decimal v_mth4 = 0;
        decimal v_mth5 = 0;
        decimal v_mth6 = 0;
        decimal v_mth7 = 0;
        decimal v_mth8 = 0;
        decimal v_mth9 = 0;
        decimal v_mth10 = 0;
        decimal v_mth11 = 0;
        decimal v_mth12 = 0;
        decimal v_tot = 0;


        decimal v_mth1_Revenue = 0;
        decimal v_mth2_Revenue = 0;
        decimal v_mth3_Revenue = 0;
        decimal v_mth4_Revenue = 0;
        decimal v_mth5_Revenue = 0;
        decimal v_mth6_Revenue = 0;
        decimal v_mth7_Revenue = 0;
        decimal v_mth8_Revenue = 0;
        decimal v_mth9_Revenue = 0;
        decimal v_mth10_Revenue = 0;
        decimal v_mth11_Revenue = 0;
        decimal v_mth12_Revenue = 0;
        decimal v_tot_Revenue = 0;
        while (reader.Read())
        {

            string acCode = reader["Code"].ToString();
            string acCodeName = reader["AcDesc"].ToString();
            string acSource = reader["AcDorc"].ToString();
            string acCodeType = reader["AcType"].ToString(); ;
            string acSubType = reader["AcSubType"].ToString();
            if (acCodeType == "P")
            {

                #region create new row
                if (acSubType == "S")
                {
                    DataRow row = tab_Pl.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;
                    row["SortIndex"] = 0;
                    row["AcSubType"] = "Sales";

                    row["Mth1_V"] = "0.00";
                    row["Mth2_V"] = "0.00";
                    row["Mth3_V"] = "0.00";
                    row["Mth4_V"] = "0.00";
                    row["Mth5_V"] = "0.00";
                    row["Mth6_V"] = "0.00";
                    row["Mth7_V"] = "0.00";
                    row["Mth8_V"] = "0.00";
                    row["Mth9_V"] = "0.00";
                    row["Mth10_V"] = "0.00";
                    row["Mth11_V"] = "0.00";
                    row["Mth12_V"] = "0.00";
                    row["Tot"] = "0.00";
                    row["Mth1_V_1"] = "0.00";
                    row["Mth2_V_1"] = "0.00";
                    row["Mth3_V_1"] = "0.00";
                    row["Mth4_V_1"] = "0.00";
                    row["Mth5_V_1"] = "0.00";
                    row["Mth6_V_1"] = "0.00";
                    row["Mth7_V_1"] = "0.00";
                    row["Mth8_V_1"] = "0.00";
                    row["Mth9_V_1"] = "0.00";
                    row["Mth10_V_1"] = "0.00";
                    row["Mth11_V_1"] = "0.00";
                    row["Mth12_V_1"] = "0.00";
                    row["Tot_1"] = "0.00";
                    tab_Pl.Rows.Add(row);
                }
                else if (acSubType == "C")
                {
                    DataRow row = tab_Pl.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;
                    row["SortIndex"] = 1;
                    row["AcSubType"] = "Cost of Sales";

                    row["Mth1_V"] = "0.00";
                    row["Mth2_V"] = "0.00";
                    row["Mth3_V"] = "0.00";
                    row["Mth4_V"] = "0.00";
                    row["Mth5_V"] = "0.00";
                    row["Mth6_V"] = "0.00";
                    row["Mth7_V"] = "0.00";
                    row["Mth8_V"] = "0.00";
                    row["Mth9_V"] = "0.00";
                    row["Mth10_V"] = "0.00";
                    row["Mth11_V"] = "0.00";
                    row["Mth12_V"] = "0.00";
                    row["Tot"] = "0.00";
                    row["Mth1_V_1"] = "0.00";
                    row["Mth2_V_1"] = "0.00";
                    row["Mth3_V_1"] = "0.00";
                    row["Mth4_V_1"] = "0.00";
                    row["Mth5_V_1"] = "0.00";
                    row["Mth6_V_1"] = "0.00";
                    row["Mth7_V_1"] = "0.00";
                    row["Mth8_V_1"] = "0.00";
                    row["Mth9_V_1"] = "0.00";
                    row["Mth10_V_1"] = "0.00";
                    row["Mth11_V_1"] = "0.00";
                    row["Mth12_V_1"] = "0.00";
                    row["Tot_1"] = "0.00";
                    tab_Pl.Rows.Add(row);
                }
                else if (acSubType == "O")
                {
                    DataRow row = tab_Expense.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["SortIndex"] = 2;
                    row["AcSubType"] = "Operating Expense";

                    row["Mth1_V"] = "0.00";
                    row["Mth2_V"] = "0.00";
                    row["Mth3_V"] = "0.00";
                    row["Mth4_V"] = "0.00";
                    row["Mth5_V"] = "0.00";
                    row["Mth6_V"] = "0.00";
                    row["Mth7_V"] = "0.00";
                    row["Mth8_V"] = "0.00";
                    row["Mth9_V"] = "0.00";
                    row["Mth10_V"] = "0.00";
                    row["Mth11_V"] = "0.00";
                    row["Mth12_V"] = "0.00";
                    row["Tot"] = "0.00";
                    row["Mth1_V_1"] = "0.00";
                    row["Mth2_V_1"] = "0.00";
                    row["Mth3_V_1"] = "0.00";
                    row["Mth4_V_1"] = "0.00";
                    row["Mth5_V_1"] = "0.00";
                    row["Mth6_V_1"] = "0.00";
                    row["Mth7_V_1"] = "0.00";
                    row["Mth8_V_1"] = "0.00";
                    row["Mth9_V_1"] = "0.00";
                    row["Mth10_V_1"] = "0.00";
                    row["Mth11_V_1"] = "0.00";
                    row["Mth12_V_1"] = "0.00";
                    row["Tot_1"] = "0.00";
                    tab_Expense.Rows.Add(row);
                }
                else
                {
                    DataRow row = tab_Other.NewRow();
                    row["AcCode"] = acCode;
                    row["AcName"] = acCodeName;

                    row["SortIndex"] = 0;
                    row["AcSubType"] = "Other Income";

                    row["Mth1_V"] = "0.00";
                    row["Mth2_V"] = "0.00";
                    row["Mth3_V"] = "0.00";
                    row["Mth4_V"] = "0.00";
                    row["Mth5_V"] = "0.00";
                    row["Mth6_V"] = "0.00";
                    row["Mth7_V"] = "0.00";
                    row["Mth8_V"] = "0.00";
                    row["Mth9_V"] = "0.00";
                    row["Mth10_V"] = "0.00";
                    row["Mth11_V"] = "0.00";
                    row["Mth12_V"] = "0.00";
                    row["Tot"] = "0.00";
                    row["Mth1_V_1"] = "0.00";
                    row["Mth2_V_1"] = "0.00";
                    row["Mth3_V_1"] = "0.00";
                    row["Mth4_V_1"] = "0.00";
                    row["Mth5_V_1"] = "0.00";
                    row["Mth6_V_1"] = "0.00";
                    row["Mth7_V_1"] = "0.00";
                    row["Mth8_V_1"] = "0.00";
                    row["Mth9_V_1"] = "0.00";
                    row["Mth10_V_1"] = "0.00";
                    row["Mth11_V_1"] = "0.00";
                    row["Mth12_V_1"] = "0.00";
                    row["Tot_1"] = "0.00";
                    tab_Other.Rows.Add(row);
                }

                #endregion

                //decimal tot = 0;
                string where = string.Format("AcYear='{0}' and AcCode='{1}'", year, acCode);
                string sql = "SELECT AcPeriod, sum(CurrencyCrAmt) CrAmt, sum(CurrencyDbAmt) DbAmt FROM XAGlEntryDet";
                sql += " where " + where;
                sql += " group BY AcPeriod";
                DataTable current = Helper.Sql.List(sql);
                for (int i = 0; i < current.Rows.Count; i++)
                {
                    #region get result
                    int period = SafeValue.SafeInt(current.Rows[i]["AcPeriod"], 1);
                    decimal amtCurrent = 0;
                    decimal dbAmt = 0;
                    decimal crAmt = 0;
                    string sql_closeInd = string.Format("SELECT CloseInd FROM XXAccPeriod where  Year='{0}' and Period='{1}'", year, period);
                    string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_closeInd), "N");
                    if (closeInd == "N")
                        continue;
                    if (acSource == "DB")
                    {
                        dbAmt += SafeValue.SafeDecimal(current.Rows[i]["DbAmt"], 0) - SafeValue.SafeDecimal(current.Rows[i]["CrAmt"], 0);
                    }
                    else if (acSource == "CR")
                    {
                        crAmt = -SafeValue.SafeDecimal(current.Rows[i]["DbAmt"], 0) + SafeValue.SafeDecimal(current.Rows[i]["CrAmt"], 0);
                    }

                    if (acSource == "DB")
                    {
                        amtCurrent = amtCurrent + dbAmt;
                    }
                    else if (acSource == "CR")
                    {
                        amtCurrent = amtCurrent + crAmt;
                    }

                    if (acSubType == "S")
                    {
                        DataRow[] row_Current = tab_Pl.Select(string.Format("AcCode='{0}'", acCode));
                        DataRow row = row_Current[0];

                        row["Mth" + period.ToString() + "_V"] = amtCurrent.ToString("#,##0.00");
                        row["Mth" + period.ToString() + "_V_1"] = amtCurrent;
                        row["Tot"] = (SafeValue.SafeDecimal(row["Tot"], 0) + amtCurrent).ToString("#,##0.00");
                        row["Tot_1"] = SafeValue.SafeDecimal(row["Tot_1"], 0) + amtCurrent;
                        if (period == 1)
                        {
                            v_mth1 += amtCurrent;
                            v_mth1_Revenue += amtCurrent;
                        }
                        else if (period == 2)
                        {
                            v_mth2 += amtCurrent;
                            v_mth2_Revenue += amtCurrent;
                        }
                        else if (period == 3)
                        {
                            v_mth3 += amtCurrent;
                            v_mth3_Revenue += amtCurrent;
                        }
                        else if (period == 4)
                        {
                            v_mth4 += amtCurrent;
                            v_mth4_Revenue += amtCurrent;
                        }
                        else if (period == 5)
                        {
                            v_mth5 += amtCurrent;
                            v_mth5_Revenue += amtCurrent;
                        }
                        else if (period == 6)
                        {
                            v_mth6 += amtCurrent;
                            v_mth6_Revenue += amtCurrent;
                        }
                        else if (period == 7)
                        {
                            v_mth7 += amtCurrent;
                            v_mth7_Revenue += amtCurrent;
                        }
                        else if (period == 8)
                        {
                            v_mth8 += amtCurrent;
                            v_mth8_Revenue += amtCurrent;
                        }
                        else if (period == 9)
                        {
                            v_mth9 += amtCurrent;
                            v_mth9_Revenue += amtCurrent;
                        }
                        else if (period == 10)
                        {
                            v_mth10 += amtCurrent;
                            v_mth10_Revenue += amtCurrent;
                        }
                        else if (period == 11)
                        {
                            v_mth11 += amtCurrent;
                            v_mth11_Revenue += amtCurrent;
                        }
                        else if (period == 12)
                        {
                            v_mth12 += amtCurrent;
                            v_mth12_Revenue += amtCurrent;
                        }
                        v_tot += amtCurrent;
                        v_tot_Revenue += amtCurrent;

                    }
                    else if (acSubType == "C")
                    {
                        DataRow[] row_Current = tab_Pl.Select(string.Format("AcCode='{0}'", acCode));
                        DataRow row = row_Current[0];

                        row["Mth" + period.ToString() + "_V"] = amtCurrent.ToString("#,##0.00");
                        row["Mth" + period.ToString() + "_V_1"] = -amtCurrent;
                        row["Tot"] = (SafeValue.SafeDecimal(row["Tot"], 0) - amtCurrent).ToString("#,##0.00");
                        row["Tot_1"] = SafeValue.SafeDecimal(row["Tot_1"], 0) - amtCurrent;
                        if (period == 1)
                        {
                            v_mth1 -= amtCurrent;
                            v_mth1_Revenue -= amtCurrent;
                        }
                        else if (period == 2)
                        {
                            v_mth2 -= amtCurrent;
                            v_mth2_Revenue -= amtCurrent;
                        }
                        else if (period == 3)
                        {
                            v_mth3 -= amtCurrent;
                            v_mth3_Revenue -= amtCurrent;
                        }
                        else if (period == 4)
                        {
                            v_mth4 -= amtCurrent;
                            v_mth4_Revenue -= amtCurrent;
                        }
                        else if (period == 5)
                        {
                            v_mth5 -= amtCurrent;
                            v_mth5_Revenue -= amtCurrent;
                        }
                        else if (period == 6)
                        {
                            v_mth6 -= amtCurrent;
                            v_mth6_Revenue -= amtCurrent;
                        }
                        else if (period == 7)
                        {
                            v_mth7 -= amtCurrent;
                            v_mth7_Revenue -= amtCurrent;
                        }
                        else if (period == 8)
                        {
                            v_mth8 -= amtCurrent;
                            v_mth8_Revenue -= amtCurrent;
                        }
                        else if (period == 9)
                        {
                            v_mth9 -= amtCurrent;
                            v_mth9_Revenue -= amtCurrent;
                        }
                        else if (period == 10)
                        {
                            v_mth10 -= amtCurrent;
                            v_mth10_Revenue -= amtCurrent;
                        }
                        else if (period == 11)
                        {
                            v_mth11 -= amtCurrent;
                            v_mth11_Revenue -= amtCurrent;
                        }
                        else if (period == 12)
                        {
                            v_mth12 -= amtCurrent;
                            v_mth12_Revenue -= amtCurrent;
                        }
                        v_tot -= amtCurrent;
                        v_tot_Revenue -= amtCurrent;
                    }
                    else if (acSubType == "O")
                    {
                        DataRow[] row_Current = tab_Expense.Select(string.Format("AcCode='{0}'", acCode));
                        DataRow row = row_Current[0];

                        row["Mth" + period.ToString() + "_V"] = amtCurrent.ToString("#,##0.00");
                        row["Mth" + period.ToString() + "_V_1"] = -amtCurrent;
                        row["Tot"] = (SafeValue.SafeDecimal(row["Tot"], 0) - amtCurrent).ToString("#,##0.00");
                        row["Tot_1"] = SafeValue.SafeDecimal(row["Tot_1"], 0) - amtCurrent;
                        if (period == 1)
                        {
                            v_mth1 -= amtCurrent;
                        }
                        else if (period == 2)
                        {
                            v_mth2 -= amtCurrent;
                        }
                        else if (period == 3)
                        {
                            v_mth3 -= amtCurrent;
                        }
                        else if (period == 4)
                        {
                            v_mth4 -= amtCurrent;
                        }
                        else if (period == 5)
                        {
                            v_mth5 -= amtCurrent;
                        }
                        else if (period == 6)
                        {
                            v_mth6 -= amtCurrent;
                        }
                        else if (period == 7)
                        {
                            v_mth7 -= amtCurrent;
                        }
                        else if (period == 8)
                        {
                            v_mth8 -= amtCurrent;
                        }
                        else if (period == 9)
                        {
                            v_mth9 -= amtCurrent;
                        }
                        else if (period == 10)
                        {
                            v_mth10 -= amtCurrent;
                        }
                        else if (period == 11)
                        {
                            v_mth11 -= amtCurrent;
                        }
                        else if (period == 12)
                        {
                            v_mth12 -= amtCurrent;
                        }
                        v_tot -= amtCurrent;
                    }
                    else
                    {
                        DataRow[] row_Current = tab_Other.Select(string.Format("AcCode='{0}'", acCode));
                        DataRow row = row_Current[0];

                        row["Mth" + period.ToString() + "_V"] = amtCurrent.ToString("#,##0.00");
                        row["Mth" + period.ToString() + "_V_1"] = amtCurrent;
                        row["Tot"] = (SafeValue.SafeDecimal(row["Tot"], 0) + amtCurrent).ToString("#,##0.00");
                        row["Tot_1"] = SafeValue.SafeDecimal(row["Tot_1"], 0) + amtCurrent;
                        if (period == 1)
                        {
                            v_mth1 += amtCurrent;
                            v_mth1_Revenue += amtCurrent;
                        }
                        else if (period == 2)
                        {
                            v_mth2 += amtCurrent;
                            v_mth2_Revenue += amtCurrent;
                        }
                        else if (period == 3)
                        {
                            v_mth3 += amtCurrent;
                            v_mth3_Revenue += amtCurrent;
                        }
                        else if (period == 4)
                        {
                            v_mth4 += amtCurrent;
                            v_mth4_Revenue += amtCurrent;
                        }
                        else if (period == 5)
                        {
                            v_mth5 += amtCurrent;
                            v_mth5_Revenue += amtCurrent;
                        }
                        else if (period == 6)
                        {
                            v_mth6 += amtCurrent;
                            v_mth6_Revenue += amtCurrent;
                        }
                        else if (period == 7)
                        {
                            v_mth7 += amtCurrent;
                            v_mth7_Revenue += amtCurrent;
                        }
                        else if (period == 8)
                        {
                            v_mth8 += amtCurrent;
                            v_mth8_Revenue += amtCurrent;
                        }
                        else if (period == 9)
                        {
                            v_mth9 += amtCurrent;
                            v_mth9_Revenue += amtCurrent;
                        }
                        else if (period == 10)
                        {
                            v_mth10 += amtCurrent;
                            v_mth10_Revenue += amtCurrent;
                        }
                        else if (period == 11)
                        {
                            v_mth11 += amtCurrent;
                            v_mth11_Revenue += amtCurrent;
                        }
                        else if (period == 12)
                        {
                            v_mth12 += amtCurrent;
                            v_mth12_Revenue += amtCurrent;
                        }
                        v_tot += amtCurrent;
                        v_tot_Revenue += amtCurrent;

                    }
                    #endregion
                }
            }
        }
        DataRow row_mast = mast.NewRow();
        row_mast["DatePeriod"] = string.Format("Year :{0} )", year);
        row_mast["UserId"] = userId;
        int firstMth = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountFirstMonth"], 1);
        for (int i = 0; i < 12; i++)
        {
            row_mast["Mth" + (i + 1).ToString()] = (new DateTime(2000, firstMth, 1)).AddMonths(i).ToString("MMM");
        }
        row_mast["Mth1_V"] = v_mth1.ToString("0.00");
        row_mast["Mth2_V"] = v_mth2.ToString("0.00");
        row_mast["Mth3_V"] = v_mth3.ToString("0.00");
        row_mast["Mth4_V"] = v_mth4.ToString("0.00");
        row_mast["Mth5_V"] = v_mth5.ToString("0.00");
        row_mast["Mth6_V"] = v_mth6.ToString("0.00");
        row_mast["Mth7_V"] = v_mth7.ToString("0.00");
        row_mast["Mth8_V"] = v_mth8.ToString("0.00");
        row_mast["Mth9_V"] = v_mth9.ToString("0.00");
        row_mast["Mth10_V"] = v_mth10.ToString("0.00");
        row_mast["Mth11_V"] = v_mth11.ToString("0.00");
        row_mast["Mth12_V"] = v_mth12.ToString("0.00");
        row_mast["Tot"] = v_tot.ToString("0.00");
        mast.Rows.Add(row_mast);
        DataRow row_Revenue = tab_Revenue.NewRow();
        row_Revenue["Mth1_V"] = v_mth1_Revenue.ToString("0.00");
        row_Revenue["Mth2_V"] = v_mth2_Revenue.ToString("0.00");
        row_Revenue["Mth3_V"] = v_mth3_Revenue.ToString("0.00");
        row_Revenue["Mth4_V"] = v_mth4_Revenue.ToString("0.00");
        row_Revenue["Mth5_V"] = v_mth5_Revenue.ToString("0.00");
        row_Revenue["Mth6_V"] = v_mth6_Revenue.ToString("0.00");
        row_Revenue["Mth7_V"] = v_mth7_Revenue.ToString("0.00");
        row_Revenue["Mth8_V"] = v_mth8_Revenue.ToString("0.00");
        row_Revenue["Mth9_V"] = v_mth9_Revenue.ToString("0.00");
        row_Revenue["Mth10_V"] = v_mth10_Revenue.ToString("0.00");
        row_Revenue["Mth11_V"] = v_mth11_Revenue.ToString("0.00");
        row_Revenue["Mth12_V"] = v_mth12_Revenue.ToString("0.00");
        row_Revenue["Tot"] = v_tot_Revenue.ToString("0.00");
        tab_Revenue.Rows.Add(row_Revenue);

        set.Tables.Add(mast);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Other);
        set.Tables.Add(tab_Revenue);
        set.Tables.Add(tab_Expense);
        return set;
    }
    public static DataSet DsGlAuditTrial_AccCode(string year, string period, string period1, string acCode, string userId)
    {
        DataTable mast = new DataTable("Mast");

        mast.Columns.Add("Year");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("OpenAmt");

        mast.Columns.Add("AcCode");
        mast.Columns.Add("AcDesc");
        mast.Columns.Add("Amt_Current_Db");
        mast.Columns.Add("Amt_Current_Cr");
        mast.Columns.Add("Amt_Current_Bl");
        mast.Columns.Add("Amt_Open_Db");
        mast.Columns.Add("Amt_Open_Cr");
        mast.Columns.Add("Amt_Open_Bl");
        mast.Columns.Add("Amt_Close_Db");
        mast.Columns.Add("Amt_Close_Cr");
        mast.Columns.Add("Amt_Close_Bl");
        DataTable det = new DataTable("Detail");

        det.Columns.Add("Year");
        det.Columns.Add("PartyCode");
        det.Columns.Add("PartyName");
        det.Columns.Add("DocDate");
        det.Columns.Add("DocNo");
        det.Columns.Add("DocType");
        det.Columns.Add("ExRate");
        det.Columns.Add("Currency");
        det.Columns.Add("DocAmt");
        det.Columns.Add("ChqNo");
        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");
        det.Columns.Add("BlAmt");
        decimal openAmt = SafeValue.SafeDecimal(GetObj(string.Format("SELECT OpenBal FROM XXAccStatus where Year='{0}' and AcPeriod='{1}' and AcCode='{2}'", year, period, acCode)), 0);
        string acDes = GetObj("SELECT AcDesc FROM XXChartAcc WHERE  (Code = '" + acCode + "')");
        string acSource = GetObj("SELECT AcDorc FROM XXChartAcc WHERE  (Code = '" + acCode + "')");


        string sql = string.Format(@"SELECT mast.DocDate, mast.DocNo, mast.DocType,mast.PartyTo, det.CurrencyId, det.ExRate, det.AcSource, det.CrAmt, det.CurrencyCrAmt, det.DbAmt,det.CurrencyDbAmt, mast.ChqNo, mast.OtherPartyName, mast.Remark, det.GlLineNo
FROM XAGlEntryDet AS det INNER JOIN  XAGlEntry AS mast ON det.GlNo = mast.SequenceId
WHERE (det.AcCode = '{0}') AND (mast.AcYear = '{1}') AND (mast.AcPeriod >= '{2}')  AND (mast.AcPeriod <= '{3}')ORDER BY mast.DocDate, mast.DocType, mast.DocNo"
            , acCode, year, period, period1);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        decimal dbAmtCurrent = 0;
        decimal crAmtCurrent = 0;
        decimal blAmtCurrent = 0;
        while (reader.Read())
        {

            DataRow row = det.NewRow();
            string partyCode = reader["PartyTo"].ToString();
            string docType = reader["DocType"].ToString();
            if (true)//docType!="PL"&&docType!="SC"&&docType!="SD")
            {
                string docNo = reader["DocNo"].ToString();
                int docLineNo = SafeValue.SafeInt(reader["GlLineNo"], 0);
                string partyName = EzshipHelper.GetPartyName(partyCode);
                row["PartyCode"] = partyCode;
                row["PartyName"] = partyName;
                if (reader["DocType"].ToString() == "PS" && (partyName == "" || partyName=="NA"))
                {
                    row["PartyName"] = reader["OtherPartyName"].ToString();
                }
                if (row["PartyName"].ToString().Length > 32)
                    row["PartyName"] = row["PartyName"].ToString().Substring(0, 32);
                if (false)
                {
                    //string sql_des = string.Format("SELECT ChgDes1 FROM XAApPayableDet WHERE DocNo='{0}' and  DocType='{1}' and DocLineNo='{2}'", docNo, docType, docLineNo);
                    //row["PartyName"] += "\n" + SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_des), "");
                }
                else
                {
                    row["PartyName"] += "\n" + reader["Remark"].ToString();
                }
                row["Year"] = year;
                row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
                row["DocNo"] = docNo;
                row["DocType"] = docType;
                row["ExRate"] = SafeValue.SafeDecimal(reader["ExRate"], 1).ToString("0.000");
                row["Currency"] = reader["CurrencyId"].ToString();
                decimal crAmt = SafeValue.SafeDecimal(reader["CrAmt"], 0);
                decimal dbAmt = SafeValue.SafeDecimal(reader["DbAmt"], 0);
                decimal currencyCrAmt = SafeValue.SafeDecimal(reader["CurrencyCrAmt"], 0);
                decimal currencyDbAmt = SafeValue.SafeDecimal(reader["CurrencyDbAmt"], 0);
                decimal blAmt = currencyDbAmt - currencyCrAmt;
                blAmtCurrent += blAmt;
                row["DocAmt"] = (crAmt > dbAmt ? crAmt : dbAmt).ToString("#,##0.00");
                row["ChqNo"] = SafeValue.SafeString(reader["ChqNo"], "");
                row["DbAmt"] = currencyDbAmt.ToString("#,##0.00");
                row["CrAmt"] = currencyCrAmt.ToString("#,##0.00");
                row["BlAmt"] = blAmtCurrent.ToString("#,##0.00");

                dbAmtCurrent += currencyDbAmt;
                crAmtCurrent += currencyCrAmt;

                det.Rows.Add(row);
            }
        }
        DataRow rowMast = mast.NewRow();
        rowMast["Year"] = year;
        rowMast["OpenAmt"] = openAmt; string sql_accperiod = string.Format("SELECT StartDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period);
        string from = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        sql_accperiod = string.Format("SELECT EndDate FROM XXAccPeriod where Year='{0}' and Period='{1}'", year, period1);
        string end = SafeValue.SafeDateStr(ConnectSql.ExecuteScalar(sql_accperiod));
        rowMast["DatePeriod"] = string.Format("Year :{0}  Period :{1}-{2}   ({3}-{4})", year, period, period1, from, end);

        //rowMast["DatePeriod"] = string.Format("Year:{0} Period:{1}", year, period);
        rowMast["UserId"] = userId;
        rowMast["OpenAmt"] = openAmt;
        rowMast["AcCode"] = acCode;
        rowMast["AcDesc"] = acDes;
        rowMast["Amt_Current_Db"] = dbAmtCurrent.ToString("#,##0.00");
        rowMast["Amt_Current_Cr"] = crAmtCurrent.ToString("#,##0.00");
        rowMast["Amt_Current_Bl"] = blAmtCurrent.ToString("#,##0.00");

        rowMast["Amt_Open_Db"] = openAmt.ToString("#,##0.00");
        rowMast["Amt_Open_Cr"] = "0.00";
        if (acSource == "CR")
        {
            rowMast["Amt_Open_Cr"] = openAmt.ToString("#,##0.00");
            rowMast["Amt_Open_Db"] = "0.00";
        }
        rowMast["Amt_Open_Bl"] = openAmt.ToString("#,##0.00");

        rowMast["Amt_Close_Db"] = (dbAmtCurrent + openAmt).ToString("#,##0.00");
        rowMast["Amt_Close_Cr"] = crAmtCurrent.ToString("#,##0.00");
        rowMast["Amt_Close_Bl"] = (blAmtCurrent + openAmt).ToString("#,##0.00");
        if (acSource == "CR")
        {
            rowMast["Amt_Open_Cr"] = openAmt.ToString("#,##0.00");
            rowMast["Amt_Open_Db"] = "0.00";

            rowMast["Amt_Close_Db"] = dbAmtCurrent.ToString("#,##0.00");
            rowMast["Amt_Close_Cr"] = (crAmtCurrent + openAmt).ToString("#,##0.00");
            rowMast["Amt_Close_Bl"] = (blAmtCurrent - openAmt).ToString("#,##0.00");
        }
        mast.Rows.Add(rowMast);

        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Year"], det.Columns["Year"]);
        ds.Relations.Add(r);
        return ds;
    }

    public static DataSet DsGlJournalList(DateTime d1, DateTime d2, string docType, string userId)
    {
        DataTable mast = new DataTable("Mast");

        mast.Columns.Add("Year");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("DocType");

        DataTable det = new DataTable("Detail");

        det.Columns.Add("Year");
        det.Columns.Add("GroupId");
        det.Columns.Add("PartyCode");
        det.Columns.Add("PartyName");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcDesc");
        det.Columns.Add("DocNo");
        det.Columns.Add("DocDate");
        det.Columns.Add("DocType");

        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");

        if (docType == "3")
            docType = "GE";
        string sql = string.Format(@"SELECT mast.DocDate, mast.DocNo, mast.DocType, mast.PartyTo, det.AcCode, det.AcSource, det.CurrencyCrAmt, det.CurrencyDbAmt
FROM  XAGlEntryDet AS det INNER JOIN  XAGlEntry AS mast ON det.GlNo = mast.SequenceId
WHERE (mast.DocDate >= '{0}') AND (mast.DocDate< '{1}') AND (mast.DocType = '{2}')
ORDER BY mast.DocDate, mast.DocType, mast.DocNo", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {

            DataRow row = det.NewRow();
            string acCode = reader["AcCode"].ToString();
            string acDes = GetObj("SELECT AcDesc FROM XXChartAcc WHERE  (Code = '" + acCode + "')");
            row["Year"] = d1.ToString("yyyy");
            row["GroupId"] = 0;
            row["AcCode"] = acCode;
            row["AcDesc"] = acDes;
            row["DocDate"] = SafeValue.SafeDateStr(reader["DocDate"]);
            row["DocNo"] = reader["DocNo"].ToString();
            row["DocType"] = reader["DocType"].ToString();
            decimal crAmt = SafeValue.SafeDecimal(reader["CurrencyCrAmt"], 0);
            decimal dbAmt = SafeValue.SafeDecimal(reader["CurrencyDbAmt"], 0);

            if (dbAmt != 0)
                row["DbAmt"] = dbAmt.ToString("#,##0.00");
            if (crAmt != 0)
                row["CrAmt"] = crAmt.ToString("#,##0.00");

            det.Rows.Add(row);
        }
        DataRow rowMast = mast.NewRow();
        rowMast["Year"] = d1.ToString("yyyy"); ;
        rowMast["DatePeriod"] = string.Format("From:{0} To:{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        rowMast["UserId"] = userId;
        if (docType == "GE")
            docType = "JOURNAL ENTRY";
        rowMast["DocType"] = docType;
        mast.Rows.Add(rowMast);

        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(det);
        DataRelation r = new DataRelation("", mast.Columns["Year"], det.Columns["Year"]);
        ds.Relations.Add(r);
        return ds;
    }
    public static DataSet DsGlJournalList_1(DateTime d1, DateTime d2, string docType, string userId)
    {
        DataTable mast = new DataTable("Mast");

        mast.Columns.Add("Year");
        mast.Columns.Add("DatePeriod");
        mast.Columns.Add("UserId");
        mast.Columns.Add("DocType");

        DataTable det = new DataTable("Detail1");

        det.Columns.Add("Year");
        det.Columns.Add("AcCode");
        det.Columns.Add("AcDesc");
        det.Columns.Add("DbAmt");
        det.Columns.Add("CrAmt");

        if (docType == "3")
            docType = "GE";
        string sql = string.Format(@"SELECT mast.DocDate, mast.DocNo, mast.DocType, mast.PartyTo, det.AcCode, det.AcSource, det.CurrencyCrAmt, det.CurrencyDbAmt
FROM  XAGlEntryDet AS det INNER JOIN  XAGlEntry AS mast ON det.GlNo = mast.SequenceId
WHERE (mast.DocDate >= '{0}') AND (mast.DocDate< '{1}') AND (mast.DocType = '{2}')
ORDER BY mast.DocDate, mast.DocType, mast.DocNo", d1.ToString("yyyy-MM-dd"), d2.AddDays(1).ToString("yyyy-MM-dd"), docType);

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {

            DataRow row = det.NewRow();
            string acCode = reader["AcCode"].ToString();
            string acDes = GetObj("SELECT AcDesc FROM XXChartAcc WHERE  (Code = '" + acCode + "')");
            row["Year"] = d1.ToString("yyyy");
            row["AcCode"] = acCode;
            row["AcDesc"] = acDes;
            decimal crAmt = SafeValue.SafeDecimal(reader["CurrencyCrAmt"], 0);
            decimal dbAmt = SafeValue.SafeDecimal(reader["CurrencyDbAmt"], 0);

            row["DbAmt"] = dbAmt.ToString("#,##0.00");
            row["CrAmt"] = crAmt.ToString("#,##0.00");

            det.Rows.Add(row);
        }
        DataRow rowMast = mast.NewRow();
        rowMast["Year"] = d1.ToString("yyyy"); ;
        rowMast["DatePeriod"] = string.Format("From:{0} To:{1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
        rowMast["UserId"] = userId;
        if (docType == "GE")
            docType = "JOURNAL ENTRY";
        rowMast["DocType"] = docType;
        mast.Rows.Add(rowMast);


        DataTable detail = new DataTable("Detail");
        DataSetHelper help = new DataSetHelper();
        detail = help.SelectGroupByInto("Detail", det, "Year,AcCode,AcDesc,sum(DbAmt) DbAmt,sum(CrAmt) CrAmt", "", "Year,AcCode,AcDesc");
        DataSet ds = new DataSet();
        ds.Tables.Add(mast);
        ds.Tables.Add(detail);
        DataRelation r = new DataRelation("", mast.Columns["Year"], detail.Columns["Year"]);
        ds.Relations.Add(r);
        return ds;
    }

    



    #endregion


 
}
