﻿using System;

namespace C2
{
    public class MobileChatGroup
    {
        private int id;
        private string groupName;
        private DateTime createDate;
        private string createBy;
        private string note1;
        private string note2;
        private string note3;
        private string note4;

        public int Id
        {
            get { return this.id; }
        }

        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }

        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }
    }
}