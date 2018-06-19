using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL.Tool
{
    public class Convert
    {
        public static void To<T, Y>(T obj, ref Y res)
        {
            Type t_res = res.GetType();
            var p_res = t_res.GetProperties();

            Type from_type = obj.GetType();
            foreach (var ff in p_res)
            {
                var temp = from_type.GetProperty(ff.Name);
                if (temp != null)
                {
                    ff.SetValue(res, temp.GetValue(obj));
                }
            }
        }

        public static T To<T>(object from)
        {
            //Type res_type = typeof(T);
            T res = Activator.CreateInstance<T>();
            To(from, ref res);
            return res;
        }

        public static void DeserializeByJson<R>(object from, out R res)
        {
            try
            {
                res = JsonConvert.DeserializeObject<R>(JsonConvert.SerializeObject(from));
            }
            catch
            {
                res = default(R);
            }
        }

        public static R DeserializeByJson<R>(object from)
        {
            R res = default(R);
            try
            {
                res = JsonConvert.DeserializeObject<R>(JsonConvert.SerializeObject(from));
            }
            catch { }
            return res;
        }

    }
}
