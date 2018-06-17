using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_SelectPage_JobReceiptList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = SafeValue.SafeString(Request.QueryString["no"]);
            string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
            lbl_Id.Text = id;
            lbl_JobNo.Text = jobNo;
        }
    }
    #region Receipt
    protected void grid_receipt_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobReceipt));
    }
    protected void grid_receipt_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_receipt_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsReceipt.FilterExpression = "TrailerId=" + lbl_Id.Text + "";

    }
    protected void grid_receipt_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void grid_receipt_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grid_receipt_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
}