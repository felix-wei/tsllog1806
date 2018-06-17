using System;

namespace C2
{
    public class Dimension
	{
		private int id;
        private int houseId;
		private decimal qty;
		private string uom;
		private decimal weight;
		private string weightUnit;
		private decimal width;
		private string widthUnit;
		private decimal height;
		private string heightUnit;
		private decimal length;
		private string lengthUnit;
		private decimal volume;
		private string volumeUnit;
		private string remark;
        private string rowCreateUser;
        private DateTime rowCreateTime;
        private string rowUpdateUser;
        private DateTime rowUpdateTime;
        private string description;

		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}
        public int HouseId
        {
            get { return this.houseId; }
            set { this.houseId = value; }
        }

		public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public string Uom
		{
			get { return this.uom; }
			set { this.uom = value; }
		}

		public decimal Weight
		{
			get { return this.weight; }
			set { this.weight = value; }
		}

		public string WeightUnit
		{
			get { return this.weightUnit; }
			set { this.weightUnit = value; }
		}

		public decimal Width
		{
			get { return this.width; }
			set { this.width = value; }
		}

		public string WidthUnit
		{
			get { return this.widthUnit; }
			set { this.widthUnit = value; }
		}

		public decimal Height
		{
			get { return this.height; }
			set { this.height = value; }
		}

		public string HeightUnit
		{
			get { return this.heightUnit; }
			set { this.heightUnit = value; }
		}

		public decimal Length
		{
			get { return this.length; }
			set { this.length = value; }
		}

		public string LengthUnit
		{
			get { return this.lengthUnit; }
			set { this.lengthUnit = value; }
		}

		public decimal Volume
		{
			get { return this.volume; }
			set { this.volume = value; }
		}

		public string VolumeUnit
		{
			get { return this.volumeUnit; }
			set { this.volumeUnit = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
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

        private decimal totalWt;
        private decimal totalVol;
        public decimal TotalWt
        {
            get { return this.totalWt; }
            set { this.totalWt = value; }
        }
        public decimal TotalVol
        {
            get { return this.totalVol; }
            set { this.totalVol = value; }
        }

        private decimal skuQty;
        public decimal SkuQty
        {
            get { return this.skuQty; }
            set { this.skuQty = value; }
        }
        private decimal packQty;
        public decimal PackQty
        {
            get { return this.packQty; }
            set { this.packQty = value; }
        }
        private decimal wholeQty;
        public decimal WholeQty
        {
            get { return this.wholeQty; }
            set { this.wholeQty = value; }
        }

        private decimal looseQty;
        public decimal LooseQty
        {
            get { return this.looseQty; }
            set { this.looseQty = value; }
        }
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        private decimal singleWeight;
        public decimal SingleWeight
        {
           
            get {
                string sql = string.Format(@"select sum(SkuQty) from Dimension where HouseId={0}", this.houseId);
                decimal totalQty = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                decimal res =0;
                if (totalQty > 0)
                {
                    sql = string.Format(@"select sum(TotalWt) from Dimension where HouseId={0}", this.houseId);
                    decimal totalWt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                    res = SafeValue.ChinaRound((totalWt / totalQty) * this.skuQty, 3);
                }
                return res;

            }
            set {
                string sql = string.Format(@"select sum(SkuQty) from Dimension where HouseId={0}", this.houseId);
                decimal totalQty = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                decimal res=0;
                if (totalQty > 0)
                {
                    sql = string.Format(@"select sum(TotalWt) from Dimension where HouseId={0}", this.houseId);
                    decimal totalWt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                    res = SafeValue.ChinaRound((totalWt / totalQty) * this.skuQty, 3);
                    
                }
                this.singleWeight = res;
            }

        }
        public static decimal get_weight(int houseId, decimal skuQty)
        {
            string sql = string.Format(@"select sum(SkuQty) from Dimension where HouseId={0}", houseId);
            decimal totalQty = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));

            sql = string.Format(@"select sum(TotalWt) from Dimension where HouseId={0}", houseId);
            decimal totalWt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
            decimal res = SafeValue.ChinaRound((totalWt / totalQty) * skuQty, 3);
            return res;
        }

        private string pipeNo;
        public string PipeNo
        {
            get { return this.pipeNo; }
            set { this.pipeNo = value; }
        }
        private string heatNo;
        public string HeatNo
        {
            get { return this.heatNo; }
            set { this.heatNo = value; }
        }
        private string typeCode;
        public string TypeCode
        {
            get { return this.typeCode; }
            set { this.typeCode = value; }
        }
    }
}
