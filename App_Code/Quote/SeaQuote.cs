using System;

namespace C2
{
    public class SeaQuoteTitle
    {
        private int sequenceId;
        private string title;
        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
    }
    public class SeaQuote1
    {
        private int sequenceId;
        private string title;
        private string partyTo;
        private string pol;
        private string pod;
        private string viaPort;
        private string impExpInd;
        private string fclLclInd;
        private string frequency;
        private decimal lclChg;
        private string subject;
        private string quoteNo;
        private string status;
        private string createUser;
        private DateTime createDate;
        private string updateUser;
        private DateTime updateDate;

        private DateTime quoteDate;
        private DateTime expireDate;
        private string rmk;
        private string note;
        private string attention;
        private string transmitDay;
        private decimal gp20;
        private decimal gp40;
        private decimal hc40;
        private string currencyId;
        private string salesmanId;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }
        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string PartyName
        {
            get
            {
                return EzshipHelper.GetPartyName(this.partyTo);
            }
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

        public string ViaPort
        {
            get { return this.viaPort; }
            set { this.viaPort = value; }
        }

        public string ImpExpInd
        {
            get { return this.impExpInd; }
            set { this.impExpInd = value; }
        }

        public string FclLclInd
        {
            get { return this.fclLclInd; }
            set { this.fclLclInd = value; }
        }

        public string Frequency
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }


        public decimal LclChg
        {
            get { return this.lclChg; }
            set { this.lclChg = value; }
        }

        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }

        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
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

        public DateTime QuoteDate
        {
            get { return this.quoteDate; }
            set { this.quoteDate = value; }
        }

        public DateTime ExpireDate
        {
            get { return this.expireDate; }
            set { this.expireDate = value; }
        }

        public string Rmk
        {
            get { return this.rmk; }
            set { this.rmk = value; }
        }

        public string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }

        public string Attention
        {
            get { return this.attention; }
            set { this.attention = value; }
        }

        public string TransmitDay
        {
            get { return this.transmitDay; }
            set { this.transmitDay = value; }
        }

        public decimal Gp20
        {
            get { return this.gp20; }
            set { this.gp20 = value; }
        }

        public decimal Gp40
        {
            get { return this.gp40; }
            set { this.gp40 = value; }
        }

        public decimal Hc40
        {
            get { return this.hc40; }
            set { this.hc40 = value; }
        }

        public string CurrencyId
        {
            get { return this.currencyId; }
            set { this.currencyId = value; }
        }

        public string SalesmanId
        {
            get { return this.salesmanId; }
            set { this.salesmanId = value; }
        }
        private string contType;
        public string ContType
        {
            get { return this.contType; }
            set { this.contType = value; }
        }
        private decimal contPrice;
        public decimal ContPrice
        {
            get { return this.contPrice; }
            set { this.contPrice = value; }
        }
        private decimal exRate;
        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }
    }
}
