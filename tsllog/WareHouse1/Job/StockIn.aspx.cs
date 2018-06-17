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
using DevExpress.Web.ASPxHtmlEditor;

public partial class WareHouse_Job_StockIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            txt_from.Date = DateTime.Today;
        }
    }
    #region Inventory Received
    protected void grid_Inventory_BeforePerformDataSelect(object sender, EventArgs e)
    {
    }

    protected void grid_Inventory_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobInventory));
        }
    }
    protected void grid_Inventory_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DoType"] = "WI";
        e.NewValues["Qty"] = 0;
        e.NewValues["DoDate"] = DateTime.Now;
    }
    protected void grid_Inventory_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["DoType"] = "WI";
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
        e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
        e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);
    }
    protected void grid_Inventory_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Inventory_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["DoType"] = SafeValue.SafeString(e.NewValues["DoType"]);
        e.NewValues["Unit"] = SafeValue.SafeString(e.NewValues["Unit"]);
        e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
        e.NewValues["Weight"] = SafeValue.SafeDecimal(e.NewValues["Weight"]);
        e.NewValues["Volume"] = SafeValue.SafeDecimal(e.NewValues["Volume"]);
        e.NewValues["PalletNo"] = SafeValue.SafeString(e.NewValues["PalletNo"]);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Now);
    }
    public string GetImgUrl(string id, string jobNo)
    {
        string url = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", id, jobNo))).Replace("\\", "/");
        return "/Photos/" + url;
    }
    #endregion
    #region Item Received Photo
    protected void grid_img_R_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;

        this.dsItemRImg.FilterExpression = "JobNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "' and FileType='Image'";
    }
    protected void grid_img_R_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_img_R_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobAttachment));
        }
    }
    protected void grid_img_R_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void grid_img_R_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    #endregion
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string date = "";
        if (txt_from.Value != null)
        {
            date = txt_from.Date.ToString("yyyy-MM-dd");
        }
        dsInventory.FilterExpression = "DoDate<='"+date+"' and DoType='WI'";
    }
}