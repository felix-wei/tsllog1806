using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class WareHouse_MastData_MastType : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = SafeValue.SafeSqlString(Request.QueryString["type"]);
            this.dsWhMastData.FilterExpression = "Type='" + type + "'";
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhMastData));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string type =SafeValue.SafeSqlString(Request.QueryString["type"]);
        e.NewValues["Code"] = "";
        e.NewValues["Description"] = " ";
        e.NewValues["Type"] = type;
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        string type = SafeValue.SafeSqlString(Request.QueryString["type"]);
        this.dsWhMastData.FilterExpression = "Type='" + type + "'";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("The Code not null");
        }
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        string type = SafeValue.SafeSqlString(Request.QueryString["type"]);
        e.NewValues["Type"] = type;
        this.dsWhMastData.FilterExpression = "Type='" + type + "'";
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        string type = SafeValue.SafeSqlString(Request.QueryString["type"]);
        this.dsWhMastData.FilterExpression = "Type='" + type + "'";
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid.FindEditRowCellTemplateControl(null, "txt_Code") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
}