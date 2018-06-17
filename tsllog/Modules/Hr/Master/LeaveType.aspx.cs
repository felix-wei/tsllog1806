using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Hr_Master_LeaveType : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["LeaveType"] = "Type='LeaveType' and Code<>''";
        }
        if (Session["LeaveType"] != null)
            this.dsHrMastData.FilterExpression = Session["LeaveType"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region PayItem
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrMastData));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Type"] = "LeaveType";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Type"] = "LeaveType";
        btn_search_Click(null, null);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            //Session["LeaveType"] = "Code='" + e.NewValues["Code"] + "'";
            //this.dsHrMastData.FilterExpression = "Code='" + e.NewValues["Code"] + "'";
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        btn_search_Click(null, null);
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

    #endregion
    protected void btn_search_Click(object sender, EventArgs e)
    {
        this.dsHrMastData.FilterExpression = Session["LeaveType"].ToString();
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}