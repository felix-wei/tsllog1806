using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wilson.ORMapper;


namespace C2
{
    /// <summary>
    /// ManagerEdi 的摘要说明
    /// </summary>
    static public class ManagerEdi
    {
        static private ObjectSpace engine;

        static public ObjectSpace ORManager
        {
            get
            {
                return engine;
            }
        }

        static ManagerEdi()
        {
            string cnString = System.Configuration.ConfigurationManager.ConnectionStrings["edi_str"].ConnectionString.ToString();
            engine = new ObjectSpace("Mappings.config", cnString, Provider.MsSql);
        }
    }
}