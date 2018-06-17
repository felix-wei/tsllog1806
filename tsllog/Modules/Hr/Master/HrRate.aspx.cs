using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Hr_Master_HrRate : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["HrRate"] = "1=1";
        }
        if (Session["HrRate"] != null)
            this.dsHrRate.FilterExpression = Session["HrRate"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("HrRate", true);
    }
    #region HrGroup
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrRate));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PayItem"] = "";
        e.NewValues["EmployeeRate"] = 0;
        e.NewValues["EmployerRate"] = 0;
        e.NewValues["EmployeeRate55"] = 0;
        e.NewValues["EmployerRate55"] = 0;
        e.NewValues["EmployeeRate60"] = 0;
        e.NewValues["EmployerRate60"] = 0;
        e.NewValues["EmployeeRate65"] = 0;
        e.NewValues["EmployerRate65"] = 0;
        int day = DateTime.Today.Day;
        if (day < 20)
        {
            e.NewValues["FromDate"] = DateTime.Today.AddMonths(-1).AddDays(-DateTime.Today.Day + 1);
            e.NewValues["ToDate"] = DateTime.Today.AddDays(-DateTime.Today.Day + 1);

        }
        else
        {
            e.NewValues["FromDate"] = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            e.NewValues["ToDate"] = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.AddMonths(1).Day);
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
       // if (SafeValue.SafeString(e.NewValues["PayItem"], "") == "")
       //     throw new Exception("Payroll Item can not be null !!!");

    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
      //  if (SafeValue.SafeString(e.NewValues["PayItem"], "") == "")
       //     throw new Exception("Payroll Item can not be null !!!");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    #endregion
}