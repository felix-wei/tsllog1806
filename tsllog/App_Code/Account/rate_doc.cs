using System;

namespace C2
{
    public class rate_doc
    {
        private int id;
        private int acYear;
        private int acPeriod;
        private string acCode;
        private string acSource;
        private string docType;
        private string docNo;
        private DateTime docDate;
        private DateTime docDueDate;
        private string partyTo;
        private string mastRefNo;
        private string jobRefNo;
        private string mastType;
        private string currencyId;
        private decimal exRate;
        private string term;
        private string description;
        private decimal docAmt;//
        private decimal locAmt;//=docamt*exrate
        private decimal balanceAmt;
        private string exportInd;
        private string cancelInd;
        private DateTime cancelDate;
        private string userId;
        private DateTime entryDate;

        public int Id
        {
            get { return this.id; }
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

        public DateTime DocDate
        {
            get { return this.docDate; }
            set { this.docDate = value; }
        }

        public DateTime DocDueDate
        {
            get { return this.docDueDate; }
            set { this.docDueDate = value; }
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

        public string CancelInd
        {
            get { return this.cancelInd; }
            set { this.cancelInd = value; }
        }

        public DateTime CancelDate
        {
            get { return this.cancelDate; }
            set { this.cancelDate = value; }
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

        public string Term
        {
            get { return this.term; }
            set { this.term = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }


        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { this.entryDate = value; }
        }

        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
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

        public decimal LocAmt1
        {
            get { if (this.docType == "SC") return -this.locAmt; else return this.locAmt; }
        }

        public decimal BalanceAmt
        {
            get { return this.balanceAmt; }
            set { this.balanceAmt = value; }
        }

        public string ExportInd
        {
            get { return this.exportInd; }
            set { this.exportInd = value; }
        }
        public string ExportIndStr
        {
            get
            {
                if (this.exportInd != "Y")
                {
                    string user = System.Web.HttpContext.Current.User.Identity.Name;
                    string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("SELECT Role FROM [User] where Name='" + user + "'"), "Staff").ToLower();
                    if (role.IndexOf("staff") != -1)
                    {

                        int day = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountModifyDay"], 15);
                        if ((DateTime.Today - this.docDate).TotalDays > day)
                        {
                            return "Y";
                        }
                    }
                    else return "N";
                }
                return this.exportInd;
            }
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





        private string pol;
        public string Pol
        {
            get { return this.pol; }
            set { this.pol = value; }
        }
        private string pod;
        public string Pod
        {
            get { return this.pod; }
            set { this.pod = value; }
        }
        private string pic;
        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
        }
        private DateTime eta;
        public DateTime Eta
        {
            get { return this.eta; }
            set { this.eta = value; }
        }
        private string vessel;
        public string Vessel
        {
            get { return this.vessel; }
            set { this.vessel = value; }
        }
        private string voyage;
        public string Voyage
        {
            get { return this.voyage; }
            set { this.voyage = value; }
        }
        private string refNo;
        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
        private string bkgRefNo;
        public string BkgRefNo
        {
            get { return this.bkgRefNo; }
            set { this.bkgRefNo = value; }
        }
        private string custRef;
        public string CustRef
        {
            get { return this.custRef; }
            set { this.custRef = value; }
        }
        private string specialNote;
        public string SpecialNote
        {
            get { return this.specialNote; }
            set { this.specialNote = value; }
        }
        //add 2014-3-25
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
    }
}
