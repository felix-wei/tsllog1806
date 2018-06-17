using System;

namespace C2
{
    public class SeaCertificateDet
    {
        private int id;
        private string cerNo;
        private string refNo;
        private string jobNo;
        private string refType;
        private string marking;
        private string description;
        private int qty;
        private string packageType;
        private decimal amt;
        private string currency;
        private decimal exRate;

        public int Id
        {
            get { return this.id; }
        }

        public string CerNo
        {
            get { return this.cerNo; }
            set { this.cerNo = value; }
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

        public decimal Amt
        {
            get { return this.amt; }
            set { this.amt = value; }
        }

        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        public decimal ExRate
        {
            get { return this.exRate; }
            set { this.exRate = value; }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExport where JobNo='" + this.jobNo + "'";
                if (refType == "SI")
                    sql = "select StatusCode from SeaImport where JobNo='" + this.jobNo + "'";
                else if (refType == "AE" || refType == "AI")
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
                string sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";
                if (refType == "AE" || refType == "AI")
                    sql = "select StatusCode from air_ref where RefNo='" + this.refNo + "'";
                else if (refType == "SE")
                    sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}
