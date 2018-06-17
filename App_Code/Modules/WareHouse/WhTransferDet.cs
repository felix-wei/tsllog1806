using System;

namespace C2
{
    public class WhTransferDet
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string transferNo;
        private string product;
        private string des1;
        private string fromWarehouseId;
        private string fromLocId;
        private string toWarehouseId;
        private string toLocId;
        private decimal price;
        private string lotNo;
        private int qty1;
        private int qty2;
        private int qty3;
        private string uom1;
        private string uom2;
        private string uom3;
        private string uom4;
        private int qtyPackWhole;
        private int qtyWholeLoose;
        private int qtyLooseBase;
        private string doInId;
        private string doOutId;
        private string packing;
        private string att1;
        private string att2;
        private string att3;
        private string att4;
        private string att5;
        private string att6;
        private decimal weight;

        public int Id
        {
            get { return this.id; }
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

        public string TransferNo
        {
            get { return this.transferNo; }
            set { this.transferNo = value; }
        }

        public string FromWarehouseId
        {
            get { return this.fromWarehouseId; }
            set { this.fromWarehouseId = value; }
        }

        public string FromLocId
        {
            get { return this.fromLocId; }
            set { this.fromLocId = value; }
        }

        public string ToWarehouseId
        {
            get { return this.toWarehouseId; }
            set { this.toWarehouseId = value; }
        }

        public string ToLocId
        {
            get { return this.toLocId; }
            set { this.toLocId = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }
        public string Des1
        {
            get { return this.des1; }
            set { this.des1 = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
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

        public int Qty3
        {
            get { return this.qty3; }
            set { this.qty3 = value; }
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
        public string Packing
        {
            get { return this.packing; }
            set { this.packing = value; }
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
            set { this.att5= value; }
        }
        public string Att6
        {
            get { return this.att6; }
            set { this.att6 = value; }
        }
        public string DoInId
        {
            get { return this.doInId; }
            set { this.doInId = value; }
        }
        public string DoOutId
        {
            get { return this.doOutId; }
            set { this.doOutId = value; }
        }
        public string StatusCode
        {
            get { return SafeValue.SafeString(ConnectSql.ExecuteScalar("select StatusCode from wh_transfer where transferNo='"+this.transferNo+"'")); }
        }
        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
    }
}
