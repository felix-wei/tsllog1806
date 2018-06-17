using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_MastData_WareHouse : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["NameWhere"] = null;
            this.dsRefWarehouse.FilterExpression = "1=0";
        }
        if (Session["NameWhere"] != null)
        {
            this.dsRefWarehouse.FilterExpression = Session["NameWhere"].ToString();
        }
    }

    #region WareHouse
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefWarehouse));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);

    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void AddOrUpdate()
    {

        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Id = pageControl.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        ASPxTextBox txtName = pageControl.FindControl("txtName") as ASPxTextBox;
        string id = SafeValue.SafeString(txt_Id.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefWarehouse), "Id='" + id + "'");
        RefWarehouse wh = C2.Manager.ORManager.GetObject(query) as RefWarehouse;
        bool action = false;
        string code = "";
        string name = txtName.Text;
        if (wh == null)
        {
            action = true;
            wh = new RefWarehouse();
           // Regex replaceSpace = new Regex(@"\s{1,}", RegexOptions.IgnoreCase);
            //name = replaceSpace.Replace(name, "_").Trim();
            string sql = "select Code from ref_warehouse";
            DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    code = dt.Rows[i]["Code"].ToString();
                    if (txt_Code.Text.ToUpper() == code.ToUpper())
                    {
                        throw new Exception("The Warehous code is duplicate!");
                        return;
                    }

                }
            }
        }
        if (txt_Code.Text == "")
        {
            throw new Exception("Code not be null!!!");
            return;
        }
        wh.Code = txt_Code.Text;
        if (txtName.Text == "")
        {
            throw new Exception("Name not be null!!!");
            return;
        }
        wh.Name = name;
        ASPxMemo memoAddress = pageControl.FindControl("txtAddress") as ASPxMemo;
        wh.Address = memoAddress.Text;
        ASPxTextBox txtTel1 = pageControl.FindControl("txtTel") as ASPxTextBox;
        wh.Tel = txtTel1.Text;
        ASPxTextBox txtFax1 = pageControl.FindControl("txtFax") as ASPxTextBox;
        wh.Fax = txtFax1.Text;
        ASPxTextBox txtContact1 = pageControl.FindControl("txtContact") as ASPxTextBox;
        wh.Contact = txtContact1.Text;
        ASPxMemo memoRemark = pageControl.FindControl("memoRemark") as ASPxMemo;
        wh.Remark = memoRemark.Text;
        ASPxComboBox cmb_StockType = pageControl.FindControl("cmb_StockType") as ASPxComboBox;
        wh.StockType= cmb_StockType.Text;
        if (action)
        {
            string num = SafeValue.SafeString(wh.Code);
            C2Setup.SetNextNo("WareHouse", num);

            wh.CreateBy = EzshipHelper.GetUserName();
            wh.CreateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(wh, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(wh);
            Session["NameWhere"] = "Code='" + wh.Code + "'";
            this.dsRefWarehouse.FilterExpression = Session["NameWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
        }
        else
        {
            wh.UpdateBy = EzshipHelper.GetUserName();
            wh.UpdateDateTime= DateTime.Now;
            Manager.ORManager.StartTracking(wh, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(wh);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("NAME LIKE '{0}%'", name);
        }
        else
        {
            where = "1=1";
        }
        this.dsRefWarehouse.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        AddOrUpdate();
        e.Cancel = true;
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
            txt_Code.Enabled = false;
        }

    }
    #endregion

    #region Zone
    protected void grid_Zone_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefLocation));
        }
    }
    protected void grid_Zone_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        e.NewValues["WarehouseCode"] = txt_Code.Text; ;
        e.NewValues["Address"] = " ";
        e.NewValues["ZoneCode"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["Loclevel"] = "Zone";

    }
    protected void grid_Zone_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string code = SafeValue.SafeString(e.Values["Code"]);
        string sql = string.Format("select count(*) from ref_location where ZoneCode='{0}' and Loclevel='Store'", code);
        int n =SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
        if(n>0){
            throw new Exception("Have Store,Can not Detele !!!");
            return;
        }
        else
           e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);

    }
    protected void grid_Zone_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void grid_Zone_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
    }
    protected void grid_Zone_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (e.NewValues["Code"] == null || e.NewValues["Code"].ToString().Trim() == "")
        {
            throw new Exception("Code not be null !!!");
            return;
        }
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        if (SafeValue.SafeString(e.NewValues["Code"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("The Code not null");
        }
        string sql = "select Code from ref_location where LocLevel='Zone' and WareHouseCode='" + txt_Code.Text + "'";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               string oldCode = dt.Rows[i]["Code"].ToString();
               string code = SafeValue.SafeString(e.NewValues["Code"]);
               if (code==oldCode)
                {
                    throw new Exception("Please enter another value of the Code again!");
                    return;
                }

            }
        }
        e.NewValues["WarehouseCode"] = txt_Code.Text; ;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Loclevel"] = "Zone";

    }
    protected void grid_Zone_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("pageControl_Zone") as ASPxPageControl;
            ASPxTextBox txt_Zone_Code = pageControl.FindControl("txt_Zone_Code") as ASPxTextBox;
            txt_Zone_Code.Enabled = false;
            ASPxTextBox txt_PartyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox txt_WarehouseName = pageControl.FindControl("txt_WarehouseName") as ASPxTextBox;

            txt_PartyName.Text = EzshipHelper.GetPartyName(grid.GetRowValues(grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            txt_WarehouseName.Text = EzshipHelper.GetWarehouse(grid.GetRowValues(grid.EditingRowVisibleIndex, new string[] { "WarehouseCode" }));
        }

    }
    protected void grid_Zone_BeforePerformDataSelect(object sender, EventArgs e)
    {
         ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
         ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        string where = "";
        if (txt_Code.Text.Trim() != "")
        {
            where = "LocLevel='Zone'";
            where += string.Format(" and WarehouseCode='{0}'", txt_Code.Text.Trim());
        }
        else {
            where = "1=0";
        }
        this.dsRefZone.FilterExpression = where;
    }
    protected void grid_Zone_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = grid.FindEditRowCellTemplateControl(null, "txt_ZoneCode") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
    #endregion

    #region Store
    protected void grid_Store_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefLocation));
        }
    }
    protected void grid_Store_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Address"] = " ";
        e.NewValues["ZoneCode"] =" ";
        e.NewValues["Remark"] = " ";
        e.NewValues["Loclevel"] = "Store";

    }
    protected void grid_Store_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_WarehouseCode = pageControl.FindControl("txt_Code") as ASPxTextBox;
        string code = SafeValue.SafeString(e.Values["Code"]);
        string sql = string.Format("select count(*) from ref_location where WareHouseCode='{1}' and StoreCode='{0}' and Loclevel='Unit'", code,txt_WarehouseCode.Text);
        int n = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (n > 0)
        {
            throw new Exception("Have Location,Can not Detele !!!");
            return;
        }
        else
           e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Store_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {

    } 
    protected void grid_Store_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);

        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Store_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        if (SafeValue.SafeString(e.NewValues["Code"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("The Code not null");
        }
        string sql = "select Code from ref_location where LocLevel='Store' and WareHouseCode='" + txt_Code.Text + "'";
        DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string oldCode = dt.Rows[i]["Code"].ToString();
                string code = SafeValue.SafeString(e.NewValues["Code"]);
                if (code == oldCode)
                {
                    throw new Exception("Please enter another value of the Code again!");
                    return;
                }

            }
        }
        e.NewValues["WarehouseCode"] = txt_Code.Text;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Loclevel"] = "Store";
        
    }
    protected void grid_Store_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
       

    }
    protected void grid_Store_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        this.dsRefStore.FilterExpression = "LocLevel='Store' and WarehouseCode='" + txt_Code .Text+ "'";
    }
    #endregion

    #region Location
    protected void grid_Location_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefLocation));
        }
    }
    protected void grid_Location_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        e.NewValues["WarehouseCode"] = txt_Code.Text;
        e.NewValues["Loclevel"] = "Unit";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["Height"] = 0;
        e.NewValues["SpaceM3"] = 0;
        e.NewValues["Length"] = 0;
        e.NewValues["Width"] = 0;
    }

    protected void grid_Location_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    protected void SaveLocation(ASPxGridView grid)
    {
        ASPxPageControl pageControl_Location = grid.FindEditFormTemplateControl("pageControl_Location") as ASPxPageControl;
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_WarehouseCode = pageControl.FindControl("txt_Code") as ASPxTextBox;
        ASPxTextBox txt_Id = pageControl_Location.FindControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txt_Code = pageControl_Location.FindControl("txt_Location_Code") as ASPxTextBox;
        string id = SafeValue.SafeString(txt_Id.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefLocation), "Id='" + id + "'");
        RefLocation obj = C2.Manager.ORManager.GetObject(query) as RefLocation;
        bool action = false;
        string code = "";
        if (obj == null)
        {
            action = true;
            obj = new RefLocation();
            string sql = "select Code from ref_location where LocLevel='Unit' and WarehouseCode='" + txt_WarehouseCode.Text + "'";
            DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    code = dt.Rows[i]["Code"].ToString();
                    if (txt_Code.Text.ToUpper() == code.ToUpper())
                    {
                        throw new Exception("Please enter another value of the Code again!");
                        return;
                    }

                }
            }
        }
        if (txt_Code.Text == "")
        {
            throw new Exception("Code not be null!!!");
            return;
        }
        obj.Code = txt_Code.Text;
        ASPxTextBox txtName = pageControl_Location.FindControl("txt_Location_Name") as ASPxTextBox;
        string name = txtName.Text.Trim();
        if (txtName.Text == "")
        {
            throw new Exception("Name not be null!!!");
            return;
        }
        obj.Name = name;
        obj.Code = txt_Code.Text;
        obj.WarehouseCode = txt_WarehouseCode.Text;
        ASPxButtonEdit txt_ZoneCode = pageControl_Location.FindControl("btn_L_ZoneCode") as ASPxButtonEdit;
        obj.ZoneCode = txt_ZoneCode.Text;
        ASPxButtonEdit txt_PartyId = pageControl_Location.FindControl("txt_PartyId") as ASPxButtonEdit;
        obj.PartyId = txt_PartyId.Text;
        ASPxButtonEdit txt_StoreCode = pageControl_Location.FindControl("btn_L_StoreCode") as ASPxButtonEdit;
        obj.StoreCode = txt_StoreCode.Text;
        ASPxSpinEdit txt_Length = pageControl_Location.FindControl("txt_Length") as ASPxSpinEdit;
        obj.Length = SafeValue.SafeDecimal(txt_Length.Text);
        ASPxSpinEdit txt_Width = pageControl_Location.FindControl("txt_Width") as ASPxSpinEdit;
        obj.Width = SafeValue.SafeDecimal(txt_Width.Text);
        ASPxSpinEdit txt_Height = pageControl_Location.FindControl("txt_Height") as ASPxSpinEdit;
        obj.Height = SafeValue.SafeDecimal(txt_Height.Text);
        ASPxSpinEdit txt_SpaceM3 = pageControl_Location.FindControl("txt_SpaceM3") as ASPxSpinEdit;
        obj.SpaceM3 = SafeValue.SafeDecimal(txt_SpaceM3.Text);
        ASPxMemo memoRemark = pageControl_Location.FindControl("memo_Remark") as ASPxMemo;
        obj.Remark = memoRemark.Text;
        if (action)
        {
            obj.CreateBy = EzshipHelper.GetUserName();
            obj.CreateDateTime = DateTime.Now;
            obj.Loclevel = "Unit";
            Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(obj);
        }
        else
        {
            obj.UpdateBy = EzshipHelper.GetUserName();
            obj.UpdateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(obj);
        }
    }

    protected void grid_Location_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        if (grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = grid.FindEditFormTemplateControl("pageControl_Location") as ASPxPageControl;
            ASPxTextBox txt_PartyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox txt_WarehouseName = pageControl.FindControl("txt_WarehouseName") as ASPxTextBox;
            ASPxTextBox txt_Location_Code = pageControl.FindControl("txt_Location_Code") as ASPxTextBox;
            txt_Location_Code.Enabled = false;

            txt_PartyName.Text = EzshipHelper.GetPartyName(grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
 
        }

    }
    protected void grid_Location_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        ASPxTextBox txt_Code = pageControl.FindControl("txt_Code") as ASPxTextBox;
        this.dsRefLocation.FilterExpression = "LocLevel='Unit' and WarehouseCode='" + txt_Code .Text+ "'";
    }
    protected void grid_Location_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        string s = e.Parameters;
        if (s == "Save")
        {
            SaveLocation(grid);
        }
    }
    protected void grid_Location_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        SaveLocation(grid);
    }
    protected void grid_Location_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        SaveLocation(grid);
    }
    #endregion

}