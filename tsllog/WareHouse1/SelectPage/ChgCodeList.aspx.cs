﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_ChgCodeList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        this.dsChgcodeMast.FilterExpression = "1=1";
    }
}
