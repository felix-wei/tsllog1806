using System;

namespace C2
{
    public class SeaQuotationDet1
    {
        private int id;
        private string quoteNo;
        private string chgCode;
        private string chgCodeDes;
        private decimal saleQty;
        private decimal salePrice;
        private string unit;
        private string saleCurrency;
        private decimal saleExRate;
        private decimal saleDocAmt;
        private decimal saleLocAmt;
        private decimal costPrice;
        private decimal costDocAmt;
        private decimal costLocAmt;
        private string splitType;
        private string costCurrency;
        private decimal costExRate;
        private decimal saleGst;
        private decimal costGst;
        private decimal costQty;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string ChgCodeDes
        {
            get { return this.chgCodeDes; }
            set { this.chgCodeDes = value; }
        }

        public decimal SaleQty
        {
            get { return this.saleQty; }
            set { this.saleQty = value; }
        }

        public decimal SalePrice
        {
            get { return this.salePrice; }
            set { this.salePrice = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string SaleCurrency
        {
            get { return this.saleCurrency; }
            set { this.saleCurrency = value; }
        }

        public decimal SaleExRate
        {
            get { return this.saleExRate; }
            set { this.saleExRate = value; }
        }

        public decimal SaleDocAmt
        {
            get { return this.saleDocAmt; }
            set { this.saleDocAmt = value; }
        }

        public decimal SaleLocAmt
        {
            get { return this.saleLocAmt; }
            set { this.saleLocAmt = value; }
        }

        public decimal CostPrice
        {
            get { return this.costPrice; }
            set { this.costPrice = value; }
        }

        public decimal CostDocAmt
        {
            get { return this.costDocAmt; }
            set { this.costDocAmt = value; }
        }

        public decimal CostLocAmt
        {
            get { return this.costLocAmt; }
            set { this.costLocAmt = value; }
        }

        public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
        }

        public string CostCurrency
        {
            get { return this.costCurrency; }
            set { this.costCurrency = value; }
        }

        public decimal CostExRate
        {
            get { return this.costExRate; }
            set { this.costExRate = value; }
        }

        public decimal SaleGst
        {
            get { return this.saleGst; }
            set { this.saleGst = value; }
        }

        public decimal CostGst
        {
            get { return this.costGst; }
            set { this.costGst = value; }
        }

        public decimal CostQty
        {
            get { return this.costQty; }
            set { this.costQty = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
    }
}
