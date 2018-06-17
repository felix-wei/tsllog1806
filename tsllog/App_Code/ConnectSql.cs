using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ConnectOracle
/// </summary>
public class ConnectSql
{
    static public DataTable GetTab(string sql)
    {
        //dddd
       return C2.Manager.ORManager.GetDataSet(sql).Tables[0];
    }
    static public DataSet GetDataSet(string[] sqls)
    {
        DataSet set = new DataSet("set");
        for (int i = 0; i < sqls.Length; i++)
        {
            string sql = sqls[i];
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            tab.TableName = "Name" + i.ToString();
            set.Tables.Add(tab);
        }
        return set;
    }
    static public DataSet GetDataSet(string sql)
    {
        return C2.Manager.ORManager.GetDataSet(sql);
    }
    static public int ExecuteSql(string sql)
    {
        return C2.Manager.ORManager.ExecuteCommand(sql); ;
    }
    static public object ExecuteScalar(string sql)
    {
        
        return C2.Manager.ORManager.ExecuteScalar(sql);
    }


    static public DataTable GetRemoteTab(string sql)
    {
        DataTable tab = new DataTable();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnectString1"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
        try
        {

            sda.Fill(tab);
        }
        catch { }
        finally
        {
            sda.Dispose();
            conn.Close();
            conn.Dispose();
        }
        return tab;// C2.Manager.ORManager.GetDataSet(sql).Tables[0];
    }
}

