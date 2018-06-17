using System;
using System.Data;

namespace C2
{
	public class HrPayroll
	{
		private int id;
        private DateTime date;
		private int person;
        private DateTime fromDate;
        private DateTime toDate;
		private decimal amt;
		private string remark;
		private string term;
		private string pic;

		public int Id
		{
			get { return this.id; }
		}

        public DateTime Date
		{
			get { return this.date; }
			set { this.date = value; }
		}

        public int Person
		{
			get { return this.person; }
			set { this.person = value; }
		}

        public DateTime FromDate
		{
			get { return this.fromDate; }
			set { this.fromDate = value; }
		}

        public DateTime ToDate
		{
			get { return this.toDate; }
			set { this.toDate = value; }
		}

		public decimal Amt
		{
			get { return this.amt; }
			set { this.amt = value; }
		}

		public string Remark
		{
			get { return this.remark; }
			set { this.remark = value; }
		}

		public string Term
		{
			get { return this.term; }
			set { this.term = value; }
		}

		public string Pic
		{
			get { return this.pic; }
			set { this.pic = value; }
        }

        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }
        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }
        public string UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }
        public DateTime UpdateDateTime
        {
            get { return this.updateDateTime; }
            set { this.updateDateTime = value; }
        }

        //add 2014-1-23 10:20:14
        private string statusCode;
        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }
        //add 2018-3-4 10:20:14
        private string autoInd;
        public string AutoInd
        {
            get { return this.autoInd; }
            set { this.autoInd = value; }
        }

        public static void UpdateMaster(int payrollId)
        {
            decimal total = 0;
            string sql = string.Format(@"select Amt from Hr_PayrollDet where PayrollId={0}", payrollId);
            DataTable dt = ConnectSql_mb.GetDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal amt = SafeValue.SafeDecimal(dt.Rows[i]["Amt"]);
                total += amt;
            }

            sql = string.Format(@"update Hr_Payroll set Amt={1} where Id={0}", payrollId, total);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
		
		decimal total1;
		decimal total2;
		decimal cpf0;
		decimal cpf1;
		decimal cpf2;
		decimal cpf1Amt;
		decimal cpf2Amt;

		public decimal Total1
		{
			get { return this.total1; }
			set { this.total1 = value; }
		}

		public decimal Total2
		{
			get { return this.total2; }
			set { this.total2 = value; }
		}		
		
		public decimal Cpf0
		{
			get { return this.cpf0; }
			set { this.cpf0 = value; }
		}

		public decimal Cpf1
		{
			get { return this.cpf1; }
			set { this.cpf1 = value; }
		}

		public decimal Cpf2
		{
			get { return this.cpf2; }
			set { this.cpf2 = value; }
		}
		
		public decimal Cpf1Amt
		{
			get { return this.cpf1Amt; }
			set { this.cpf1Amt = value; }
		}

		public decimal Cpf2Amt
		{
			get { return this.cpf2Amt; }
			set { this.cpf2Amt = value; }
		}		
		
	}
}
