using System;

namespace C2
{
    public class WhTransDet
    {
        private int id;
        private string doNo;
        private string doType;
        private string productCode;
        private int qty;
        private int balQty;
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
        private string contractNo;
        private string doInId;
        private int preQty;
        private string isSch;
        private int lastSchQty;
        private string jobStatus;
        private string batchNo;
        private DateTime expiredDate;
        private string color;
        private decimal size;
        private string packCode;
        private int qty1;//pack qty
        private int qty2;//whole qty
        private int qty3;//loose qty
        private string locationCode;
        private string unit;
        private string des1;
        private string packing;
        private int qty4;
        private int qty5;
        private string createBy;
        private string updateBy;
        private DateTime createDateTime;
        private DateTime updateDateTime;
        private string remark;
        private string customsLot;
        private string lotNo;

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

        public int BalQty
        {
            get { return this.balQty; }
            set { this.balQty = value; }
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

        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }

        public string DoInId
        {
            get { return this.doInId; }
            set { this.doInId = value; }
        }

        public int PreQty
        {
            get { return this.preQty; }
            set { this.preQty = value; }
        }

        public string IsSch
        {
            get { return this.isSch; }
            set { this.isSch = value; }
        }

        public int LastSchQty
        {
            get { return this.lastSchQty; }
            set { this.lastSchQty = value; }
        }

        public string JobStatus
        {
            get { return this.jobStatus; }
            set { this.jobStatus = value; }
        }

        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
        }

        public DateTime ExpiredDate
        {
            get { return this.expiredDate; }
            set { this.expiredDate = value; }
        }

        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public decimal Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public string PackCode
        {
            get { return this.packCode; }
            set { this.packCode = value; }
        }

        public int Qty1
        {
            get { return this.qty1; }
            set { this.qty1 = value; }
        }

        public int Qty2
        {
            get { return this.qty2; }
            set { this.qty2 = value; }
        }

        public string LocationCode
        {
            get { return this.locationCode; }
            set { this.locationCode = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

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

        public int Qty4
        {
            get { return this.qty4; }
            set { this.qty4 = value; }
        }

        public int Qty5
        {
            get { return this.qty5; }
            set { this.qty5 = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string CustomsLot
        {
            get { return this.customsLot; }
            set { this.customsLot = value; }
        }

        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }
        public string DoStatus
        {
            get
            {
                string s = "USE";
                string sql = "select DoStatus from Wh_Trans where DoNo='" + this.doNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "Draft");
                return s;
            }
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
    }
}
