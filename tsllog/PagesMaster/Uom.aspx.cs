﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class PagesMaster_Uom : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        dsXXUom.FilterExpression = "CodeType='" + type + "'";

    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.XXUom));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        e.NewValues["CodeType"] = type;
        e.NewValues["Description"] = " ";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        e.NewValues["CodeType"] = type;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
   //
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        dsXXUom.FilterExpression = "CodeType='" + type + "'";
    }
    protected void grid_PageSizeChanged(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        dsXXUom.FilterExpression = "CodeType='" + type + "'";
    }
}