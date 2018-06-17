using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using Wilson.ORMapper;
using System.Data.SqlClient;

public partial class ReportFreightSea_ExportSalesProfit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = new DateTime(2011, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_Cust.Value = "NA";
            this.cmb_Sales.Value = "NA";
            this.rbt_Party.Value = "0";

            string userName = HttpContext.Current.User.Identity.Name;
            string role = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select role from [user] where Name='" + userName + "'"), "").ToUpper();
            if (role == "SALESSTAFF")
            {
                this.dsSalesman.FilterExpression = "Code='" + userName + "'";
                this.dsCustomerMast.FilterExpression = "SalesmanId='" + userName + "'";
                this.cmb_Sales.Value = userName;

                this.cmb_Cust.Value = "";
            }
            else if (role == "SALESMANAGER")
            {
                string exceptSales = System.Configuration.ConfigurationManager.AppSettings["ExceptSales"];
                if (exceptSales.Length > 0)
                {
                    this.dsSalesman.FilterExpression = "Code!='" + exceptSales + "'";
                    this.dsCustomerMast.FilterExpression = "SalesmanId!='" + exceptSales + "'";
                }
            }
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {
               
    }

}
