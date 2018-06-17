using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Helper
{
    public class Safe
    {
        public static string ExportCompany;

        public static string GetUserCode()
        {
            string[] parts = HttpContext.Current.User.Identity.Name.Split(new char[] { '@' });
            return parts[0].Substring(0, 1).ToUpper() + parts[0].Substring(1);
        }

        public static string GetUserMail()
        {
            return HttpContext.Current.User.Identity.Name.Trim();
        }

        public static string GetUserSite()
        {
            string email = HttpContext.Current.User.Identity.Name;
            return C2.Manager.ORManager.ExecuteScalar("select company_code from sys_user where email='" + email + "'").ToString();
        }

        public static string CheckUrl(string site,string ret)
        {
            string f = "";
            string url = HttpContext.Current.Request.RawUrl.ToLower();
            if (url.IndexOf(site) >= 0)
                f = ret;
            return f;
        }

        public static string SafeStringModel(object o)
        {
            string t = "";
            try
            {
                t = o.ToString();
                t = t.Replace("'", " ");
            }
            catch
            {
            }
            if (t.Length == 0)
                t = " "; 
            return t;
        }

        public static string SafeString(object o)
        {
            string t = " ";
            try
            {
                t = o.ToString();
                t = t.Replace("'", " ");
            }
            catch
            {
            }
            return t;
        }
        public static string SafeString(object o, string value)
        {
            string t = "";

            try
            {
                t = o.ToString();
                t = t.Replace("'", " ");
            }
            catch
            {
                t = value;
            }
            if (t == "")
            {
                t = " ";
            }
            return t;
        }
        public static string SafeStringNoEmpty(object o)
        {
            string t = "";
            try
            {
                t = o.ToString();
                t = t.Replace("'", "");
            }
            catch
            {
            }
            if (t.Length == 0)
                t = " ";
            return t;
        }
        public static string SafeString40(object o)
        {
            string t = "";
            try
            {
                t = o.ToString();
                t = t.Replace("'", " ");
                if (t.Length > 40)
                    t = t.Substring(0, 40);
            }
            catch
            {
            }
            return t;
        }
        public static DateTime SafeDate(object s)
        {
            DateTime dt = DateTime.Now;
            try
            {
                dt = Convert.ToDateTime(s);
            }
            catch
            {
                dt = new DateTime(1900, 1, 1);
            }
            if (dt > new DateTime(2100, 12, 31) || dt < new DateTime(1900, 1, 1))
                dt = new DateTime(1900, 1, 1);
            return dt;
        }
        public static DateTime SafeNewDate(object s, DateTime value)
        {
            DateTime dt = DateTime.Now;
            try
            {
                dt = Convert.ToDateTime(s);
            }
            catch
            {
                dt = value;
            }
            if (dt > new DateTime(2100, 12, 31) || dt < new DateTime(1900, 1, 1))
                dt = new DateTime(1900, 1, 1);
            return dt;
        }

		public static string AccountEx(object s)
		{
			decimal d = SafeDecimal(s);
			if(d==1)
				return "";
			string v = string.Format("{0:#,##0.0000}",d);
			return v;		
		}

		public static string AccountNz(object s)
		{
			string r = "";
			decimal d = SafeDecimal(s);
			if(d>0)
				r = string.Format("{0:#,##0.00}",d);
			if(d<0)
				r = string.Format("{0:#,##0.00}",d);
			return r;		
			
		}

		public static string AccountSz(object s)
		{

			string r = "";
			decimal d = SafeDecimal(s);
			if(d==0)
				r = "0.00";
			if(d>0)
				r = string.Format("{0:#,##0.00}",d);
			if(d<0)
				r = string.Format("({0:#,##0.00})",-1 * d);
			return r;		
		
		}

		
        public static Decimal SafeDecimal(object s)
        {
            Decimal dec = 0;
            try
            {
                dec = Convert.ToDecimal(s);
            }
            catch
            {
                dec = 0;
            }
            return dec;
        }
        public static double SafeDouble(object s)
        {
            double dec = 0;
            try
            {
                dec = Convert.ToDouble(s);
            }
            catch
            {
                dec = 0;
            }
            return dec;
        }
        public static double SafeNewDouble(object s, double value)
        {
            double dec = 0;
            try
            {
                dec = Convert.ToDouble(s);
            }
            catch
            {
                dec = value;
            }
            return dec;
        }

        public static int SafeInt(object o)
        {
            int value = 0;
            if (o != null)
            {

                try
                {
                    value = Convert.ToInt32(o); // int.Parse(o.ToString());
                }
                catch
                {
                    value = 0;
                }
            }
            return value;
        }
        public static int SafeInt(object s, int t)
        {
            try
            {
                t = Convert.ToInt32(s);
            }
            catch
            {
            }
            return t;
        }
        public static string SafeBoolForAccess(object s)
        {
            bool flag = true;
            try
            {
                flag = Convert.ToBoolean(s);
            }
            catch
            {
            }
            string t = "-1";
            if (!flag)
                t = "0";
            return t;
        }

        public static string SetStringFormat(string value, int length)
        {
            if (value.Length < length)
            {
                for (int i = value.Length; i < length; i++)
                {
                    value += " ";
                }
            }
            else if (value.Length > length)
            {
                value = value.Substring(0, length);
            }

            return value;
        }

        public static string SetNumberFormat(string value, int length)
        {
            if (value.Length < length)
            {
                for (int i = value.Length; i < length; i++)
                {
                    value = "0" + value;
                }
            }
            else if (value.Length > length)
            {
                value = value.Substring(0, length);
            }

            return value;
        }

        public static string SetNumberFormat2(string value, int length)
        {
            if (value.Length < length)
            {
                for (int i = value.Length; i < length; i++)
                {
                    value = value + "0";
                }
            }
            else if (value.Length > length)
            {
                value = value.Substring(0, length);
            }

            return value;
        }

        public static DateTime SafeExcelDate(object o)
        {
            TimeSpan datefromexcel = new TimeSpan(Convert.ToInt32(o) - 2, 0, 0, 0);
            //val[1,1] is the cell value that is extracted from excel.
            DateTime d = new DateTime(1900, 1, 1).Add(datefromexcel);
            return d;
        }

        public static DateTime SafeDateTime(object o)
        {
            return SafeDate(o, DateTime.Today);
        }

        public static DateTime SafeDate(object o, DateTime exDate)
        {
            DateTime dt = exDate;
            try
            {
                dt = Convert.ToDateTime(o);
            }
            catch { }
            return dt;
        }

        public static string SafeDateStr(object o)
        {
            string s = "";
            try
            {
                DateTime exDate = Convert.ToDateTime(o);
                if (exDate > new DateTime(2000, 1, 1))
                    s = string.Format("{0:dd/MM/yyyy}", exDate);
            }
            catch { }

            return s;
        }

        public static string SafeDateTimeStr(object o)
        {
            string s = "";
            try
            {
                DateTime exDate = Convert.ToDateTime(o);
                if (exDate > new DateTime(2000, 1, 1))
                    s = string.Format("{0:dd/MM/yyyy HH:mm}", exDate);
            }
            catch { }

            return s;
        }

        public static string SafeTimeStr(object o)
        {
            string s = "";
            try
            {
                DateTime exDate = Convert.ToDateTime(o);
                if (exDate > new DateTime(2000, 1, 1))
                    s = string.Format("{0:HH:mm}", exDate);
            }
            catch { }

            return s;
        }
        public static bool SafeBool(object o)
        {
            return SafeBool(o, false);
        }

        public static bool SafeBool(object o, bool exValue)
        {
            bool value = exValue;
            if (o != null)
            {

                try
                {
                    value = Convert.ToBoolean(o); // int.Parse(o.ToString());
                }
                catch
                {
                    value = exValue;
                }
            }
            return value;
        }

        public static decimal RoundUp(decimal v, int decimals)
        {
            //value = double.Parse(value.ToString());
            if (v < 0)
            {
                //," "return Math.Round(v + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(v, decimals, MidpointRounding.AwayFromZero);
            }
            return v;
        }

        public static decimal RoundDown(decimal v, int decimals)
        {
            //value = double.Parse(value.ToString());
            if (v < 0)
            {
                //," "return Math.Round(v + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(v, decimals, MidpointRounding.ToEven);
            }
            return v;
        }

        public static string SafeDecimalFormat(object o, int digit, bool thousand, bool bracket)
        {
            string s = "";
            string p1 = "#,###,###,###,##0.";
            string p2 = "00";
            if (digit <= 0)
                p2 = "";
            if (digit == 1)
                p2 = "0";
            if (digit == 3)
                p2 = "000";
            if (digit >= 4)
                p2 = "0000";

            try
            {
                decimal ex = SafeDecimal(o);
                s = string.Format("{0:" + p1 + p2 + "}", ex);
                if (bracket && ex < 0)
                {
                    s = "(" + s.Substring(1) + ")";
                }
            }
            catch { }

            return s;
        }
    }
}
