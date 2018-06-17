using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_DeliveryResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = string.Format(@"type='doresult'");
        
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Type"] = "doresult";
        e.NewValues["Code"] = Guid.NewGuid().ToString();
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //if (SafeValue.SafeDecimal(e.Values["Price1"]) == 0)
        //{
        //    throw new Exception("This value [0] can't be delete!");
        //}
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Type"] = "doresult";
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
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
    
    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
}