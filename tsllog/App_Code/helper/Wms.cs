using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class Wms
    {
        public static string ProductName(string code)
        {
            return Safe.SafeString(Sql.One("Select Name from ref_product where code='"+code+"'"));
        }
        public static string ProductCountry(string code)
        {
            return Safe.SafeString(Sql.One("Select Country from ref_product where code='" + code + "'"));
        }
        public static string ProductAtt(string idx, string code)
        {
            return Safe.SafeString(Sql.One("Select Att"+idx+" from ref_product where code='"+code+"'"));
        }

    }
}