using System;

namespace C2
{
    public class WhPacking
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string containerNo;
        private string sealNo;
        private string userId;
        private DateTime entryDate;
        private string marking;
        private string description;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string remark;
        private string mkgType;
        private string containerType;
        private DateTime podEta;
        private DateTime podClearDate;
        private DateTime podReturnDate;
        private DateTime polEta;
        private DateTime polClearDate;
        private DateTime polReturnDate;
        private string polRemark;
        private string podRemark;

        public int Id
        {
            get { return this.id; }
        }
        public string CloseInd
        {
            get
            {
                string s = "N";
                string sql = "select RefCloseInd from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                return s;
            }
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

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
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
        private decimal grossWt;
        public decimal GrossWt
        {
            get { return this.grossWt; }
            set { this.grossWt = value; }
        }
        private decimal netWt;
        public decimal NetWt
        {
            get { return this.netWt; }
            set { this.netWt = value; }
        }

        public DateTime PodEta
        {
            get { return this.podEta; }
            set { this.podEta = value; }
        }

        public DateTime PodClearDate
        {
            get { return this.podClearDate; }
            set { this.podClearDate = value; }
        }

        public DateTime PodReturnDate
        {
            get { return this.podReturnDate; }
            set { this.podReturnDate = value; }
        }

        public DateTime PolEta
        {
            get { return this.polEta; }
            set { this.polEta = value; }
        }

        public DateTime PolClearDate
        {
            get { return this.polClearDate; }
            set { this.polClearDate = value; }
        }

        public DateTime PolReturnDate
        {
            get { return this.polReturnDate; }
            set { this.polReturnDate = value; }
        }

        public string PolRemark
        {
            get { return this.polRemark; }
            set { this.polRemark = value; }
        }

        public string PodRemark
        {
            get { return this.podRemark; }
            set { this.podRemark = value; }
        }

        public string Vessel
        {
            get
            {
                string s = "";
                string sql = "select Vessel from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
                return s;
            }
        }

        public string Voyage
        {
            get
            {
                string s = "";
                string sql = "select Voyage from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
                return s;
            }
        }

        public string Pol
        {
            get
            {
                string s = "";
                string sql = "select Pol from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
                return s;
            }
        }

        public string Pod
        {
            get
            {
                string s = "";
                string sql = "select Pod from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
                return s;
            }
        }

        public DateTime Eta
        {
            get
            {
                DateTime s;
                string sql = "select Eta from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql), DateTime.Today);
                return s;
            }
        }

        public DateTime EtaDest
        {
            get
            {
                DateTime s;
                string sql = "select EtaDest from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql), DateTime.Today);
                return s;
            }
        }

        public string AgentId
        {
            get
            {
                string s = "";
                string sql = "select AgentId from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "");
                return s;
            }
        }
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
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
        private int sortIndex;
        public int SortIndex
        {
            get { return this.sortIndex; }
            set { this.sortIndex = value; }
        }
    }
}