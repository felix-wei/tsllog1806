using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectDb:DataBase.Entities
    {
        public ConnectDb()
        {
            this.Database.Log = log;
        }
        private void log(string par)
        {
            Console.WriteLine(par);
        }

        private static ConnectDb dbConnect = null;

        public static ConnectDb Instance
        {
            get
            {
                if (dbConnect == null) { dbConnect = new ConnectDb(); }
                return dbConnect;
            }
        }
    }
}
