using System;

namespace C2
{
    public class SeaCommercial
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string refType;
        private int lineCNo;
        private string code;
        private string description;
        private int qty;
        private decimal price;
        private decimal amount;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
        }

        public int LineCNo
        {
            get { return this.lineCNo; }
            set { this.lineCNo = value; }
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

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                if (refType == "AE")
                    sql = "select statuscode from air_ref where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExport where JobNo='" + this.jobNo + "'";
                if (refType == "AE")
                    sql = "select StatusCode from air_job where JobNo='" + this.jobNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string packageType;
        public string PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
        }
        private decimal value;
        public decimal Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
