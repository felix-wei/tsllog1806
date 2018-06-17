using System;

namespace C2
{
    public class WhContractDet
    {
        private int id;
        private string contractNo;
        private string productCode;
        private string productClass;
        private bool isFixed;
        private bool isYearly;
        private bool isMonthly;
        private bool isWeekly;
        private bool isDaily;
        private int dailyNo;

        public int Id
        {
            get { return this.id; }
        }

        public string ContractNo
        {
            get { return this.contractNo; }
            set { this.contractNo = value; }
        }

        public string ProductCode
        {
            get { return this.productCode; }
            set { this.productCode = value; }
        }
        public string ProductClass
        {
            get { return this.productClass; }
            set { this.productClass = value; }
        }
        public bool IsFixed
        {
            get { return this.isFixed; }
            set { this.isFixed = value; }
        }

        public bool IsYearly
        {
            get { return this.isYearly; }
            set { this.isYearly = value; }
        }

        public bool IsMonthly
        {
            get { return this.isMonthly; }
            set { this.isMonthly = value; }
        }
        public bool IsWeekly
        {
            get { return this.isWeekly; }
            set { this.isWeekly = value; }
        }

        //private decimal yearlyFee;
        //private decimal monthlyFee;
        //private decimal weeklyFee;
        //private decimal dailyFee;
        //public decimal YearlyFee
        //{
        //    get { return this.yearlyFee; }
        //    set { this.yearlyFee = value; }
        //}

        //public decimal MonthlyFee
        //{
        //    get { return this.monthlyFee; }
        //    set { this.monthlyFee = value; }
        //}


        //public decimal WeeklyFee
        //{
        //    get { return this.weeklyFee; }
        //    set { this.weeklyFee = value; }
        //}
        //public decimal DailyFee
        //{
        //    get { return this.dailyFee; }
        //    set { this.dailyFee = value; }
        //}

        public bool IsDaily
        {
            get { return this.isDaily; }
            set { this.isDaily = value; }
        }

        public int DailyNo
        {
            get { return this.dailyNo; }
            set { this.dailyNo = value; }
        }
        public string SchStr
        {
            get
            {
                if (isYearly)
                    return "Yearly";
                else if (isMonthly)
                    return "Monthly";
                else if (isWeekly)
                    return "Weekly";
                else if (isDaily)
                    return "Per " + this.dailyNo.ToString() + " Days" ;
                return "";
            }
        }

        public string StatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from wh_Contract where ContractNo='" + this.contractNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private decimal price1;
        public decimal Price1
        {
            get { return this.price1; }
            set { this.price1 = value; }
        }
        private decimal price2;
        public decimal Price2
        {
            get { return this.price2; }
            set { this.price2 = value; }
        }
        private decimal price3;
        public decimal Price3
        {
            get { return this.price3; }
            set { this.price3 = value; }
        }
        private decimal handlingFee;
        public decimal HandlingFee
        {
            get { return this.handlingFee; }
            set { this.handlingFee = value; }
        }
        private decimal storageFee;
        public decimal StorageFee
        {
            get { return this.storageFee; }
            set { this.storageFee = value; }
        }
        private string chgCode;
        public string ChgCode {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }
        private string unit;
        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }
        private string storageType;
        public string StorageType
        {
            get { return this.storageType; }
            set { this.storageType = value; }
        }
    }
}
