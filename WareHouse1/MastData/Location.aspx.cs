using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_MastData_Location : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["NameWhere"] = null;
            this.dsRefLocation.FilterExpression = "1=0";
        }
        if (Session["NameWhere"] != null)
        {
            this.dsRefLocation.FilterExpression = Session["NameWhere"].ToString();
        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefLocation));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
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
        ASPxLabel lblMess = pageControl.FindControl("lblMessage") as ASPxLabel;
        string id = SafeValue.SafeString(txt_Id.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefLocation), "Id='" + id + "'");
        RefLocation obj = C2.Manager.ORManager.GetObject(query) as RefLocation;
        bool action = false;
        string code = "";
        string pId = "";
        if (obj == null)
        {
            action = true;
            obj = new RefLocation();
            string sql = "select Code from ref_location";
            DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    code = dt.Rows[i]["Code"].ToString();
                    if (txt_Code.Text.ToUpper() == code.ToUpper())
                    {
                        throw new Exception("Please enter another value of the Short Name again!");
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
        ASPxTextBox txtName = pageControl.FindControl("txt_Name") as ASPxTextBox;
        string name = txtName.Text.Trim();
        if (txtName.Text == "")
        {
            throw new Exception("Name not be null!!!");
            return;
        }
        obj.Name = name;
        obj.Code = txt_Code.Text;
        ASPxButtonEdit txt_WarehouseCode = pageControl.FindControl("txt_WarehouseCode") as ASPxButtonEdit;
        obj.WarehouseCode = txt_WarehouseCode.Text;
        ASPxButtonEdit btn_ZoneCode = pageControl.FindControl("btn_ZoneCode") as ASPxButtonEdit;
        obj.ZoneCode = btn_ZoneCode.Text;
        ASPxButtonEdit txt_PartyId = pageControl.FindControl("txt_PartyId") as ASPxButtonEdit;
        obj.PartyId= txt_PartyId.Text;
        ASPxButtonEdit btn_StoreCode = pageControl.FindControl("btn_StoreCode") as ASPxButtonEdit;
        obj.StoreCode = btn_StoreCode.Text;
        ASPxSpinEdit txt_Length = pageControl.FindControl("txt_Length") as ASPxSpinEdit;
        obj.Length = SafeValue.SafeDecimal(txt_Length.Text);
        ASPxSpinEdit txt_Width = pageControl.FindControl("txt_Width") as ASPxSpinEdit;
        obj.Width = SafeValue.SafeDecimal(txt_Width.Text);
        ASPxSpinEdit txt_Height = pageControl.FindControl("txt_Height") as ASPxSpinEdit;
        obj.Height = SafeValue.SafeDecimal(txt_Height.Text);
        ASPxSpinEdit txt_SpaceM3 = pageControl.FindControl("txt_SpaceM3") as ASPxSpinEdit;
        obj.SpaceM3 = SafeValue.SafeDecimal(txt_SpaceM3.Text);
        ASPxMemo memoRemark = pageControl.FindControl("memo_Remark") as ASPxMemo;
        obj.Remark = memoRemark.Text;
        ASPxTextBox txt_Loclevel = pageControl.FindControl("txt_Loclevel") as ASPxTextBox;
        obj.Loclevel = txt_Loclevel.Text;

        
        if (action)
        {


            obj.CreateBy = HttpContext.Current.User.Identity.Name;
            obj.CreateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(obj, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(obj);

            Session["NameWhere"] = "Id='" + obj.Id+ "'";
            this.dsRefLocation.FilterExpression = Session["NameWhere"].ToString();
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

        string name = this.txt_Name.Text.Trim();
        string where = "";
        if (name.Length > 0)
        {
            where =GetWhere(where,string.Format("WarehouseCode=(select Code from ref_warehouse where Name like '{0}%') ", name));
        }
       where =GetWhere(where,"Loclevel='Unit'");

        this.dsRefLocation.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
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
            ASPxTextBox txt_PartyName = pageControl.FindControl("txt_PartyName") as ASPxTextBox;
            ASPxTextBox txt_WarehouseName = pageControl.FindControl("txt_WarehouseName") as ASPxTextBox;

            txt_PartyName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            txt_WarehouseName.Text = EzshipHelper.GetWarehouse(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "WarehouseCode" }));
        }

    }
}