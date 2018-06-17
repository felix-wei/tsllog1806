using System;

namespace C2
{
    public class SeaDn
    {
        private int sequenceId;
        private string refNo;
        private string jobNo;
        private string refType;
        private string marking;
        private string description;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string address;

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

        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
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

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "";
                if (refType == "SI")
                {
                    sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";
                }
                if(sql.Length>0)
                    s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "";
                if (refType == "SI")
                {
                    sql = "select StatusCode from SeaImport where JobNo='" + this.jobNo + "'";
                }
                if (sql.Length > 0)
                    s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}
