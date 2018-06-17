using System;

namespace C2
{
    public class XAApPayable
    {
        private int sequenceId;
        private int acYear;
        private int acPeriod;
        private string acCode;
        private string acSource;
        private string docType;
        private string docNo;
        private DateTime docDate;
        private string partyTo;
        private string mastRefNo;
        private string jobRefNo;
        private string mastType;
        private string currencyId;
        private decimal exRate;
        private string term;
        private string description;
        private decimal docAmt;
        private decimal locAmt;
        private decimal balanceAmt;
        private string exportInd;
        private string cancelInd;
        private DateTime cancelDate;
        private string userId;
        private DateTime entryDate;
        private string otherPartyName;
        public string PartyName
        {
            get
            {
                string name = "";
                if (SafeValue.SafeString(partyTo).Length>0)
                {
                        name = EzshipHelper.GetPartyName(partyTo);
                }
                else
                    name = otherPartyName;
                return name;
            }
        }
        public string OtherPartyName
        {
            get { return this.otherPartyName; }
            set { this.otherPartyName = value; }
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

        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
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
                return this.exportInd;
            }
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


        //only for VO:voucher 
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
        private DateTime eta;
        public DateTime Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }
    }
}
