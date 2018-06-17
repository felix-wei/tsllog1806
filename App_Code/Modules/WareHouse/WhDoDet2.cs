using System;

namespace C2
{
    public class WhDoDet2
    {
        private int id;
        private string doNo;
        private string doType;
        private string product;
        private string location;
        private int qty;
        private decimal price;
        private decimal qty1;
        private decimal qty2;
        private int qty3;
        private DateTime doDate;
        private string des1;
        private string lotNo;
        private string pkgType;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string customsLot;

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

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
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
        public int Qty3
        {
            get { return this.qty3; }
            set { this.qty3 = value; }
        }
        public DateTime DoDate
        {
            get { return this.doDate; }
            set { this.doDate = value; }
        }

        public string Des1
        {
            get { return this.des1; }
            set { this.des1 = value; }
        }

        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }

        public string PkgType
        {
            get { return this.pkgType; }
            set { this.pkgType = value; }
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
        private string processStatus;
        public string ProcessStatus
        {
            get { return this.processStatus; }
            set { this.processStatus = value; }
        }
        public string CustomsLot
        {
            get { return this.customsLot; }
            set { this.customsLot = value; }
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
        private string batchNo;
        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
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
        private int relaId;
        public int RelatId
        {
            get { return this.relaId; }
            set { this.relaId = value; }
        }
        private string packing;
        private bool isSch;
        private int lastSchQty1;
        private int lastSchQty2;
        private int lastSchQty3;
        private int preQty1;
        private int preQty2;
        private int preQty3;

        public string Packing
        {
            get { return this.packing; }
            set { this.packing = value; }
        }

        public bool IsSch
        {
            get { return this.isSch; }
            set { this.isSch = value; }
        }

        public int LastSchQty1
        {
            get { return this.lastSchQty1; }
            set { this.lastSchQty1 = value; }
        }

        public int LastSchQty2
        {
            get { return this.lastSchQty2; }
            set { this.lastSchQty2 = value; }
        }

        public int LastSchQty3
        {
            get { return this.lastSchQty3; }
            set { this.lastSchQty3 = value; }
        }

        public int PreQty1
        {
            get { return this.preQty1; }
            set { this.preQty1 = value; }
        }

        public int PreQty2
        {
            get { return this.preQty2; }
            set { this.preQty2 = value; }
        }

        public int PreQty3
        {
            get { return this.preQty3; }
            set { this.preQty3 = value; }
        }
        private string partyId;
        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }
        private string partyName;
        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
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
        private decimal size;
        public decimal Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        private string remark;
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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