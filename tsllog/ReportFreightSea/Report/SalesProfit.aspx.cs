using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using Wilson.ORMapper;
using System.Data.SqlClient;

public partial class ReportFreightSea_SalesProfit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = new DateTime(2011, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_Sales.Value = "NA";

            string userName = HttpContext.Current.User.Identity.Name;
            string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
            if (role == "SALESSTAFF")
            {
                this.dsSalesman.FilterExpression = "Code='"+userName+"'";
                this.cmb_Sales.Value = userName;
            }
            else if (role == "SALESMANAGER")
            {
                this.dsSalesman.FilterExpression = "Code='" + userName + "'";
                this.cmb_Sales.Value = userName;
                //string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"];
                //if (exceptSales.Length > 0)
                //{
                //    this.dsSalesman.FilterExpression = "Code!='" + exceptSales + "'";
                //}
            }
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {
               
    }

}
