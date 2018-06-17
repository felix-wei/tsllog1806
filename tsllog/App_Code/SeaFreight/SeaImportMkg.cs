using System;

namespace C2
{
    public class SeaImportMkg
    {
        private int sequenceId;
        private string refNo;
        private string jobNo;
        private string containerNo;
        private string sealNo;
        private string marking;
        private string description;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string remark;
        private string mkgType;
        private string containerType;

        public int SequenceId
        {
            get { return this.sequenceId; }
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

        public string Marking
        {
            get { return this.marking; }
            set { this.marking = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        public string MkgType
        {
            get { return this.mkgType; }
            set { this.mkgType = value; }
        }

        public string ContainerType
        {
            get { return this.containerType; }
            set { this.containerType = value; }
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
        private string statusCode;
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
    }
}
