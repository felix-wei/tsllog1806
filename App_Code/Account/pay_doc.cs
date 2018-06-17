using System;

namespace C2
{
    public class pay_doc:BaseObject
    {
        private int id;
        private int acYear;
        private int acPeriod;
        private string docType;
        private string docType1;
        private string docNo;
        private DateTime docDate;
        private string docCurrency;
        private decimal docExRate;
        private string partyTo;
        private decimal docAmt;
        private decimal locAmt;
        private string acCode;
        private string acSource;
        private string chqNo;
        private DateTime chqDate;
        private string exportInd;
        private string bankName;
        private string remark;

        public int Id
        {
            get { return this.id; }
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

        public string DocCurrency
        {
            get { return this.docCurrency; }
            set { this.docCurrency = value; }
        }

        public decimal DocExRate
        {
            get { return this.docExRate; }
            set { this.docExRate = value; }
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
                string name = EzshipHelper.GetPartyName(partyTo);
                if(name.Length==0)
                    name = this.otherPartyName;
                return name;
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

        public string ExportInd
        {
            get { return this.exportInd; }
            set { this.exportInd = value; }
        }

        public string BankName
        {
            get { return this.bankName; }
            set { this.bankName = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        private int cnId;
        public int CnId
        {
            get { return this.cnId; }
            set { this.cnId = value; }
        }
        private string cnNo;
        public string CnNo
        {
            get { return this.cnNo; }
            set { this.cnNo = value; }
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
        private string bankRec;
        private DateTime bankDate;
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

        private string otherPartyName;
        public string OtherPartyName
        {
            get { return this.otherPartyName; }
            set { this.otherPartyName = value; }
        }

        private string generateInd;
        public string GenerateInd
        {
            get { return this.generateInd; }
            set { this.generateInd = value; }
        }
        private string pic;
        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
        }
        private string userId;
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }
        private string slipNo;
        public string SlipNo
        {
            get { return this.slipNo; }
            set { this.slipNo = value; }
        }
        private bool isReturn;
        public bool IsReturn
        {
            get { return this.isReturn; }
            set { this.isReturn = value; }
        }
    }
}
