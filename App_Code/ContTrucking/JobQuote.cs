using System;

namespace C2
{
    public class JobQuote
    {
        private int id;
        private string docNo;
        private DateTime docDate;
        private string docType;	// ar or ap quote
        private string jobType;
        private string jobNo;
        private string refNo;
        private string partyId;
        private string partyContact;
        private string partyEmail;
        private string partyMobile;
        private string partyAddress;
        private string partyRemark;
        private string salesId;
        private string salesRemark;
        private string docNote;
        private string docStatus;
        private string workStatus;
        private string currencyId;
        private decimal exRate;
        private string docRemark;
        private DateTime dateEffective;
        private DateTime dateExpiry;
        private int companyId;
        private string rowStatus;
        private string rowCreateUser;
        private DateTime rowCreateTime;
        private string rowUpdateUser;
        private DateTime rowUpdateTime;

        public int Id
        {
            get { return this.id; }
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

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string PartyContact
        {
            get { return this.partyContact; }
            set { this.partyContact = value; }
        }

        public string PartyEmail
        {
            get { return this.partyEmail; }
            set { this.partyEmail = value; }
        }

        public string PartyMobile
        {
            get { return this.partyMobile; }
            set { this.partyMobile = value; }
        }

        public string PartyAddress
        {
            get { return this.partyAddress; }
            set { this.partyAddress = value; }
        }

        public string PartyRemark
        {
            get { return this.partyRemark; }
            set { this.partyRemark = value; }
        }

        public string SalesId
        {
            get { return this.salesId; }
            set { this.salesId = value; }
        }

        public string SalesRemark
        {
            get { return this.salesRemark; }
            set { this.salesRemark = value; }
        }

        public string DocNote
        {
            get { return this.docNote; }
            set { this.docNote = value; }
        }

        public string DocStatus
        {
            get { return this.docStatus; }
            set { this.docStatus = value; }
        }

        public string WorkStatus
        {
            get { return this.workStatus; }
            set { this.workStatus = value; }
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

        public string DocRemark
        {
            get { return this.docRemark; }
            set { this.docRemark = value; }
        }

        public DateTime DateEffective
        {
            get { return this.dateEffective; }
            set { this.dateEffective = value; }
        }

        public DateTime DateExpiry
        {
            get { return this.dateExpiry; }
            set { this.dateExpiry = value; }
        }

        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        public string RowStatus
        {
            get { return this.rowStatus; }
            set { this.rowStatus = value; }
        }

        public string RowCreateUser
        {
            get { return this.rowCreateUser; }
            set { this.rowCreateUser = value; }
        }

        public DateTime RowCreateTime
        {
            get { return this.rowCreateTime; }
            set { this.rowCreateTime = value; }
        }

        public string RowUpdateUser
        {
            get { return this.rowUpdateUser; }
            set { this.rowUpdateUser = value; }
        }

        public DateTime RowUpdateTime
        {
            get { return this.rowUpdateTime; }
            set { this.rowUpdateTime = value; }
        }
    }
}
