using System;

namespace C2
{
	public class SeaQuoteDet1
	{
		private int sequenceId;
		private string quoteId;
		private int quoteLineNo;
		private string chgCode;
		private string chgDes;
		private string currency;
		private decimal qty;
		private string gstType;
		private decimal gst;
		private decimal price;
		private string unit;
		private decimal amt;
		private decimal minAmt;
		private string rmk;

		public int SequenceId
		{
			get { return this.sequenceId; }
		}

		public string QuoteId
		{
			get { return this.quoteId; }
			set { this.quoteId = value; }
		}

		public int QuoteLineNo
		{
			get { return this.quoteLineNo; }
			set { this.quoteLineNo = value; }
		}

		public string ChgCode
		{
			get { return this.chgCode; }
			set { this.chgCode = value; }
		}

		public string ChgDes
		{
			get { return this.chgDes; }
			set { this.chgDes = value; }
		}

		public string Currency
		{
			get { return this.currency; }
			set { this.currency = value; }
		}

		public string GstType
		{
			get { return this.gstType; }
			set { this.gstType = value; }
		}

		public decimal Gst
		{
			get { return this.gst; }
			set { this.gst = value; }
		}

		public decimal Price
		{
			get { return this.price; }
			set { this.price = value; }
		}

		public decimal Qty
		{
			get { return this.qty; }
			set { this.qty = value; }
		}

		public string Unit
		{
			get { return this.unit; }
			set { this.unit = value; }
		}

		public decimal MinAmt
		{
			get { return this.minAmt; }
			set { this.minAmt = value; }
		}

		public decimal Amt
		{
			get { return this.amt; }
			set { this.amt = value; }
		}

		public string Rmk
		{
			get { return this.rmk; }
			set { this.rmk = value; }
		}

		private string groupTitle;
		public string GroupTitle
		{
			get { return this.groupTitle; }
			set { this.groupTitle = value; }
		}

		private string fclLclInd;
		public string FclLclInd
		{
			get { return this.fclLclInd; }
			set { this.fclLclInd = value; }
		}

		private decimal exRate;
		public decimal ExRate
		{
			get { return this.exRate; }
			set { this.exRate = value; }
		}

		private string impExpInd;
		public string ImpExpInd
		{
			get { return this.impExpInd; }
			set { this.impExpInd = value; }
		}

		private string pol;
		public string Pol
		{
			get { return this.pol; }
			set { this.pol = value; }
		}

		private string pod;
		public string Pod
		{
			get { return this.pod; }
			set { this.pod = value; }
		}

		private string partyTo;
		public string PartyTo
		{
			get { return this.partyTo; }
			set { this.partyTo = value; }
		}

		public string PartyToName
		{
			get
			{
				return EzshipHelper.GetPartyName(this.partyTo);
			}
		}

		private string agentId;
		public string AgentId
		{
			get { return this.agentId; }
			set { this.agentId = value; }
		}

		private string cngId;
		public string CngId
		{
			get { return this.cngId; }
			set { this.cngId = value; }
		}

		private string carrierId;
		public string CarrierId
		{
			get { return this.carrierId; }
			set { this.carrierId = value; }
		}

		private string shipType;
		public string ShipType
		{
			get { return this.shipType; }
			set { this.shipType = value; }
		}
		
		     private string status1;
        private string status2;
        private string status3;
        private string status4;
        private string status5;
        private string status6;
        private string status7;
        private string status8;
  

 

        public string Status1
        {
            get { return Helper.Safe.SafeString(this.status1); }
            set { this.status1 = Helper.Safe.SafeString(value); }
        }

        public string Status2
        {
            get { return Helper.Safe.SafeString(this.status2); }
            set { this.status2 = Helper.Safe.SafeString(value); }
        }

        public string Status3
        {
            get { return Helper.Safe.SafeString(this.status3); }
            set { this.status3 = Helper.Safe.SafeString(value); }
        }

        public string Status4
        {
            get { return Helper.Safe.SafeString(this.status4); }
            set { this.status4 = Helper.Safe.SafeString(value); }
        }

        public string Status5
        {
            get { return Helper.Safe.SafeString(this.status5); }
            set { this.status5 = Helper.Safe.SafeString(value); }
        }

        public string Status6
        {
            get { return Helper.Safe.SafeString(this.status6); }
            set { this.status6 = Helper.Safe.SafeString(value); }
        }

        public string Status7
        {
            get { return Helper.Safe.SafeString(this.status7); }
            set { this.status7 = Helper.Safe.SafeString(value); }
        }

        public string Status8
        {
            get { return Helper.Safe.SafeString(this.status8); }
            set { this.status8 = Helper.Safe.SafeString(value); }
        }


        private DateTime date1;
        private DateTime date2;
        private DateTime date3;
        private DateTime date4;
        private DateTime date5;
        private DateTime date6;
        private DateTime date7;
        private DateTime date8;
  

        private string _user_create;
        public string user_create
        {
            get { return Helper.Safe.SafeString(this._user_create); }
            set { this._user_create = Helper.Safe.SafeString(value); }
        }
        private DateTime _time_create;
        public DateTime time_create
        {
            get { return Helper.Safe.SafeDate(this._time_create); }
            set { this._time_create = Helper.Safe.SafeDate(value); }
        }
        private string _user_update;
        public string user_update
        {
            get { return Helper.Safe.SafeString(this._user_update); }
            set { this._user_update = Helper.Safe.SafeString(value); }
        }
        private DateTime _time_update;
        public DateTime time_update
        {
            get { return Helper.Safe.SafeDate(this._time_update); }
            set { this._time_update = Helper.Safe.SafeDate(value); }
        }
 

        public DateTime Date1
        {
            get { return Helper.Safe.SafeDate(this.date1); }
            set { this.date1 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date2
        {
            get { return Helper.Safe.SafeDate(this.date2); }
            set { this.date2 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date3
        {
            get { return Helper.Safe.SafeDate(this.date3); }
            set { this.date3 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date4
        {
            get { return Helper.Safe.SafeDate(this.date4); }
            set { this.date4 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date5
        {
            get { return Helper.Safe.SafeDate(this.date5); }
            set { this.date5 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date6
        {
            get { return Helper.Safe.SafeDate(this.date6); }
            set { this.date6 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date7
        {
            get { return Helper.Safe.SafeDate(this.date7); }
            set { this.date7 = Helper.Safe.SafeDate(value); }
        }

        public DateTime Date8
        {
            get { return Helper.Safe.SafeDate(this.date8); }
            set { this.date8 = Helper.Safe.SafeDate(value); }
        }

		  private Decimal rate1;
        private Decimal rate2;
        private Decimal rate3;
        private Decimal rate4;
        private Decimal rate5;
        private Decimal rate6;
        private Decimal rate7;
        private Decimal rate8;
        private Decimal rate9;
        private Decimal rate10;
		  private Decimal rate11;
        private Decimal rate12;
        private Decimal rate13;
        private Decimal rate14;
        private Decimal rate15;
        private Decimal rate16;
        private Decimal rate17;
        private Decimal rate18;
        private Decimal rate19;
  

 

        public Decimal Rate1
        {
            get { return Helper.Safe.SafeDecimal(this.rate1); }
            set { this.rate1 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate2
        {
            get { return Helper.Safe.SafeDecimal(this.rate2); }
            set { this.rate2 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate3
        {
            get { return Helper.Safe.SafeDecimal(this.rate3); }
            set { this.rate3 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate4
        {
            get { return Helper.Safe.SafeDecimal(this.rate4); }
            set { this.rate4 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate5
        {
            get { return Helper.Safe.SafeDecimal(this.rate5); }
            set { this.rate5 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate6
        {
            get { return Helper.Safe.SafeDecimal(this.rate6); }
            set { this.rate6 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate7
        {
            get { return Helper.Safe.SafeDecimal(this.rate7); }
            set { this.rate7 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate8
        {
            get { return Helper.Safe.SafeDecimal(this.rate8); }
            set { this.rate8 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate9
        {
            get { return Helper.Safe.SafeDecimal(this.rate9); }
            set { this.rate9 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate10
        {
            get { return Helper.Safe.SafeDecimal(this.rate10); }
            set { this.rate10 = Helper.Safe.SafeDecimal(value); }
        }
		
        public Decimal Rate11
        {
            get { return Helper.Safe.SafeDecimal(this.rate11); }
            set { this.rate11 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate12
        {
            get { return Helper.Safe.SafeDecimal(this.rate12); }
            set { this.rate12 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate13
        {
            get { return Helper.Safe.SafeDecimal(this.rate13); }
            set { this.rate13 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate14
        {
            get { return Helper.Safe.SafeDecimal(this.rate14); }
            set { this.rate14 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate15
        {
            get { return Helper.Safe.SafeDecimal(this.rate15); }
            set { this.rate15 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate16
        {
            get { return Helper.Safe.SafeDecimal(this.rate16); }
            set { this.rate16 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate17
        {
            get { return Helper.Safe.SafeDecimal(this.rate17); }
            set { this.rate17 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate18
        {
            get { return Helper.Safe.SafeDecimal(this.rate18); }
            set { this.rate18 = Helper.Safe.SafeDecimal(value); }
        }

        public Decimal Rate19
        {
            get { return Helper.Safe.SafeDecimal(this.rate19); }
            set { this.rate19 = Helper.Safe.SafeDecimal(value); }
        }
    }
}
