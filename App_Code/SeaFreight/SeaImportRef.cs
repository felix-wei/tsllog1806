using System;

namespace C2
{
    public class SeaImportRef:BaseObject
    {
        private int sequenceId;
        private string refNo;
        private string jobType;
        private string agentId;
        private string crAgentId;
        private string warehouseId;
        private string nvoccAgentId;
        private string nvoccBl;
        private string forwardAgentId;
        private string transportId;
        private string oblNo;
        private string crBkgNo;
        private string vessel;
        private string voyage;
        private DateTime eta;
        private DateTime etd;
        private string pol;
        private string pod;
        private string currencyId;
        private decimal exRate;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string sShipperRemark;
        private string sAgentRemark;
        private string sConsigneeRemark;
        private string sNotifyPartyRemark;
        private string haulierName;
        private string haulierCrNo;
        private string haulierAttention;
        private string haulierCollect;
        private string haulierTruck;
        private DateTime haulierCollectDate;
        private string haulierRemark;
        private string permitRemark;


        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string AgentId
        {
            get { return this.agentId; }
            set { this.agentId = value; }
        }

        public string CrAgentId
        {
            get { return this.crAgentId; }
            set { this.crAgentId = value; }
        }

        public string WarehouseId
        {
            get { return this.warehouseId; }
            set { this.warehouseId = value; }
        }

        public string NvoccAgentId
        {
            get { return this.nvoccAgentId; }
            set { this.nvoccAgentId = value; }
        }

        public string NvoccBl
        {
            get { return this.nvoccBl; }
            set { this.nvoccBl = value; }
        }
        public string ForwardAgentId
        {
            get { return this.forwardAgentId; }
            set { this.forwardAgentId = value; }
        }

        public string TransportId
        {
            get { return this.transportId; }
            set { this.transportId = value; }
        }

        public string OblNo
        {
            get { return this.oblNo; }
            set { this.oblNo = value; }
        }


        public string CrBkgNo
        {
            get { return this.crBkgNo; }
            set { this.crBkgNo = value; }
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

        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
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

        public string SShipperRemark
        {
            get { return this.sShipperRemark; }
            set { this.sShipperRemark = value; }
        }

        public string SAgentRemark
        {
            get { return this.sAgentRemark; }
            set { this.sAgentRemark = value; }
        }

        public string SConsigneeRemark
        {
            get { return this.sConsigneeRemark; }
            set { this.sConsigneeRemark = value; }
        }

        public string SNotifyPartyRemark
        {
            get { return this.sNotifyPartyRemark; }
            set { this.sNotifyPartyRemark = value; }
        }

        public string HaulierName
        {
            get { return this.haulierName; }
            set { this.haulierName = value; }
        }

        public string HaulierCrNo
        {
            get { return this.haulierCrNo; }
            set { this.haulierCrNo = value; }
        }

        public string HaulierAttention
        {
            get { return this.haulierAttention; }
            set { this.haulierAttention = value; }
        }

        public string HaulierCollect
        {
            get { return this.haulierCollect; }
            set { this.haulierCollect = value; }
        }

        public string HaulierTruck
        {
            get { return this.haulierTruck; }
            set { this.haulierTruck = value; }
        }

        public DateTime HaulierCollectDate
        {
            get { return this.haulierCollectDate; }
            set { this.haulierCollectDate = value; }
        }

        public string HaulierRemark
        {
            get { return this.haulierRemark; }
            set { this.haulierRemark = value; }
        }

        public string PermitRemark
        {
            get { return this.permitRemark; }
            set { this.permitRemark = value; }
        }
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private int haulierQty;
        public int HaulierQty
        {
            get { return this.haulierQty; }
            set
            {
                this.haulierQty = value;
            }
        }
        private string haulierPkgType;
        public string HaulierPkgType
        {
            get { return this.haulierPkgType; }
            set
            {
                this.haulierPkgType = value;
            }
        }
        private decimal haulierM3;
        public decimal HaulierM3
        {
            get { return this.haulierM3; }
            set
            {
                this.haulierM3 = value;
            }
        }
        private decimal haulierWt;
        public decimal HaulierWt
        {
            get { return this.haulierWt; }
            set
            {
                this.haulierWt = value;
            }
        }
        private string refType;
        public string RefType
        {
            get { return this.refType; }
            set { this.refType = value; }
        }
        private decimal estSaleAmt;
        public decimal EstSaleAmt
        {
            get { return this.estSaleAmt; }
            set { this.estSaleAmt = value; }
        }
        private decimal estCostAmt;
        public decimal EstCostAmt
        {
            get { return this.estCostAmt; }
            set { this.estCostAmt = value; }
        }
        private DateTime refDate;
        public DateTime RefDate
        {
            get { return this.refDate; }
            set { this.refDate = value; }
        }
        private string localCust;
        public string LocalCust
        {
            get { return this.localCust; }
            set { this.localCust = value; }
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


        //2013-11-23 ADD
        private string haulierFax;
        public string HaulierFax
        {
            get { return this.haulierFax; }
            set { this.haulierFax = value; }
        }
        private string driverName;
        public string DriverName
        {
            get { return this.driverName; }
            set { this.driverName = value; }
        }
        private string driverMobile;
        public string DriverMobile
        {
            get { return this.driverMobile; }
            set { this.driverMobile = value; }
        }
        private string driverLicense;
        public string DriverLicense
        {
            get { return this.driverLicense; }
            set { this.driverLicense = value; }
        }
        private string vehicleNo;
        public string VehicleNo
        {
            get { return this.vehicleNo; }
            set { this.vehicleNo = value; }
        }
        private string vehicleType;
        public string VehicleType
        {
            get { return this.vehicleType; }
            set { this.vehicleType = value; }
        }
        private string driverRemark;
        public string DriverRemark
        {
            get { return this.driverRemark; }
            set { this.driverRemark = value; }
        }
        private string haulierCollectTime;
        public string HaulierCollectTime
        {
            get { return this.haulierCollectTime; }
            set { this.haulierCollectTime = value; }
        }
        private string haulierDeliveryTime;
        public string HaulierDeliveryTime
        {
            get { return this.haulierDeliveryTime; }
            set { this.haulierDeliveryTime = value; }
        }
        private string haulierSendTo;
        public string HaulierSendTo
        {
            get { return this.haulierSendTo; }
            set { this.haulierSendTo = value; }
        }
        private string haulierStuffBy;
        public string HaulierStuffBy
        {
            get { return this.haulierStuffBy; }
            set { this.haulierStuffBy = value; }
        }
        private string haulierCoload;
        public string HaulierCoload
        {
            get { return this.haulierCoload; }
            set { this.haulierCoload = value; }
        }
        private string haulierPerson;
        public string HaulierPerson
        {
            get { return this.haulierPerson; }
            set { this.haulierPerson = value; }
        }
        private string haulierPersonTel;
        public string HaulierPersonTel
        {
            get { return this.haulierPersonTel; }
            set { this.haulierPersonTel = value; }
        }
        private DateTime haulierDeliveryDate;
        public DateTime HaulierDeliveryDate
        {
            get { return this.haulierDeliveryDate; }
            set { this.haulierDeliveryDate = value; }
        }



        //new
        private string poNo;
        public string PoNo {
            get { return this.poNo;}
            set { this.poNo = value; }
        }
    }
}
