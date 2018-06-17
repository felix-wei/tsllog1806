using System;

namespace C2
{
    public class WhDoDet3
    {
        private int id;
        private string doNo;
        private string doType;
        private string containerNo;
        private string sealNo;
        private string containerType;
        private decimal weight;
        private decimal m3;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public int Id
        {
            get { return this.id; }
        }

        public string DoNo
        {
            get { return this.doNo; }
            set { this.doNo = value; }
        }

        public string DoType
        {
            get { return this.doType; }
            set { this.doType = value; }
        }

        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
        }

        public string SealNo
        {
            get { return this.sealNo; }
            set { this.sealNo = value; }
        }

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = value; }
        }

        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public decimal M3
        {
            get { return this.m3; }
            set { this.m3 = value; }
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
        private int qty;
        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }
        private string pkgType;
        public string PkgType
        {
            get { return this.pkgType; }
            set { this.pkgType = value; }
        }
        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from Wh_DO where DoNo='" + this.doNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string relaId;
        public string RelatId
        {
            get { return this.relaId; }
            set { this.relaId = value; }
        }
        private string containerStatus;
        public string ContainerStatus
        {
            get { return this.containerStatus; }
            set { this.containerStatus = value; }
        }
        private DateTime jobStart;
        public DateTime JobStart
        {
            get { return this.jobStart; }
            set { this.jobStart = value; }
        }
        private DateTime jobEnd;
        public DateTime JobEnd
        {
            get { return this.jobEnd; }
            set { this.jobEnd = value; }
        }
    }
}
