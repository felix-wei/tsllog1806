using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using C2;
using Wilson.ORMapper;

/// <summary>
/// InvoiceReport 的摘要说明
/// </summary>
public class InvoiceReport
{
    private static DataTable InitMastDataTable()
    {
        DataTable tab = new DataTable("Mast");
        tab.Columns.Add("BillType");
        tab.Columns.Add("DocNo");
        tab.Columns.Add("DocType");
        tab.Columns.Add("DocDate");
        tab.Columns.Add("Terms");
        tab.Columns.Add("DocDueDate");
        tab.Columns.Add("SupplierBillNo");
        tab.Columns.Add("CustRefNo");
        tab.Columns.Add("SupplierBillDate");
        tab.Columns.Add("Voyage");
        tab.Columns.Add("Vessel");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Etd");
        tab.Columns.Add("PartyTo");
        tab.Columns.Add("PartyName");
        tab.Columns.Add("PartyAdd");
        tab.Columns.Add("AcCode");
        tab.Columns.Add("ChqNo");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("Currency");
        tab.Columns.Add("DocAmt");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("Remark");
        tab.Columns.Add("SubTotal");
        tab.Columns.Add("TotalAmt");
        tab.Columns.Add("GstTotal");
        tab.Columns.Add("Total");
        tab.Columns.Add("JobNo");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("HblNo");
        tab.Columns.Add("CrBkgNo");
        tab.Columns.Add("MoneyWords");

        tab.Columns.Add("QuoteNo");
        tab.Columns.Add("QuoteStatus");
        tab.Columns.Add("QuoteDate");
        tab.Columns.Add("QuoteRemark");
        tab.Columns.Add("JobDes");
        tab.Columns.Add("TerminalRemark");

        tab.Columns.Add("CrNo");
        tab.Columns.Add("Contact1");
        tab.Columns.Add("Tel1");
        tab.Columns.Add("Fax1");
        tab.Columns.Add("ContainerNo");
        tab.Columns.Add("CompanyName");
        return tab;
    }
    private static DataTable InitDetailDataTable()
    {
        DataTable tab = new DataTable("Det");
        tab.Columns.Add("No");
        tab.Columns.Add("DocNo");
        tab.Columns.Add("ChgDes1");
        tab.Columns.Add("OtherChgDes");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Price");
        tab.Columns.Add("Amt");
        tab.Columns.Add("LineAmt");
        tab.Columns.Add("Unit");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("LocAmt");
        tab.Columns.Add("GstAmt");
        tab.Columns.Add("Gst");
        tab.Columns.Add("Rmk");

        return tab;
    }
    public static DataSet DsQuotation(string orderNo, string docType)
    {
        DataSet set = new DataSet();
        #region Quotation
        DataTable mast = InitMastDataTable();
        DataRow row = mast.NewRow();


        Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "QuoteNo='" + orderNo + "'");

        C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
        if (ctmJob != null)
        {
            row["Vessel"] = ctmJob.Vessel;
            row["Voyage"] = ctmJob.Voyage;
            row["Eta"] = ctmJob.EtaDate.ToString("dd/MM/yy");
            row["CustRefNo"] = ctmJob.ClientRefNo;
            row["Pol"] = ctmJob.Pol;
            row["Pod"] = ctmJob.Pod;
            row["CrBkgNo"] = ctmJob.CarrierBkgNo;
            row["HblNo"] = ctmJob.CarrierBlNo;
            row["QuoteNo"] = ctmJob.QuoteNo;
            row["QuoteDate"] = ctmJob.QuoteDate.ToString("dd/MM/yy");
            row["JobNo"] = ctmJob.JobNo;
            row["BillType"] = "QUOTATION";
            row["UserId"] = HttpContext.Current.User.Identity.Name;
            row["PartyName"] = EzshipHelper.GetPartyName(ctmJob.ClientId);
            string sql = string.Format(@"select Address from XXParty where PartyId='{0}'", ctmJob.ClientId);
            row["PartyAdd"] = C2.Manager.ORManager.ExecuteScalar(sql);
            row["Terms"] = ctmJob.IncoTerm;
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["MoneyWords"] = "";
            row["QuoteRemark"] = ctmJob.QuoteRemark;
            row["JobDes"] = ctmJob.JobDes;
            row["TerminalRemark"] = ctmJob.TerminalRemark;
            decimal subTotal = 0;
            decimal gstTotal = 0;


            DataTable details = InitDetailDataTable();

            sql = string.Format(@"select count(*) from job_rate where JobNo='{0}' and ChgCode='LUMPSUM'", orderNo);
            int count = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
            string sql_n = "";
            if (count > 0)
            {
                sql = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price from job_rate where JobNo='{0}' and ChgCode='LUMPSUM'", orderNo);
                sql_n = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price from job_rate where JobNo='{0}' and ChgCode<>'LUMPSUM'", orderNo);
            }
            else {
                sql = string.Format(@"select LineType,LineStatus,JobNo,JobType,ClientId,BillScope,BillType,BillClass,ContSize,ContType,ChgCode,ChgCodeDes,Remark,Price from job_rate where JobNo='{0}'", orderNo);

            }
            DataTable det = ConnectSql.GetTab(sql);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow row1 = details.NewRow();
                string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                string chgCodeDes = SafeValue.SafeString(det.Rows[i]["ChgCodeDes"]);
                row1["No"] = i + 1;
                string chgDes = "";
                DataTable det_n = ConnectSql.GetTab(sql_n);
                for (int j = 0; j < det_n.Rows.Count; j++) {
                    chgDes +="- "+ SafeValue.SafeString(det_n.Rows[j]["ChgCodeDes"]) + "\n";
                }
                if (chgCode == "LUMPSUM")
                    chgCodeDes = ctmJob.LumSumRemark;
                row1["ChgDes1"] = chgCodeDes;
                row1["OtherChgDes"] = chgDes;
                row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["Qty"] = 1;
                row1["Rmk"] = SafeValue.SafeString(det.Rows[i]["Remark"]);
                row1["LineAmt"] = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(det.Rows[i]["Price"]), 2);
                string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes='{0}'", chgCode);
                DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                decimal gst = 0;
                string gstType = "";
                string chgTypeId = "";
                if (dt_chgCode.Rows.Count > 0)
                {
                    gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                    gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                    chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                }
                row1["Price"] = string.Format("{0:#,##0.00}", SafeValue.SafeString(det.Rows[i]["Price"]));
                row1["Gst"] = SafeValue.SafeInt(gst * 100, 0) + "% " + gstType + "R";
                decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * gst), 2);
                row1["Amt"] = amt;
                row1["GstAmt"] = gstAmt;
                row1["LocAmt"] = amt + gstAmt;


                subTotal += amt;
                gstTotal += gstAmt;
                details.Rows.Add(row1);
            }

            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["TotalAmt"] = subTotal + gstTotal;
            mast.Rows.Add(row);

     

            set.Tables.Add(mast);
            set.Tables.Add(details);
        }
        #endregion
        return set;
    }
    public static DataSet DsImpTs(string orderNo, string docType)
    {
        DataSet set = new DataSet();
        if (docType == "IV" || docType == "IV1")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAArInvoice), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAArInvoice obj = objSet[0] as C2.XAArInvoice;
            #region Invoice
            DataTable mast = InitMastDataTable();
            DataRow row = mast.NewRow();
            string mastRefNo = obj.MastRefNo;
            string mastType = obj.MastType;

            Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

            C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
            if (ctmJob != null)
            {
                row["Vessel"] = ctmJob.Vessel;
                row["Voyage"] = ctmJob.Voyage;
                row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
                row["CustRefNo"] = ctmJob.ClientRefNo;
                row["Pol"] = ctmJob.Pol;
                row["Pod"] = ctmJob.Pod;
                row["CrBkgNo"] = ctmJob.CarrierBkgNo;
                row["HblNo"] = ctmJob.CarrierBlNo;
            }
            row["JobNo"] = mastRefNo;
            row["BillType"] = "TAX INVOICE";
            row["DocNo"] = obj.DocNo;
            row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
            row["DocDueDate"] = obj.DocDueDate.ToString("dd/MM/yyyy");
            row["UserId"] = HttpContext.Current.User.Identity.Name;
            row["PartyName"] = obj.PartyName;
            row["PartyTo"] = obj.PartyTo;
            string sql = string.Format(@"select Address,Contact1,Fax1,Tel1,CrNo from XXParty where PartyId='{0}'", obj.PartyTo);
            DataTable tab = ConnectSql_mb.GetDataTable(sql);
            if (tab.Rows.Count > 0)
            {
                row["PartyAdd"] = tab.Rows[0]["Address"].ToString();
                row["Contact1"] = tab.Rows[0]["Contact1"].ToString();
                row["Fax1"] = tab.Rows[0]["Fax1"].ToString();
                row["Tel1"] = tab.Rows[0]["Tel1"].ToString();
                row["CrNo"] = tab.Rows[0]["CrNo"].ToString();
            }
            row["Terms"] = obj.Term;
            row["Terms"] = obj.Term;
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["MoneyWords"] = "";
            row["Remark"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Description from XAArInvoice where MastRefNo='{0}'", mastRefNo)));
            decimal subTotal = 0;
            decimal gstTotal = obj.TaxAmt;


            DataTable details = InitDetailDataTable();
            sql = string.Format(@"select ChgCode,Qty,MastType,DocNo,ChgDes1,Price,Gst,GstAmt,Unit,
 LineLocAmt, Currency,ExRate,
LocAmt,JobRefNo,GstType from XAArInvoiceDet where DocId={0}  order by Gst desc", obj.SequenceId);
            DataTable det = ConnectSql.GetTab(sql);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow row1 = details.NewRow();
                string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["No"] = i + 1;
                row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["LineAmt"] = SafeValue.SafeString(det.Rows[i]["LineLocAmt"]);
                row1["Unit"] = SafeValue.SafeString(det.Rows[i]["Unit"]);
                row1["Currency"] = SafeValue.SafeString(det.Rows[i]["Currency"]);
                row1["ExRate"] = SafeValue.SafeString(det.Rows[i]["ExRate"]);
                row1["Price"] = string.Format("{0:#,##0.00}", SafeValue.SafeString(det.Rows[i]["Price"]));
                row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeInt(det.Rows[i]["Qty"], 0) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
                row1["Amt"] = amt;
                row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                row1["Qty"] = SafeValue.SafeInt(det.Rows[i]["Qty"], 0);
                details.Rows.Add(row1);


                subTotal += amt;

            }
            sql = string.Format(@"select ContainerNo,SealNo,ContainerType from CTM_JobDet1 where JobNo='{0}' ", mastRefNo);
            DataTable cnt = ConnectSql.GetTab(sql);
            for (int i = 0; i < cnt.Rows.Count; i++)
            {

                if (cnt.Rows.Count - i > 1)
                {
                    row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                        + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]) + "\n";
                }
                else
                {
                    row["ContainerNo"] += SafeValue.SafeString(cnt.Rows[i]["ContainerNo"]) + " / " + SafeValue.SafeString(cnt.Rows[i]["SealNo"])
                        + " / " + SafeValue.SafeString(cnt.Rows[i]["ContainerType"]);
                }

            }

            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["TotalAmt"] = subTotal + gstTotal;
            mast.Rows.Add(row);

            #endregion

            set.Tables.Add(mast);
            set.Tables.Add(details);
        }
        if (docType == "PL" || docType == "PL1")
        {
            ObjectQuery query = new ObjectQuery(typeof(C2.XAApPayable), "DocNo='" + orderNo + "'", "");
            ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
            if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
            C2.XAApPayable obj = objSet[0] as C2.XAApPayable;
            #region PL
            DataTable mast = InitMastDataTable();
            DataRow row = mast.NewRow();
            string mastRefNo = obj.MastRefNo;
            string mastType = obj.MastType;
            if (mastType == "CTM")
            {

                Wilson.ORMapper.OPathQuery job = new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + mastRefNo + "'");

                C2.CtmJob ctmJob = C2.Manager.ORManager.GetObject(job) as C2.CtmJob;
                row["Vessel"] = ctmJob.Vessel;
                row["Voyage"] = ctmJob.Voyage;
                row["Eta"] = ctmJob.EtaDate.ToString("dd.MM.yy");
            }
            row["DocNo"] = obj.DocNo;
            row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
            row["SupplierBillNo"] = obj.SupplierBillNo;
            row["SupplierBillDate"] = obj.SupplierBillDate.ToString("dd/MM/yyyy");

            row["PartyName"] = obj.PartyName;
            string sql = string.Format(@"select Address from XXParty where PartyId='{0}'", obj.PartyTo);
            row["PartyAdd"] = C2.Manager.ORManager.ExecuteScalar(sql);
            row["Terms"] = obj.Term;

            decimal subTotal = 0;
            decimal gstTotal = 0;


            DataTable details = InitDetailDataTable();
            sql = string.Format(@"select ChgCode,sum(Qty) as Qty,MastType,max(DocNo) DocNo,max(ChgDes1) as ChgDes1,Price,max(Gst) Gst,max(GstAmt) GstAmt,
max(LocAmt) LocAmt,max(JobRefNo) JobRefNo,max(GstType) GstType from XAApPayableDet where DocId={0} group by ChgCode,MastType,Price order by Gst desc", obj.SequenceId);
            DataTable det = ConnectSql.GetTab(sql);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow row1 = details.NewRow();
                string cntType = SafeValue.SafeString(det.Rows[i]["JobRefNo"]);
                string chgCode = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["DocNo"] = SafeValue.SafeString(det.Rows[i]["DocNo"]);
                row1["ChgDes1"] = SafeValue.SafeString(det.Rows[i]["ChgDes1"]);
                row1["ChgCode"] = SafeValue.SafeString(det.Rows[i]["ChgCode"]);
                row1["Price"] = string.Format("{0:#,##0.00}", SafeValue.SafeString(det.Rows[i]["Price"]));
                row1["Gst"] = SafeValue.SafeInt(SafeValue.SafeDecimal(det.Rows[i]["Gst"]) * 100, 0) + "% " + det.Rows[i]["GstType"] + "R";
                decimal amt = SafeValue.ChinaRound(SafeValue.SafeInt(det.Rows[i]["Qty"], 0) * SafeValue.SafeDecimal(det.Rows[i]["Price"], 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(det.Rows[i]["Gst"], 0)), 2);
                row1["Amt"] = amt;
                row1["GstAmt"] = SafeValue.SafeString(det.Rows[i]["GstAmt"]);
                row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);
                row1["Qty"] = SafeValue.SafeInt(det.Rows[i]["Qty"], 0);
                details.Rows.Add(row1);


                subTotal += amt;
                gstTotal += gstAmt;

            }

            row["SubTotal"] = subTotal;
            row["GstTotal"] = gstTotal;
            row["Total"] = subTotal + gstTotal;
            mast.Rows.Add(row);

            #endregion

            set.Tables.Add(mast);
            set.Tables.Add(details);
        }
        return set;
    }
    public static DataSet DsPayTs(string orderNo)
    {
        ObjectQuery query = new ObjectQuery(typeof(C2.XAApPayment), "DocNo='" + orderNo + "'", "");
        ObjectSet objSet = C2.Manager.ORManager.GetObjectSet(query);
        if (objSet.Count == 0 || objSet[0] == null) return new DataSet();
        C2.XAApPayment obj = objSet[0] as C2.XAApPayment;
        #region Invoice
        DataTable mast = InitMastDataTable();
        DataRow row = mast.NewRow();
        //string mastRefNo = obj.MastRefNo;
        row["DocNo"] = obj.DocNo;
        row["DocType"] = obj.DocType;
        row["DocDate"] = obj.DocDate.ToString("dd/MM/yyyy");
        row["PartyTo"] = obj.PartyTo;
        row["PartyName"] = obj.OtherPartyName;
        row["ExRate"] = obj.ExRate;
        row["Currency"] = obj.CurrencyId;

        row["Remark"] = obj.Remark;

        row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];

        DataTable details = InitDetailDataTable();
        string sql = string.Format(@"select sum(LocAmt) as LocAmt,Remark1 from XAApPaymentDet where PayNo='{0}' group by Remark1", orderNo);
        DataTable det = ConnectSql.GetTab(sql);
        for (int i = 0; i < det.Rows.Count; i++)
        {
            DataRow row1 = details.NewRow();
            row1["Rmk"] = SafeValue.SafeString(det.Rows[i]["Remark1"]);
            row1["LocAmt"] = SafeValue.SafeString(det.Rows[i]["LocAmt"]);

            details.Rows.Add(row1);

        }
        row["LocAmt"] = obj.LocAmt;
        mast.Rows.Add(row);

        #endregion
        DataSet set = new DataSet();
        set.Tables.Add(mast);
        set.Tables.Add(details);
        //set.Relations.Add("", mast.Columns["DocNo"], details.Columns["DocNo"]);

        return set;
    }
}