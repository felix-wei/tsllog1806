using System;

namespace C2
{
    public class CtmDriverMessage
    {
        private int id;
        private string sendTo;
        private DateTime sendDate;
        private string content;
        private string statusCode;
        private string createBy;
        private DateTime createDate;

        public int Id
        {
            get { return this.id; }
        }

        public string SendTo
        {
            get { return this.sendTo; }
            set { this.sendTo = value; }
        }

        public DateTime SendDate
        {
            get { return this.sendDate; }
            set { this.sendDate = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public string StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }
    }
}
