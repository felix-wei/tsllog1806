﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_Setup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            string masterId = e.Values["MasterId"].ToString();
            string sql = string.Format("Delete from MenuSub where MasterId='{0}'", masterId);
            C2.Manager.ORManager.ExecuteCommand(sql);

        }

    }
}