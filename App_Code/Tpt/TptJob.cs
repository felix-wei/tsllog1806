using System;

namespace C2
{
    public class TptJob
    {
        private int id;
        private string jobNo;
        private string jobType;
        private string closeInd;
        private string jobProgress;
        private DateTime jobDate;
        private string jobPic;
        private string jobLevel;
        private string jobRmk;
        private string cust;
        private string custRef;
        private decimal wt;
        private decimal m3;
        private int qty;
        private string pkgType;
        private string blRef;
        private string bkgRef;
        private string vessel;
        private string voyage;
        private string pol;
        private string pod;
        private string finDest;
        private DateTime eta;
        private DateTime etd;
        private string driver;
        private string term;
        private string pickFrm1;
        private string pickFrm2;
        private string deliveryTo1;
        private string deliveryTo2;
        private string invoiceNo;
        private string cnNo;
        private string voucherNo;
        private string masterRef;
        private string houseRef;
        private string jobCode;
        private string cargoMkg;
        private string cargoDesc;
        private int sortIndex;

        public string CustName
        {
            get
            {
                return EzshipHelper.GetPartyName(this.cust);
            }
        }
        public int Id
        {
            get { return this.id; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string CloseInd
        {
            get { return this.closeInd; }
            set { this.closeInd = value; }
        }

        public string JobProgress
        {
            get { return this.jobProgress; }
            set { this.jobProgress = value; }
        }

        public DateTime JobDate
        {
            get { return this.jobDate; }
            set { this.jobDate = value; }
        }

        public string JobPic
        {
            get { return this.jobPic; }
            set { this.jobPic = value; }
        }

        public string JobLevel
        {
            get { return this.jobLevel; }
            set { this.jobLevel = value; }
        }

        public string JobRmk
        {
            get { return this.jobRmk; }
            set { this.jobRmk = value; }
        }

        public string Cust
        {
            get { return this.cust; }
            set { this.cust = value; }
        }

        public string CustRef
        {
            get { return this.custRef; }
            set { this.custRef = value; }
        }

        public decimal Wt
        {
            get { return this.wt; }
            set { this.wt = value; }
        }

        public decimal M3
        {
            get { return this.m3; }
            set { this.m3 = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string PkgType
        {
            get { return this.pkgType; }
            set { this.pkgType = value; }
        }

        public string BlRef
        {
            get { return this.blRef; }
            set { this.blRef = value; }
        }

        public string BkgRef
        {
            get { return this.bkgRef; }
            set { this.bkgRef = value; }
        }

        public string Vessel
        {
            get { return this.vessel; }
            set { this.vessel = value; }
        }

        public string Voyage
        {
            get { return this.voyage; }
            set { this.voyage = value; }
        }

        public string Pol
        {
            get { return this.pol; }
            set { this.pol = value; }
        }

        public string Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }

        public string FinDest
        {
            get { return this.finDest; }
            set { this.finDest = value; }
        }

        public DateTime Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }

        public DateTime Etd
        {
            get { return this.etd; }
            set { this.etd = value; }
        }

        public string Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        public string Term
        {
            get { return this.term; }
            set { this.term = value; }
        }

        public string PickFrm1
        {
            get { return this.pickFrm1; }
            set { this.pickFrm1 = value; }
        }

        public string PickFrm2
        {
            get { return this.pickFrm2; }
            set { this.pickFrm2 = value; }
        }

        public string DeliveryTo1
        {
            get { return this.deliveryTo1; }
            set { this.deliveryTo1 = value; }
        }

        public string DeliveryTo2
        {
            get { return this.deliveryTo2; }
            set { this.deliveryTo2 = value; }
        }

        public string InvoiceNo
        {
            get { return this.invoiceNo; }
            set { this.invoiceNo = value; }
        }

        public string CnNo
        {
            get { return this.cnNo; }
            set { this.cnNo = value; }
        }

        public string VoucherNo
        {
            get { return this.voucherNo; }
            set { this.voucherNo = value; }
        }

        public string MasterRef
        {
            get { return this.masterRef; }
            set { this.masterRef = value; }
        }

        public string HouseRef
        {
            get { return this.houseRef; }
            set { this.houseRef = value; }
        }

        public string JobCode
        {
            get { return this.jobCode; }
            set { this.jobCode = value; }
        }
        private string jobCodeRmk;
        public string JobCodeRmk
        {
            get { return this.jobCodeRmk; }
            set { this.jobCodeRmk = value; }
        }

        public string CargoMkg
        {
            get { return this.cargoMkg; }
            set { this.cargoMkg = value; }
        }

        public string CargoDesc
        {
            get { return this.cargoDesc; }
            set { this.cargoDesc = value; }
        }

        public int SortIndex
        {
            get { return this.sortIndex; }
            set { this.sortIndex = value; }
        }
        private string userId;
		public string UserId
		{
			get { return this.userId; }
			set { this.userId = value; }
		}
        private DateTime entryDate;
        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }
        private string vehicleType;
        public string VehicleType
        {
            get { return this.vehicleType; }
            set { this.vehicleType = value; }
        }
        private string soKey;
        public string SoKey
        {
            get { return this.soKey; }
            set { this.soKey = value; }
        }
        private DateTime arriveDate;
        public DateTime ArriveDate
        {
            get { return this.arriveDate; }
            set { this.arriveDate = value; }
        }
        private string vehicleNo;
        public string VehicleNo
        {
            get { return this.vehicleNo; }
            set { this.vehicleNo = value; }
        }

        private string statusCode;
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
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



        private string tptType;
        private DateTime tptDate;
        private string tptTime;
        private string custPic;
        private string custContact;
        private string custEmail;
        private string custDocNo;
        private string custDocType;
        private DateTime bkgDate;
        private string bkgTime;
        private decimal bkgWt;
        private decimal bkgM3;
        private int bkgQty;
        private string bkgPkgType;
        private decimal feeTpt;
        private decimal feeLabour;
        private decimal feeOt;
        private decimal feeAdmin;
        private decimal feeReimberse;
        private decimal feeMisc;
        private decimal feeTotal;
        private string feeRemark;
        private string tripCode;

        public string TptType
        {
            get { return this.tptType; }
            set { this.tptType = value; }
        }

        public DateTime TptDate
        {
            get { return this.tptDate; }
            set { this.tptDate = value; }
        }

        public string TptTime
        {
            get { return this.tptTime; }
            set { this.tptTime = value; }
        }

        public string CustPic
        {
            get { return this.custPic; }
            set { this.custPic = value; }
        }

        public string CustContact
        {
            get { return this.custContact; }
            set { this.custContact = value; }
        }

        public string CustEmail
        {
            get { return this.custEmail; }
            set { this.custEmail = value; }
        }

        public string CustDocNo
        {
            get { return this.custDocNo; }
            set { this.custDocNo = value; }
        }

        public string CustDocType
        {
            get { return this.custDocType; }
            set { this.custDocType = value; }
        }

        public DateTime BkgDate
        {
            get { return this.bkgDate; }
            set { this.bkgDate = value; }
        }

        public string BkgTime
        {
            get { return this.bkgTime; }
            set { this.bkgTime = value; }
        }

        public decimal BkgWt
        {
            get { return this.bkgWt; }
            set { this.bkgWt = value; }
        }

        public decimal BkgM3
        {
            get { return this.bkgM3; }
            set { this.bkgM3 = value; }
        }

        public int BkgQty
        {
            get { return this.bkgQty; }
            set { this.bkgQty = value; }
        }

        public string BkgPkgType
        {
            get { return this.bkgPkgType; }
            set { this.bkgPkgType = value; }
        }

        public decimal FeeTpt
        {
            get { return this.feeTpt; }
            set { this.feeTpt = value; }
        }

        public decimal FeeLabour
        {
            get { return this.feeLabour; }
            set { this.feeLabour = value; }
        }

        public decimal FeeOt
        {
            get { return this.feeOt; }
            set { this.feeOt = value; }
        }

        public decimal FeeAdmin
        {
            get { return this.feeAdmin; }
            set { this.feeAdmin = value; }
        }

        public decimal FeeReimberse
        {
            get { return this.feeReimberse; }
            set { this.feeReimberse = value; }
        }

        public decimal FeeMisc
        {
            get { return this.feeMisc; }
            set { this.feeMisc = value; }
        }

        public decimal FeeTotal
        {
            get { return this.feeTotal; }
            set { this.feeTotal = value; }
        }

        public string FeeRemark
        {
            get { return this.feeRemark; }
            set { this.feeRemark = value; }
        }
        public string TripCode
        {
            get { return this.tripCode; }
            set { this.tripCode = value; }
        }
    }
}
