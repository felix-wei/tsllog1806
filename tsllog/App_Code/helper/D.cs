using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public static class D
{
        public static DataTable List(string sql)
        {
            return List(sql, "local");
        }

        public static string Test()
        {
			string url = HttpContext.Current.Request.Url.Host;
			return url;
        }
		
		 
        public static object Exec(string sql)
        {
            return Exec(sql, "local");
        }

        public static object Exec(SqlCommand cmd)
        {
            return Exec(cmd, "local");
        }
		
		
        public static object One(string sql)
        {
            return One(sql, "local");
        }

        public static int Count(string sql)
        {
            return Helper.Safe.SafeInt(One(sql, "local"));
        }

        public static string Text(string sql)
        {
            return Helper.Safe.SafeString(One(sql, "local"));
        }

        public static decimal Dec(string sql)
        {
            return S.Decimal(One(sql, "local"));
        }

        
        public static DataTable List(string sql,  string source)
        {
            string _source = "live";
            if(source!="")
                _source = source;
            DataTable tb = new DataTable();
            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_source].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            sqlConn.Close();
            return tb;
        }
		
        public static object Exec(SqlCommand cmd, string source)
        {
            string _source = "live";
            if (source != "")
                _source = source;
            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_source].ConnectionString);
            cmd.Connection = sqlConn;
            sqlConn.Open();
            object o = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return o;
        }


        public static object Exec(string sql, string source)
        {
            string _source = "live";
            if (source != "")
                _source = source;
            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_source].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            sqlConn.Open();
            object o = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return o;
        }

        public static object One(string sql, string source)
        {
            string _source = "live";
            if (source != "")
                _source = source;
            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_source].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            sqlConn.Open();
            object o = cmd.ExecuteScalar();
            sqlConn.Close();
            return o;
        }
    }
 