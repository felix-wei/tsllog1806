using System;

namespace C2
{
    public class RefProductPrice
    {
        private int id;
        private string partyId;
        private string product;
        private string description;
        private decimal price1;
        private decimal price2;
        private string statusCode;
        private DateTime fromDate;
        private DateTime toDate;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal Price1
        {
            get { return this.price1; }
            set { this.price1 = value; }
        }

        public decimal Price2
        {
            get { return this.price2; }
            set { this.price2 = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public DateTime FromDate
        {
            get { return this.fromDate; }
            set { this.fromDate = value; }
        }
        public DateTime ToDate
        {
            get { return this.toDate; }
            set { this.toDate = value; }
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
    }
}
