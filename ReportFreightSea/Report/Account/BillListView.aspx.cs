﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using C2;


public partial class ReportFreigtSea_Account_BillListView : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
          string refType = SafeValue.SafeString(Request.QueryString["refType"]).ToUpper();
          Session["rType"] = refType;
        }
    }
}
