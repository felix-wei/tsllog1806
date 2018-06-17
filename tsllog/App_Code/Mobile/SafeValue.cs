using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SafeValue 的摘要说明
/// </summary>
public class SafeValue_mb
{
    public SafeValue_mb()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string SafeString(object par, string par1)
    {
        string re_value = par1;
        if (par == null)
        {
            return re_value;
        }
        try
        {
            re_value = par.ToString();
        }
        catch { }
        return re_value;
    }
    public static string SafeString(object par)
    {
        return SafeString(par, "");
    }

    public static string SafeString_DataBaseValue(string par)
    {
        string result = par;
        result = result.Replace("!DYIN!", "'");
        result = result.Replace("'", "''");
        return result;
    }
    public static string SafeString_DataBaseValue(JObject par)
    {
        string result = (string)par;
        result = result.Replace("!DYIN!", "'");
        result = result.Replace("'", "''");
        return result;
    }

    public static int SafeInt(object par, int par1)
    {
        int re_value = par1;
        if (par == null)
        {
            return re_value;
        }
        try
        {
            re_value = Convert.ToInt32(par);
        }
        catch { }
        return re_value;
    }
    public static int SafeInt(object par)
    {
        return SafeInt(par, 0);
    }
    public static float SafeFloat(object par, float par1)
    {
        float re_value = par1;
        if (par == null)
        {
            return re_value;
        }
        try
        {
            re_value = (float)Convert.ToDouble(par);
        }
        catch { }
        return re_value;
    }
    public static float SafeFloat(object par)
    {
        return SafeFloat(par, 0);
    }

    public static string Fun_Currency_ToWords(double par)
    {
        string[] seat_e3 = { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion" };
        string str_par = Convert.ToString(Math.Round(par, 2));
        string par1 = "";
        string par2 = "";
        if (str_par.IndexOf(".") < 0)
        {
            par1 = str_par;
        }
        else
        {
            par1 = str_par.Substring(0, str_par.IndexOf("."));
            par2 = str_par.Substring(str_par.IndexOf(".") + 1);
        }
        string temp = par1;
        string result_par1 = "";
        while (temp.Length > 0)
        {
            string temp_in = "";
            if (temp.Length % 3 > 0)
            {
                temp_in = Fun_Currency_ToWords_part1(temp.Substring(0, temp.Length % 3));
                temp = temp.Substring(temp.Length % 3);
            }
            else
            {
                temp_in = Fun_Currency_ToWords_part1(temp.Substring(0, 3));
                temp = temp.Substring(3);
            }
            if (temp_in.Length > 0)
            {
                temp_in = Fun_Currency_ToWords_AddStr(temp_in, seat_e3[temp.Length / 3]);
                result_par1 = Fun_Currency_ToWords_AddStr(result_par1, temp_in);
            }
        }
        string result_par2 = Fun_Currency_ToWords_part2(par2);
        string result = Fun_Currency_ToWords_AddStr(result_par1, "dollars");
        if (result_par2.Length > 0)
        {
            result = Fun_Currency_ToWords_AddStr(result, result_par2);
        }
        result = result.ToUpper();
        return result;
    }
    private static string Fun_Currency_ToWords_part2(string par)
    {
        string[] seat_d20 = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                                "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] seat_d100 = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        string result = "";
        if (par.Length > 0)
        {
            string temp_par = par;
            if (temp_par.Length == 1)
            {
                temp_par += "0";
            }
            else
            {
                if (par.Length > 2)
                {
                    temp_par = temp_par.Substring(0, 2);
                }
            }
            string temp = "";
            try
            {
                int num = Convert.ToInt32(temp_par);
                if (num < 20)
                {
                    temp = seat_d20[num];
                }
                else
                {
                    int num1 = num / 10;
                    int num2 = num % 10;
                    temp = num2 == 0 ? seat_d100[num1 - 2] : seat_d100[num1 - 2] + " " + seat_d20[num2];
                }
            }
            catch (Exception e) { }
            if (temp.Length > 0)
            {
                temp = Fun_Currency_ToWords_AddStr("and", temp);
                temp = Fun_Currency_ToWords_AddStr(temp, "cents");
                result = temp;
            }
        }
        return result;
    }
    private static string Fun_Currency_ToWords_part1(string par)
    {
        //string[] seat_d10 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string[] seat_d20 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", 
                                "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] seat_d100 = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety", "hundred" };
        string result = "";
        try
        {
            int num1 = 0;
            int num2 = 0;
            if (par.Length == 3)
            {
                num1 = Convert.ToInt32(par.Substring(0, 1));
                num2 = Convert.ToInt32(par.Substring(1));
                result = seat_d20[num1] + " " + seat_d100[8];
            }
            else
            {
                num2 = Convert.ToInt32(par);
            }
            if (num2 < 20)
            {
                result = Fun_Currency_ToWords_AddStr(result, seat_d20[num2]);
            }
            else
            {
                int num2_1 = num2 / 10;
                int num2_2 = num2 % 10;
                result = Fun_Currency_ToWords_AddStr(result, seat_d100[num2_1 - 2]);
                result = Fun_Currency_ToWords_AddStr(result, seat_d20[num2_2]);
            }
        }
        catch (Exception e) { }
        return result;
    }
    private static string Fun_Currency_ToWords_AddStr(string par, string par1)
    {
        return par.Length > 0 ? par + " " + par1 : par1;
    }


    public static DateTime DateTime_ClearTime(DateTime dt)
    {
        if (dt == null)
        {
            dt = DateTime.Now;
        }
        return dt.AddHours(-dt.Hour).AddMinutes(-dt.Minute).AddSeconds(-dt.Second).AddMilliseconds(-dt.Millisecond);
    }

    public static DateTime DateTime_ParseExact(string ss,string format)
    {
        return DateTime.ParseExact(ss, format, System.Globalization.CultureInfo.CurrentCulture);
    }
    public static string convertTimeFormat(string par)
    {
        return convertTimeFormat(par, ":");
    }
    public static string convertTimeFormat(string par,string par1)
    {
        string bt_h = "00";
        string bt_m = "00";
        if (par != null)
        {
            if (par.Length == 4)
            {
                int int_h = SafeValue.SafeInt(par.Substring(0, 2), 0) + 100;
                int int_m = SafeValue.SafeInt(par.Substring(2, 2), 0) + 100;
                bt_h = int_h.ToString().Substring(1);
                bt_m = int_m.ToString().Substring(1);
            }
            if (par.Length == 5)
            {
                int int_h = SafeValue.SafeInt(par.Substring(0, 2), 0) + 100;
                int int_m = SafeValue.SafeInt(par.Substring(3, 2), 0) + 100;
                bt_h = int_h.ToString().Substring(1);
                bt_m = int_m.ToString().Substring(1);
            }
        }
        string res = bt_h + par1 + bt_m;
        return res;
    }
}