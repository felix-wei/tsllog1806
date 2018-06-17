using System;

namespace C2
{
    public class WhDoDet
    {
        private int id;
        private string doNo;
        private string doType;
        private string productCode;

        private decimal qty1;//pack qty
        private decimal qty2;//whole qty
        private int qty3;//loose qty
        private decimal qty4;//expected qty
        private decimal qty5;//intrans qty

        private int qty;
        private decimal price;
        private string currency;
        private decimal exRate;
        private string gstType;
        private decimal gst;
        private decimal docAmt;
        private decimal gstAmt;
        private decimal locAmt;
        private decimal lineLocAmt;
        private DateTime peroidDate;
        private string doInNo;
        private string doInId;
        private string contractNo;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

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

        public string ProductCode
        {
            get { return this.productCode; }
            set { this.productCode = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
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

        public string GstType
        {
            get { return this.gstType; }
            set { this.gstType = value; }
        }

        public decimal Gst
        {
            get { return this.gst; }
            set { this.gst = value; }
        }

        public decimal DocAmt
        {
            get { return this.docAmt; }
            set { this.docAmt = value; }
        }

        public decimal GstAmt
        {
            get { return this.gstAmt; }
            set { this.gstAmt = value; }
        }

        public decimal LocAmt
        {
            get { return this.locAmt; }
            set { this.locAmt = value; }
        }

        public decimal LineLocAmt
        {
            get { return this.lineLocAmt; }
            set { this.lineLocAmt = value; }
        }

        public DateTime PeroidDate
        {
            get { return this.peroidDate; }
            set { this.peroidDate = value; }
        }

        public string DoInNo
        {
            get { return this.doInNo; }
            set { this.doInNo = value; }
        }
        public string DoInId
        {
            get { return this.doInId; }
            set { this.doInId = value; }
        }
        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }
        private int balQty;
        public int BalQty
        {
            get { return this.balQty; }
            set { this.balQty = value; }
        }
        private int preQty;
        public int PreQty
        {
            get { return this.preQty; }
            set { this.preQty = value; }
        }
        private string isSch;
        public string IsSch
        {
            get { return this.isSch; }
            set { this.isSch = value; }
        }
        private int lastSchQty;
        public int LastSchQty
        {
            get { return this.lastSchQty; }
            set { this.lastSchQty = value; }
        }
        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from Wh_DO where DoNo='" + this.doNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string color;
        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        private string packCode;
        public string PackCode
        {
            get { return this.packCode; }
            set { this.packCode = value; }
        }
        private decimal size;
        public decimal Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        private string jobStatus;
        public string JobStatus
        {
            get { return this.jobStatus; }
            set { this.jobStatus = value; }
        }
        private string batchNo;
        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
        }
        private DateTime expiredDate;
        public DateTime ExpiredDate
        {
            get { return this.expiredDate; }
            set { this.expiredDate = value; }
        }
        private string unit;
        private string locationCode;
        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }
        public decimal Qty1
        {
            get { return this.qty1; }
            set { this.qty1 = value; }
        }

        public decimal Qty2
        {
            get { return this.qty2; }
            set { this.qty2 = value; }
        }

        public string LocationCode
        {
            get { return this.locationCode; }
            set { this.locationCode = value; }
        }
        private string des1;
        private string packing;

        public string Des1
        {
            get { return this.des1; }
            set { this.des1 = value; }
        }
        public string Packing
        {
            get { return this.packing; }
            set { this.packing = value; }
        }
       public int Qty3
        {
            get { return this.qty3; }
            set { this.qty3 = value; }
        }
       public decimal Qty4
        {
            get { return this.qty4; }
            set { this.qty4 = value; }
        }
       public decimal Qty5
        {
            get { return this.qty5; }
            set { this.qty5 = value; }
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

        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        private string customsLot;
        public string CustomsLot
        {
            get { return this.customsLot; }
            set { this.customsLot = value; }
        }
        private string lotNo;
        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }


        private int qtyPackWhole;
        private int qtyWholeLoose;
        private int qtyLooseBase;
        private string uom1;
        private string uom2;
        private string uom3;
        private string uom4;
        private string att1;
        private string att2;
        private string att3;
        private string att4;
        private string att5;
        private string att6;
        private string att7;
        private string att8;
        private string att9;
        private string att10;

        public int QtyPackWhole
        {
            get { return this.qtyPackWhole; }
            set { this.qtyPackWhole = value; }
        }

        public int QtyWholeLoose
        {
            get { return this.qtyWholeLoose; }
            set { this.qtyWholeLoose = value; }
        }

        public int QtyLooseBase
        {
            get { return this.qtyLooseBase; }
            set { this.qtyLooseBase = value; }
        }


        public string Uom1
        {
            get { return this.uom1; }
            set { this.uom1 = value; }
        }

        public string Uom2
        {
            get { return this.uom2; }
            set { this.uom2 = value; }
        }

        public string Uom3
        {
            get { return this.uom3; }
            set { this.uom3 = value; }
        }

        public string Uom4
        {
            get { return this.uom4; }
            set { this.uom4 = value; }
        }
        public string Att1
        {
            get { return this.att1; }
            set { this.att1 = value; }
        }

        public string Att2
        {
            get { return this.att2; }
            set { this.att2 = value; }
        }

        public string Att3
        {
            get { return this.att3; }
            set { this.att3 = value; }
        }

        public string Att4
        {
            get { return this.att4; }
            set { this.att4 = value; }
        }
        public string Att5
        {
            get { return this.att5; }
            set { this.att5 = value; }
        }

        public string Att6
        {
            get { return this.att6; }
            set { this.att6 = value; }
        }

        public string Att7
        {
            get { return this.att7; }
            set { this.att7 = value; }
        }

        public string Att8
        {
            get { return this.att8; }
            set { this.att8 = value; }
        }

        public string Att9
        {
            get { return this.att9; }
            set { this.att9 = value; }
        }
        public string Att10
        {
            get { return this.att10; }
            set { this.att10 = value; }
        }
        private string relaId;
        public string RelatId
        {
            get { return this.relaId; }
            set { this.relaId = value; }
        }
        private string containerNo;
        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
        }
        private string palletNo;
        public string PalletNo
        {
            get { return this.palletNo; }
            set { this.palletNo = value; }
        }
        private decimal grossWeight;
        private decimal nettWeight;
        public decimal GrossWeight
        {
            get { return this.grossWeight; }
            set { this.grossWeight = value; }
        }
        public decimal NettWeight
        {
            get { return this.nettWeight; }
            set { this.nettWeight = value; }
        }
    }
}
