using System;

namespace C2
{
    public class SeaApQuotation
    {
        private int id;
        private string quoteNo;
        private DateTime quoteDate;
        private DateTime expireDate;
        private string quoteType;
        private string partyTo;
        private string partyName;
        private string tel;
        private string fax;
        private string contact;
        private string pol;
        private string pod;
        private string finDest;
        private string vessel;
        private string voyage;
        private DateTime eta;
        private DateTime etd;
        private DateTime etaDest;
        private string currencyId;
        private decimal exRate;
        private string salesmanId;
        private string subject;
        private string status;
        private string createUser;
        private DateTime createDate;
        private string updateUser;
        private DateTime updateDate;
        private string rmk;
        private string note1;
        private string note2;
        private string note3;
        private string note4;

        public int Id
        {
            get { return this.id; }
        }

        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
        }

        public DateTime QuoteDate
        {
            get { return this.quoteDate; }
            set { this.quoteDate = value; }
        }

        public DateTime ExpireDate
        {
            get { return this.expireDate; }
            set { this.expireDate =value; }
        }

        public string QuoteType
        {
            get { return this.quoteType; }
            set { this.quoteType = value; }
        }

        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
        }

        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
        }

        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }

        public string Fax
        {
            get { return this.fax; }
            set { this.fax = value; }
        }

        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }

        public string Pol
        {
            get { return this.pol; }
            set { this.pol = value; }
        }

        public string Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }

        public string FinDest
        {
            get { return this.finDest; }
            set { this.finDest = value; }
        }

        public string Vessel
        {
            get { return this.vessel; }
            set { this.vessel = value; }
        }

        public string Voyage
        {
            get { return this.voyage; }
            set { this.voyage = value; }
        }

        public DateTime Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }

        public DateTime Etd
        {
            get { return this.etd; }
            set { this.etd = value; }
        }

        public DateTime EtaDest
        {
            get { return this.etaDest; }
            set { this.etaDest = value; }
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

        public string SalesmanId
        {
            get { return this.salesmanId; }
            set { this.salesmanId = value; }
        }

        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string CreateUser
        {
            get { return this.createUser; }
            set { this.createUser = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        public string UpdateUser
        {
            get { return this.updateUser; }
            set { this.updateUser = value; }
        }

        public DateTime UpdateDate
        {
            get { return this.updateDate; }
            set { this.updateDate = value; }
        }

        public string Rmk
        {
            get { return this.rmk; }
            set { this.rmk = value; }
        }

        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }
    }
}
