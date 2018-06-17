using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class WareHouse_Inventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["NameWhere"] = null;
            this.dsWhInventory.FilterExpression = "1=0";
        }
        if (Session["NameWhere"] != null)
        {
            this.dsWhInventory.FilterExpression = Session["NameWhere"].ToString();
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhInventory));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["InventoryNo"] = " ";
        e.NewValues["InventoryDate"] = DateTime.Now;
        e.NewValues["InventoryUser"] = "";
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;
    }
    protected void AddOrUpdate()
    {

        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_pId = pageControl.FindControl("txt_Id") as ASPxTextBox;
        string pId = SafeValue.SafeString(txt_pId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhInventory), "Id='" + pId + "'");
        WhInventory obj = C2.Manager.ORManager.GetObject(query) as WhInventory;
        bool action = false;
        string inventoryNo = "";
        if (obj == null)
        {
            action = true;
            obj = new WhInventory();
            inventoryNo = C2Setup.GetNextNo("Inventory");
        }

        ASPxTextBox txt_InventoryNo = pageControl.FindControl("txt_InventoryNo") as ASPxTextBox;
        obj.InventoryNo = txt_InventoryNo.Text.Trim();
        ASPxDateEdit date_InventoryDate = pageControl.FindControl("date_InventoryDate") as ASPxDateEdit;
        obj.InventoryDate = date_InventoryDate.Date;

        ASPxTextBox txt_InventoryUser = pageControl.FindControl("txt_InventoryUser") as ASPxTextBox;
        obj.InventoryUser = txt_InventoryUser.Text;

        if (action)
        {
            obj.InventoryNo = inventoryNo;
            obj.CreateBy = HttpContext.Current.User.Identity.Name;
            obj.CreateDateTime = DateTime.Now;
            C2Setup.SetNextNo("Inventory", inventoryNo);
            Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(obj);
            Session["NameWhere"] = "InventoryNo='" + inventoryNo + "'";
            this.dsWhInventory.FilterExpression = Session["NameWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
        else
        {
            obj.UpdateBy = HttpContext.Current.User.Identity.Name;
            obj.UpdateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(obj);
        }
    }

    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("InventoryNo LIKE '{0}%'", name);
        }
        else
        {
            where = "1=1";
        }
        this.dsWhInventory.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    #region Inventory det
    protected void grid_det_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhInventoryDet));
    }
    protected void grid_det_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsWhInventoryDet.FilterExpression = " LineINo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_det_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        e.NewValues["LineTNo"] =SafeValue.SafeInt( txt_id.Text,0);
        ASPxTextBox txt_InventoryNo = pageControl.FindControl("txt_InventoryNo") as ASPxTextBox;

        e.NewValues["InventoryNo"] = SafeValue.SafeInt(txt_InventoryNo.Text, 0);
    }
    protected void grid_det_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox pid = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_InventoryNo = pageControl.FindControl("txt_InventoryNo") as ASPxTextBox;
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["AdjustQty"] = SafeValue.SafeInt(e.NewValues["AdjustQty"], 0);
        e.NewValues["LineINo"] = SafeValue.SafeInt(pid.Text, 0);
        e.NewValues["InventoryNo"] = txt_InventoryNo.Text;
        e.NewValues["CreatedBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;

    }
    protected void grid_det_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Qty"] = SafeValue.SafeInt(e.NewValues["Qty"], 0);
        e.NewValues["AdjustQty"] = SafeValue.SafeInt(e.NewValues["AdjustQty"], 0);
        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }

    protected void grid_det_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_det_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        
    }
    #endregion
}