using System;

namespace C2
{
	public class JobReceipt
	{
		private int id;
		private int trailerId;
		private string trailerNo;
		private int cargoId;
		private string jobNo;
		private int lineId;
		private string bookingNo;
		private string hblNo;
		private string skuCode;
		private string uomCode;
		private decimal weight;
		private decimal volume;
		private decimal qty;
		private decimal packQty;
		private string location;
		private string cargoType;
		private string marking1;
		private string marking2;
		private string remark1;
		private string remark2;

		public int Id
		{
			get { return this.id; }
		}

		public int TrailerId
		{
			get { return this.trailerId; }
			set { this.trailerId = value; }
		}

		public string TrailerNo
		{
			get { return this.trailerNo; }
			set { this.trailerNo = value; }
		}

		public int CargoId
		{
			get { return this.cargoId; }
			set { this.cargoId = value; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
		}

		public int LineId
		{
			get { return this.lineId; }
			set { this.lineId = value; }
		}

		public string BookingNo
		{
			get { return this.bookingNo; }
			set { this.bookingNo = value; }
		}

		public string HblNo
		{
			get { return this.hblNo; }
			set { this.hblNo = value; }
		}

		public string SkuCode
		{
			get { return this.skuCode; }
			set { this.skuCode = value; }
		}

		public string UomCode
		{
			get { return this.uomCode; }
			set { this.uomCode = value; }
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

		public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public decimal PackQty
		{
			get { return this.packQty; }
			set { this.packQty = value; }
		}

		public string Location
		{
			get { return this.location; }
			set { this.location = value; }
		}

		public string CargoType
		{
			get { return this.cargoType; }
			set { this.cargoType = value; }
		}

		public string Marking1
		{
			get { return this.marking1; }
			set { this.marking1 = value; }
		}

		public string Marking2
		{
			get { return this.marking2; }
			set { this.marking2 = value; }
		}

		public string Remark1
		{
			get { return this.remark1; }
			set { this.remark1 = value; }
		}

		public string Remark2
		{
			get { return this.remark2; }
			set { this.remark2 = value; }
		}
	}
}
