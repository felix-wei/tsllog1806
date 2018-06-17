using System;

namespace C2
{
    public class SeaExport:BaseObject
    {
        private int sequenceId;
        private string refNo;
        private string containerNo;
        private string jobNo;
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
        private string placeReceiveId;
        private string placeLoadingName;
        private string placeDischargeName;
        private string placeDeliveryName;
        private string placeReceiveName;
        private string placeDeliveryTerm;
        private string placeReceiveTerm;
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
        private string haulierName;
        private string haulierCrNo;
        private string haulierAttention;
        private string haulierCollect;
        private string haulierTruck;
        private DateTime haulierCollectDate;
        private string haulierRemark;
        private string clauseFreightTerm;
        private string clauseBl;
        private string clauseCargo;


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
        public string CustomerName
        {
            get
            {
                return EzshipHelper.GetPartyName(customerId);
            }
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

        public string PlaceReceiveId
        {
            get { return this.placeReceiveId; }
            set { this.placeReceiveId = value; }
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


        public string PlaceDeliveryName
        {
            get { return this.placeDeliveryName; }
            set { this.placeDeliveryName = value; }
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

        private decimal impCharge;
        public decimal ImpCharge
        {
            get { return this.impCharge; }
            set { this.impCharge = value; }
        }
        private string preCarriage;
        public string PreCarriage
        {
            get { return this.preCarriage; }
            set { this.preCarriage = value; }
        }

        public string ClauseFreightTerm
        {
            get { return this.clauseFreightTerm; }
            set { this.clauseFreightTerm = value; }
        }
        public string ClauseBl
        {
            get { return this.clauseBl; }
            set { this.clauseBl = value; }
        }
        public string ClauseCargo
        {
            get { return this.clauseCargo; }
            set { this.clauseCargo = value; }
        }
        private string surrenderBl;
        public string SurrenderBl
        {
            get { return this.surrenderBl; }
            set { this.surrenderBl = value; }
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
        //private string cancelInd;
        //private string cancelRmk;
        //private string cancelUser;
        //private DateTime cancelDate;
        private string quoteNo;
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
        public string QuoteNo
        {
            get { return this.quoteNo; }
            set { this.quoteNo = value; }
        }
        private string poNo;
        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }

        private string valueCurrency;
        public string ValueCurrency
        {
            get { return this.valueCurrency; }
            set { this.valueCurrency = value; }
        }
        private decimal valueExRate;
        public decimal ValueExRate
        {
            get { return this.valueExRate; }
            set { this.valueExRate = value; }
        }
        private decimal valueAmt;
        public decimal ValueAmt
        {
            get { return this.valueAmt; }
            set { this.valueAmt = value; }
        }

        private string driverName;
        private string driverMobile;
        private string driverLicense;
        private string driverRemark;
        private string vehicleNo;
        private string vehicleType;
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

        /*--------2013年9月5日 Add--------*/
        private DateTime pODTime;
        private string pODBy;
        private string pODRemark;
        private string pODUpdateUser;
        private DateTime pODUpdateTime;
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

        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private DateTime haulierDeliveryDate;
        public DateTime HaulierDeliveryDate
        {
            get { return this.haulierDeliveryDate; }
            set { this.haulierDeliveryDate = value; }
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
        private string dept;
        public string Dept
        {
            get { return this.dept; }
            set { this.dept = value; }
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


        /*--------2013年11月21日 Add by Clare--------*/
        private string dgCategory;
        public string DgCategory
        {
            get { return this.dgCategory; }
            set { this.dgCategory = value; }
        }
        private string dgImdgClass;
        public string DgImdgClass
        {
            get { return this.dgImdgClass; }
            set { this.dgImdgClass = value; }
        }
        private string dgUnNo;
        public string DgUnNo
        {
            get { return this.dgUnNo; }
            set { this.dgUnNo = value; }
        }
        private string dgShippingName;
        public string DgShippingName
        {
            get { return this.dgShippingName; }
            set { this.dgShippingName = value; }
        }
        private string dgTecnicalName;
        public string DgTecnicalName
        {
            get { return this.dgTecnicalName; }
            set { this.dgTecnicalName = value; }
        }
        private string dgMfagNo1;
        public string DgMfagNo1
        {
            get { return this.dgMfagNo1; }
            set { this.dgMfagNo1 = value; }
        }
        private string dgMfagNo2;
        public string DgMfagNo2
        {
            get { return this.dgMfagNo2; }
            set { this.dgMfagNo2 = value; }
        }
        private string dgEmsFire;
        public string DgEmsFire
        {
            get { return this.dgEmsFire; }
            set { this.dgEmsFire = value; }
        }
        private string dgEmsSpill;
        public string DgEmsSpill
        {
            get { return this.dgEmsSpill; }
            set { this.dgEmsSpill = value; }
        }
        private string dgLimitedQtyInd;
        public string DgLimitedQtyInd
        {
            get { return this.dgLimitedQtyInd; }
            set { this.dgLimitedQtyInd = value; }
        }
        private string dgExemptedQtyInd;
        public string DgExemptedQtyInd
        {
            get { return this.dgExemptedQtyInd; }
            set { this.dgExemptedQtyInd = value; }
        }
        private string dgNetWeight;
        public string DgNetWeight
        {
            get { return this.dgNetWeight; }
            set { this.dgNetWeight = value; }
        }
        private string dgFlashPoint;
        public string DgFlashPoint
        {
            get { return this.dgFlashPoint; }
            set { this.dgFlashPoint = value; }
        }
        private string dgRadio;
        public string DgRadio
        {
            get { return this.dgRadio; }
            set { this.dgRadio = value; }
        }
        private string dgPageNo;
        public string DgPageNo
        {
            get { return this.dgPageNo; }
            set { this.dgPageNo = value; }
        }
        private string dgPackingGroup;
        public string DgPackingGroup
        {
            get { return this.dgPackingGroup; }
            set { this.dgPackingGroup = value; }
        }
        private string dgPackingTypeCode;
        public string DgPackingTypeCode
        {
            get { return this.dgPackingTypeCode; }
            set { this.dgPackingTypeCode = value; }
        }
        private string dgTransportCode;
        public string DgTransportCode
        {
            get { return this.dgTransportCode; }
            set { this.dgTransportCode = value; }
        }

        //2013-11-23 ADD
        private string haulierFax;
        public string HaulierFax
        {
            get { return this.haulierFax; }
            set { this.haulierFax = value; }
        }
    }
}
