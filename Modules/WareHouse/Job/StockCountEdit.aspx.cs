using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Modules_WareHouse_Job_StockCountEdit : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_end.Date = DateTime.Today;
        }

        if (Session["DoInWhere"] != null)
        {
            this.dsStockCount.FilterExpression = "StockNo='" + Request.QueryString["no"].ToString() + "' ";
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
    }
    #region Stock
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { 
           
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.StockCount));
        }

    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        //e.NewValues["CreditDay"] = 0;
        e.NewValues["Count"] = 0;
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["StockNo"] = SafeValue.SafeString(e.NewValues["StockNo"]);
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
        e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        e.NewValues["PartyAdd"] = SafeValue.SafeString(e.NewValues["PartyAdd"]);
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Today);
        e.NewValues["StockDate"] = SafeValue.SafeDate(e.NewValues["StockDate"], DateTime.Today);
        e.NewValues["WareHouseId"] = SafeValue.SafeString(e.NewValues["WareHouseId"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = SafeValue.SafeString(e.NewValues["UpdateBy"]);
        e.NewValues["UpdateDateTime"] = SafeValue.SafeString(e.NewValues["UpdateDateTime"]);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["StockNo"] = SafeValue.SafeString(e.NewValues["StockNo"]);
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
        e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        e.NewValues["PartyAdd"] = SafeValue.SafeString(e.NewValues["PartyAdd"]);
        e.NewValues["DoDate"] = SafeValue.SafeDate(e.NewValues["DoDate"], DateTime.Today);
        e.NewValues["StockDate"] = SafeValue.SafeDate(e.NewValues["StockDate"],DateTime.Today);
        e.NewValues["WareHouseId"] = SafeValue.SafeString(e.NewValues["WareHouseId"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["CreateBy"] = SafeValue.SafeString(e.NewValues["CreateBy"]);
        e.NewValues["CreateDateTime"] = SafeValue.SafeDate(e.NewValues["CreateDateTime"], DateTime.Today);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }

    #endregion

    #region  Line
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.StockCountDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox txtDoNo = this.grid.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        this.dsStockCountDet.FilterExpression = " RefNo='" + txtDoNo.Text + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 0.000;
        e.NewValues["Qty2"] = 0.000;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Price"] = 0;
    }
    protected void grid_det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txtDoNo = this.grid.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["RefNo"] = SafeValue.SafeString(txtDoNo.Text);
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        if (SafeValue.SafeString(e.NewValues["LotNo"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the LotNo");
        }

        e.NewValues["Qty1"] = SafeValue.SafeDecimal(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeDecimal(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeDecimal(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["NewQty"] = SafeValue.SafeDecimal(e.NewValues["NewQty"], 0);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxTextBox;
        ASPxTextBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxTextBox;
        ASPxTextBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxTextBox;
        ASPxTextBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxTextBox;
        ASPxTextBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxTextBox;
        ASPxTextBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxTextBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"], DateTime.Today);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Uom"]= SafeValue.SafeString(e.NewValues["Uom"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
        e.NewValues["GrossWeight"] = SafeValue.SafeDecimal(e.NewValues["GrossWeight"], 0);
        e.NewValues["NettWeight"] = SafeValue.SafeDecimal(e.NewValues["NettWeight"], 0);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
    }
    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox txtDoNo = this.grid.FindEditFormTemplateControl("txt_DoNo") as ASPxTextBox;
        e.NewValues["RefNo"] = SafeValue.SafeString(txtDoNo.Text);
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        if (SafeValue.SafeString(e.NewValues["LotNo"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the LotNo");
        }

        e.NewValues["Qty1"] = SafeValue.SafeDecimal(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeDecimal(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeDecimal(e.NewValues["Qty3"], 0);
        //e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["NewQty"] = SafeValue.SafeDecimal(e.NewValues["NewQty"], 0);
        e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxTextBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxTextBox;
        ASPxTextBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxTextBox;
        ASPxTextBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxTextBox;
        ASPxTextBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxTextBox;
        ASPxTextBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxTextBox;
        ASPxTextBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxTextBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        e.NewValues["ExpiryDate"] = SafeValue.SafeDate(e.NewValues["ExpiryDate"], DateTime.Today);
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["Uom"] = SafeValue.SafeString(e.NewValues["Uom"]);

        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Packing"] = SafeValue.SafeString(e.NewValues["Packing"]);
        e.NewValues["GrossWeight"] = SafeValue.SafeDecimal(e.NewValues["GrossWeight"], 0);
        e.NewValues["NettWeight"] = SafeValue.SafeDecimal(e.NewValues["NettWeight"], 0);
        e.NewValues["Location"] = SafeValue.SafeString(e.NewValues["Location"]);
    }
    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    #endregion
}