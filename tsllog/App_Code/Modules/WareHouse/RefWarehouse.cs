using System;

namespace C2
{
    public class RefWarehouse
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string code;
        private string name;
        private string remark;
        private string address;
        private string contact;
        private string tel;
        private string fax;

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

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }

        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }

        public string Fax
        {
            get { return this.fax; }
            set { this.fax = value; }
        }
        private string stockType;
        public string StockType
        {
            get { return this.stockType; }
            set { this.stockType = value; }
        }
        private string isBonded;
        public string IsBonded
        {
            get { return this.isBonded; }
            set { this.isBonded = value; }
        }
        private string lotCode;
        public string LotCode
        {
            get { return this.lotCode; }
            set { this.lotCode = value; }
        }
        private string licenseNo;
        public string LicenseNo
        {
            get { return this.licenseNo; }
            set { this.licenseNo = value; }
        }
        private DateTime licenseExpiry;
        public DateTime LicenseExpiry
        {
            get { return this.licenseExpiry; }
            set { this.licenseExpiry = value; }
        }
        private string licenseRemark;
        public string LicenseRemark
        {
            get { return this.licenseRemark; }
            set { this.licenseRemark = value; }
        }

    }
}
