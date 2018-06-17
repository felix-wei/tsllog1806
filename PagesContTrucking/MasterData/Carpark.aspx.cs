using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_Carpark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "type='carpark' ";
        if (txt_search_Code.Text.Trim().Length > 0)
        {
            where += " and Code like '%" + txt_search_Code.Text.Trim() + "%' ";
        }
        if (txt_search_Name.Text.Trim().Length > 0)
        {
            where += " and Name like '%" + txt_search_Name.Text.Trim() + "%' ";
        }
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
        check_save(Code.Text);
        e.NewValues["Type"] = "carpark";
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    private void check_save(string Code)
    {
        if (Code.Trim().Length == 0)
        {
            throw new Exception("Code is request");
        }
    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox Code = this.grid_Transport.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
        check_save(Code.Text);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"], "");
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmMastData));
        }
    }
}