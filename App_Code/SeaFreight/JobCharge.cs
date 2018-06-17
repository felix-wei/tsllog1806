using System;

namespace C2
{
    public class JobCharge
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string refType;
        private string code;
        private string chgCodeDes;
        private decimal salePrice1;
        private decimal salePrice2;
        private decimal costPrice1;
        private decimal costPrice2;

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

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string ChgCodeDes
        {
            get { return this.chgCodeDes; }
            set { this.chgCodeDes = value; }
        }

        public decimal SalePrice1
        {
            get { return this.salePrice1; }
            set { this.salePrice1 = value; }
        }

        public decimal SalePrice2
        {
            get { return this.salePrice2; }
            set { this.salePrice2 = value; }
        }

        public decimal CostPrice1
        {
            get { return this.costPrice1; }
            set { this.costPrice1 = value; }
        }
        public decimal CostPrice2
        {
            get { return this.costPrice2; }
            set { this.costPrice2 = value; }
        }

        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaImport where JobNo='" + this.jobNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}
