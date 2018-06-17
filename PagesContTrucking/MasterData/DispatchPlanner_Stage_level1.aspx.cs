using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_DispatchPlanner_Stage_level1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ParId"] != null)
            {
                string parId = Request.QueryString["ParId"];
                lb_ParId.Text = parId;
            }
        }
        search_data();
    }
    private void search_data()
    {
        this.ds_stage.FilterExpression = "ParId='" + lb_ParId.Text + "'";
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Stage"] = SafeValue.SafeString(e.NewValues["Stage"]);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"],0);
        e.NewValues["ParId"] = SafeValue.SafeInt(lb_ParId.Text, 0);
    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"],"0");
    }
    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmDispatchPlannerStage));
        }
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Stage"] = SafeValue.SafeString(e.NewValues["Stage"]);
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"],0);
        
    }
}