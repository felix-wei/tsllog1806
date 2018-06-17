using System;
using System.Collections.Generic;
using System.Data;

namespace C2
{
    public class XAArInvoice
    {
        private int sequenceId;
        private int acYear;
        private int acPeriod;
        private string acCode;
        private string acSource;
        private string docType;
        private string docNo;
        private DateTime docDate;
        private DateTime docDueDate;
        private string partyTo;
        private string mastRefNo;
        private string jobRefNo;
        private string mastType;
        private string currencyId;
        private decimal exRate;
        private string term;
        private string description;
        private decimal docAmt;//
        private decimal locAmt;//=docamt*exrate
        private decimal balanceAmt;
        private string exportInd;
        private string cancelInd;
        private string reviseInd;
        private DateTime cancelDate;
        private string userId;
        private DateTime entryDate;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public int AcYear
        {
            get { return this.acYear; }
            set { this.acYear = value; }
        }

        public int AcPeriod
        {
            get { return this.acPeriod; }
            set { this.acPeriod = value; }
        }

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }

        public DateTime DocDate
        {
            get { return this.docDate; }
            set { this.docDate = value; }
        }

        public DateTime DocDueDate
        {
            get { return this.docDueDate; }
            set { this.docDueDate = value; }
        }

        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
        }
        public string PartyName
        {
            get {
                string name = "";
                if (SafeValue.SafeString(partyTo).Length>0)
                {
                        name = EzshipHelper.GetPartyName(partyTo);
                }
                else
                        name = EzshipHelper.GetPartyName(partyTo);
                 
              
                return name; }
        }

        public string MastRefNo
        {
            get { return this.mastRefNo; }
            set { this.mastRefNo = value; }
        }

        public string JobRefNo
        {
            get { return this.jobRefNo; }
            set { this.jobRefNo = value; }
        }

        public string MastType
        {
            get { return this.mastType; }
            set { this.mastType = value; }
        }

        public string CancelInd
        {
            get { return this.cancelInd; }
            set { this.cancelInd = value; }
        }

        public DateTime CancelDate
        {
            get { return this.cancelDate; }
            set { this.cancelDate = value; }
        }
        public string ReviseInd
        {
            get { return this.reviseInd; }
            set { this.reviseInd = value; }
        }

        public string CurrencyId
        {
            get { return this.currencyId; }
            set { this.currencyId = value; }
        }

        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }

        public string Term
        {
            get { return this.term; }
            set { this.term = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }


        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }

        public decimal LocAmt
        {
            get { return this.locAmt; }
            set { this.locAmt = value; }
        }

        public decimal BalanceAmt
        {
            get { return this.balanceAmt; }
            set { this.balanceAmt = value; }
        }

        public string ExportInd
        {
            get { return this.exportInd; }
            set { this.exportInd = value; }
        }
        public string ExportIndStr
        {
            get
            {
                if (this.exportInd != "Y")
                {
                    string user = System.Web.HttpContext.Current.User.Identity.Name;
                    string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("SELECT Role FROM [User] where Name='" + user + "'"), "Staff").ToLower();
                    if (role.IndexOf("staff") != -1)
                    {

                        int day = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountModifyDay"], 15);
                        if ((DateTime.Today - this.docDate).TotalDays > day)
                        {
                            return "Y";
                        }
                    }
                    else return "N";
                }
                return this.exportInd;
            }
        }

        public string AcCode
        {
            get { return this.acCode; }
            set { this.acCode = value; }
        }
        public string AcSource
        {
            get { return this.acSource; }
            set { this.acSource = value; }
        }





        private string pol;
        public string Pol
        {
            get { return this.pol; }
            set { this.pol = value; }
        }
        private string pod;
        public string Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }
        private string pic;
        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
        }
        private DateTime eta;
        public DateTime Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }
        private string vessel;
        public string Vessel
        {
            get { return this.vessel; }
            set { this.vessel = value; }
        }
        private string voyage;
        public string Voyage
        {
            get { return this.voyage; }
            set { this.voyage = value; }
        }
        private string refNo;
        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
        private string bkgRefNo;
        public string BkgRefNo
        {
            get { return this.bkgRefNo; }
            set { this.bkgRefNo = value; }
        }
        private string custRef;
        public string CustRef
        {
            get { return this.custRef; }
            set { this.custRef = value; }
        }
        private string specialNote;
        public string SpecialNote
        {
            get { return this.specialNote; }
            set { this.specialNote = value; }
        }
        private string invType;
        public string InvType
        {
            get { return this.invType; }
            set { this.invType = value; }
        }

        private decimal taxableAmt;
        private decimal taxAmt;
        private decimal taxableAmt1;
        private decimal taxAmt1;
        private decimal taxableAmt2;
        private decimal taxAmt2;
        private decimal nonTaxableAmt;
        private decimal nonTaxableAmt1;
        private decimal nonTaxableAmt2;
        public decimal TaxableAmt
        {
            get { return this.taxableAmt; }
            set { this.taxableAmt = value; }
        }

        public decimal TaxAmt
        {
            get { return this.taxAmt; }
            set { this.taxAmt = value; }
        }

        public decimal TaxableAmt1
        {
            get { return this.taxableAmt1; }
            set { this.taxableAmt1 = value; }
        }

        public decimal TaxAmt1
        {
            get { return this.taxAmt1; }
            set { this.taxAmt1 = value; }
        }

        public decimal TaxableAmt2
        {
            get { return this.taxableAmt2; }
            set { this.taxableAmt2 = value; }
        }

        public decimal TaxAmt2
        {
            get { return this.taxAmt2; }
            set { this.taxAmt2 = value; }
        }

        public decimal NonTaxableAmt
        {
            get { return this.nonTaxableAmt; }
            set { this.nonTaxableAmt = value; }
        }

        public decimal NonTaxableAmt1
        {
            get { return this.nonTaxableAmt1; }
            set { this.nonTaxableAmt1 = value; }
        }

        public decimal NonTaxableAmt2
        {
            get { return this.nonTaxableAmt2; }
            set { this.nonTaxableAmt2 = value; }
        }
        private string contact;
        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }


        public static bool update_invoice_mast(int docId)
        {
            bool res = false;
            decimal taxableAmt = 0;
            decimal taxableAmt1 = 0;
            decimal taxAmt = 0;
            decimal tabAmt1 = 0;
            decimal nonTaxableAmt = 0;
            decimal nonTaxableAmt1 = 0;
            decimal docAmt = 0;
            decimal _locAmt = 0;
            string gstType = "";
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            string sql = string.Format(@"select LocAmt,GstType from XAArInvoiceDet where DocId=@DocId");
            list.Add(new ConnectSql_mb.cmdParameters("@DocId", docId, SqlDbType.Int));
            DataTable tab=ConnectSql_mb.GetDataTable(sql,list);
            for (int i = 0; i < tab.Rows.Count; i++) {
                DataRow row = tab.Rows[i];
                _locAmt = SafeValue.SafeDecimal(row["LocAmt"]);
                gstType = SafeValue.SafeString(row["GstType"]);
                if(gstType=="S")
                {
                    taxableAmt += _locAmt;
                }
                if (gstType == "T")
                {
                    taxableAmt1 += _locAmt;
                }
                if (gstType == "Z")
                {
                    nonTaxableAmt += _locAmt;
                }
                if (gstType == "E")
                {
                    nonTaxableAmt1 += _locAmt;
                }
                
            }
            taxAmt += SafeValue.ChinaRound(taxableAmt * SafeValue.SafeDecimal(0.07),2);
            tabAmt1 += SafeValue.ChinaRound(taxableAmt1 * SafeValue.SafeDecimal(0.035), 2);
            list = new List<ConnectSql_mb.cmdParameters>();
            decimal locAmt = 0;
            sql = string.Format(@"update XAArInvoice set TaxableAmt=@TaxableAmt,TaxableAmt1=@TaxableAmt,TaxAmt=@TaxAmt,TaxAmt1=@TaxAmt1,NonTaxableAmt=@NonTaxableAmt,NonTaxableAmt1=@NonTaxableAmt1 where SequenceId=@DocId");
            list.Add(new ConnectSql_mb.cmdParameters("@TaxableAmt", taxableAmt, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@TaxableAmt1",taxableAmt1,SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@TaxAmt",taxAmt,SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@TaxAmt1",tabAmt1,SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@NonTaxableAmt", nonTaxableAmt, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@NonTaxableAmt1", nonTaxableAmt1, SqlDbType.Decimal));
            list.Add(new ConnectSql_mb.cmdParameters("@DocId",docId,SqlDbType.Int));
            ConnectSql_mb.sqlResult result=ConnectSql_mb.ExecuteNonQuery(sql,list);
            if (result.status)
            {
                res = result.status;

                sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId={0}", docId);
                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
                tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    if (tab.Rows[i]["AcSource"].ToString() == "CR")
                    {
                        docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                        locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
                    }
                    else
                    {
                        docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                        locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
                    }
                }

                decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

                balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

                sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
                C2.Manager.ORManager.ExecuteCommand(sql);
            }
            return res;
        }
    }
}
		