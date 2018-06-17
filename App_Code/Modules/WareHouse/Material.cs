using System;

namespace C2
{
    public class Material
    {
        private int id;
        private string description;
        private string unit;
		private string code;
        private int requisitionNew;
        private int requisitionUsed;
        private int requisitionNew1;
        private int requisitionUsed1;
        private int requisitionNew2;
        private int requisitionUsed2;
        private int additionalNew;
        private int additionalUsed;
        private int additionalNew1;
        private int additionalUsed1;
        private int additionalNew2;
        private int additionalUsed2;
        private int returnedNew;
        private int returnedUsed;
        private int totalNew;
        private int totalUsed;

        public int Id
        {
            get { return this.id; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public int RequisitionNew
        {
            get { return this.requisitionNew; }
            set { this.requisitionNew = value; }
        }

        public int RequisitionUsed
        {
            get { return this.requisitionUsed; }
            set { this.requisitionUsed = value; }
        }
        public int RequisitionNew1
        {
            get { return this.requisitionNew1; }
            set { this.requisitionNew1 = value; }
        }

        public int RequisitionUsed1
        {
            get { return this.requisitionUsed1; }
            set { this.requisitionUsed1 = value; }
        }
        public int RequisitionNew2
        {
            get { return this.requisitionNew2; }
            set { this.requisitionNew2 = value; }
        }

        public int RequisitionUsed2
        {
            get { return this.requisitionUsed2; }
            set { this.requisitionUsed2 = value; }
        }
        public int AdditionalNew
        {
            get { return this.additionalNew; }
            set { this.additionalNew = value; }
        }

        public int AdditionalUsed
        {
            get { return this.additionalUsed; }
            set { this.additionalUsed = value; }
        }

        public int AdditionalNew1
        {
            get { return this.additionalNew1; }
            set { this.additionalNew1 = value; }
        }

        public int AdditionalUsed1
        {
            get { return this.additionalUsed1; }
            set { this.additionalUsed1 = value; }
        }

		
		        public int AdditionalNew2
        {
            get { return this.additionalNew2; }
            set { this.additionalNew2 = value; }
        }

        public int AdditionalUsed2
        {
            get { return this.additionalUsed2; }
            set { this.additionalUsed2 = value; }
        }

		
        public int ReturnedNew
        {
            get { return this.returnedNew; }
            set { this.returnedNew = value; }
        }

        public int ReturnedUsed
        {
            get { return this.returnedUsed; }
            set { this.returnedUsed = value; }
        }

        public int TotalNew
        {
            get { return this.totalNew; }
            set { this.totalNew = value; }
        }

        public int TotalUsed
        {
            get { return this.totalUsed; }
            set { this.totalUsed = value; }
        }
		private string refNo;
        public string RefNo 
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
		public string JobStatus
        {
            get
            {
                string s = "USE";
                string sql = "select JobStatus from JobInfo where JobNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
		private string doType;
		public string DoType{
            get { return this.doType; }
            set { this.doType = value; }
        }
		
		
        private string docOwner;
        private string docType;
        private string docNo;
        private string billNo;
        private string billTerm;
        private string poNo;
        private string docStatus;
        private DateTime docDate;
        private string partyTo;
        private string partyAddress;

        private decimal qty;
        private decimal price;
        private string gstType;
        private decimal gstAmt;//gstAmt=amt*gst  2 decimal
        private decimal docAmt;//docAmt=amt+gstAmt  2 decimal

        private string actionType;
        private string actionUser;
        private DateTime actionTime;
		
        public string DocOwner
        {
            get { return this.docOwner; }
            set { this.docOwner = value; }
        }
        public string DocType
        {
            get { return this.docType; }
            set { this.docType = value; }
        }
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        public string BillNo
        {
            get { return this.billNo; }
            set { this.billNo = value; }
        }

        public string BillTerm
        {
            get { return this.billTerm; }
            set { this.billTerm = value; }
        }
        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }
        
		public string DocStatus
        {
            get { return this.docStatus; }
            set { this.docStatus = value; }
        }
        public DateTime DocDate
        {
            get { return this.docDate; }
            set { this.docDate = value; }
        }

        public string PartyTo
        {
            get { return this.partyTo; }
            set { this.partyTo = value; }
        }
        public string PartyName
        {
            get {
                string name = "";
                if (SafeValue.SafeString(partyTo).Length>0)
                {
                        name = EzshipHelper.GetPartyName(partyTo);
                }
                else
                        name = EzshipHelper.GetPartyName(partyTo);
                 
              
                return name; }
        }
        public string PartyAddress
        {
            get { return this.partyAddress; }
            set { this.partyAddress = value; }
        }
		
		
        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal QtyIn
        {
            get { return (this.doType=="IN3" || this.doType=="IN") ? this.qty : 0 ; }
        }

        public decimal QtyOut
        {
            get { return (this.doType=="OUT3" || this.doType=="OUT") ? this.qty : 0 ; }
        }
		

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
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

		public string ActionType
        {
            get { return this.actionType; }
            set { this.actionType = value; }
        }

		public string ActionUser
        {
            get { return this.actionUser; }
            set { this.actionUser = value; }
        }

        public DateTime ActionTime
        {
            get { return this.actionTime; }
            set { this.actionTime = value; }
        }
		
    }
}
