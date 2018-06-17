using System;

namespace C2
{
	public class JobStock
	{
		private int id;
		private string refNo;
		private string jobNo;
		private string contNo;
		private int lineId;
		private string marks1;
		private string marks2;
		private string desc1;
		private string desc2;
		private int qty1;
		private int qty2;
		private string uom1;
		private string uom2;
		private decimal price1;
		private decimal price2;
		private decimal amount1;
		private decimal amount2;
		private decimal weight;
		private decimal cbm;
		private string dimension;
		private decimal w1;
		private decimal h1;
		private decimal l1;
		private decimal w2;
		private decimal h2;
		private decimal l2;

		public int Id
		{
			get { return this.id; }
		}

		public string RefNo
		{
			get { return this.refNo; }
			set { this.refNo = value; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
		}

		public string ContNo
		{
			get { return this.contNo; }
			set { this.contNo = value; }
		}

		public int LineId
		{
			get { return this.lineId; }
			set { this.lineId = value; }
		}

		public string Marks1
		{
			get { return this.marks1; }
			set { this.marks1 = value; }
		}

		public string Marks2
		{
			get { return this.marks2; }
			set { this.marks2 = value; }
		}

		public string Desc1
		{
			get { return this.desc1; }
			set { this.desc1 = value; }
		}

		public string Desc2
		{
			get { return this.desc2; }
			set { this.desc2 = value; }
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

		public decimal Price1
		{
			get { return this.price1; }
			set { this.price1 = value; }
		}

		public decimal Price2
		{
			get { return this.price2; }
			set { this.price2 = value; }
		}

		public decimal Amount1
		{
			get { return this.amount1; }
			set { this.amount1 = value; }
		}

		public decimal Amount2
		{
			get { return this.amount2; }
			set { this.amount2 = value; }
		}

		public decimal Weight
		{
			get { return this.weight; }
			set { this.weight = value; }
		}

		public decimal Cbm
		{
			get { return this.cbm; }
			set { this.cbm = value; }
		}

		public string Dimension
		{
			get { return this.dimension; }
			set { this.dimension = value; }
		}

		public decimal W1
		{
			get { return this.w1; }
			set { this.w1 = value; }
		}

		public decimal H1
		{
			get { return this.h1; }
			set { this.h1 = value; }
		}

		public decimal L1
		{
			get { return this.l1; }
			set { this.l1 = value; }
		}

		public decimal W2
		{
			get { return this.w2; }
			set { this.w2 = value; }
		}

		public decimal H2
		{
			get { return this.h2; }
			set { this.h2 = value; }
		}

		public decimal L2
		{
			get { return this.l2; }
			set { this.l2 = value; }
		}

        private int orderId;
        public int OrderId
        {
            get { return this.orderId; }
            set { this.orderId = value; }
        }
        private int sortIndex;
        public int SortIndex
        {
            get { return this.sortIndex; }
            set { this.sortIndex = value; }
        }
        private string pipeNo;
        public string PipeNo
        {
            get { return this.pipeNo; }
            set { this.pipeNo = value; }
        }
        private string hintNo;
        public string HintNo
        {
            get { return this.hintNo; }
            set { this.hintNo = value; }
        }
    
	}
}
