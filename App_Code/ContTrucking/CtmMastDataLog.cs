using System;

namespace C2
{
    public class CtmMastDataLog
    {
        private int id;
        private int mastId;
        private string eventCode;
        private DateTime eventDate;
        private string remark;

        public int Id
        {
            get { return this.id; }
        }

        public int MastId
        {
            get { return this.mastId; }
            set { this.mastId = value; }
        }

        public string EventCode
        {
            get { return this.eventCode; }
            set { this.eventCode = value; }
        }

        public DateTime EventDate
        {
            get { return this.eventDate; }
            set { this.eventDate = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }


        public static bool addBorrow(CtmMastData mast)
        {
            bool res = true;
            CtmMastDataLog data = new C2.CtmMastDataLog();
            data.EventDate = DateTime.Now;
            data.MastId = mast.Id;
            //======== is self
            if (mast.Note2.Equals("N"))
            {
                data.EventCode = "Borrow";
                mast.Type1 = "Active";
                C2.Manager.ORManager.StartTracking(mast, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(mast);
            }
            else
            {
                data.EventCode = "Lend";
            }

            C2.Manager.ORManager.StartTracking(data, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(data);
            return res;
        }
        public static bool addReturn(CtmMastData mast)
        {
            bool res = true;
            CtmMastDataLog data = new C2.CtmMastDataLog();
            data.EventDate = DateTime.Now;
            data.MastId = mast.Id;
            //======== is self
            if (mast.Note2.Equals("N"))
            {
                data.EventCode = "BorrowReturn";
                mast.Type1 = "InActive";
                C2.Manager.ORManager.StartTracking(mast, Wilson.ORMapper.InitialState.Updated);
                C2.Manager.ORManager.PersistChanges(mast);
            }
            else
            {
                data.EventCode = "LendReturn";
            }
            C2.Manager.ORManager.StartTracking(data, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(data);
            return res;
        }
    }
}
