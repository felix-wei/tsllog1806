using System;

namespace C2
{
    public class StockCountDet
    {
        private int id;
        private string partyId;
        private string partyName;
        private string partyAdd;
        private string doNo;
        private DateTime doDate;
        private string wareHouseId;
        private string product;
        private string lotNo;
        private string description;
        private string remark;
        private string palletNo;
        private decimal qty1;
        private decimal qty2;
        private decimal qty3;
        private decimal newQty;
        private decimal price;
        private decimal newPrice;
        private string packing;
        private string location;
        private string uom;
        private string att1;
        private string att2;
        private string att3;
        private string att4;
        private string att5;
        private string att6;
        private DateTime expiryDate;
        private decimal grossWeight;
        private decimal nettWeight;

        public int Id
        {
            get { return this.id; }
        }
        public string PartyId
        {
            get { return this.partyId; }
            set { this.partyId = value; }
        }

        public string PartyName
        {
            get { return this.partyName; }
            set { this.partyName = value; }
        }

        public string PartyAdd
        {
            get { return this.partyAdd; }
            set { this.partyAdd = value; }
        }
        public string DoNo
        {
            get { return this.doNo; }
            set { this.doNo = value; }
        }
        public DateTime DoDate
        {
            get { return this.doDate; }
            set { this.doDate = value; }
        }
        public string WareHouseId
        {
            get { return this.wareHouseId; }
            set { this.wareHouseId = value; }
        }

        public string Product
        {
            get { return this.product; }
            set { this.product = value; }
        }

        public string LotNo
        {
            get { return this.lotNo; }
            set { this.lotNo = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        public string PalletNo
        {
            get { return this.palletNo; }
            set { this.palletNo = value; }
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
        public decimal Qty3
        {
            get { return this.qty3; }
            set { this.qty3 = value; }
        }

        public decimal NewQty
        {
            get { return this.newQty; }
            set { this.newQty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public decimal NewPrice
        {
            get { return this.newPrice; }
            set { this.newPrice = value; }
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

        public string Uom
        {
            get { return this.uom; }
            set { this.uom = value; }
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

        public DateTime ExpiryDate
        {
            get { return this.expiryDate; }
            set { this.expiryDate = value; }
        }

        private string refNo;
        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
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
