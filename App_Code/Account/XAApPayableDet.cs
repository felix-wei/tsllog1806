using System;

namespace C2
{
	public class XAApPayableDet
	{
		private int sequenceId;
		private int docId;
		private int docLineNo;
		private string acCode;
		private string acSource;
		private string chgCode;
		private string chgDes1;
		private string chgDes2;
		private string chgDes3;
		private string chgDes4;
		private string gstType;
		private decimal qty;
		private decimal price;
		private string unit;
		private string currency;
		private decimal exRate;
		private decimal gst;
		private decimal gstAmt;
		private decimal docAmt;
		private decimal locAmt;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}
        public string Display
        {
            get
            {
                string flag = "block";
                if (this.docLineNo > 0)
                {
                    string sql = "select ExportInd from XAApPayable where SequenceId='" + docId + "'";
                    flag = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                    if (flag == "Y")
                        flag = "none";
                    else
                    {
                        sql = "select DocDate from XAApPayable where SequenceId='" + docId + "'";
                        DateTime docDate = SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql), DateTime.Today);
                        string user = System.Web.HttpContext.Current.User.Identity.Name;
                        string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("SELECT Role FROM [User] where Name='" + user + "'"), "Staff").ToLower();
                        if (role.IndexOf("staff") != -1)
                        {

                            int day = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountModifyDay"], 15);
                            if ((DateTime.Today - docDate).TotalDays > day)
                            {
                                flag = "none";
                            }
                        }
                    }
                }
                return flag;
            }
        }
        public string CancelInd
        {
            get {
                string sql = "select CancelInd from XAApPayable where SequenceId='" + docId + "'";
                string res = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                if (res.ToUpper() == "Y")
                {
                    return "none";
                }
                return "block";
            }
        }
		public int DocId
		{
			get { return this.docId; }
			set { this.docId = value; }
		}

		public int DocLineNo
		{
			get { return this.docLineNo; }
			set { this.docLineNo = value; }
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

		public string ChgCode
		{
			get { return this.chgCode; }
			set { this.chgCode = value; }
		}

		public string ChgDes1
		{
			get { return this.chgDes1; }
			set { this.chgDes1 = value; }
		}

		public string ChgDes2
		{
			get { return this.chgDes2; }
			set { this.chgDes2 = value; }
		}

		public string ChgDes3
		{
			get { return this.chgDes3; }
			set { this.chgDes3 = value; }
		}

		public string ChgDes4
		{
			get { return this.chgDes4; }
			set { this.chgDes4 = value; }
		}

		public string GstType
		{
			get { return this.gstType; }
			set { this.gstType = value; }
		}

		public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		public string Unit
		{
			get { return this.unit; }
			set { this.unit = value; }
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

		public decimal Gst
		{
			get { return this.gst; }
			set { this.gst = value; }
		}

		public decimal GstAmt
		{
			get { return this.gstAmt; }
			set { this.gstAmt = value; }
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

        public decimal DocAmt1
        {
            get
            {
                if ((this.docType == "VO" || this.docType == "PL" || this.docType == "SD") && this.acSource == "DB")
                    return this.docAmt;
                else if (this.docType == "SC" && this.acSource == "CR")
                    return this.docAmt;
                else return -this.docAmt;
            }
        }
        public decimal LocAmt1
        {
            get
            {
                if ((this.docType=="VO"||this.docType=="PL"||this.docType=="SD")&&this.acSource == "DB")
                    return this.locAmt;
                else if (this.docType=="SC"&&this.acSource=="CR")
                    return this.locAmt;
                else return -this.locAmt;
            }
        }
        private string docNo;
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        private string docType;
        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }
        private decimal lineLocAmt;
        public decimal LineLocAmt
        {
            get { return this.lineLocAmt; }
            set { this.lineLocAmt = value; }
        }
		
	    private string splitType;
		public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
        }
        private int polineId;
        public int POlineId
        {
            get { return this.polineId; }
            set { this.polineId = value; }
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
