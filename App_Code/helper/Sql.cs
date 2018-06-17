using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class Sql
    {
        public static DataSet Set(string sql)
        {
            return Set(sql, "local");
        }

        public static DataTable List(string sql)
        {
            return List(sql, "local");
        }

        public static object Exec(string sql)
        {
            return Exec(sql, "local");
        }

        public static object One(string sql)
        {
            return One(sql, "local");
        }

        public static string Text(string sql)
        {
            return Helper.Safe.SafeString(One(sql, "local"));
        }
        public static DataSet Set(string sql,  string source)
        {
            string _source = "live";
            if(source!="")
                _source = source;
            DataSet tb = new DataSet();
            SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[_source].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tb);
            sqlConn.Close();
            return tb;
        }

        public static DataTable List(string sql, string source)
        {
            string _source = "live";
            if (source != "")
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
}