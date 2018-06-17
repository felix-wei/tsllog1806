using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region Write Json

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result">json format</param>
    public static void WriteJson(string result)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.Write(result);
        HttpContext.Current.Response.End();
    }
    /// <summary>
    /// 0 false, 1 success; 
    /// use status to control other number.
    /// </summary>
    /// <param name="status">string format</param>
    /// <param name="result">json format</param>
    public static void WriteJson(string status, string result)
    {
        string temp = "{\"status\":\"" + status + "\",\"context\":" + result + "}";
        WriteJson(temp);
    }

    /// <summary>
    /// 0 false, 1 success; 
    /// use status to control other number.
    /// </summary>
    /// <param name="status">int format</param>
    /// <param name="result">json format</param>
    public static void WriteJson(int status, string result)
    {
        string temp = "{\"status\":\"" + status + "\",\"context\":" + result + "}";
        WriteJson(temp);
    }
    /// <summary>
    /// 0 false, 1 success; 
    /// use status to control other number.
    /// </summary>
    /// <param name="status">bool format</param>
    /// <param name="result">json format</param>
    public static void WriteJson(bool status, string result)
    {
        string i = "0";
        if (status) { i = "1"; }
        WriteJson(i, result);
    }

    #endregion

    #region Write Jsonp

    public static void WriteJsonP(string result)
    {
        string callback = SafeValue.SafeString(HttpContext.Current.Request.Params["callback"]);
        string re_value = callback + "({result:" + result + "})";
        HttpContext.Current.Response.Write(re_value);
        HttpContext.Current.Response.End();
    }
    /// <summary>
    /// 0 false, 1 success; 
    /// use status to control other number.
    /// </summary>
    /// <param name="status">string format</param>
    /// <param name="result">json format</param>
    public static void WriteJsonP(string status, string result)
    {
        string temp = "{status:'" + status + "',context:" + result + "}";
        WriteJsonP(temp);
    }
    /// <summary>
    /// 0 false, 1 success; 
    /// use status to control other number.
    /// </summary>
    /// <param name="status">string format</param>
    /// <param name="result">json format</param>
    public static void WriteJsonP(int status, string result)
    {
        string temp = "{status:'" + status + "',context:" + result + "}";
        WriteJsonP(temp);
    }
    /// <summary>
    /// 0 false, 1 success; 
    /// </summary>
    /// <param name="status">string format</param>
    /// <param name="result">json format</param>
    public static void WriteJsonP(bool status, string result)
    {
        string i = "0";
        if (status) { i = "1"; }
        string temp = "{status:'" + i + "',context:" + result + "}";
        WriteJsonP(temp);
    }
    #endregion

    public static string StringToJson(string par)
    {
        //return "\"" + par + "\"";
        return ObjectToJson(par);
    }

    //public static string DataTableToJson(DataTable dt)
    //{
    //    string result = "";
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string str_row = "";
    //        foreach (DataColumn dc in dt.Columns)
    //        {
    //            str_row += dc.ColumnName + ":\"" + Json2_SafeValue(SafeValue.SafeString(dr[dc.ColumnName])) + "\",";
    //        }
    //        str_row = DelComma_AddBracket(str_row) + ",";
    //        result += str_row;
    //    }
    //    result = DelComma_AddBracket(result, true);
    //    return result;
    //}


    //============ 20150205
    public static string DataTableToJson_150205(DataTable dt)
    {
        string result = "";
        foreach (DataRow dr in dt.Rows)
        {
            string str_row = "";
            foreach (DataColumn dc in dt.Columns)
            {
                switch (dc.DataType.ToString())
                {
                    case "System.Int32":
                        str_row += dc.ColumnName + ":" + dr[dc.ColumnName] + ",";
                        break;
                    case "System.Decimal":
                        str_row += dc.ColumnName + ":" + dr[dc.ColumnName] + ",";
                        break;
                    case "System.Boolean":
                        str_row += dc.ColumnName + ":" + dr[dc.ColumnName].ToString().ToLower() + ",";
                        break;
                    default:
                        str_row += dc.ColumnName + ":\"" + Json2_SafeValue(SafeValue.SafeString(dr[dc.ColumnName])) + "\",";
                        break;
                }
            }
            str_row = DelComma_AddBracket(str_row) + ",";
            result += str_row;
        }
        result = DelComma_AddBracket(result, true);
        return result;
    }
    //============ 20150205
    public static string DataRowToJson_150205(DataTable dt, bool isGetColName)
    {
        string result = "";
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            foreach (DataColumn dc in dt.Columns)
            {
                switch (dc.DataType.ToString())
                {
                    case "System.Int32":
                        result += dc.ColumnName + ":" + dr[dc.ColumnName] + ",";
                        break;
                    case "System.Boolean":
                        result += dc.ColumnName + ":" + dr[dc.ColumnName].ToString().ToLower() + ",";
                        break;
                    case "System.Decimal":
                        result += dc.ColumnName + ":" + dr[dc.ColumnName] + ",";
                        break;
                    default:
                        result += dc.ColumnName + ":\"" + Json2_SafeValue(SafeValue.SafeString(dr[dc.ColumnName])) + "\",";
                        break;
                }
            }
        }
        else
        {
            if (isGetColName)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    switch (dc.DataType.ToString())
                    {
                        case "System.Int32":
                            result += dc.ColumnName + ":0,";
                            break;
                        case "System.Decimal":
                            result += dc.ColumnName + ":0,";
                            break;
                        case "System.Boolean":
                            result += dc.ColumnName + ":false,";
                            break;
                        default:
                            result += dc.ColumnName + ":\"\",";
                            break;
                    }
                }
            }
        }
        result = DelComma_AddBracket(result);
        return result;
    }

    //public static string DataRowToJson(DataTable dt)
    //{
    //    //string result = "";
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    DataRow dr = dt.Rows[0];
    //    //    foreach (DataColumn dc in dt.Columns)
    //    //    {
    //    //        result += dc.ColumnName + ":\"" + SafeValue.SafeString(dr[dc.ColumnName]) + "\",";
    //    //    }
    //    //}
    //    //result = DelComma_AddBracket(result);
    //    //return result;
    //    return DataRowToJson(dt, false);
    //}

    //public static string DataRowToJson(DataTable dt, bool isGetColName)
    //{
    //    string result = "";
    //    if (dt.Rows.Count > 0)
    //    {
    //        DataRow dr = dt.Rows[0];
    //        foreach (DataColumn dc in dt.Columns)
    //        {
    //            result += dc.ColumnName + ":\"" + Json2_SafeValue(SafeValue.SafeString(dr[dc.ColumnName])) + "\",";
    //        }
    //    }
    //    else
    //    {
    //        if (isGetColName)
    //        {
    //            foreach (DataColumn dc in dt.Columns)
    //            {
    //                result += dc.ColumnName + ":\"\",";
    //            }
    //        }
    //    }
    //    result = DelComma_AddBracket(result);
    //    return result;
    //}

    static string DelComma_AddBracket(string par)
    {
        return DelComma_AddBracket(par, false);
    }
    static string DelComma_AddBracket(string par, bool IsSquare)
    {
        string result = par;
        if (result.EndsWith(","))
        {
            result = result.Substring(0, result.Length - 1);
        }
        if (IsSquare)
        {
            result = "[" + result + "]";
        }
        else
        {
            result = "{" + result + "}";
        }
        return result;
    }


    public static string Json2_Replace(string par)
    {
        string result = par;
        result = result.Replace("!AND!", "&");
        result = result.Replace("!WEN!", "?");
        result = result.Replace("!JING!", "#");
        //result = result.Replace("!DYIN!", "'");
        result = result.Replace("!HEN!", "-");
        result = result.Replace("!br!", "\n");
        return result;
    }


    public static string Json2_SafeValue(string par)
    {
        string result = par;
        result = result.Replace("\"", "\\\"");
        return result;
    }


    //static IDictionary<string, string> Json2Data_Row(string par)
    //{
    //    IDictionary<string, string> ht = new Dictionary<string, string>();
    //    string[] temp = par.Split('\'');
    //    for (int i = 1; i < temp.Length; i = i + 4)
    //    {
    //        ht.Add(temp[i], temp[i + 2]);
    //    }
    //    return ht;
    //}

    //static List<object> Json2Data_List(string par)
    //{
    //    List<object> list = new List<object>();
    //    //string[] temp=par.Split("},{")
    //    return list;
    //}
    static public List<IDictionary<string, string>> Json2Data(string par)
    {
        string par_temp = Json2_Replace(par);
        List<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
        int begin = par_temp.IndexOf("{");
        string temp = par_temp.Substring(begin + 1);
        Json2Data_part(temp, list);
        return list;
    }

    static void Json2Data_part(string par, List<IDictionary<string, string>> list)
    {
        int end = par.IndexOf("}");
        int begin = par.IndexOf("{");
        if (begin > end || begin < 0)
        {
            string temp = par.Split('}')[0];
            list.Add(Json2Data_part2(temp));
            if (begin >= 0)
            {
                temp = par.Substring(begin + 1);
                Json2Data_part(temp, list);
            }
        }
        else
        {
            string temp = par.Substring(begin + 1);
            Json2Data_part(temp, list);
        }

    }

    static IDictionary<string, string> Json2Data_part2(string par)
    {
        IDictionary<string, string> ht = new Dictionary<string, string>();
        string[] temp = par.Split('\'');
        for (int i = 1; i < temp.Length; i = i + 4)
        {
            ht.Add(temp[i], temp[i + 2]);
        }
        return ht;
    }




    ///// <summary>
    ///// class 转成 Json
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class ReflectionClass<T>
    //{
    //    public string GetJsonString(T t)
    //    {
    //        string result = "";
    //        Type type = typeof(T);
    //        foreach (System.Reflection.PropertyInfo info in type.GetProperties())
    //        {
    //            if (info.PropertyType.IsValueType || info.PropertyType.Name.StartsWith("String"))
    //                result += info.Name + ":\"" + SafeValue.SafeString(info.GetValue(t, null)) + "\",";
    //        }
    //        result = Common.DelComma_AddBracket(result);
    //        return result;
    //    }
    //}

    static public string ClassToJson<T>(T t)
    {
        string result = "";
        Type type = typeof(T);
        foreach (System.Reflection.PropertyInfo info in type.GetProperties())
        {
            if (info.PropertyType.IsValueType || info.PropertyType.Name.StartsWith("String"))
                result += info.Name + ":\"" + SafeValue.SafeString(info.GetValue(t, null)) + "\",";
        }
        result = Common.DelComma_AddBracket(result);
        return result;
    }

    public static string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
        //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        //MemoryStream stream = new MemoryStream();
        //serializer.WriteObject(stream, obj);
        //byte[] dataBytes = new byte[stream.Length];
        //stream.Position = 0;
        //stream.Read(dataBytes, 0, (int)stream.Length);
        //return Encoding.UTF8.GetString(dataBytes);
    }



    //============================== in 20150305 update

    public static string DataTableToJson(DataTable dt)
    {
        return ObjectToJson(dt);
    }

    public static string DataRowToJson(DataTable dt)
    {
        string re = ObjectToJson(dt);
        re = re.Substring(1, re.Length - 2);
        return re;
    }

    public static string DataRowToJson(DataTable dt, bool isGetColName)
    {
        string result = "";
        if (dt.Rows.Count > 0)
        {
            result = ObjectToJson(dt);
            result = result.Substring(1, result.Length - 2);
        }
        else
        {
            if (isGetColName)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    result += dc.ColumnName + ":\"\",";
                }
                result = DelComma_AddBracket(result);
            }
        }
        return result;
    }
}