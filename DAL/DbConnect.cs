using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbConnect : db_booxl_tsl_acEntities
    {
        public DbConnect()
        {
            this.Database.Log = log;
        }
        private void log(string par)
        {
            Console.WriteLine(par);
        }

        private static DbConnect dbConnect = null;
        //public static DbConnect Instance()
        //{
        //    if (dbConnect ==null)
        //    {
        //        dbConnect = new DbConnect();
        //    }
        //    return dbConnect;
        //}

        public static DbConnect Instance
        {
            get
            {
                if (dbConnect == null) { dbConnect = new DbConnect(); }
                return dbConnect;
            }
        }
    }
}
