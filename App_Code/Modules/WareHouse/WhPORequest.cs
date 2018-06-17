using System;

namespace C2
{
    public class WhPORequest
    {
        private int id;
        private string poNo;
        private string soNo;
        private string product;
        private int qty;
        private decimal price;
        private string currency;
        private string unit;
        private string packing;
        private string location;
        private string remark;
        private string lotNo;
        private string batchNo;


        public int Id
        {
            get { return this.id; }
        }

        public string PoNo
        {
            get { return this.poNo; }
            set { this.poNo = value; }
        }

        public string SoNo
        {
            get { return this.soNo; }
            set { this.soNo = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
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

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string Packing
        {
            get { return this.packing; }
            set { this.packing = value; }
        }

        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }

        public string BatchNo
        {
            get { return this.batchNo; }
            set { this.batchNo = value; }
        }
        private string createBy;
        private string updateBy;
        private DateTime createDateTime;
        private DateTime updateDateTime;
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
        public string DoStatus
        {
            get
            {
                string s = "USE";
                string sql = "select DoStatus from Wh_Trans where DoNo='" + SafeValue.SafeString(this.soNo) + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "Draft");
                return s;
            }
        }


        private int qty1;
        private int qty2;
        private int qty3;
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
        private string des1;


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
        public string Des1
        {
            get { return this.des1; }
            set { this.des1 = value; }
        }
        private DateTime requestDateTime;
        public DateTime RequestDateTime
        {
            get { return this.requestDateTime; }
            set { this.requestDateTime = value; }
        }
    }
}
