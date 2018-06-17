using System;

namespace C2
{
    public class XXCurrency
    {
        private string currencyId;
        private string currencyName;
        private decimal currencyExRate;

        public string CurrencyId
        {
            get { return this.currencyId; }
            set { this.currencyId = value; }
        }

        public string CurrencyName
        {
            get { return this.currencyName; }
            set { this.currencyName = value; }
        }

        public decimal CurrencyExRate
        {
            get { return this.currencyExRate; }
            set { this.currencyExRate = value; }
        }
    }
}
