using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class Modules_Hr_Master_HrPayItem : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Item"] = "1=1";
        }
        if (Session["Item"] != null)
            this.dsPayItem.FilterExpression = Session["Item"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Session["Item"] = null;
        //    this.dsPayItem.FilterExpression = "1=0";
        //    btn_search_Click(null, null);
        //}
        //if (Session["Item"] != null)
        //{
        //    this.dsPayItem.FilterExpression = Session["Item"].ToString();
        //}
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txt_Code.Text.Trim() != "")
        {
            where = "Code like '" + this.txt_Code.Text.Trim() + "%'";
        }
        else {
            where = "1=1";
        }
        this.dsPayItem.FilterExpression = where;
        Session["Item"] = where;
        
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("PayItem", true);
    }
    #region PayItem
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPayItem));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);

        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        if (SafeValue.SafeString(e.NewValues["Description"]).Length == 0)
            e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Code"]);

        btn_search_Click(null,null);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            Session["Item"] = "Code='" + e.NewValues["Code"] + "'";
            this.dsPayItem.FilterExpression = "Code='" + e.NewValues["Code"] + "'";
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        if (SafeValue.SafeString(e.NewValues["Description"]).Length == 0)
            e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Code"]);

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
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
}
