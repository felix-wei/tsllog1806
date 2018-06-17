using System;

namespace C2
{
    public class MastertRate
    {
        private int id;
        private string customerId;
        private string customerName;
        private string code;
        private string description;
        private decimal price;
        private DateTime loadedTime;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string CustomerId
        {
            get { return this.customerId; }
            set { this.customerId = value; }
        }

        public string CustomerName
        {
            get { return this.customerName; }
            set { this.customerName = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public DateTime LoadedTime
        {
            get { return this.loadedTime; }
            set { this.loadedTime = value; }
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
