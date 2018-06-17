using System;

namespace C2
{
    public class WhSORelease
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string releaseNo;
        private DateTime releaseDate;
        private string partyId;
        private string partyRefNo;
        private string warehouseId;
        private string salesmanId;
        private string currency;
        private decimal exRate;
        private string remark;
        private string statusCode;

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

        public string ReleaseNo
        {
            get { return this.releaseNo; }
            set { this.releaseNo = value; }
        }

        public DateTime ReleaseDate
        {
            get { return this.releaseDate; }
            set { this.releaseDate = value; }
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
    }
}
