using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using C2;


public partial class ReportAccount_RptGl_PlStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_Year.Text = DateTime.Today.Year.ToString();
            this.cmb_Period.Text ="1";
            int fromYear = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountFirstYear"], 2010);
            int endYear = 2020;
            cmb_Year.Items.Clear();
            for (int i = fromYear; i < endYear; i++)
            {
                cmb_Year.Items.Add(i.ToString(), i);
            }
        }
    }
}
