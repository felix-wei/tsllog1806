﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using C2;

public partial class ReportAccount_RptAr_AgingDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = new DateTime(1900, 1, 1);
            this.date_To.Date = DateTime.Today;
            this.cmb_Salesman.Value = "NA";
            this.rbt.SelectedIndex = 0;
        }
    }

 
}
