using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_DispatchPlanner_Stage_level0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_search_Click(null, null);

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string stage = search_stage.Text;
        if (stage != "")
        {
            stage = " and Stage like '%" + stage + "%' ";
        }
        this.ds_stage.FilterExpression = "ParId=0" + stage;
    }
    protected void gv_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void gv_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void gv_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["SortIndex"] = SafeValue.SafeInt(e.NewValues["SortIndex"], 0);
        e.NewValues["ParId"] = 0;
    }
    protected void gv_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SortIndex"] = 0;
    }
    protected void gv_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmDispatchPlannerStage));
        }
    }
}