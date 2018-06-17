using System;

namespace C2
{
	public class XAApPayment:BaseObject
	{
		private int sequenceId;
		private int acYear;
		private int acPeriod;
		private string acCode;
		private string acSource;
		private string docType;
		private string docType1;
		private string docNo;
		private DateTime docDate;
		private string currencyId;
		private decimal exRate;
		private string partyTo;
		private decimal docAmt;
		private decimal locAmt;
		private string chqNo;
		private DateTime chqDate;
		private string closeInd;
		private string exportInd;
		private string bankRec;
		private DateTime bankDate;
		private string remark;
        private string generateInd;

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

		public string DocType1
		{
			get { return this.docType1; }
			set { this.docType1 = value; }
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

		public string PartyTo
		{
			get { return this.partyTo; }
			set { this.partyTo = value; }
		}
        public string PartyName
        {
            get
            {
                string name = "";
                if (SafeValue.SafeString(EzshipHelper.GetPartyName(partyTo)).Length > 0)
                {
                    name = EzshipHelper.GetPartyName(partyTo);
                }
                else if (SafeValue.SafeString(otherPartyName).Length > 0)
                {
                    name = otherPartyName;
                }
                else {
                    name = partyTo;
                }
                return name;
            }
        }
        public string ListName
        {
            get
            {
                if (this.partyTo.Length > 1)
                    return this.PartyName;
                else
                    return this.remark;
            }
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

		public string ChqNo
		{
			get { return this.chqNo; }
			set { this.chqNo = value; }
		}

		public DateTime ChqDate
		{
			get { return this.chqDate; }
			set { this.chqDate = value; }
		}

		public string CloseInd
		{
			get { return this.closeInd; }
			set { this.closeInd = value; }
		}

		public string ExportInd
		{
			get { return this.exportInd; }
			set { this.exportInd = value; }
		}

		public string BankRec
		{
			get { return this.bankRec; }
			set { this.bankRec = value; }
		}

		public DateTime BankDate
		{
			get { return this.bankDate; }
			set { this.bankDate = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

        public string GenerateInd
		{
			get { return this.generateInd; }
			set { this.generateInd = value; }
		}

        private string cancelInd;
        private DateTime cancelDate;
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
        private string otherPartyName;
        public string OtherPartyName
        {
            get { return this.otherPartyName; }
            set { this.otherPartyName = value; }
        }
        private string pic;
        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
        }
        private string poNo;
        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }
        private string mastRefNo;
        private string jobRefNo;
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
    }
}
