using System;

namespace C2
{
    public class WhTrans
    {
        private int id;
        private string doNo;
        private string doType;
        private DateTime doDate;
        private string doStatus;
        private string statusCode;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string partyId;
        private string partyName;
        private string partyAdd;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public string DoNo
        {
            get { return this.doNo; }
            set { this.doNo = value; }
        }

        public string DoType
        {
            get { return this.doType; }
            set { this.doType = value; }
        }

        public DateTime DoDate
        {
            get { return this.doDate; }
            set { this.doDate = value; }
        }

        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
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

        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
        }

        public string PartyAdd
        {
            get { return this.partyAdd; }
            set { this.partyAdd = value; }
        }


        public string DoStatus
        {
            get { return this.doStatus; }
            set { this.doStatus = value; }
        }

        private string pic;
        private string currency;
        private decimal exRate;
        private string payTerm;
        private string incoTerm;
        public string Pic
        {
            get { return this.pic; }
            set { this.pic = value; }
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
        public string PayTerm
        {
            get { return this.payTerm; }
            set { this.payTerm = value; }
        }

        public string IncoTerm
        {
            get { return this.incoTerm; }
            set { this.incoTerm = value; }
        }
        private string supplierId;
        public string SupplierId
        {
            get { return this.supplierId; }
            set { this.supplierId = value; }
        }
        private string supplierName;
        public string SupplierName
        {
            get { return this.supplierName; }
            set { this.supplierName = value; }
        }
        private string masterDocNo;
        public string MasterDocNo
        {
            get { return this.masterDocNo; }
            set { this.masterDocNo = value; }
        }
        private string lcNo;
        public string LcNo
        {
            get { return this.lcNo; }
            set { this.lcNo = value; }
        }
        #region no use

        private string wareHouseId;
        private string contractNo;
        private string partyPostalcode;
        private string partyCity;
        private string partyCountry;
        private DateTime partyRefDate;
        private string customsSealNo;
        private string tptMode;
        private DateTime expectedDate;
        private bool palletizedInd;
        private DateTime poDate;
        private string partyInvNo;
        private DateTime partyInvDate;
        private string permitNo;
        private DateTime permitApprovalDate;
        private string tptName;
        private bool populateInd;
        private string groupId;
        private DateTime gateInDate;
        private DateTime gateOutDate;
        private string issueNo;
        private string jobNo;
        private string quotation;
        private string equipNo;
        private string personnel;
        private string remark1;
        private string remark2;
        private string collectFrom;
        private string deliveryTo;
        private string vessel;
        private string voyage;
        private DateTime eta;
        private DateTime etd;
        private DateTime etaDest;
        private string pol;
        private string pod;
        private string obl;
        private string hbl;
        private string vehicle;
        private string coo;
        private string carrier;
        private string agentId;
        private string agentName;
        private string agentAdd;
        private string agentZip;
        private string agentCity;
        private string agentCountry;
        private string agentTel;
        private string agentContact;
        private string notifyId;
        private string notifyName;
        private string notifyAdd;
        private string notifyZip;
        private string notifyCity;
        private string notifyCountry;
        private string notifyTel;
        private string notifyContact;
        private string consigneeId;
        private string consigneeName;
        private string consigneeAdd;
        private string consigneeZip;
        private string consigneeCity;
        private string consigneeCountry;
        private string consigneeTel;
        private string consigneeContact;
        private string remark3;
        private string remark4;
        private string priority;
        private string poNo;
        private string xDockReference;
        private string receiptNo;
        private string route;
        private DateTime customerDate;
        private string customerReference;
        private string externalReference;
        private DateTime externalDate;
        private bool receiptInd;
        private bool xDockInd;



        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }
        public string WareHouseId
        {
            get { return this.wareHouseId; }
            set { this.wareHouseId = value; }
        }



        public string PartyPostalcode
        {
            get { return this.partyPostalcode; }
            set { this.partyPostalcode = value; }
        }

        public string PartyCity
        {
            get { return this.partyCity; }
            set { this.partyCity = value; }
        }

        public string PartyCountry
        {
            get { return this.partyCountry; }
            set { this.partyCountry = value; }
        }

        public DateTime PartyRefDate
        {
            get { return this.partyRefDate; }
            set { this.partyRefDate = value; }
        }

        public string CustomsSealNo
        {
            get { return this.customsSealNo; }
            set { this.customsSealNo = value; }
        }

        public string TptMode
        {
            get { return this.tptMode; }
            set { this.tptMode = value; }
        }

        public DateTime ExpectedDate
        {
            get { return this.expectedDate; }
            set { this.expectedDate = value; }
        }

        public bool PalletizedInd
        {
            get { return this.palletizedInd; }
            set { this.palletizedInd = value; }
        }

        public DateTime PoDate
        {
            get { return this.poDate; }
            set { this.poDate = value; }
        }

        public string PartyInvNo
        {
            get { return this.partyInvNo; }
            set { this.partyInvNo = value; }
        }

        public DateTime PartyInvDate
        {
            get { return this.partyInvDate; }
            set { this.partyInvDate = value; }
        }

        public string PermitNo
        {
            get { return this.permitNo; }
            set { this.permitNo = value; }
        }

        public DateTime PermitApprovalDate
        {
            get { return this.permitApprovalDate; }
            set { this.permitApprovalDate = value; }
        }

        public string TptName
        {
            get { return this.tptName; }
            set { this.tptName = value; }
        }

        public bool PopulateInd
        {
            get { return this.populateInd; }
            set { this.populateInd = value; }
        }

        public string GroupId
        {
            get { return this.groupId; }
            set { this.groupId = value; }
        }

        public DateTime GateInDate
        {
            get { return this.gateInDate; }
            set { this.gateInDate = value; }
        }

        public DateTime GateOutDate
        {
            get { return this.gateOutDate; }
            set { this.gateOutDate = value; }
        }

        public string IssueNo
        {
            get { return this.issueNo; }
            set { this.issueNo = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string Quotation
        {
            get { return this.quotation; }
            set { this.quotation = value; }
        }

        public string EquipNo
        {
            get { return this.equipNo; }
            set { this.equipNo = value; }
        }

        public string Personnel
        {
            get { return this.personnel; }
            set { this.personnel = value; }
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

        public string CollectFrom
        {
            get { return this.collectFrom; }
            set { this.collectFrom = value; }
        }

        public string DeliveryTo
        {
            get { return this.deliveryTo; }
            set { this.deliveryTo = value; }
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

        public DateTime EtaDest
        {
            get { return this.etaDest; }
            set { this.etaDest = value; }
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

        public string Obl
        {
            get { return this.obl; }
            set { this.obl = value; }
        }

        public string Hbl
        {
            get { return this.hbl; }
            set { this.hbl = value; }
        }

        public string Vehicle
        {
            get { return this.vehicle; }
            set { this.vehicle = value; }
        }

        public string Coo
        {
            get { return this.coo; }
            set { this.coo = value; }
        }

        public string Carrier
        {
            get { return this.carrier; }
            set { this.carrier = value; }
        }

        public string AgentId
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }

        public string AgentName
        {
            get { return this.agentName; }
            set { this.agentName = value; }
        }

        public string AgentAdd
        {
            get { return this.agentAdd; }
            set { this.agentAdd = value; }
        }

        public string AgentZip
        {
            get { return this.agentZip; }
            set { this.agentZip = value; }
        }

        public string AgentCity
        {
            get { return this.agentCity; }
            set { this.agentCity = value; }
        }

        public string AgentCountry
        {
            get { return this.agentCountry; }
            set { this.agentCountry = value; }
        }

        public string AgentTel
        {
            get { return this.agentTel; }
            set { this.agentTel = value; }
        }

        public string AgentContact
        {
            get { return this.agentContact; }
            set { this.agentContact = value; }
        }

        public string NotifyId
        {
            get { return this.notifyId; }
            set { this.notifyId = value; }
        }

        public string NotifyName
        {
            get { return this.notifyName; }
            set { this.notifyName = value; }
        }

        public string NotifyAdd
        {
            get { return this.notifyAdd; }
            set { this.notifyAdd = value; }
        }

        public string NotifyZip
        {
            get { return this.notifyZip; }
            set { this.notifyZip = value; }
        }

        public string NotifyCity
        {
            get { return this.notifyCity; }
            set { this.notifyCity = value; }
        }

        public string NotifyCountry
        {
            get { return this.notifyCountry; }
            set { this.notifyCountry = value; }
        }

        public string NotifyTel
        {
            get { return this.notifyTel; }
            set { this.notifyTel = value; }
        }

        public string NotifyContact
        {
            get { return this.notifyContact; }
            set { this.notifyContact = value; }
        }

        public string ConsigneeId
        {
            get { return this.consigneeId; }
            set { this.consigneeId = value; }
        }

        public string ConsigneeName
        {
            get { return this.consigneeName; }
            set { this.consigneeName = value; }
        }

        public string ConsigneeAdd
        {
            get { return this.consigneeAdd; }
            set { this.consigneeAdd = value; }
        }

        public string ConsigneeZip
        {
            get { return this.consigneeZip; }
            set { this.consigneeZip = value; }
        }

        public string ConsigneeCity
        {
            get { return this.consigneeCity; }
            set { this.consigneeCity = value; }
        }

        public string ConsigneeCountry
        {
            get { return this.consigneeCountry; }
            set { this.consigneeCountry = value; }
        }

        public string ConsigneeTel
        {
            get { return this.consigneeTel; }
            set { this.consigneeTel = value; }
        }

        public string ConsigneeContact
        {
            get { return this.consigneeContact; }
            set { this.consigneeContact = value; }
        }

        public string Remark3
        {
            get { return this.remark3; }
            set { this.remark3 = value; }
        }

        public string Remark4
        {
            get { return this.remark4; }
            set { this.remark4 = value; }
        }

        public string Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }

        public string XDockReference
        {
            get { return this.xDockReference; }
            set { this.xDockReference = value; }
        }

        public string ReceiptNo
        {
            get { return this.receiptNo; }
            set { this.receiptNo = value; }
        }

        public string Route
        {
            get { return this.route; }
            set { this.route = value; }
        }

        public DateTime CustomerDate
        {
            get { return this.customerDate; }
            set { this.customerDate = value; }
        }

        public string CustomerReference
        {
            get { return this.customerReference; }
            set { this.customerReference = value; }
        }

        public string ExternalReference
        {
            get { return this.externalReference; }
            set { this.externalReference = value; }
        }

        public DateTime ExternalDate
        {
            get { return this.externalDate; }
            set { this.externalDate = value; }
        }

        public bool ReceiptInd
        {
            get { return this.receiptInd; }
            set { this.receiptInd = value; }
        }

        public bool XDockInd
        {
            get { return this.xDockInd; }
            set { this.xDockInd = value; }
        }
        #endregion
    }
}
