using System;

namespace C2
{
    public class jnl_line
    {
        private int id;
        private int repId;
        private int repLineNo;
        private string acCode;
        private string acSource;
        private int docId;
        private string docNo;
        private DateTime docDate;
        private string docType;
        private decimal docAmt;
        private decimal locAmt;
        private string currency;
        private decimal exRate;
        private string remark1;
        private string remark2;
        private string remark3;

        public string Display
        {
            get
            {
                string flag = "block";
                if (this.repLineNo > 0)
                {
                    string sql = "select ExportInd from jnl_doc where Id='" + this.repId + "'";
                    flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                    if (flag == "Y")
                        flag = "none";
                }
                return flag;
            }
        }
        public int Id
        {
            get { return this.id; }
        }

        public int RepId
        {
            get { return this.repId; }
            set { this.repId = value; }
        }

        public int RepLineNo
        {
            get { return this.repLineNo; }
            set { this.repLineNo = value; }
        }

        public string AcCode
        {
            get { return this.acCode; }
            set { this.acCode = value; }
        }
        public string AcCodeStr
        {
            get
            {
                string name = "";
                if (this.acCode.Length > 0)
                {
                    name = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select acDesc from XXChartAcc where Code='" + this.acCode + "'"), "");
                }
                return name;
            }
        }

        public string AcSource
        {
            get { return this.acSource; }
            set { this.acSource = value; }
        }

        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        public int DocId
        {
            get { return this.docId; }
            set { this.docId = value; }
        }

        public DateTime DocDate
        {
            get { return this.docDate; }
            set { this.docDate = value; }
        }

        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }

        public decimal LocAmt
        {
            get { return this.locAmt; }
            set { this.locAmt = value; }
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

        public string Remark1
        {
            get { return this.remark1; }
            set { this.remark1 = value; }
        }

        public string Remark2
        {
            get { return this.remark2; }
            set { this.remark2 = value; }
        }

        public string Remark3
        {
            get { return this.remark3; }
            set { this.remark3 = value; }
        }


        public decimal DocAmt1
        {
            get
            {
                string mastCurrency = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocCurrency from jnl_doc where Id='" + this.repId + "'"), "");
                if (mastCurrency.ToUpper() != this.currency.ToUpper())
                    return 0;
                string repTypeStr = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocType from jnl_doc where Id='" + this.repId + "'"), "");
                if (this.acSource == "CR" && repTypeStr == "RE")
                    return this.docAmt;
                else if (this.acSource == "DB" && repTypeStr == "PC")
                    return this.docAmt;
                else return -this.docAmt;
            }
        }

        public decimal LocAmt1
        {
            get
            {
                string repTypeStr = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocType from jnl_doc where Id='" + this.repId + "'"), "");
                if (this.acSource == "CR" && repTypeStr == "RE")
                    return this.locAmt;
                else if (this.acSource == "DB" && repTypeStr == "PC")
                    return this.locAmt;

                else return -this.locAmt;
            }
        }

        private string repNo;
        public string RepNo
        {
            get { return this.repNo; }
            set { this.repNo = value; }
        }
        private string repType;
        public string RepType
        {
            get { return this.repType; }
            set { this.repType = value; }
        }
    }
}
