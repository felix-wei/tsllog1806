using System;

namespace C2
{
    public class XAGlEntry
    {
        private int sequenceId;
        private int acYear;
        private int acPeriod;
        private string currencyId;
        private decimal exRate;
        private decimal crAmt;
        private decimal dbAmt;
        private decimal currencyCrAmt;
        private decimal currencyDbAmt;
        private string remark;
        private string postInd;
        private DateTime postDate;
        private string userId;
        private DateTime entryDate;

        private string docType;
        private string arApInd;
        private string cancelInd;
        private DateTime cancelDate;
        private DateTime docDate;
        public DateTime DocDate
        {
            get { return this.docDate; }
            set { this.docDate = value; }
        }
        private string docNo;
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        public string ArApInd
        {
            get { return this.arApInd; }
            set { this.arApInd = value; }
        }
        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
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

        public decimal CrAmt
        {
            get { return this.crAmt; }
            set { this.crAmt = value; }
        }

        public decimal DbAmt
        {
            get { return this.dbAmt; }
            set { this.dbAmt = value; }
        }

        public decimal CurrencyCrAmt
        {
            get { return this.currencyCrAmt; }
            set { this.currencyCrAmt = value; }
        }

        public decimal CurrencyDbAmt
        {
            get { return this.currencyDbAmt; }
            set { this.currencyDbAmt = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string PostInd
        {
            get
            {
                if (this.acYear > 0 && this.acPeriod > 0)
                {
                    this.postInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(string.Format("SELECT  CloseInd FROM XXAccPeriod where Year='{0}' and Period='{1}'"
                        , this.acYear, this.acPeriod)), "Y");
                }
                return this.postInd;
            }
            set { this.postInd = value; }
        }

        public DateTime PostDate
        {
            get { return this.postDate; }
            set { this.postDate = value; }
        }

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }
        private string partyTo;
        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
        }
        public string PartyName
        {
            get
            {
                string name = EzshipHelper.GetPartyName(partyTo);
                if (name.Length == 0)
                    name = otherPartyName;
                return name;
            }
        }
        private string chqNo;
        public string ChqNo
        {
            get { return this.chqNo; }
            set { this.chqNo = value; }
        }
        private DateTime chqDate;
        public DateTime ChqDate
        {
            get { return this.chqDate; }
            set { this.chqDate = value; }
        }
        private string supplierBillNo;
        public string SupplierBillNo
        {
            get { return this.supplierBillNo; }
            set { this.supplierBillNo = value; }
        }
        private DateTime supplierBillDate;
        public DateTime SupplierBillDate
        {
            get { return this.supplierBillDate; }
            set { this.supplierBillDate = value; }
        }

        private string otherPartyName;
        public string OtherPartyName
        {
            get { return this.otherPartyName; }
            set { this.otherPartyName = value; }
        }
    }
}
