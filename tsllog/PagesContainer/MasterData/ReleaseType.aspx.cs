using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class PagesMaster_Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        e.NewValues["Description"] = " ";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        e.NewValues["CodeType"] = "ReleaseType";
        ASPxTextBox txt_code = this.grid.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
        if (checkExist("0", txt_code.Text))
        {
            throw new Exception("This Code is exited");
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;

    }
   //
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    private bool checkExist(string id, string code)
    {
        string sql = "select COUNT(*) from XXUom where CodeType='ReleaseType' and Code='" + code + "' and Id<>" + id;
        int result = Convert.ToInt32(ConnectSql.GetTab(sql).Rows[0][0]);
        if (result > 0)
        {
            return true;
        }
        return false;
    }
}