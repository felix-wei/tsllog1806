using System;

namespace C2
{
    public class WhPOReceiptDet
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string receiptNo;
        private string receiptType;
        private int linePNo;
        private string poNo;
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
        private string locCode;
        private string lotNo;
        private string dimension;
        private string batchNo;
        private int qty1;
        private int qty2;

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

        public string ReceiptNo
        {
            get { return this.receiptNo; }
            set { this.receiptNo = value; }
        }

        public string ReceiptType
        {
            get { return this.receiptType; }
            set { this.receiptType = value; }
        }

        public int LinePNo
        {
            get { return this.linePNo; }
            set { this.linePNo = value; }
        }

        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
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

        public string LocCode
        {
            get { return this.locCode; }
            set { this.locCode = value; }
        }
        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from wh_POReceipt where ReceiptNo='" + this.ReceiptNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }
        public string Dimension
        {
            get { return this.dimension; }
            set { this.dimension = value; }
        }
        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
        }
        public int Qty1
        {
            get { return this.qty1; }
            set { this.qty1 = value; }
        }
        public int Qty2
        {
            get { return this.qty2; }
            set { this.qty2= value; }
        }
    }
}
