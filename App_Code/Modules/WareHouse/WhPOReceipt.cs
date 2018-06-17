using System;

namespace C2
{
    public class WhPOReceipt
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string receiptNo;
        private DateTime receiptDate;
        private string receiptType;
        private string partyId;
        private string partyRefNo;
        private string warehouseId;
        private string salesmanId;
        private string currency;
        private decimal exRate;
        private string remark;
        private string statusCode;
        private string poNo;

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

        public DateTime ReceiptDate
        {
            get { return this.receiptDate; }
            set { this.receiptDate = value; }
        }

        public string ReceiptType
        {
            get { return this.receiptType; }
            set { this.receiptType = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string PartyRefNo
        {
            get { return this.partyRefNo; }
            set { this.partyRefNo = value; }
        }

        public string WarehouseId
        {
            get { return this.warehouseId; }
            set { this.warehouseId = value; }
        }

        public string SalesmanId
        {
            get { return this.salesmanId; }
            set { this.salesmanId = value; }
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

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }
    }
}
