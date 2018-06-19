using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Class1
    {
        public void show(Model.CTM_JobEventLog par)
        {
            DAL.DataBase.CTM_JobEventLog ev = DAL.Tool.Convert.DeserializeByJson<DAL.DataBase.CTM_JobEventLog>(par);
            DAL.ConnectDb.Instance.CTM_JobEventLog.Add(ev);
            DAL.ConnectDb.Instance.SaveChanges();
        }
    }
}
