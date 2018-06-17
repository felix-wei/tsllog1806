using System;

namespace C2
{
    public class SeaPacking
    {
        private int id;
        private string refNo;
        private string jobNo;
        private string refType;
        private int linePNo;
        private string marking;
        private string description;
        private int qty;
        private string packageType;
        private decimal weight;
        private decimal volume;
        private string dimension;

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

        public int LinePNo
        {
            get { return this.linePNo; }
            set { this.linePNo = value; }
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

        public string Dimension
        {
            get { return this.dimension; }
            set { this.dimension = value; }
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

        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                if (refType == "AE")
                    sql = "select StatusCode from air_ref where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string packingType;
        public string PackingType
        {
            get { return this.packingType; }
            set { this.packingType = value; }
        }

    }
}
