using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Class1
    {
        public void test()
        {
            CTM_JobEventLog temp = new CTM_JobEventLog();
            ConnectDb.Instance.CTM_JobEventLog.Add(temp);
            ConnectDb.Instance.SaveChanges();
        }
    }
}
