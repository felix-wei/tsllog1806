using System;

namespace C2
{
    public class WhSoDet
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string soNo;
        private int lineSNo;
        private string product;
        private string uom;
        private int qty;
        private string currency;
        private decimal exRate;
        private decimal price;
        private string gstType;
        private decimal gst;
        private decimal docAmt;
        private decimal gstAmt;
        private decimal locAmt;
        private decimal lineLocAmt;
        private int balQty;

        public int Id
        {
            get { return this.id; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        public string SoNo
        {
            get { return this.soNo; }
            set { this.soNo = value; }
        }

        public int LineSNo
        {
            get { return this.lineSNo; }
            set { this.lineSNo = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string Uom
        {
            get { return this.uom; }
            set { this.uom = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
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

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }

        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }

        public decimal LocAmt
        {
            get { return this.locAmt; }
            set { this.locAmt = value; }
        }

        public decimal LineLocAmt
        {
            get { return this.lineLocAmt; }
            set { this.lineLocAmt = value; }
        }

        public int BalQty
        {
            get { return this.balQty; }
            set { this.balQty = value; }
        }
        private int qty1;
        public int Qty1
        {
            get { return this.qty1; }
            set { this.qty1 = value; }
        }
        private int qty2;
        public int Qty2
        {
            get { return this.qty2; }
            set { this.qty2 = value; }
        }
        private string batchNo;
        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
        }
        private string statusCode;
        public string StatusCode
        {
            get
            {
                return this.statusCode; 
            }
            set { this.statusCode = value; }
        }
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
    }
}
