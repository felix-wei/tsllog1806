using System;

namespace C2
{
    public class XAJournalEntryDet
	{
		private int sequenceId;
		private int glNo;
		private int glLineNo;
		private int acYear;
		private int acPeriod;
		private string acCode;
		private string acSource;
		private string docNo;
		private string docType;
		private string currencyId;
		private decimal exRate;
		private decimal crAmt;
		private decimal dbAmt;
		private decimal currencyCrAmt;
		private decimal currencyDbAmt;
		private string remark;
        private string arApInd;
        public string ArApInd
        {
            get { return this.arApInd; }
            set { this.arApInd = value; }
        }

		public int SequenceId
		{
			get { return this.sequenceId; }
		}
        public string Display
        {
            get
            {
                string flag = "block";
                if (this.glLineNo > 0)
                {
                    string sql = "select PostInd from XAJournalEntry where SequenceId='" + glNo + "'";
                    flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                    if (flag == "Y")
                        flag = "none";
                    else
                    {
                        sql = "select CancelInd from XAJournalEntry where SequenceId='" + glNo + "'";
                        flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                        if (flag == "Y")
                            flag = "none";
                    }
                }
                return flag;
            }
        }
		public int GlNo
		{
			get { return this.glNo; }
			set { this.glNo = value; }
		}

		public int GlLineNo
		{
			get { return this.glLineNo; }
			set { this.glLineNo = value; }
		}

		public int AcYear
		{
			get { return this.acYear; }
			set { this.acYear = value; }
		}

		public int AcPeriod
		{
			get { return this.acPeriod; }
			set { this.acPeriod = value; }
		}

		public string AcCode
		{
			get { return this.acCode; }
			set { this.acCode = value; }
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

		public string DocType
		{
			get { return this.docType; }
			set { this.docType = value; }
		}

		public string CurrencyId
		{
			get { return this.currencyId; }
			set { this.currencyId = value; }
		}

		public decimal ExRate
		{
			get { return this.exRate; }
			set { this.exRate = value; }
		}

		public decimal CrAmt
		{
			get { return this.crAmt; }
			set { this.crAmt = value; }
		}

		public decimal DbAmt
		{
			get { return this.dbAmt; }
			set { this.dbAmt = value; }
		}

		public decimal CurrencyCrAmt
		{
			get { return this.currencyCrAmt; }
			set { this.currencyCrAmt = value; }
		}

		public decimal CurrencyDbAmt
		{
			get { return this.currencyDbAmt; }
			set { this.currencyDbAmt = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
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
