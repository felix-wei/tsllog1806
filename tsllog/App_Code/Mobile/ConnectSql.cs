using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for ConnectOracle
/// </summary>
public class ConnectSql_mb
{
    public static string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString;

    public class cmdParameters
    {
        public string name;
        public SqlDbType type;
        public int size;
        public object value;

        public cmdParameters()
        {
            this.name = "";
            this.value = "";
            this.type = SqlDbType.Char;
            this.size = 0;
        }
        public cmdParameters(string _name, object _value, SqlDbType _type)
        {
            this.name = _name;
            this.value = _value;
            this.type = _type;
            this.size = 0;
        }
        public cmdParameters(string _name, object _value, SqlDbType _type, int _size)
        {
            this.name = _name;
            this.value = _value;
            this.type = _type;
            this.size = _size;
        }
        public cmdParameters(string _name, Newtonsoft.Json.Linq.JToken _value, SqlDbType _type)
        {
            this.name = _name;
            this.value = SafeValue.SafeString(_value);
            this.type = _type;
            this.size = 0;
        }
        public cmdParameters(string _name, Newtonsoft.Json.Linq.JToken _value, SqlDbType _type, int _size)
        {
            this.name = _name;
            this.value = SafeValue.SafeString(_value);
            this.type = _type;
            this.size = _size;
        }
    }
    public class sqlResult
    {
        public bool status;
        public string context;
        public sqlResult()
        {
            this.status = false;
            this.context = "";
        }
        public sqlResult(bool _status)
        {
            this.status = _status;
        }
        public sqlResult(bool _status, string _context)
        {
            this.status = _status;
            this.context = _context;
        }
    }

    #region ExecuteTrans
    /// <summary>
    /// success:1; false:0
    /// </summary>
    /// <param name="alcom"></param>
    /// <returns></returns>
    static public string[] ExecuteTrans(ArrayList alcom)
    {
        string[] temp = new string[2];
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand com = new SqlCommand();
        SqlTransaction trans = null;
        int runat = 0;
        temp[0] = "1";
        try
        {
            con.Open();
            trans = con.BeginTransaction();
            com.Transaction = trans;
            com.Connection = con;
            com.CommandType = CommandType.Text;
            for (runat = 0; runat < alcom.Count; runat++)
            {
                com.CommandText = alcom[runat].ToString();
                temp[1] += "/" + com.ExecuteNonQuery().ToString();
            }
            trans.Commit();
        }
        catch
        {
            temp[0] = "0";
            temp[1] = com.CommandText;
            trans.Rollback();
        }
        finally
        {
            trans.Dispose();
            con.Close();
        }
        return temp;
    }

    /// <summary>
    /// success:1; false:0
    /// </summary>
    /// <param name="alcom"></param>
    /// <returns></returns>
    static public string[] ExecuteTrans(string[] alcom)
    {
        string[] temp = new string[2];
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand com = new SqlCommand();
        SqlTransaction trans = null;
        int runat = 0;
        temp[0] = "1";
        try
        {
            con.Open();
            trans = con.BeginTransaction();
            com.Transaction = trans;
            com.Connection = con;
            com.CommandType = CommandType.Text;
            for (runat = 0; runat < alcom.Length; runat++)
            {
                com.CommandText = alcom[runat].ToString();
                temp[1] = com.ExecuteNonQuery().ToString();
            }
            trans.Commit();
        }
        catch
        {
            temp[0] = "0";
            temp[1] = com.CommandText;
            trans.Rollback();
        }
        finally
        {
            trans.Dispose();
            con.Close();
        }
        return temp;
    }
    #endregion

    /// <summary>
    /// 获取返回的第一行第一列数据
    /// </summary>
    /// <param name="strcom"></param>
    /// <returns></returns>
    static public string ExecuteScalar(string strcom)
    {
        string temp;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            using (SqlCommand com = new SqlCommand(strcom, con))
            {
                temp = SafeValue.SafeString(com.ExecuteScalar());
            }
        }
        return temp;
    }
    static public sqlResult ExecuteScalar(string strcom, List<cmdParameters> pars)
    {
        string temp = "";
        sqlResult sRe = new sqlResult();
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(strcom, con))
                {
                    foreach (cmdParameters par in pars)
                    {
                        if (par.size == 0)
                        {
                            com.Parameters.Add(par.name, par.type);
                        }
                        else
                        {
                            com.Parameters.Add(par.name, par.type, par.size);
                        }
                        com.Parameters[par.name].Value = par.value;
                    }
                    temp = SafeValue.SafeString(com.ExecuteScalar());
                    sRe.status = true;
                    sRe.context = temp;
                }
            }
        }
        catch { }
        return sRe;
    }

    /// <summary>
    /// 返回受影响的行数
    /// </summary>
    /// <param name="strcom"></param>
    /// <returns></returns>
    static public sqlResult ExecuteNonQuery(string strcom, List<cmdParameters> pars)
    {
        int temp;
        sqlResult sRe = new sqlResult();
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(strcom, con))
                {
                    foreach (cmdParameters par in pars)
                    {
                        if (par.size == 0)
                        {
                            com.Parameters.Add(par.name, par.type);
                        }
                        else
                        {
                            com.Parameters.Add(par.name, par.type, par.size);
                        }
                        com.Parameters[par.name].Value = par.value;
                    }
                    temp = com.ExecuteNonQuery();
                    sRe.status = true;
                    sRe.context = temp.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            sRe.context = ex.Message + ex.StackTrace;
        }
        return sRe;
    }
    static public int ExecuteNonQuery(string strcom)
    {
        int temp;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            using (SqlCommand com = new SqlCommand(strcom, con))
            {
                temp = com.ExecuteNonQuery();
            }
        }
        return temp;
    }

    /// <summary>
    /// 返回第一个DataTable
    /// </summary>
    /// <param name="strcom"></param>
    /// <returns></returns>
    static public DataTable GetDataTable(string strcom)
    {
        DataSet ds = GetDataSet(strcom);
        if (ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
    static public DataTable GetDataTable(string strcom, List<cmdParameters> pars)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(strcom, con))
                {
                    foreach (cmdParameters par in pars)
                    {
                        if (par.size == 0)
                        {
                            com.Parameters.Add(par.name, par.type);
                        }
                        else
                        {
                            com.Parameters.Add(par.name, par.type, par.size);
                        }
                        com.Parameters[par.name].Value = par.value;
                    }
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
        }
        catch { }
        return dt;
    }

    static public DataSet GetDataSet(string strcom)
    {
        DataSet ds = new DataSet();
        using (SqlConnection con = new SqlConnection(strcon))
        {
            con.Open();
            using (SqlDataAdapter da = new SqlDataAdapter(strcom, con))
            {
                da.Fill(ds);
            }
        }
        return ds;

    }


}

