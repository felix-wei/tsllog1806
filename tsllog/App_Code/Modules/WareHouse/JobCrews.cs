using System;

namespace C2
{
    public class JobCrews
    {
        private int id;
        private string refNo;
        private string code;
        private string name;
        private string tel;
        private string status;
        private string address;
        private string contact;
        private string email;
        private string postalcode;
        private string remark;
        private DateTime jobTime;
        private DateTime payDate;
        private decimal workHour;
        private decimal otHour;
        private decimal payTotal;
        private string payNote;
        private decimal amount1;
        private decimal amount2;
        private decimal amount3;
        private decimal amount4;
        private decimal amount5;
        private decimal amount6;
        private decimal amount7;
        private decimal amount8;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Postalcode
        {
            get { return this.postalcode; }
            set { this.postalcode = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public DateTime JobTime
        {
            get { return this.jobTime; }
            set { this.jobTime = value; }
        }

        public DateTime PayDate
        {
            get { return this.payDate; }
            set { this.payDate = value; }
        }

        public decimal WorkHour
        {
            get { return this.workHour; }
            set { this.workHour = value; }
        }

        public decimal OtHour
        {
            get { return this.otHour; }
            set { this.otHour = value; }
        }

        public decimal PayTotal
        {
            get { return this.payTotal; }
            set { this.payTotal = value; }
        }

        public string PayNote
        {
            get { return this.payNote; }
            set { this.payNote = value; }
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

        public decimal Amount3
        {
            get { return this.amount3; }
            set { this.amount3 = value; }
        }

        public decimal Amount4
        {
            get { return this.amount4; }
            set { this.amount4 = value; }
        }

        public decimal Amount5
        {
            get { return this.amount5; }
            set { this.amount5 = value; }
        }

        public decimal Amount6
        {
            get { return this.amount6; }
            set { this.amount6 = value; }
        }

        public decimal Amount7
        {
            get { return this.amount7; }
            set { this.amount7 = value; }
        }

        public decimal Amount8
        {
            get { return this.amount8; }
            set { this.amount8 = value; }
        }
        public decimal OverTimeValue
        {
            get
            {
                int value = 0;
                if (status == "Supervisor" || status == "Crew")
                {
                    value=5;
                }
                return value;
            }
        }
        public string JobStatus
        {
            get
            {
                string s = "USE";
                string sql = "select JobStatus from JobInfo where JobNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        private string isPay;
        public string IsPay
        {
            get { return this.isPay; }
            set { this.isPay = value; }
        }
        private string hrRole;
        public string HrRole
        {
            get { return this.hrRole; }
            set { this.hrRole = value; }
        }
        #region no use
        //private string chgCode1;
        //private string chgCode2;
        //private string chgCode3;
        //private string chgCode4;
        //private string chgCode5;
        //private string chgCode6;
        //private string chgCode7;
        //private string chgCode8;

        //public string ChgCode1
        //{
        //    get { return this.chgCode1; }
        //    set { this.chgCode1 = value; }
        //}

        //public string ChgCode2
        //{
        //    get { return this.chgCode2; }
        //    set { this.chgCode2 = value; }
        //}

        //public string ChgCode3
        //{
        //    get { return this.chgCode3; }
        //    set { this.chgCode3 = value; }
        //}

        //public string ChgCode4
        //{
        //    get { return this.chgCode4; }
        //    set { this.chgCode4 = value; }
        //}

        //public string ChgCode5
        //{
        //    get { return this.chgCode5; }
        //    set { this.chgCode5 = value; }
        //}

        //public string ChgCode6
        //{
        //    get { return this.chgCode6; }
        //    set { this.chgCode6 = value; }
        //}

        //public string ChgCode7
        //{
        //    get { return this.chgCode7; }
        //    set { this.chgCode7 = value; }
        //}

        //public string ChgCode8
        //{
        //    get { return this.chgCode8; }
        //    set { this.chgCode8 = value; }
        //}
        #endregion
    }
}
