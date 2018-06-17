using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class Modules_Hr_Job_LeaveTmp : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string sql = "select Year from hr_LeaveTmp group by Year order by Year ASC";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
            txtSchYear.Items.Add(tab.Rows[i][0].ToString());
        this.txtSchYear.Text = DateTime.Today.Year.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["LeaveTmp"] = null;
            this.dsLeaveTmp.FilterExpression = "1=0";
        }
        if (Session["LeaveTmp"] != null)
        {
            this.dsLeaveTmp.FilterExpression = Session["LeaveTmp"].ToString();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string year = SafeValue.SafeString(txtSchYear.Value, "");
        string where = "1=1";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        if (year.Length > 0)
            where = GetWhere(where, string.Format("Year='{0}'", year));
        this.dsLeaveTmp.FilterExpression = where;
        Session["LeaveTmp"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("LeaveTemplate", true);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrLeaveTmp));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Year"] = DateTime.Now.Year;
        e.NewValues["Days"] = 0;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Year"] = SafeValue.SafeString(e.NewValues["Year"]);
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Days"] = SafeValue.SafeInt(e.NewValues["Days"],0);
        e.NewValues["LeaveType"] = SafeValue.SafeString(e.NewValues["LeaveType"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}
