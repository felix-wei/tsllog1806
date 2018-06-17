using System;

namespace C2
{
    public class JobInventory
    {
        private int id;
        private string jobNo;
        private string itemNo;
        private string description;
        private int qty;
        private string unit;
        private string packing;
        private decimal weight;
        private decimal volume;
        private DateTime doDate;
        private string remark;
        private string createdBy;
        private DateTime createDateTime;
        private string updateBy;
        private DateTime updateDateTime;
        private string doType;

        public int Id
        {
            get { return this.id; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string ItemNo
        {
            get { return this.itemNo; }
            set { this.itemNo = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public int Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string Packing
        {
            get { return this.packing; }
            set { this.packing = value; }
        }

        public decimal Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { this.volume = value; }
        }

        public DateTime DoDate
        {
            get { return this.doDate; }
            set { this.doDate = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
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

        public string DoType
        {
            get { return this.doType; }
            set { this.doType = value; }
        }
        private string palletNo;
        public string PalletNo
        {
            get { return this.palletNo; }
            set { this.palletNo = value; }
        }
        private string location;
        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }
        private int balQty;
        public int BalQty
        {
            get { return this.balQty; }
            set { this.balQty = value; }
        }
        private string relaId;
        public string RelaId
        {
            get { return this.relaId; }
            set { this.relaId = value; }
        }
        public string FilePath
        {
            get
            {
                string sql_url = string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", id, this.jobNo);
                return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_url));
            }

        }
        public string FileType
        {
            get
            {
                string sql_url = string.Format(@"select FileType from Job_Attachment where JobNo='{0}' and RefNo='{1}'", id, this.jobNo);
                return SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_url));
            }

        }

       
        public string Path
        {
            get
            {
                if (this.FilePath != null && this.FilePath.Length > 0)
                    return "/Photos/" + this.FilePath.Replace("\\", "/");
                return "";
            }
        }
        public string ImgPath
        {
            get
            {
                if (this.FilePath != null && this.FilePath.Length > 0)
                {
                    if (this.FileType != null && this.FileType.ToLower() == "image")
                        return "/Photos/" + this.FilePath.Replace("\\", "/");
                    else
                        return "/images/File.jpg";
                }
                return "";
            }
        }
        public string JobStatus
        {
            get
            {
                string s = "USE";
                string sql = "select JobStatus from JobInfo where JobNo='" + this.jobNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
		
		 private string note1;
        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        private string note2;
        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        private string note3;
        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        private string note4;
        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }

        private string note5;
        public string Note5
        {
            get { return this.note5; }
            set { this.note5 = value; }
        }

        private string note6;
        public string Note6
        {
            get { return this.note6; }
            set { this.note6 = value; }
        }

        private string note7;
        public string Note7
        {
            get { return this.note7; }
            set { this.note7 = value; }
        }

        private string note8;
        public string Note8
        {
            get { return this.note8; }
            set { this.note8 = value; }
        }

        private DateTime date1;
        public DateTime Date1
        {
            get { return this.date1; }
            set { this.date1 = value; }
        }

        private DateTime date2;
        public DateTime Date2
        {
            get { return this.date2; }
            set { this.date2 = value; }
        }

        private DateTime date3;
        public DateTime Date3
        {
            get { return this.date3; }
            set { this.date3 = value; }
        }

        private DateTime date4;
        public DateTime Date4
        {
            get { return this.date4; }
            set { this.date4 = value; }
        }

        private DateTime date5;
        public DateTime Date5
        {
            get { return this.date5; }
            set { this.date5 = value; }
        }

        private DateTime date6;
        public DateTime Date6
        {
            get { return this.date6; }
            set { this.date6 = value; }
        }

        private DateTime date7;
        public DateTime Date7
        {
            get { return this.date7; }
            set { this.date7 = value; }
        }

        private DateTime date8;
        public DateTime Date8
        {
            get { return this.date8; }
            set { this.date8 = value; }
        }
		private string status1;
        public string Status1
        {
            get { return this.status1; }
            set { this.status1 = value; }
        }

        private string status2;
        public string Status2
        {
            get { return this.status2; }
            set { this.status2 = value; }
        }

        private string status3;
        public string Status3
        {
            get { return this.status3; }
            set { this.status3 = value; }
        }

        private string status4;
        public string Status4
        {
            get { return this.status4; }
            set { this.status4 = value; }
        }

        private string status5;
        public string Status5
        {
            get { return this.status5; }
            set { this.status5 = value; }
        }

        private string status6;
        public string Status6
        {
            get { return this.status6; }
            set { this.status6 = value; }
        }

        private string status7;
        public string Status7
        {
            get { return this.status7; }
            set { this.status7 = value; }
        }

        private string status8;
        public string Status8
        {
            get { return this.status8; }
            set { this.status8 = value; }
        }
    }
}
