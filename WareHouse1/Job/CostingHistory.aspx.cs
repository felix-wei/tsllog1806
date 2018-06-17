using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.IO;
using DevExpress.Web.ASPxUploadControl;
using C2;
using System.Data;

public partial class CostingHistory : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
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
        string from = "";
        string to = "";
        gridExport.WriteXlsToResponse(string.Format("Audit Record_{0}-{1}", from, to), true);
    }

    protected void grd_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Cost));
        }
    }
    protected void grd_Cost_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string sql = "select RefNo,CostIndex from Cost where SequenceId='" + SafeValue.SafeString(Request.Params["id"], "0") + "'";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.dsCost.FilterExpression = "RefNo='" + SafeValue.SafeString(dt.Rows[0]["RefNo"]) + "' and CostIndex='" + SafeValue.SafeString(dt.Rows[0]["CostIndex"]) + "' and SequenceId!='" + SafeValue.SafeString(Request.Params["id"], "0") + "' order by Version";
    }

    protected void grd_CostDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CostDet));
        }
    }
    protected void grd_CostDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        dsCostDet.FilterExpression = "ParentId='" + SafeValue.SafeString(grd.GetMasterRowFieldValues("SequenceId")) + "'";
    }
}
