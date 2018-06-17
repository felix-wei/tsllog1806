using System;

namespace C2
{
    public class JobMCST
    {
        private int id;
        private string refNo;
        private string mcstNo;
        private DateTime mcstDate1;
        private DateTime mcstDate2;
        private string state;
        private string mcstRemark1;
        private string mcstRemark2;
        private decimal amount1;
        private decimal amount2;

        public int Id
        {
            get { return this.id; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }

        public string McstNo
        {
            get { return this.mcstNo; }
            set { this.mcstNo = value; }
        }

        public DateTime McstDate1
        {
            get { return this.mcstDate1; }
            set { this.mcstDate1 = value; }
        }

        public DateTime McstDate2
        {
            get { return this.mcstDate2; }
            set { this.mcstDate2 = value; }
        }

        public string States
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public string McstRemark1
        {
            get { return this.mcstRemark1; }
            set { this.mcstRemark1 = value; }
        }

        public string McstRemark2
        {
            get { return this.mcstRemark2; }
            set { this.mcstRemark2 = value; }
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
        private string condoTel;
        private string mcstRemark3;
        private string createBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        public string CondoTel
        {
            get { return this.condoTel; }
            set { this.condoTel = value; }
        }

        public string McstRemark3
        {
            get { return this.mcstRemark3; }
            set { this.mcstRemark3 = value; }
        }

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
    }
}
