using System;

namespace C2
{
	public class JobOrder
	{
		private int sequenceId;
		private string refNo;
		private string containerNo;
		private string jobNo;
		private string createBy;
		private DateTime createDateTime;
		private string hblNo;
		private string customerId;
		private decimal weight;
		private decimal volume;
		private int qty;
		private string packageType;
		private string frtTerm;
		private string pol;
		private string pod;
		private string placeDeliveryId;
		private string placeDeliveryname;
		private string placeReceiveId;
		private string placeReceiveName;
		private string placeDeliveryTerm;
		private string placeReceiveTerm;
		private string preCarriage;
		private string shipOnBoardInd;
		private DateTime shipOnBoardDate;
		private string shipLoadInd;
		private string expressBl;
		private string tsInd;
		private string tsJobNo;
		private string permitRmk;
		private string sShipperRemark;
		private string sAgentRemark;
		private string sConsigneeRemark;
		private string sNotifyPartyRemark;
		private decimal impCharge;
		private string haulierName;
		private string haulierCrNo;
		private string haulierAttention;
		private string haulierCollect;
		private string haulierTruck;
		private DateTime haulierCollectDate;
		private string haulierRemark;
		private string haulierPkgType;
		private int haulierQty;
		private decimal haulierWt;
		private decimal haulierM3;
		private string placeLoadingName;
		private string placeDischargeName;
		private string clauseFreightTerm;
		private string clauseBL;
		private string clauseCargo;
		private string surrenderBl;
		private string bkgRefNo;
		private string finDest;
		private string shipperId;
		private string shipperName;
		private string shipperContact;
		private string shipperTel;
		private string shipperFax;
		private string shipperEmail;
		private string asAgent;
		private string remark;
		private string marking;
		private string collectFrom;
		private string cancelInd;
		private string cancelRmk;
		private string cancelUser;
		private DateTime cancelDate;
		private string statusCode;
		private string updateBy;
		private DateTime updateDateTime;
		private string quoteNo;
		private string poNo;
		private string valueCurrency;
		private decimal valueExRate;
		private decimal valueAmt;
		private string driverName;
		private string driverMobile;
		private string driverLicense;
		private string driverRemark;
		private string vehicleNo;
		private string vehicleType;
		private DateTime pODTime;
		private string pODBy;
		private string pODRemark;
		private string pODUpdateUser;
		private DateTime pODUpdateTime;
		private string note1;
		private string note2;
		private string note3;
		private string note4;
		private string note5;
		private string note6;
		private string note7;
		private string note8;
		private string note9;
		private string note10;
		private string note11;
		private string note12;
		private string note13;
		private string note14;
		private string note15;
		private string note16;
		private string note17;
		private string note18;
		private string note19;
		private string note20;
		private DateTime haulierDeliveryDate;
		private string haulierSendTo;
		private string haulierStuffBy;
		private string haulierCoload;
		private string haulierPerson;
		private string haulierPersonTel;
		private string dept;
		private string haulierCollectTime;
		private string haulierDeliveryTime;
		private string ediRefNo;
		private string ediJobNo;
		private string custRefNo;
		private string serviceType;
		private string sku;
		private string cargoClass;
		private string totMkg;
		private string totDe;
		private decimal volumeInv;
		private string insuranceVendor;
		private decimal weightInv;
		private string carrier;
		private string vessel;
		private string voyage;
		private DateTime eta;
		private DateTime etd;
		private DateTime etaDest;
		private string carrierId;
        private string warehouseId;
        private string agentId;
        private DateTime jobDate;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}

		public string RefNo
		{
			get { return this.refNo; }
			set { this.refNo = value; }
		}

		public string ContainerNo
		{
			get { return this.containerNo; }
			set { this.containerNo = value; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
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

		public string HblNo
		{
			get { return this.hblNo; }
			set { this.hblNo = value; }
		}

		public string CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
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

		public string FrtTerm
		{
			get { return this.frtTerm; }
			set { this.frtTerm = value; }
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

		public string PlaceDeliveryId
		{
			get { return this.placeDeliveryId; }
			set { this.placeDeliveryId = value; }
		}

		public string PlaceDeliveryname
		{
			get { return this.placeDeliveryname; }
			set { this.placeDeliveryname = value; }
		}

		public string PlaceReceiveId
		{
			get { return this.placeReceiveId; }
			set { this.placeReceiveId = value; }
		}

		public string PlaceReceiveName
		{
			get { return this.placeReceiveName; }
			set { this.placeReceiveName = value; }
		}

		public string PlaceDeliveryTerm
		{
			get { return this.placeDeliveryTerm; }
			set { this.placeDeliveryTerm = value; }
		}

		public string PlaceReceiveTerm
		{
			get { return this.placeReceiveTerm; }
			set { this.placeReceiveTerm = value; }
		}

		public string PreCarriage
		{
			get { return this.preCarriage; }
			set { this.preCarriage = value; }
		}

		public string ShipOnBoardInd
		{
			get { return this.shipOnBoardInd; }
			set { this.shipOnBoardInd = value; }
		}

		public DateTime ShipOnBoardDate
		{
			get { return this.shipOnBoardDate; }
			set { this.shipOnBoardDate = value; }
		}

		public string ShipLoadInd
		{
			get { return this.shipLoadInd; }
			set { this.shipLoadInd = value; }
		}

		public string ExpressBl
		{
			get { return this.expressBl; }
			set { this.expressBl = value; }
		}

		public string TsInd
		{
			get { return this.tsInd; }
			set { this.tsInd = value; }
		}

		public string TsJobNo
		{
			get { return this.tsJobNo; }
			set { this.tsJobNo = value; }
		}

		public string PermitRmk
		{
			get { return this.permitRmk; }
			set { this.permitRmk = value; }
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

		public decimal ImpCharge
		{
			get { return this.impCharge; }
			set { this.impCharge = value; }
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

		public string HaulierPkgType
		{
			get { return this.haulierPkgType; }
			set { this.haulierPkgType = value; }
		}

		public int HaulierQty
		{
			get { return this.haulierQty; }
			set { this.haulierQty = value; }
		}

		public decimal HaulierWt
		{
			get { return this.haulierWt; }
			set { this.haulierWt = value; }
		}

		public decimal HaulierM3
		{
			get { return this.haulierM3; }
			set { this.haulierM3 = value; }
		}

		public string PlaceLoadingName
		{
			get { return this.placeLoadingName; }
			set { this.placeLoadingName = value; }
		}

		public string PlaceDischargeName
		{
			get { return this.placeDischargeName; }
			set { this.placeDischargeName = value; }
		}

		public string ClauseFreightTerm
		{
			get { return this.clauseFreightTerm; }
			set { this.clauseFreightTerm = value; }
		}

		public string ClauseBL
		{
			get { return this.clauseBL; }
			set { this.clauseBL = value; }
		}

		public string ClauseCargo
		{
			get { return this.clauseCargo; }
			set { this.clauseCargo = value; }
		}

		public string SurrenderBl
		{
			get { return this.surrenderBl; }
			set { this.surrenderBl = value; }
		}

		public string BkgRefNo
		{
			get { return this.bkgRefNo; }
			set { this.bkgRefNo = value; }
		}

		public string FinDest
		{
			get { return this.finDest; }
			set { this.finDest = value; }
		}

		public string ShipperId
		{
			get { return this.shipperId; }
			set { this.shipperId = value; }
		}

		public string ShipperName
		{
			get { return this.shipperName; }
			set { this.shipperName = value; }
		}

		public string ShipperContact
		{
			get { return this.shipperContact; }
			set { this.shipperContact = value; }
		}

		public string ShipperTel
		{
			get { return this.shipperTel; }
			set { this.shipperTel = value; }
		}

		public string ShipperFax
		{
			get { return this.shipperFax; }
			set { this.shipperFax = value; }
		}

		public string ShipperEmail
		{
			get { return this.shipperEmail; }
			set { this.shipperEmail = value; }
		}

		public string AsAgent
		{
			get { return this.asAgent; }
			set { this.asAgent = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

		public string Marking
		{
			get { return this.marking; }
			set { this.marking = value; }
		}

		public string CollectFrom
		{
			get { return this.collectFrom; }
			set { this.collectFrom = value; }
		}

		public string CancelInd
		{
			get { return this.cancelInd; }
			set { this.cancelInd = value; }
		}

		public string CancelRmk
		{
			get { return this.cancelRmk; }
			set { this.cancelRmk = value; }
		}

		public string CancelUser
		{
			get { return this.cancelUser; }
			set { this.cancelUser = value; }
		}

		public DateTime CancelDate
		{
			get { return this.cancelDate; }
			set { this.cancelDate = value; }
		}

		public string StatusCode
		{
			get { return this.statusCode; }
			set { this.statusCode = value; }
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

		public string QuoteNo
		{
			get { return this.quoteNo; }
			set { this.quoteNo = value; }
		}

		public string PoNo
		{
			get { return this.poNo; }
			set { this.poNo = value; }
		}

		public string ValueCurrency
		{
			get { return this.valueCurrency; }
			set { this.valueCurrency = value; }
		}

		public decimal ValueExRate
		{
			get { return this.valueExRate; }
			set { this.valueExRate = value; }
		}

		public decimal ValueAmt
		{
			get { return this.valueAmt; }
			set { this.valueAmt = value; }
		}

		public string DriverName
		{
			get { return this.driverName; }
			set { this.driverName = value; }
		}

		public string DriverMobile
		{
			get { return this.driverMobile; }
			set { this.driverMobile = value; }
		}

		public string DriverLicense
		{
			get { return this.driverLicense; }
			set { this.driverLicense = value; }
		}

		public string DriverRemark
		{
			get { return this.driverRemark; }
			set { this.driverRemark = value; }
		}

		public string VehicleNo
		{
			get { return this.vehicleNo; }
			set { this.vehicleNo = value; }
		}

		public string VehicleType
		{
			get { return this.vehicleType; }
			set { this.vehicleType = value; }
		}

		public DateTime PODTime
		{
			get { return this.pODTime; }
			set { this.pODTime = value; }
		}

		public string PODBy
		{
			get { return this.pODBy; }
			set { this.pODBy = value; }
		}

		public string PODRemark
		{
			get { return this.pODRemark; }
			set { this.pODRemark = value; }
		}

		public string PODUpdateUser
		{
			get { return this.pODUpdateUser; }
			set { this.pODUpdateUser = value; }
		}

		public DateTime PODUpdateTime
		{
			get { return this.pODUpdateTime; }
			set { this.pODUpdateTime = value; }
		}

		public string Note1
		{
			get { return this.note1; }
			set { this.note1 = value; }
		}

		public string Note2
		{
			get { return this.note2; }
			set { this.note2 = value; }
		}

		public string Note3
		{
			get { return this.note3; }
			set { this.note3 = value; }
		}

		public string Note4
		{
			get { return this.note4; }
			set { this.note4 = value; }
		}

		public string Note5
		{
			get { return this.note5; }
			set { this.note5 = value; }
		}

		public string Note6
		{
			get { return this.note6; }
			set { this.note6 = value; }
		}

		public string Note7
		{
			get { return this.note7; }
			set { this.note7 = value; }
		}

		public string Note8
		{
			get { return this.note8; }
			set { this.note8 = value; }
		}

		public string Note9
		{
			get { return this.note9; }
			set { this.note9 = value; }
		}

		public string Note10
		{
			get { return this.note10; }
			set { this.note10 = value; }
		}

		public string Note11
		{
			get { return this.note11; }
			set { this.note11 = value; }
		}

		public string Note12
		{
			get { return this.note12; }
			set { this.note12 = value; }
		}

		public string Note13
		{
			get { return this.note13; }
			set { this.note13 = value; }
		}

		public string Note14
		{
			get { return this.note14; }
			set { this.note14 = value; }
		}

		public string Note15
		{
			get { return this.note15; }
			set { this.note15 = value; }
		}

		public string Note16
		{
			get { return this.note16; }
			set { this.note16 = value; }
		}

		public string Note17
		{
			get { return this.note17; }
			set { this.note17 = value; }
		}

		public string Note18
		{
			get { return this.note18; }
			set { this.note18 = value; }
		}

		public string Note19
		{
			get { return this.note19; }
			set { this.note19 = value; }
		}

		public string Note20
		{
			get { return this.note20; }
			set { this.note20 = value; }
		}

		public DateTime HaulierDeliveryDate
		{
			get { return this.haulierDeliveryDate; }
			set { this.haulierDeliveryDate = value; }
		}

		public string HaulierSendTo
		{
			get { return this.haulierSendTo; }
			set { this.haulierSendTo = value; }
		}

		public string HaulierStuffBy
		{
			get { return this.haulierStuffBy; }
			set { this.haulierStuffBy = value; }
		}

		public string HaulierCoload
		{
			get { return this.haulierCoload; }
			set { this.haulierCoload = value; }
		}

		public string HaulierPerson
		{
			get { return this.haulierPerson; }
			set { this.haulierPerson = value; }
		}

		public string HaulierPersonTel
		{
			get { return this.haulierPersonTel; }
			set { this.haulierPersonTel = value; }
		}

		public string Dept
		{
			get { return this.dept; }
			set { this.dept = value; }
		}

		public string HaulierCollectTime
		{
			get { return this.haulierCollectTime; }
			set { this.haulierCollectTime = value; }
		}

		public string HaulierDeliveryTime
		{
			get { return this.haulierDeliveryTime; }
			set { this.haulierDeliveryTime = value; }
		}

		public string EdiRefNo
		{
			get { return this.ediRefNo; }
			set { this.ediRefNo = value; }
		}

		public string EdiJobNo
		{
			get { return this.ediJobNo; }
			set { this.ediJobNo = value; }
		}

		public string CustRefNo
		{
			get { return this.custRefNo; }
			set { this.custRefNo = value; }
		}

		public string ServiceType
		{
			get { return this.serviceType; }
			set { this.serviceType = value; }
		}

		public string Sku
		{
			get { return this.sku; }
			set { this.sku = value; }
		}

		public string CargoClass
		{
			get { return this.cargoClass; }
			set { this.cargoClass = value; }
		}

		public string TotMkg
		{
			get { return this.totMkg; }
			set { this.totMkg = value; }
		}

		public string TotDe
		{
			get { return this.totDe; }
			set { this.totDe = value; }
		}

		public decimal VolumeInv
		{
			get { return this.volumeInv; }
			set { this.volumeInv = value; }
		}

		public string InsuranceVendor
		{
			get { return this.insuranceVendor; }
			set { this.insuranceVendor = value; }
		}

		public decimal WeightInv
		{
			get { return this.weightInv; }
			set { this.weightInv = value; }
		}

		public string Carrier
		{
			get { return this.carrier; }
			set { this.carrier = value; }
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

		public string CarrierId
		{
			get { return this.carrierId; }
			set { this.carrierId = value; }
		}

		public string WarehouseId
		{
			get { return this.warehouseId; }
			set { this.warehouseId = value; }
		}

		public string AgentId
		{
			get { return this.agentId; }
			set { this.agentId = value; }
		}
        public DateTime JobDate
        {
            get { return this.jobDate; }
            set { this.jobDate = value; }
        }
	}
}
