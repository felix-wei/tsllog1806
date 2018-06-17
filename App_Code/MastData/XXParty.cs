using System;

namespace C2
{
    public class XXParty
    {
        private int sequenceId;
        private string partyId;
        private string code;
        private string name;
        private bool isCustomer;
        private bool isVendor;
        private bool isAgent;
        private string status;
        private string address;
        private string address1;
        private string tel1;
        private string tel2;
        private string fax1;
        private string fax2;
        private string email1;
        private string email2;
        private string contact1;
        private string contact2;
        private string countryId;
        private string salesmanId;
        private string portId;
        private string remark;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string termId;
        private string groupId;
        private string crNo;



        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool IsCustomer
        {
            get { return this.isCustomer; }
            set { this.isCustomer = value; }
        }

        public bool IsVendor
        {
            get { return this.isVendor; }
            set { this.isVendor = value; }
        }

        public bool IsAgent
        {
            get { return this.isAgent; }
            set { this.isAgent = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        public string Address1
        {
            get { return this.address1; }
            set { this.address1 = value; }
        }
        public string Tel1
        {
            get { return this.tel1; }
            set { this.tel1 = value; }
        }

        public string Tel2
        {
            get { return this.tel2; }
            set { this.tel2 = value; }
        }

        public string Fax1
        {
            get { return this.fax1; }
            set { this.fax1 = value; }
        }

        public string Fax2
        {
            get { return this.fax2; }
            set { this.fax2 = value; }
        }

        public string Email1
        {
            get { return this.email1; }
            set { this.email1 = value; }
        }

        public string Email2
        {
            get { return this.email2; }
            set { this.email2 = value; }
        }

        public string Contact1
        {
            get { return this.contact1; }
            set { this.contact1 = value; }
        }

        public string Contact2
        {
            get { return this.contact2; }
            set { this.contact2 = value; }
        }

        public string CountryId
        {
            get { return this.countryId; }
            set { this.countryId = value; }
        }

        public string SalesmanId
        {
            get { return this.salesmanId; }
            set { this.salesmanId = value; }
        }

        public string PortId
        {
            get { return this.portId; }
            set { this.portId = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

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
        public string TermId
        {
            get { return this.termId; }
            set { this.termId = value; }
        }

        public string GroupId
        {
            get { return this.groupId; }
            set { this.groupId = value; }
        }
        public string CrNo
        {
            get { return this.crNo; }
            set { this.crNo = value; }
        }
        private string carrierCode;
        public string CarrierCode
        {
            get { return this.carrierCode; }
            set { this.carrierCode = value; }
        }


        private decimal warningAmt;
        private int warningQty;
        private decimal blockAmt;
        private int blockQty;

        public decimal WarningAmt
        {
            get { return this.warningAmt; }
            set { this.warningAmt = value; }
        }

        public int WarningQty
        {
            get { return this.warningQty; }
            set { this.warningQty = value; }
        }

        public decimal BlockAmt
        {
            get { return this.blockAmt; }
            set { this.blockAmt = value; }
        }

        public int BlockQty
        {
            get { return this.blockQty; }
            set { this.blockQty = value; }
        }

        //2013-12-26 10:50:52
        private string zipCode;
        public string ZipCode
        {
            get { return this.zipCode; }
            set { this.zipCode = value; }
        }
        private string city;
        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }


        private string clientType;
        public string ClientType
        {
            get { return this.clientType; }
            set { this.clientType = value; }
        }
        private string parentCode;
        public string ParentCode
        {
            get { return this.parentCode; }
            set { this.parentCode = value; }
        }
        private string billingAlert;
        public string BillingAlert
        {
            get { return this.billingAlert; }
            set { this.billingAlert = value; }
        }

        private string loginInd;
        public string LoginInd
        {
            get { return this.loginInd; }
            set { this.loginInd = value; }
        }


        private string loginCode;
        public string LoginCode
        {
            get { return this.loginCode; }
            set { this.loginCode = value; }
        }
    }
}
