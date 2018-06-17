using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Xml;
using System.IO;

public partial class PagesMaster_TaskList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
           // string userId = HttpContext.Current.User.Identity.Name;
           // dsTask.FilterExpression =string.Format(@"CreateBy='{0}'",userId);
       
    }
    protected void grid_Detail_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql = "select Description from Tasks where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        grd.DataSource = ConnectSql.GetTab(sql);

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["TaskName"] = SafeValue.SafeString(e.NewValues["TaskName"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["TaskName"] = SafeValue.SafeString(e.NewValues["TaskName"]);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["TaskName"] = "";
        e.NewValues["Description"] = "";
        e.NewValues["TaskType"] = "";
        e.NewValues["StatusCode"] = "Pending";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Tasks));
        }
    }
}