using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class MastData_CustVendor : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXCustVendor));
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("CustVendor", true);
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //e.NewValues["CreditDay"] = 0;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string custId = SafeValue.SafeString(e.NewValues["CustId"], "");
        string vendorId = SafeValue.SafeString(e.NewValues["VendorId"], "");
        if (custId == "" || vendorId == "")
        {
            throw new Exception("Please select Party");
        }
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string custId = SafeValue.SafeString(e.NewValues["CustId"], "");
        string vendorId = SafeValue.SafeString(e.NewValues["VendorId"], "");
        if (custId == "" || vendorId == "")
        {
            throw new Exception("Please select Party");
        }
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
}
