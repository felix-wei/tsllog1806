using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// data2Json 的摘要说明
/// </summary>
public class data2Json
{
    public data2Json()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string DataRowToJson(DataTable dt)
    {
        return DataRowToJson(dt, false);
    }

    public static string DataRowToJson(DataTable dt, bool isGetColName)
    {
        string result = "";
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            foreach (DataColumn dc in dt.Columns)
            {
                result += "\"" + dc.ColumnName + "\":\"" + Helper.Safe.SafeString(dr[dc.ColumnName]) + "\",";
            }
        }
        else
        {
            if (isGetColName)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    result += "\"" + dc.ColumnName + "\":\"\",";
                }
            }
        }
        result = DelComma_AddBracket(result);
        return result;
    }
    public static string DataTableToJson(DataTable dt)
    {
        string result = "";
        foreach (DataRow dr in dt.Rows)
        {
            string str_row = "";
            foreach (DataColumn dc in dt.Columns)
            {
                str_row += "\"" + dc.ColumnName + "\":\"" + Helper.Safe.SafeString(dr[dc.ColumnName]) + "\",";
            }
            str_row = DelComma_AddBracket(str_row) + ",";
            result += str_row;
        }
        result = DelComma_AddBracket(result, true);
        return result;
    }

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

}
