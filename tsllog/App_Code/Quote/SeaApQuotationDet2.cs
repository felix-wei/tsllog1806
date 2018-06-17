using System;

namespace C2
{
    public class SeaApQuotationDet2
    {
        private int id;
        private string quoteNo;
        private string term;
        private string description;

        public int Id
        {
            get { return this.id; }
        }

        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
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
    }
}
