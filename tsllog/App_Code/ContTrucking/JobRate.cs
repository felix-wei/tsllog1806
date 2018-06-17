using System;

namespace C2
{
    public class JobRate
    {
        private int id;
        private int docId;
        private int lineId;
        private string lineType;
        private string lineStatus;
        private string jobNo;
        private string jobType;
        private string refNo;
        private string clientId;
        private string subClientId;
        private string vendorId;
        private string billScope;
        private string billType;
        private string billClass;
        private string contSize;
        private string contType;
        private string skuCode;
        private string chgCode;
        private string chgCodeDe;
        private string remark;
        private decimal qty;
        private decimal price;
        private string unit;
        private decimal minPrice;
        private decimal minQty;
        private decimal minAmt;
        private string currencyId;
        private decimal exRate;
        private decimal docAmt;
        private decimal locAmt;
        private string status1;
        private string status2;
        private string status3;
        private string status4;
        private DateTime date1;
        private DateTime date2;
        private DateTime date3;
        private DateTime date4;
        private string lineRemark;
        private DateTime dateEffective;
        private DateTime dateExpiry;
        private int companyId;
        private string rowStatus;
        private string rowCreateUser;
        private DateTime rowCreateTime;
        private string rowUpdateUser;
        private DateTime rowUpdateTime;

        public int Id
        {
            get { return this.id; }
        }

        public int DocId
        {
            get { return this.docId; }
            set { this.docId = value; }
        }

        public int LineId
        {
            get { return this.lineId; }
            set { this.lineId = value; }
        }

        public string LineType
        {
            get { return this.lineType; }
            set { this.lineType = value; }
        }

        public string LineStatus
        {
            get { return this.lineStatus; }
            set { this.lineStatus = value; }
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

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string ClientId
        {
            get { return this.clientId; }
            set { this.clientId = value; }
        }

        public string SubClientId
        {
            get { return this.subClientId; }
            set { this.subClientId = value; }
        }

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }

        public string BillScope
        {
            get { return this.billScope; }
            set { this.billScope = value; }
        }

        public string BillType
        {
            get { return this.billType; }
            set { this.billType = value; }
        }

        public string BillClass
        {
            get { return this.billClass; }
            set { this.billClass = value; }
        }

        public string ContSize
        {
            get { return this.contSize; }
            set { this.contSize = value; }
        }

        public string ContType
        {
            get { return this.contType; }
            set { this.contType = value; }
        }

        public string SkuCode
        {
            get { return this.skuCode; }
            set { this.skuCode = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string ChgCodeDe
        {
            get { return this.chgCodeDe; }
            set { this.chgCodeDe = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public decimal MinPrice
        {
            get { return this.minPrice; }
            set { this.minPrice = value; }
        }

        public decimal MinQty
        {
            get { return this.minQty; }
            set { this.minQty = value; }
        }

        public decimal MinAmt
        {
            get { return this.minAmt; }
            set { this.minAmt = value; }
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

        public string Status1
        {
            get { return this.status1; }
            set { this.status1 = value; }
        }

        public string Status2
        {
            get { return this.status2; }
            set { this.status2 = value; }
        }

        public string Status3
        {
            get { return this.status3; }
            set { this.status3 = value; }
        }

        public string Status4
        {
            get { return this.status4; }
            set { this.status4 = value; }
        }

        public DateTime Date1
        {
            get { return this.date1; }
            set { this.date1 = value; }
        }

        public DateTime Date2
        {
            get { return this.date2; }
            set { this.date2 = value; }
        }

        public DateTime Date3
        {
            get { return this.date3; }
            set { this.date3 = value; }
        }

        public DateTime Date4
        {
            get { return this.date4; }
            set { this.date4 = value; }
        }

        public string LineRemark
        {
            get { return this.lineRemark; }
            set { this.lineRemark = value; }
        }

        public DateTime DateEffective
        {
            get { return this.dateEffective; }
            set { this.dateEffective = value; }
        }

        public DateTime DateExpiry
        {
            get { return this.dateExpiry; }
            set { this.dateExpiry = value; }
        }

        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        public string RowStatus
        {
            get { return this.rowStatus; }
            set { this.rowStatus = value; }
        }

        public string RowCreateUser
        {
            get { return this.rowCreateUser; }
            set { this.rowCreateUser = value; }
        }

        public DateTime RowCreateTime
        {
            get { return this.rowCreateTime; }
            set { this.rowCreateTime = value; }
        }

        public string RowUpdateUser
        {
            get { return this.rowUpdateUser; }
            set { this.rowUpdateUser = value; }
        }

        public DateTime RowUpdateTime
        {
            get { return this.rowUpdateTime; }
            set { this.rowUpdateTime = value; }
        }

        //2016/04/09
        private string skuClass;
        public string SkuClass {
            get { return this.skuClass; }
            set { this.skuClass = value; }
        }
        private string skuUnit;
        public string SkuUnit
        {
            get { return this.skuUnit; }
            set { this.skuUnit=value; }
        }
        private string storageType;
        public string StorageType {
            get { return this.storageType; }
            set { this.storageType = value; }
        }

        private string vehicleType;
        public string VehicleType
        {
            get { return this.vehicleType; }
            set { this.vehicleType = value; }
        }
        private string gstType;
        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }

        private string lumsumInd;
        public string LumsumInd
        {
            get { return this.lumsumInd; }
            set { this.lumsumInd = value; }
        }

        private decimal cost;
        public decimal Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }
        private int lineIndex;
        public int LineIndex
        {
            get { return this.lineIndex; }
            set { this.lineIndex = value; }
        }

        private int dailyNo;
        public int DailyNo
        {
            get { return this.dailyNo; }
            set { this.dailyNo = value; }
        }
        private decimal first30Days;
        private decimal after30Days;
        public decimal First30Days
        {
            get { return this.first30Days; }
            set { this.first30Days = value; }
        }
        public decimal After30Days
        {
            get { return this.after30Days; }
            set { this.after30Days = value; }
        }
    }
}
