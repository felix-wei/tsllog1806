using System;

namespace C2
{
    public class CtmJobStock
    {
        private int id;
        private string jobNo;
        private string stockStatus;
        private string stockDescription;
        private string stockMarking;
        private decimal stockQty;
        private string stockUnit;
        private decimal packingQty;
        private string packingUnit;
        private string packingDimention;
        private decimal weight;
        private decimal volume;

        public int Id
        {
            get { return this.id; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string StockStatus
        {
            get { return this.stockStatus; }
            set { this.stockStatus = value; }
        }

        public string StockDescription
        {
            get { return this.stockDescription; }
            set { this.stockDescription = value; }
        }

        public string StockMarking
        {
            get { return this.stockMarking; }
            set { this.stockMarking = value; }
        }

        public decimal StockQty
        {
            get { return this.stockQty; }
            set { this.stockQty = value; }
        }

        public string StockUnit
        {
            get { return this.stockUnit; }
            set { this.stockUnit = value; }
        }

        public decimal PackingQty
        {
            get { return this.packingQty; }
            set { this.packingQty = value; }
        }

        public string PackingUnit
        {
            get { return this.packingUnit; }
            set { this.packingUnit = value; }
        }

        public string PackingDimention
        {
            get { return this.packingDimention; }
            set { this.packingDimention = value; }
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
    }
}
