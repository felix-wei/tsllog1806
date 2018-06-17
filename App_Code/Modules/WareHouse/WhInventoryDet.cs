using System;

namespace C2
{
    public class WhInventoryDet
    {
        private int id;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string inventoryNo;
        private int lineINo;
        private string product;
        private string warehouseId;
        private string locId;
        private int qty;
        private int adjustQty;
        private string unit;

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

        public string InventoryNo
        {
            get { return this.inventoryNo; }
            set { this.inventoryNo = value; }
        }

        public int LineINo
        {
            get { return this.lineINo; }
            set { this.lineINo = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string WarehouseId
        {
            get { return this.warehouseId; }
            set { this.warehouseId = value; }
        }

        public string LocId
        {
            get { return this.locId; }
            set { this.locId = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public int AdjustQty
        {
            get { return this.adjustQty; }
            set { this.adjustQty = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }
        private string lotNo;
        private string batchNo;
        private string customsLotNo;
        private int qty1;
        private int qty2;
        private int qty3;
        private string uom1;
        private string uom2;
        private string uom3;
        private int qtyPackWhole;
        private int qtyWholeLoose;
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

        public string CustomsLotNo
        {
            get { return this.customsLotNo; }
            set { this.customsLotNo = value; }
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
    }
}
