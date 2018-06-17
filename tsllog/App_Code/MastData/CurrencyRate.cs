using System;

namespace C2
{
	public class CurrencyRate
	{
		private int id;
		private string fromCurrencyId;
		private string toCurrencyId;
		private decimal exRate1;
		private decimal exRate2;
		private decimal exRate3;
        private string remark;
        private DateTime exRateDate;

		public int Id
		{
			get { return this.id; }
		}

		public string FromCurrencyId
		{
			get { return this.fromCurrencyId; }
			set { this.fromCurrencyId = value; }
		}

		public string ToCurrencyId
		{
			get { return this.toCurrencyId; }
			set { this.toCurrencyId = value; }
		}

		public decimal ExRate1
		{
			get { return this.exRate1; }
			set { this.exRate1 = value; }
		}

		public decimal ExRate2
		{
			get { return this.exRate2; }
			set { this.exRate2 = value; }
		}

		public decimal ExRate3
		{
			get { return this.exRate3; }
			set { this.exRate3 = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

        public DateTime ExRateDate
        {
            get { return this.exRateDate; }
            set { this.exRateDate = value; }
        }
	}
}
