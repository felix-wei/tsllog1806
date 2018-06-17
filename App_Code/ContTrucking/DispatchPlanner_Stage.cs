using System;

namespace C2
{
    public class CtmDispatchPlannerStage
    {
        private int id;
        private string stage;
        private int parId;
        private int sortIndex;

        public int Id
        {
            get { return this.id; }
        }

        public string Stage
        {
            get { return this.stage; }
            set { this.stage = value; }
        }

        public int ParId
        {
            get { return this.parId; }
            set { this.parId = value; }
        }

        public int SortIndex
        {
            get { return this.sortIndex; }
            set { this.sortIndex = value; }
        }
    }
}
