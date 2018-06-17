using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_RateType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RateType));
        }
    }
    protected void gv_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Code"] = "";
        e.NewValues["Description"] = "";
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"]);
        e.NewValues["CreateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Today);
       
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"]);
        e.NewValues["UpdateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Today);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}