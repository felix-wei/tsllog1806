using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReportAccount_RptGl_AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmb_Year.Text = DateTime.Today.Year.ToString();
            int fromYear = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["AccountFirstYear"], 2010);
            int endYear = 2015;
            cmb_Year.Items.Clear();
            for (int i = fromYear; i < endYear; i++)
            {
                cmb_Year.Items.Add(i.ToString(), i);
            }
            this.cmb_From.Text ="1";
            this.cmb_To.Text = "1";
            //this.cmb_To.Text = DateTime.Today.Month.ToString();
            this.rbt.SelectedIndex = 0;
            this.txt_PartyType.Text = "Customer";
        }
    }
}
