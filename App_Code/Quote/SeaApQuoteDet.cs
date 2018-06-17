using System;

namespace C2
{
    public class SeaApQuoteDet1
    {
        private int sequenceId;
        private string quoteId;
        private int quoteLineNo;
        private string chgCode;
        private string chgDes;
        private string currency;
        private decimal qty;
        private string gstType;
        private decimal gst;
        private decimal price;
        private string unit;
        private decimal minAmt;
        private decimal amt;
        private string rmk;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string QuoteId
        {
            get { return this.quoteId; }
            set { this.quoteId = value; }
        }

         public int QuoteLineNo
        {
            get { return this.quoteLineNo; }
            set { this.quoteLineNo = value; }
        }
        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }
        public string ChgDes
        {
            get { return this.chgDes; }
            set { this.chgDes = value; }
        }

        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }
        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }

        public decimal Gst
        {
            get { return this.gst; }
            set { this.gst = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }
        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public decimal MinAmt
        {
            get { return this.minAmt; }
            set { this.minAmt = value; }
        }

        public decimal Amt
        {
            get { return this.amt; }
            set { this.amt = value; }
        }

        public string Rmk
        {
            get { return this.rmk; }
            set { this.rmk = value; }
        }
        private string groupTitle;
        public string GroupTitle
        {
            get { return this.groupTitle; }
            set { this.groupTitle = value; }
        }
        private string fclLclInd;
        public string FclLclInd
        {
            get { return this.fclLclInd; }
            set { this.fclLclInd = value; }
        }
        private decimal exRate;
        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }
        private string impExpInd;
        public string ImpExpInd
        {
            get { return this.impExpInd; }
            set { this.impExpInd = value; }
        }
    }
}
