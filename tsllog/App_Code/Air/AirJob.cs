using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2
{
    public class AirImport : BaseObject
    {
		private int id;
		private string refType;
		private string hawb;
		private string refNo;
		private string jobNo;
		private string customerId;
		private string airlineCode1;
		private string airlineName1;
		private string flightNo1;
		private string flightDate1;
		private string flightTime1;
		private string airportCode1;
		private string airportName1;
		private string airlineCode2;
		private string airlineName2;
		private string flightNo2;
		private string flightDate2;
		private string flightTime2;
		private string airportCode2;
		private string airportName2;
		private string airlineCode3;
		private string airlineName3;
		private string flightNo3;
		private string flightDate3;
		private string flightTime3;
		private string airportCode3;
		private string airportName3;
		private string airlineCode4;
		private string airlineName4;
		private string flightNo4;
		private string flightDate4;
		private string flightTime4;
		private string airportCode4;
		private string airportName4;
		private string flightDate0;
		private string flightTime0;
		private string airportCode0;
		private string airportName0;
		private string issuedBy;
		private string shipperID;
		private string shipperName;
		private string consigneeID;
		private string consigneeName;
		private string carrierAgent;
		private string accountInfo;
		private string agentIATACode;
		private string agentAccountNo;
		private string airportDeparture;
		private string connDest1;
		private string connCarrier1;
		private string connDest2;
		private string connCarrier2;
		private string connDest3;
		private string connCarrier3;
		private string airportDestination;
		private string requestedFlight;
		private string requestedDate;
		private string currency;
		private string chgsCode;
		private string ppd1;
		private string coll1;
		private string ppd2;
		private string coll2;
		private string carriageValue;
		private string customValue;
		private string amountInsurance;
		private string handlingInfo;
		private string piece;
		private string grossWeight;
		private string unit;
		private string rateClass;
		private string commodityItemNo;
		private string chargeableWeight;
		private string rateCharge;
		private string total;
		private string goodsNature;
		private string contentRemark;
		private string weightChargeP;
		private string weightChargeC;
		private string valuationChargeP;
        private string valuationChargeC;
		private string taxP;
		private string taxC;
		private string otherAgentChargeP;
		private string otherAgentChargeC;
		private string otherCarrierChargeP;
		private string otherCarrierChargeC;
		private string totalPrepaid;
		private string totalCollect;
		private string currencyRate;
		private string chargeDestCurrency;
		private string otherCharge1;
		private string otherCharge2;
		private string otherCharge3;
		private string signatureShipper;
		private string executeDate;
		private string executePlace;
		private string signatureIssuing;
		private string otherCharge4;
		private string otherCharge5;
		private string otherCharge1Currency;
		private string otherCharge2Currency;
		private string otherCharge3Currency;
		private string otherCharge4Currency;
		private string otherCharge5Currency;
		private decimal otherCharge1Amount;
		private decimal otherCharge2Amount;
		private decimal otherCharge3Amount;
		private decimal otherCharge4Amount;
		private decimal otherCharge5Amount;
		private string createBy;
		private DateTime createDateTime;
		private string updateBy;
		private DateTime updateDateTime;
		private string closeInd;
        private string airFlightDate1;
        private string airFlightTime1;
        private string airLinePortCode1;
        private string airLinePortName1;

		public int Id
		{
			get { return this.id; }
		}

		public string RefType
		{
			get { return this.refType; }
			set { this.refType = value; }
		}

		public string Hawb
		{
			get { return this.hawb; }
			set { this.hawb = value; }
		}

		public string RefNo
		{
			get { return this.refNo; }
			set { this.refNo = value; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
		}

		public string CustomerId
		{
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		public string AirlineCode1
		{
			get { return this.airlineCode1; }
			set { this.airlineCode1 = value; }
		}

		public string AirlineName1
		{
			get { return this.airlineName1; }
			set { this.airlineName1 = value; }
		}

		public string FlightNo1
		{
			get { return this.flightNo1; }
			set { this.flightNo1 = value; }
		}

		public string FlightDate1
		{
			get { return this.flightDate1; }
			set { this.flightDate1 = value; }
		}

		public string FlightTime1
		{
			get { return this.flightTime1; }
			set { this.flightTime1 = value; }
		}

		public string AirportCode1
		{
			get { return this.airportCode1; }
			set { this.airportCode1 = value; }
		}

		public string AirportName1
		{
			get { return this.airportName1; }
			set { this.airportName1 = value; }
		}

		public string AirlineCode2
		{
			get { return this.airlineCode2; }
			set { this.airlineCode2 = value; }
		}

		public string AirlineName2
		{
			get { return this.airlineName2; }
			set { this.airlineName2 = value; }
		}

		public string FlightNo2
		{
			get { return this.flightNo2; }
			set { this.flightNo2 = value; }
		}

		public string FlightDate2
		{
			get { return this.flightDate2; }
			set { this.flightDate2 = value; }
		}

		public string FlightTime2
		{
			get { return this.flightTime2; }
			set { this.flightTime2 = value; }
		}

		public string AirportCode2
		{
			get { return this.airportCode2; }
			set { this.airportCode2 = value; }
		}

		public string AirportName2
		{
			get { return this.airportName2; }
			set { this.airportName2 = value; }
		}

		public string AirlineCode3
		{
			get { return this.airlineCode3; }
			set { this.airlineCode3 = value; }
		}

		public string AirlineName3
		{
			get { return this.airlineName3; }
			set { this.airlineName3 = value; }
		}

		public string FlightNo3
		{
			get { return this.flightNo3; }
			set { this.flightNo3 = value; }
		}

		public string FlightDate3
		{
			get { return this.flightDate3; }
			set { this.flightDate3 = value; }
		}

		public string FlightTime3
		{
			get { return this.flightTime3; }
			set { this.flightTime3 = value; }
		}

		public string AirportCode3
		{
			get { return this.airportCode3; }
			set { this.airportCode3 = value; }
		}

		public string AirportName3
		{
			get { return this.airportName3; }
			set { this.airportName3 = value; }
		}

		public string AirlineCode4
		{
			get { return this.airlineCode4; }
			set { this.airlineCode4 = value; }
		}

		public string AirlineName4
		{
			get { return this.airlineName4; }
			set { this.airlineName4 = value; }
		}

		public string FlightNo4
		{
			get { return this.flightNo4; }
			set { this.flightNo4 = value; }
		}

		public string FlightDate4
		{
			get { return this.flightDate4; }
			set { this.flightDate4 = value; }
		}

		public string FlightTime4
		{
			get { return this.flightTime4; }
			set { this.flightTime4 = value; }
		}

		public string AirportCode4
		{
			get { return this.airportCode4; }
			set { this.airportCode4 = value; }
		}

		public string AirportName4
		{
			get { return this.airportName4; }
			set { this.airportName4 = value; }
		}

		public string FlightDate0
		{
			get { return this.flightDate0; }
			set { this.flightDate0 = value; }
		}

		public string FlightTime0
		{
			get { return this.flightTime0; }
			set { this.flightTime0 = value; }
		}

		public string AirportCode0
		{
			get { return this.airportCode0; }
			set { this.airportCode0 = value; }
		}

		public string AirportName0
		{
			get { return this.airportName0; }
			set { this.airportName0 = value; }
		}

		public string IssuedBy
		{
			get { return this.issuedBy; }
			set { this.issuedBy = value; }
		}

		public string ShipperID
		{
			get { return this.shipperID; }
			set { this.shipperID = value; }
		}

		public string ShipperName
		{
			get { return this.shipperName; }
			set { this.shipperName = value; }
		}

		public string ConsigneeID
		{
			get { return this.consigneeID; }
			set { this.consigneeID = value; }
		}

		public string ConsigneeName
		{
			get { return this.consigneeName; }
			set { this.consigneeName = value; }
		}

		public string CarrierAgent
		{
			get { return this.carrierAgent; }
			set { this.carrierAgent = value; }
		}

		public string AccountInfo
		{
			get { return this.accountInfo; }
			set { this.accountInfo = value; }
		}

		public string AgentIATACode
		{
			get { return this.agentIATACode; }
			set { this.agentIATACode = value; }
		}

		public string AgentAccountNo
		{
			get { return this.agentAccountNo; }
			set { this.agentAccountNo = value; }
		}

		public string AirportDeparture
		{
			get { return this.airportDeparture; }
			set { this.airportDeparture = value; }
		}

		public string ConnDest1
		{
			get { return this.connDest1; }
			set { this.connDest1 = value; }
		}

		public string ConnCarrier1
		{
			get { return this.connCarrier1; }
			set { this.connCarrier1 = value; }
		}

		public string ConnDest2
		{
			get { return this.connDest2; }
			set { this.connDest2 = value; }
		}

		public string ConnCarrier2
		{
			get { return this.connCarrier2; }
			set { this.connCarrier2 = value; }
		}

		public string ConnDest3
		{
			get { return this.connDest3; }
			set { this.connDest3 = value; }
		}

		public string ConnCarrier3
		{
			get { return this.connCarrier3; }
			set { this.connCarrier3 = value; }
		}

		public string AirportDestination
		{
			get { return this.airportDestination; }
			set { this.airportDestination = value; }
		}

		public string RequestedFlight
		{
			get { return this.requestedFlight; }
			set { this.requestedFlight = value; }
		}

		public string RequestedDate
		{
			get { return this.requestedDate; }
			set { this.requestedDate = value; }
		}

		public string Currency
		{
			get { return this.currency; }
			set { this.currency = value; }
		}

		public string ChgsCode
		{
			get { return this.chgsCode; }
			set { this.chgsCode = value; }
		}

		public string Ppd1
		{
			get { return this.ppd1; }
			set { this.ppd1 = value; }
		}

		public string Coll1
		{
			get { return this.coll1; }
			set { this.coll1 = value; }
		}

		public string Ppd2
		{
			get { return this.ppd2; }
			set { this.ppd2 = value; }
		}

		public string Coll2
		{
			get { return this.coll2; }
			set { this.coll2 = value; }
		}

		public string CarriageValue
		{
			get { return this.carriageValue; }
			set { this.carriageValue = value; }
		}

		public string CustomValue
		{
			get { return this.customValue; }
			set { this.customValue = value; }
		}

		public string AmountInsurance
		{
			get { return this.amountInsurance; }
			set { this.amountInsurance = value; }
		}

		public string HandlingInfo
		{
			get { return this.handlingInfo; }
			set { this.handlingInfo = value; }
		}

		public string Piece
		{
			get { return this.piece; }
			set { this.piece = value; }
		}

		public string GrossWeight
		{
			get { return this.grossWeight; }
			set { this.grossWeight = value; }
		}

		public string Unit
		{
			get { return this.unit; }
			set { this.unit = value; }
		}

		public string RateClass
		{
			get { return this.rateClass; }
			set { this.rateClass = value; }
		}

		public string CommodityItemNo
		{
			get { return this.commodityItemNo; }
			set { this.commodityItemNo = value; }
		}

		public string ChargeableWeight
		{
			get { return this.chargeableWeight; }
			set { this.chargeableWeight = value; }
		}

		public string RateCharge
		{
			get { return this.rateCharge; }
			set { this.rateCharge = value; }
		}

		public string Total
		{
			get { return this.total; }
			set { this.total = value; }
		}

		public string GoodsNature
		{
			get { return this.goodsNature; }
			set { this.goodsNature = value; }
		}

		public string ContentRemark
		{
			get { return this.contentRemark; }
			set { this.contentRemark = value; }
		}

		public string WeightChargeP
		{
			get { return this.weightChargeP; }
			set { this.weightChargeP = value; }
		}

		public string WeightChargeC
		{
			get { return this.weightChargeC; }
			set { this.weightChargeC = value; }
		}

		public string ValuationChargeP
		{
			get { return this.valuationChargeP; }
			set { this.valuationChargeP = value; }
		}
        public string ValuationChargeC
        {
            get { return this.valuationChargeC; }
            set { this.valuationChargeC = value; }
        }
		public string TaxP
		{
			get { return this.taxP; }
			set { this.taxP = value; }
		}

		public string TaxC
		{
			get { return this.taxC; }
			set { this.taxC = value; }
		}

		public string OtherAgentChargeP
		{
			get { return this.otherAgentChargeP; }
			set { this.otherAgentChargeP = value; }
		}

		public string OtherAgentChargeC
		{
			get { return this.otherAgentChargeC; }
			set { this.otherAgentChargeC = value; }
		}

		public string OtherCarrierChargeP
		{
			get { return this.otherCarrierChargeP; }
			set { this.otherCarrierChargeP = value; }
		}

		public string OtherCarrierChargeC
		{
			get { return this.otherCarrierChargeC; }
			set { this.otherCarrierChargeC = value; }
		}

		public string TotalPrepaid
		{
			get { return this.totalPrepaid; }
			set { this.totalPrepaid = value; }
		}

		public string TotalCollect
		{
			get { return this.totalCollect; }
			set { this.totalCollect = value; }
		}

		public string CurrencyRate
		{
			get { return this.currencyRate; }
			set { this.currencyRate = value; }
		}

		public string ChargeDestCurrency
		{
			get { return this.chargeDestCurrency; }
			set { this.chargeDestCurrency = value; }
		}

		public string OtherCharge1
		{
			get { return this.otherCharge1; }
			set { this.otherCharge1 = value; }
		}

		public string OtherCharge2
		{
			get { return this.otherCharge2; }
			set { this.otherCharge2 = value; }
		}

		public string OtherCharge3
		{
			get { return this.otherCharge3; }
			set { this.otherCharge3 = value; }
		}

		public string SignatureShipper
		{
			get { return this.signatureShipper; }
			set { this.signatureShipper = value; }
		}

		public string ExecuteDate
		{
			get { return this.executeDate; }
			set { this.executeDate = value; }
		}

		public string ExecutePlace
		{
			get { return this.executePlace; }
			set { this.executePlace = value; }
		}

		public string SignatureIssuing
		{
			get { return this.signatureIssuing; }
			set { this.signatureIssuing = value; }
		}

		public string OtherCharge4
		{
			get { return this.otherCharge4; }
			set { this.otherCharge4 = value; }
		}

		public string OtherCharge5
		{
			get { return this.otherCharge5; }
			set { this.otherCharge5 = value; }
		}

		public string OtherCharge1Currency
		{
			get { return this.otherCharge1Currency; }
			set { this.otherCharge1Currency = value; }
		}

		public string OtherCharge2Currency
		{
			get { return this.otherCharge2Currency; }
			set { this.otherCharge2Currency = value; }
		}

		public string OtherCharge3Currency
		{
			get { return this.otherCharge3Currency; }
			set { this.otherCharge3Currency = value; }
		}

		public string OtherCharge4Currency
		{
			get { return this.otherCharge4Currency; }
			set { this.otherCharge4Currency = value; }
		}

		public string OtherCharge5Currency
		{
			get { return this.otherCharge5Currency; }
			set { this.otherCharge5Currency = value; }
		}

		public decimal OtherCharge1Amount
		{
			get { return this.otherCharge1Amount; }
			set { this.otherCharge1Amount = value; }
		}

		public decimal OtherCharge2Amount
		{
			get { return this.otherCharge2Amount; }
			set { this.otherCharge2Amount = value; }
		}

		public decimal OtherCharge3Amount
		{
			get { return this.otherCharge3Amount; }
			set { this.otherCharge3Amount = value; }
		}

		public decimal OtherCharge4Amount
		{
			get { return this.otherCharge4Amount; }
			set { this.otherCharge4Amount = value; }
		}

		public decimal OtherCharge5Amount
		{
			get { return this.otherCharge5Amount; }
			set { this.otherCharge5Amount = value; }
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

		public string CloseInd
		{
			get { return this.closeInd; }
			set { this.closeInd = value; }
		}
        public string AirFlightDate1
        {
            get { return this.airFlightDate1; }
            set { this.airFlightDate1 = value; }
        }
        public string AirFlightTime1
        {
            get { return this.airFlightTime1; }
            set { this.airFlightTime1 = value; }
        }
        public string AirLinePortCode1
        {
            get { return this.airLinePortCode1; }
            set { this.airLinePortCode1 = value; }
        }
        public string AirLinePortName1
        {
            get { return this.airLinePortName1; }
            set { this.airLinePortName1 = value; }
        }
        private decimal weight;
        public decimal Weight 
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
        private decimal volume;
        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }
        private int qty;
        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }
        private string packageType;
        public string PackageType
        {
            get { return this.packageType; }
            set { this.packageType = value; }
        }
        private string tsInd;
        public string TsInd
        {
            get { return this.tsInd; }
            set { this.tsInd = value; }
        }

        private string customerName;
        public string CustomerName
        {
            get 
            { 
                return EzshipHelper.GetPartyName(customerId); 
            }
        }
        /*------2013年9月4日 Add-------*/
        private string driverName;
        private string driverMobile;
        private string driverLicense;
        private string driverRemark;
        private string vehicleNo;
        private string vehicleType;
        private string permitRmk;
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

        public string PermitRmk
        {
            get { return this.permitRmk; }
            set { this.permitRmk = value; }
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
        private DateTime deliveryDate;
        private string doReadyInd;
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
        /*------2013年9月5日 Add-------*/
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
        private string tsBkgRef;
        private DateTime tsBkgTime;
        private string tsBkgUser;
        private string remark;
        private string marking;
        public string TsBkgRef
        {
            get { return this.tsBkgRef; }
            set { this.tsBkgRef = value; }
        }

        public DateTime TsBkgTime
        {
            get { return this.tsBkgTime; }
            set { this.tsBkgTime = value; }
        }

        public string TsBkgUser
        {
            get { return this.tsBkgUser; }
            set { this.tsBkgUser = value; }
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
        private string statusCode;
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from air_ref where RefNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
    }

}