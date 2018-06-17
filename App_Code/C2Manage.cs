using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Wilson.ORMapper;


namespace C2
{
    static public class Manager
    {
        static private ObjectSpace engine;

        static public ObjectSpace ORManager
        {
            get
            {
					//	throw new Exception(D.Test());

                return engine;
            }
        }

        static Manager()
        {
            string cnType = System.Configuration.ConfigurationManager.AppSettings["DEPLOY"];
            string cnString = System.Configuration.ConfigurationManager.ConnectionStrings[cnType].ConnectionString.ToString();
            engine = new ObjectSpace("Mappings.config", cnString, Provider.MsSql);
        }
    }
}
