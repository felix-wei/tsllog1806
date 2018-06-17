using System;

namespace C2
{
	public class JobProcess
	{
		private int id;
		private string jobNo;
		private int houseId;
		private DateTime dateEntry;
		private DateTime datePlan;
		private DateTime dateInspect;
        private DateTime dateProcess;
		private string lotNo;
		private string locationCode;
        private string processType;
        private string processStatus;
		private string specs1;
		private string specs2;
		private string specs3;
		private string specs4;
		private decimal qty;
		private decimal processQty1;
		private decimal processQty2;
		private decimal processQty3;
		private string remark1;
        private string remark2;
		private DateTime rowCreateTime;
		private string rowCreateUser;
		private DateTime rowUpdateTime;
		private string rowUpdateUser;

		public int Id
		{
			get { return this.id; }
		}

		public string JobNo
		{
			get { return this.jobNo; }
			set { this.jobNo = value; }
		}

		public int HouseId
		{
			get { return this.houseId; }
			set { this.houseId = value; }
		}

		public DateTime DateEntry
		{
			get { return this.dateEntry; }
			set { this.dateEntry = value; }
		}

		public DateTime DatePlan
		{
			get { return this.datePlan; }
			set { this.datePlan = value; }
		}

		public DateTime DateInspect
		{
			get { return this.dateInspect; }
			set { this.dateInspect = value; }
		}

		public DateTime DateProcess
		{
			get { return this.dateProcess; }
            set { this.dateProcess = value; }
		}

		public string LotNo
		{
			get { return this.lotNo; }
			set { this.lotNo = value; }
		}

		public string LocationCode
		{
			get { return this.locationCode; }
			set { this.locationCode = value; }
		}
        public string ProcessType
        {
            get { return this.processType; }
            set { this.processType = value; }
        }
        public string ProcessStatus
        {
            get { return this.processStatus; }
            set { this.processStatus = value; }
        }

		public string Specs1
		{
			get { return this.specs1; }
			set { this.specs1 = value; }
		}

		public string Specs2
		{
			get { return this.specs2; }
			set { this.specs2 = value; }
		}

		public string Specs3
		{
			get { return this.specs3; }
			set { this.specs3 = value; }
		}

		public string Specs4
		{
			get { return this.specs4; }
			set { this.specs4 = value; }
		}

		public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public decimal ProcessQty1
		{
			get { return this.processQty1; }
			set { this.processQty1 = value; }
		}

		public decimal ProcessQty2
		{
			get { return this.processQty2; }
			set { this.processQty2 = value; }
		}

		public decimal ProcessQty3
		{
			get { return this.processQty3; }
			set { this.processQty3 = value; }
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

		public DateTime RowCreateTime
		{
			get { return this.rowCreateTime; }
			set { this.rowCreateTime = value; }
		}

		public string RowCreateUser
		{
			get { return this.rowCreateUser; }
			set { this.rowCreateUser = value; }
		}

		public DateTime RowUpdateTime
		{
			get { return this.rowUpdateTime; }
			set { this.rowUpdateTime = value; }
		}

		public string RowUpdateUser
		{
			get { return this.rowUpdateUser; }
			set { this.rowUpdateUser = value; }
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
        private string inventoryId;
        public string InventoryId
        {
            get { return this.inventoryId; }
            set { this.inventoryId = value; }
        }
    }
}
