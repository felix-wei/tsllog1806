using System;

namespace C2
{
    public class HelperAuthority
    {
        private int id;
        private string frame;
        private string control;
        private string controlType;
        private string status;
        private string role;
        private string isHid;
        private string remark;
        private DateTime createDate;

        public int Id
        {
            get { return this.id; }
        }

        public string Frame
        {
            get { return this.frame; }
            set { this.frame = value; }
        }

        public string Control
        {
            get { return this.control; }
            set { this.control = value; }
        }

        public string ControlType
        {
            get { return this.controlType; }
            set { this.controlType = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        public string IsHid
        {
            get { return this.isHid; }
            set { this.isHid = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }
    }
}
