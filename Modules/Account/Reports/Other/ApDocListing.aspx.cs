﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReportAccount_Other_ApDocListing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.date_From.Date = DateTime.Today.AddDays(-7);
            this.date_To.Date = DateTime.Today;
            this.cmb_DocType.Value = "PIN";
        }
    }
}
