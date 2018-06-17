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
    public static DataTable DsArGst(DateTime d1, DateTime d2, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        string where = string.Format("DocDate>='{0}' and DocDate<'{1}' and isnull(ExportInd,'N')='Y'  ", dateFrm, dateTo);

        DataTable tab = new DataTable();
        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("AcCode");

        tab.Columns.Add("StdAmt");
        tab.Columns.Add("ZeroAmt");
        tab.Columns.Add("ExemptAmt");
        tab.Columns.Add("NaAmt");
        tab.Columns.Add("TaxAmt");
        tab.Columns.Add("TotAmt");

        tab.Columns.Add("UserId");
        tab.Columns.Add("DatePeriod");

        string sql = "SELECT SequenceId,DocType, DocNo,AcCode, DocDate, PartyTo, CurrencyId, ExRate,DocAmt, LocAmt FROM XAArInvoice";
        sql += " where " + where;
        sql += " order by DocType,DocNo,DocDate ";
        string gstAcCode = GetObj("SELECT AcCode FROM XXGstAccount where GstSrc='AR'");

        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string docNo = reader["DocNo"].ToString();
            string docType = reader["DocType"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string partyTo = reader["PartyTo"].ToString();
            string acCode = reader["AcCode"].ToString();
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 0);
            //decimal locAmt = SafeValue.SafeDecimal(reader["LocAmt"], 0);

            decimal stdAmt = 0;
            decimal zeroAmt = 0;
            decimal exemptAmt = 0;
            decimal gstAmt = 0;
            string sql_det = string.Format("SELECT AcCode,GstType, Qty, Price, GstAmt,DocAmt,LocAmt FROM XAArInvoiceDet WHERE (DocId = '{0}')", docId);
            DataTable tabDet = Helper.Sql.List(sql_det);
            for (int i = 0; i < tabDet.Rows.Count; i++)
            {
                DataRow rowDet = tabDet.Rows[i];

                string gstType = rowDet["GstType"].ToString();
                string detAcCode = rowDet["AcCode"].ToString();
                if (detAcCode == gstAcCode)
                {
                    gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["Qty"], 0) * SafeValue.SafeDecimal(rowDet["Price"], 0), 2);
                }
                else
                {
                    if (gstType == "S")
                    {
                        stdAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                        gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["GstAmt"], 0) * exRate, 2);
                    }
                    else if (gstType == "Z")
                        zeroAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                    else if (gstType == "E")
                        exemptAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                }
            }
            DataRow row = tab.NewRow();
            row["DocNo"] = docNo;
            row["DocType"] = docType;

            row["DocDate"] = docDate.ToString("dd/MM/yyyy");
            row["PartyCode"] = partyTo;
            row["PartyName"] = EzshipHelper.GetPartyName(row["PartyCode"]);
            row["AcCode"] = acCode;

            row["StdAmt"] = stdAmt.ToString("#,##0.00");
            row["ZeroAmt"] = zeroAmt.ToString("#,##0.00");
            row["ExemptAmt"] = exemptAmt.ToString("#,##0.00");
            row["TaxAmt"] = gstAmt.ToString("#,##0.00");
            row["NaAmt"] = "0.00";
            row["TotAmt"] = (stdAmt + zeroAmt + exemptAmt).ToString("#,##0.00");
            if (docType == "CN")
            {
                row["StdAmt"] = (-stdAmt).ToString("#,##0.00");
                row["ZeroAmt"] = (-zeroAmt).ToString("#,##0.00");
                row["ExemptAmt"] = (-exemptAmt).ToString("#,##0.00");
                row["TaxAmt"] = (-gstAmt).ToString("#,##0.00");
                row["NaAmt"] = "0.00";
                row["TotAmt"] = (-stdAmt - zeroAmt - exemptAmt).ToString("#,##0.00");
            }
            row["UserId"] = userId;
            row["DatePeriod"] = string.Format("Date From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            //if(gstAmt>0)
            tab.Rows.Add(row);
        }
        return tab;
    }

    public static DataTable DsApGst(DateTime d1, DateTime d2, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");
        string where = string.Format("DocDate>='{0}' and DocDate<'{1}' and ( DocType='PL' or DocType='SC' or DocType='SD' or DocType='VO') and isnull(ExportInd,'N')='Y'  ", dateFrm, dateTo);
        string where1 = string.Format("d.DocDate>='{0}' and d.DocDate<'{1}'  and isnull(d.ExportInd,'N')='Y'  ", dateFrm, dateTo);

        DataTable tab = new DataTable();
        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("AcCode");

        tab.Columns.Add("StdAmt");
        tab.Columns.Add("ZeroAmt");
        tab.Columns.Add("ExemptAmt");
        tab.Columns.Add("NaAmt");
        tab.Columns.Add("TaxAmt");
        tab.Columns.Add("TotAmt");

        tab.Columns.Add("UserId");
        tab.Columns.Add("DatePeriod");

        string sql = "SELECT SequenceId,DocType, DocNo,SupplierBillNo,DocDate,AcCode, PartyTo, CurrencyId, ExRate,DocAmt, LocAmt FROM XAApPayable";
        sql += " where " + where;
        sql += " order by DocType,DocDate,DocNo,SupplierBillNo ";
      
        string gstAcCode = GetObj("SELECT AcCode FROM XXGstAccount where GstSrc='AP'");





        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        string excetpPl = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptPl"], "");
        string excetpVo = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["ExceptVo"], "");
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docId = reader["SequenceId"].ToString();
            string docNo = reader["SupplierBillNo"].ToString();
            string docType = reader["DocType"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));

            string billNo = reader["DocNo"].ToString();
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


            string partyTo = reader["PartyTo"].ToString();
            string acCode = reader["AcCode"].ToString();
            decimal exRate = SafeValue.SafeDecimal(reader["ExRate"], 0);

            decimal stdAmt = 0;
            decimal zeroAmt = 0;
            decimal exemptAmt = 0;
            decimal gstAmt = 0;
            string sql_det = string.Format("SELECT AcCode,GstType, Qty, Price, GstAmt,DocAmt,LocAmt FROM XAApPayableDet WHERE (DocId = '{0}')", docId);
            DataTable tabDet = Helper.Sql.List(sql_det);

            for (int i = 0; i < tabDet.Rows.Count; i++)
            {
                DataRow rowDet = tabDet.Rows[i];

                string gstType = rowDet["GstType"].ToString();
                string detAcCode = rowDet["AcCode"].ToString();
                if (detAcCode == gstAcCode)
                {
                    gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["Qty"], 0) * SafeValue.SafeDecimal(rowDet["Price"], 0), 2);
                }
                else
                {
                    if (gstType == "S")
                    {
                        stdAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                        gstAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["GstAmt"], 0) * exRate, 2);
                    }
                    else if (gstType == "Z")
                        zeroAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                    else if (gstType == "E")
                        exemptAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(rowDet["LocAmt"], 0) * exRate, 2);
                }
            }
            DataRow row = tab.NewRow();
            row["DocNo"] = docNo;
            row["DocType"] = docType;

            row["DocDate"] = docDate.ToString("dd/MM/yyyy");
            row["PartyCode"] = partyTo;
            row["PartyName"] = EzshipHelper.GetPartyName(row["PartyCode"]);
            row["AcCode"] = acCode;

            row["StdAmt"] = stdAmt.ToString("#,##0.00");
            row["ZeroAmt"] = zeroAmt.ToString("#,##0.00");
            row["ExemptAmt"] = exemptAmt.ToString("#,##0.00");
            row["TaxAmt"] = gstAmt.ToString("#,##0.00");
            row["NaAmt"] = "0.00";
            row["TotAmt"] = (stdAmt + zeroAmt + exemptAmt).ToString("#,##0.00");
            if (docType == "SC")
            {
                row["StdAmt"] = (-stdAmt).ToString("#,##0.00");
                row["ZeroAmt"] = (-zeroAmt).ToString("#,##0.00");
                row["ExemptAmt"] = (-exemptAmt).ToString("#,##0.00");
                row["TaxAmt"] = (-gstAmt).ToString("#,##0.00");
                row["NaAmt"] = "0.00";
                row["TotAmt"] = (-stdAmt - zeroAmt - exemptAmt).ToString("#,##0.00");
            }
            row["UserId"] = userId;
            row["DatePeriod"] = string.Format("Date From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            //if(gstAmt!=0)
            tab.Rows.Add(row);
        }

	DataTable dtPS = D.List(string.Format(@"
	SELECT SequenceId,DocType,DocNo,docNo as SupplierBillNo,DocDate, '1203' as AcCode, PartyTo, CurrencyId, ExRate, DocAmt, LocAmt,
	isnull((select sum(locamt) from xaappaymentdet where payid=d.sequenceid and accode='1203'),0)  as GstAmt
	FROM xaappayment d
         where partyto<>'IRAS' and DocDate>='{0}'  and DocDate<'{1}'  and isnull(d.ExportInd,'N')='Y' 
         order by d.DocType,d.DocDate,d.DocNo ", dateFrm, dateTo));

	    for (int i = 0; i < dtPS.Rows.Count; i++)
            {

		decimal stdAmt = 0;
            decimal zeroAmt = 0;
            decimal exemptAmt = 0;
            decimal gstAmt = 0;
                DataRow rowPS = dtPS.Rows[i];

                gstAmt = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowPS["GstAmt"], 0) , 2);
		if(gstAmt == 0)
			continue;

                string gstType = "S"; //rowDet["GstType"].ToString();
                string psAcCode= rowPS["AcCode"].ToString();
                //gstAmt = SafeValue.ChinaRound(SafeValue.SafeDecimal(rowPS["LocAmt"], 0) , 2);
		stdAmt = SafeValue.ChinaRound(gstAmt / S.Dec(0.07) , 2);
                zeroAmt = 0;
		exemptAmt = 0;
                 

	    DataRow row1 = tab.NewRow();
            row1["DocNo"] = rowPS["DocNo"];
            row1["DocType"] = rowPS["DocType"];

            row1["DocDate"] = string.Format("{0:dd/MM/yyyy}",rowPS["DocDate"]);
            row1["PartyCode"] = rowPS["PartyTo"];
            row1["PartyName"] = EzshipHelper.GetPartyName(rowPS["PartyTo"]);
            row1["AcCode"] = "1203";

            row1["StdAmt"] = stdAmt.ToString("#,##0.00");
            row1["ZeroAmt"] = zeroAmt.ToString("#,##0.00");
            row1["ExemptAmt"] = exemptAmt.ToString("#,##0.00");
            row1["TaxAmt"] = gstAmt.ToString("#,##0.00");
            row1["NaAmt"] = "0.00";
            row1["TotAmt"] = (stdAmt + zeroAmt + exemptAmt).ToString("#,##0.00");
             
            row1["UserId"] = userId;
            row1["DatePeriod"] = string.Format("Date From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            //if(gstAmt!=0)
            tab.Rows.Add(row1);

            }
            

        return tab;
    }
  
 
    public static DataTable DsGlGst(DateTime d1, DateTime d2, string userId)
    {
        string dateFrm = d1.Date.ToString("yyyy-MM-dd");
        string dateTo = d2.Date.AddDays(1).ToString("yyyy-MM-dd");

        DataTable tab = new DataTable();
        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("PartyCode");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("AcCode");

        tab.Columns.Add("ArAmt");
        tab.Columns.Add("ApAmt");

        tab.Columns.Add("UserId");
        tab.Columns.Add("DatePeriod");

        string gstArAcCode = GetObj("SELECT AcCode FROM XXGstAccount where GstSrc='AR'");
        string gstApAcCode = GetObj("SELECT AcCode FROM XXGstAccount where GstSrc='AP'");
        string sql = string.Format(@"SELECT det.ArApInd,mast.DocNo, mast.DocType, mast.DocDate, mast.PartyTo, mast.OtherPartyName, mast.SupplierBillNo, det.AcCode, det.CurrencyCrAmt, det.CurrencyDbAmt
FROM XAGlEntryDet AS det INNER JOIN  XAGlEntry AS mast ON mast.SequenceId = det.GlNo
WHERE ((det.AcCode = '{0}') AND (det.ArApInd = 'AR') or
 (det.AcCode = '{1}') AND (det.ArApInd = 'AP')) and mast.DocDate>='{2}' and DocDate<'{3}'", gstArAcCode, gstApAcCode, dateFrm, dateTo);
        sql += " order by mast.DocDate,mast.DocType,mast.DocNo";
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        con.Open();
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader reader = cmd.ExecuteReader();
        // Call Read before accessing data.
        while (reader.Read())
        {
            string docType = reader["DocType"].ToString();
            string arApInd = reader["ArApInd"].ToString();
            DateTime docDate = SafeValue.SafeDate(reader["DocDate"], new DateTime(1900, 1, 1));
            string partyTo = reader["PartyTo"].ToString();
            string otherPartyName = reader["OtherPartyName"].ToString();
            string acCode = reader["AcCode"].ToString();
            decimal crAmt = SafeValue.SafeDecimal(reader["CurrencyCrAmt"], 0);
            decimal dbAmt = SafeValue.SafeDecimal(reader["CurrencyDbAmt"], 0);
            decimal amt = crAmt > dbAmt ? crAmt : dbAmt;
            DataRow row = tab.NewRow();
            if (arApInd == "AR")
            {

                row["DocNo"] = reader["DocNo"].ToString();
                row["DocType"] = docType;

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["PartyCode"] = partyTo;
                if (partyTo.Length > 0)
                    row["PartyName"] = EzshipHelper.GetPartyName(partyTo);
                else
                    row["PartyName"] = otherPartyName;

                row["AcCode"] = acCode;

                row["ApAmt"] = "0.00";
                row["ArAmt"] = amt.ToString("#,##0.00");
                if (docType == "RE")
                {
                    continue;
                }
                else if (docType == "PC")
                {
                    continue;
                    row["ArAmt"] = (-amt).ToString("#,##0.00");
                }
                else if (docType == "CN")
                {
                    row["ArAmt"] = (-amt).ToString("#,##0.00");
                }
            }
            else if (arApInd == "AP")
            {
                row["DocNo"] = reader["SupplierBillNo"].ToString();
                row["DocType"] = docType;

                row["DocDate"] = docDate.ToString("dd/MM/yyyy");
                row["PartyCode"] = partyTo;
                if (partyTo.Length > 0)
                    row["PartyName"] = EzshipHelper.GetPartyName(partyTo);
                else
                    row["PartyName"] = otherPartyName;
                row["AcCode"] = acCode;

                row["ArAmt"] = "0.00";
                row["ApAmt"] = amt.ToString("#,##0.00");
                if (docType == "SC")
                {
                    row["ApAmt"] = (-amt).ToString("#,##0.00");
                }
                else if (docType == "PS")
                {
                    row["DocNo"] = reader["DocNo"].ToString();
                }
                else if (docType == "SR")
                {
                    row["DocNo"] = reader["DocNo"].ToString();
                    row["ApAmt"] = (-amt).ToString("#,##0.00");
                }
            }
            //if(amt!=0)
            row["UserId"] = userId;
            row["DatePeriod"] = string.Format("Date From {0} To {1}", d1.ToString("dd/MM/yyyy"), d2.ToString("dd/MM/yyyy"));
            tab.Rows.Add(row);
        }

        return tab;
    }
 
    
}
