using System;

namespace C2
{
    public class SeaImport:BaseObject
    {
        private int sequenceId;
        private string refNo;
        private string containerNo;
        private string jobNo;
        private string hblNo;
        private string forwardingId;
        private string customerId;
        private string customerName;
        private decimal weight;
        private decimal volume;
        private int qty;
        private string packageType;
        private string expressBl;
        private string truckingInd;
        private string shipper;
        private string consignee;
        private string doRealeaseTo;
        private DateTime deliveryDate;
        private string doReadyInd;
        private string frCollectInd;
        private decimal collectAmount;
        private string collectCurrency;
        private decimal collectExRate;
        private string tsInd;
        private string tsSchNo;
        private string tsBkgId;
        private string tsBkgNo;
        private string tsRefNo;
        private string tsJobNo;
        private string tsPod;
        private string tsPortFinName;
        private string tsVessel;
        private string tsVoyage;
        private string tsColoader;
        private DateTime tsEtd;
        private DateTime tsEta;
        private string tsAgentId;
        private string tsRemark;
        private decimal tsAgtRate;
        private decimal tsTotAgtRate;
        private decimal tsImpRate;
        private decimal tsTotImpRate;
        private string permitRmk;
        private string sShipperRemark;
        private string sAgentRemark;
        private string sConsigneeRemark;
        private string sNotifyPartyRemark;

        private decimal rateForklift;
        private decimal rateProcess;
        private decimal rateTracing;
        private decimal rateWarehouse;
        private decimal rateAdmin;
        private string flagNomination;
        private string cltFrmId;
        private string deliveryToId;
       

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
            get { return this.customerName; }
            set { this.customerName = value; }
        }
        public string ForwardingId
        {
            get { return this.forwardingId; }
            set { this.forwardingId = value; }
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

        public string ExpressBl
        {
            get { return this.expressBl; }
            set { this.expressBl = value; }
        }

        public string TruckingInd
        {
            get { return this.truckingInd; }
            set { this.truckingInd = value; }
        }

        public string Shipper
        {
            get { return this.shipper; }
            set { this.shipper = value; }
        }

        public string Consignee
        {
            get { return this.consignee; }
            set { this.consignee = value; }
        }

        public string DoRealeaseTo
        {
            get { return this.doRealeaseTo; }
            set { this.doRealeaseTo = value; }
        }

        public DateTime DeliveryDate
        {
            get { return this.deliveryDate; }
            set { this.deliveryDate = value; }
        }

        public string DoReadyInd
        {
            get { return this.doReadyInd; }
            set { this.doReadyInd = value; }
        }

        public string FrCollectInd
        {
            get { return this.frCollectInd; }
            set { this.frCollectInd = value; }
        }

        public decimal CollectAmount
        {
            get { return this.collectAmount; }
            set { this.collectAmount = value; }
        }

        public string CollectCurrency
        {
            get { return this.collectCurrency; }
            set { this.collectCurrency = value; }
        }

        public decimal CollectExRate
        {
            get { return this.collectExRate; }
            set { this.collectExRate = value; }
        }

        public string TsInd
        {
            get { return this.tsInd; }
            set { this.tsInd = value; }
        }

        public string TsSchNo
        {
            get { return this.tsSchNo; }
            set { this.tsSchNo = value; }
        }

        public string TsBkgId
        {
            get { return this.tsBkgId; }
            set { this.tsBkgId = value; }
        }
        public string TsBkgNo
        {
            get { return this.tsBkgNo; }
            set { this.tsBkgNo = value; }
        }

        public string TsRefNo
        {
            get { return this.tsRefNo; }
            set { this.tsRefNo = value; }
        }

        public string TsJobNo
        {
            get { return this.tsJobNo; }
            set { this.tsJobNo = value; }
        }

        public string TsPod
        {
            get { return this.tsPod; }
            set { this.tsPod = value; }
        }

        public string TsPortFinName
        {
            get { return this.tsPortFinName; }
            set { this.tsPortFinName = value; }
        }

        public string TsVessel
        {
            get { return this.tsVessel; }
            set { this.tsVessel = value; }
        }

        public string TsVoyage
        {
            get { return this.tsVoyage; }
            set { this.tsVoyage = value; }
        }

        public string TsColoader
        {
            get { return this.tsColoader; }
            set { this.tsColoader = value; }
        }

        public DateTime TsEtd
        {
            get { return this.tsEtd; }
            set { this.tsEtd = value; }
        }

        public DateTime TsEta
        {
            get { return this.tsEta; }
            set { this.tsEta = value; }
        }

        public string TsAgentId
        {
            get { return this.tsAgentId; }
            set { this.tsAgentId = value; }
        }

        public string TsRemark
        {
            get { return this.tsRemark; }
            set { this.tsRemark = value; }
        }

        public decimal TsAgtRate
        {
            get { return this.tsAgtRate; }
            set { this.tsAgtRate = value; }
        }

        public decimal TsTotAgtRate
        {
            get { return this.tsTotAgtRate; }
            set { this.tsTotAgtRate = value; }
        }

        public decimal TsImpRate
        {
            get { return this.tsImpRate; }
            set { this.tsImpRate = value; }
        }

        public decimal TsTotImpRate
        {
            get { return this.tsTotImpRate; }
            set { this.tsTotImpRate = value; }
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

        public decimal RateForklift
        {
            get { return this.rateForklift; }
            set { this.rateForklift = value; }
        }

        public decimal RateProcess
        {
            get { return this.rateProcess; }
            set { this.rateProcess = value; }
        }

        public decimal RateTracing
        {
            get { return this.rateTracing; }
            set { this.rateTracing = value; }
        }

        public decimal RateWarehouse
        {
            get { return this.rateWarehouse; }
            set { this.rateWarehouse = value; }
        }

        public decimal RateAdmin
        {
            get { return this.rateAdmin; }
            set { this.rateAdmin = value; }
        }




        public string FlagNomination
        {
            get { return this.flagNomination; }
            set { this.flagNomination = value; }
        }
        public string CltFrmId
        {
            get { return this.cltFrmId; }
            set { this.cltFrmId = value; }
        }
        public string DeliveryToId
        {
            get { return this.deliveryToId; }
            set { this.deliveryToId = value; }
        }

        private DateTime cltDoDate;
        public DateTime CltDoDate
        {
            get { return this.cltDoDate; }
        }

        private string valueCurrency;
        public string  ValueCurrency 
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
        /*------2013年9月4日 Add-------*/
        private string driverName;
        private string driverMobile;
        private string driverLicense;
        private string driverRemark;
        private string vehicleNo;
        private string vehicleType;
        private string haulierName;
        private string haulierCrNo;
        private string haulierAttention;
        private string haulierCollect;
        private string haulierTruck;
        private DateTime haulierCollectDate;
        private string haulierRemark;
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
                string sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }

        private string gstPermitNo;
        public string GstPermitNo
        {
            get { return this.gstPermitNo; }
            set { this.gstPermitNo = value; }
        }
        private decimal gstPaidAmt;
        public decimal GstPaidAmt
        {
            get { return this.gstPaidAmt; }
            set { this.gstPaidAmt = value; }
        }
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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
