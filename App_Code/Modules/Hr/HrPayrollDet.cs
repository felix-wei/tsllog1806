using System;
using System.Data;

namespace C2
{
    public class HrPayrollDet
    {
        private int id;
        private int payrollId;
        private string chgCode;
        private string description;
        private decimal amt;
        private string pic;

        public int Id
        {
            get { return this.id; }
        }

        public int PayrollId
        {
            get { return this.payrollId; }
            set { this.payrollId = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal Amt
        {
            get { return this.amt; }
            set { this.amt = value; }
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
        public string StatusCode
        {
            get
            {
                string s = "Draft";
                string sql = "select StatusCode from Hr_Payroll where Id='" + this.payrollId + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "Draft");
                return s;
            }
        }
        private decimal before;
        public decimal Before
        {
            get { return this.before; }
            set { this.before = value; }
        }


    }
}
