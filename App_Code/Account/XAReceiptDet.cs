using System;

namespace C2
{
    public class XAArReceiptDet
    {
        private int sequenceId;
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
                    string sql = "select ExportInd from XAArReceipt where SequenceId='" + this.repId + "'";
                    flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                    if (flag == "Y")
                        flag = "none";
                }
                return flag;
            }
        }
        public int SequenceId
        {
            get { return this.sequenceId; }
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
                string mastCurrency = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocCurrency from XAArReceipt where SequenceId='" + this.repId + "'"), "");
                if (mastCurrency.ToUpper() != this.currency.ToUpper())
                    return 0;
                string repTypeStr = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocType from XAArReceipt where SequenceId='" + this.repId + "'"), "");
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
                string repTypeStr = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select DocType from XAArReceipt where SequenceId='" + this.repId + "'"), "");
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
		
		 private string mastRefNo;
        private string jobRefNo;
        private string mastType;
        public string MastRefNo
        {
            get { return this.mastRefNo; }
            set { this.mastRefNo = value; }
        }

        public string JobRefNo
        {
            get { return this.jobRefNo; }
            set { this.jobRefNo = value; }
        }

        public string MastType
        {
            get { return this.mastType; }
            set { this.mastType = value; }
        }
      
		private string dim1;
		private string dim2;
		private string dim3;
		private string dim4;
		
		public string Dim1
		{
			get { return this.dim1; }
			set { this.dim1 = value; }
		}

		public string Dim2
		{
			get { return this.dim2; }
			set { this.dim2 = value; }
		}

		public string Dim3
		{
			get { return this.dim3; }
			set { this.dim3 = value; }
		}

		public string Dim4
		{
			get { return this.dim4; }
			set { this.dim4 = value; }
		}		
    }
}
