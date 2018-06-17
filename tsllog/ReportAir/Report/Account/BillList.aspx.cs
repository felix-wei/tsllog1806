using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportAir_Report_Account_BillList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.rbt.SelectedIndex = 0;
            //this.rbt_Party.SelectedIndex = 0;
            this.date_From.Date = new DateTime(2011, 1, 1);
            this.date_End.Date = DateTime.Today;
            this.cmb_DocType.Value = "IV";
        }
    }
    protected void btn_Post_click(object sender, EventArgs e)
    {

    }
}