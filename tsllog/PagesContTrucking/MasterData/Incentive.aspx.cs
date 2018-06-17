using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_MasterData_Incentive : System.Web.UI.Page
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
        string where = string.Format(@"type='incentive'");
        if (!cbb_type.Text.Equals("ALL"))
        {
            where += string.Format(@" and type1='{0}'", cbb_type.Text);
        }
        this.dsTransport.FilterExpression = where;
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Type"] = "incentive";
        e.NewValues["Type1"] = SafeValue.SafeString(e.NewValues["Type1"], "Trip");
        e.NewValues["Code"] = "inc_" + e.NewValues["Type1"] + "_" + SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "N");
        after_saved(SafeValue.SafeString(e.NewValues["Name"], "N"), e.NewValues["Type1"].ToString());
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //if (SafeValue.SafeDecimal(e.Values["Price1"]) == 0)
        //{
        //    throw new Exception("This value [0] can't be delete!");
        //}
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
        e.NewValues["Type1"] = SafeValue.SafeString(e.NewValues["Type1"], "Trip");
        e.NewValues["Code"] = "inc_" + e.NewValues["Type1"] + "_" + SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Price1"] = SafeValue.SafeDecimal(e.NewValues["Price1"]);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"], "N");
        after_saved(SafeValue.SafeString(e.NewValues["Name"], "N"), e.NewValues["Type1"].ToString());
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Price1"] = 0;
        e.NewValues["Name"] = "N";
        e.NewValues["Type1"] = "Trip";
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmMastData));
        }
    }

    private void after_saved(string isDefault,string type)
    {
        if (isDefault.Equals("Y"))
        {
            string sql = string.Format(@"update CTM_MastData set Name='N' where Type='incentive' and type1='{0}' ", type);
            ConnectSql.ExecuteSql(sql);
        }
    }
    protected void grid_Transport_BeforePerformDataSelect(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
    }
}