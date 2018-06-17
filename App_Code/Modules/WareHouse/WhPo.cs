using System;

namespace C2
{
    public class WhPo
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string poNo;
        private DateTime poDate;
        private DateTime promiseDate;
        private string partyId;
        private string partyRefNo;
        private string warehouseId;
        private string salesmanId;
        private string currency;
        private decimal exRate;
        private string remark;
        private string statusCode;
        private decimal docAmt;
        private decimal locAmt;

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

        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }

        public DateTime PoDate
        {
            get { return this.poDate; }
            set { this.poDate = value; }
        }

        public DateTime PromiseDate
        {
            get { return this.promiseDate; }
            set { this.promiseDate = value; }
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
        private string customer1;
        public string Customer1
        {
            get { return this.customer1; }
            set { this.customer1 = value; }
        }
        private string customer2;
        public string Customer2
        {
            get { return this.customer2; }
            set { this.customer2 = value; }
        }
        private string customer3;
        public string Customer3
        {
            get { return this.customer3; }
            set { this.customer3 = value; }
        }
        private string customer4;
        public string Customer4
        {
            get { return this.customer4; }
            set { this.customer4 = value; }
        }
        private string customer5;
        public string Customer5
        {
            get { return this.customer5; }
            set { this.customer5 = value; }
        }
        private string customer6;
        public string Customer6
        {
            get { return this.customer6; }
            set { this.customer6 = value; }
        }
        private string customer7;
        public string Customer7
        {
            get { return this.customer7; }
            set { this.customer7 = value; }
        }
        private string customer8;
        public string Customer8
        {
            get { return this.customer8; }
            set { this.customer8 = value; }
        }
    }
}
