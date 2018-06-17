using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Connect_Company 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Connect_Company : System.Web.Services.WebService
{

    public Connect_Company()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region Company


    [WebMethod]
    public void Company_GetList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Code", Type.GetType("System.String"));
        dt.Columns.Add("Name", Type.GetType("System.String"));
        dt.Columns.Add("ServerUri", Type.GetType("System.String"));
        dt.Columns.Add("WebSiteUri", Type.GetType("System.String"));
        DataRow dr;

        dr = dt.NewRow();
        dr["Code"] = "gke";
        dr["Name"] = "Cargo Erp Pte Ltd";
        dr["ServerUri"] = "http://192.168.1.114:82/Mobile";
        dr["WebSiteUri"] = "http://192.168.1.114:82";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Code"] = "gke1";
        dr["Name"] = "gke1";
        dr["ServerUri"] = "http://192.168.1.110:86";
        dr["WebSiteUri"] = "http://192.168.1.110:88";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Code"] = "moments";
        dr["Name"] = "Moments";
        dr["ServerUri"] = "http://moments.cargoerp.com";
        dr["WebSiteUri"] = "http://moments.cargoerp.com";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Code"] = "ilog";
        dr["Name"] = "i.Logistics";
        dr["ServerUri"] = "http://ilog.cargoerp.com";
        dr["WebSiteUri"] = "http://ismart.cargoerp.com";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Code"] = "collinsmovers";
        dr["Name"] = "Collin's Movers";
        dr["ServerUri"] = "http://moments.cargoerp.com";
        dr["WebSiteUri"] = "http://moments.cargoerp.com";
        dt.Rows.Add(dr);

        string json = Common.DataTableToJson(dt);
        Common.WriteJsonP(json);

    }

    //[WebMethod]
    //public void Company_GetList()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("Code", Type.GetType("System.String"));
    //    dt.Columns.Add("Name", Type.GetType("System.String"));
    //    dt.Columns.Add("ServerUri", Type.GetType("System.String"));
    //    dt.Columns.Add("WebSiteUri", Type.GetType("System.String"));
    //    DataRow dr;

    //    dr = dt.NewRow();
    //    dr["Code"] = "gke";
    //    dr["Name"] = "Cargo Erp Pte Ltd";
    //    dr["ServerUri"] = "http://demo.cargoerp.com/Mobile";
    //    dr["WebSiteUri"] = "http://demo.cargoerp.com";
    //    dt.Rows.Add(dr);

    //    dr = dt.NewRow();
    //    dr["Code"] = "gke1";
    //    dr["Name"] = "gke1";
    //    dr["ServerUri"] = "http://demo.cargoerp.com/Mobile";
    //    dr["WebSiteUri"] = "http://demo.cargoerp.com";
    //    dt.Rows.Add(dr);

    //    dr = dt.NewRow();
    //    dr["Code"] = "moments";
    //    dr["Name"] = "Moments";
    //    dr["ServerUri"] = "http://moments.cargoerp.com";
    //    dr["WebSiteUri"] = "http://moments.cargoerp.com";
    //    dt.Rows.Add(dr);

    //    dr = dt.NewRow();
    //    dr["Code"] = "ilog";
    //    dr["Name"] = "i.Logistics";
    //    dr["ServerUri"] = "http://ilog.cargoerp.com";
    //    dr["WebSiteUri"] = "http://ismart.cargoerp.com";
    //    dt.Rows.Add(dr);

    //    dr = dt.NewRow();
    //    dr["Code"] = "collinsmovers";
    //    dr["Name"] = "Collin's Movers";
    //    dr["ServerUri"] = "http://moments.cargoerp.com";
    //    dr["WebSiteUri"] = "http://moments.cargoerp.com";
    //    dt.Rows.Add(dr);

    //    string json = Common.DataTableToJson(dt);
    //    Common.WriteJsonP(json);

    //}

    #endregion


    [WebMethod]
    public void testOject()
    {
        Wilson.ORMapper.ObjectQuery oq = new Wilson.ORMapper.ObjectQuery(typeof(C2.CtmJob), "", "");
        Wilson.ORMapper.ObjectSet os=C2.Manager.ORManager.GetObjectSet(oq);
        string context = Common.ObjectToJson(os);
        Common.WriteJsonP(true, context);
    }
}
