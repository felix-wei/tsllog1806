using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_SelectPage_SelectProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "1=1";
        string sql = @"SELECT Id,Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;')  as Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PName FROM ref_product where ";
        if (name.Length > 0)
        {
           where =GetWhere(where, string.Format(" Name Like '{0}%'", name.Replace("'", "''")));
        }
        else
        {
            //sql += "1=1";
        }
        //sql += " ORDER BY code ";

        //DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
        dsProduct.FilterExpression = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }

    #region product
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefProduct));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["BrandName"] = "";
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["UomPacking"] = "";
        e.NewValues["UomWhole"] = "";
        e.NewValues["UomLoose"] = "";
        e.NewValues["ProductType"] = "Component Part";
        e.NewValues["Description"] = "";
        e.NewValues["VolumeLoose"] = 0;
        e.NewValues["VolumeWhole"] = 0;
        e.NewValues["VolumePacking"] = 0;
        e.NewValues["HeightLoose"] = 0;
        e.NewValues["HeightWhole"] = 0;
        e.NewValues["HeightPacking"] = 0;
        e.NewValues["WidthLoose"] = 0;
        e.NewValues["WidthWhole"] = 0;
        e.NewValues["WidthPacking"] = 0;
        e.NewValues["LengthLoose"] = 0;
        e.NewValues["LengthWhole"] = 0;
        e.NewValues["LengthPacking"] = 0;
        e.NewValues["CostPrice"] = 0;
        e.NewValues["Saleprice"] = 0;
        e.NewValues["LengthPacking"] = 0;
        e.NewValues["MinOrderQty"] = 0;
        e.NewValues["NewOrderQty"] = 0;
        e.NewValues["QtyWholeLoose"] = 0;
        e.NewValues["QtyPackingWhole"] = 1;
        e.NewValues["QtyLooseBase"] = 0;
        e.NewValues["ProductClass"] = "STOCK";
        e.NewValues["OptionType"] = "All";
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["ArCode"] = System.Configuration.ConfigurationManager.AppSettings["ItemArCode"];
        e.NewValues["ApCode"] = System.Configuration.ConfigurationManager.AppSettings["ItemApCode"];
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
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;

        if (s == "Save")
        {
            AddOrUpdate();
            this.grid.CancelEdit();
        }
        //else if(s=="Cancle"){
        //    this.grid.CancelEdit();
        //}
    }
    protected string AddOrUpdate()
    {
        ASPxTextBox txt_pId = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txtCode = grid.FindEditFormTemplateControl("txt_ProductCode") as ASPxTextBox;
        string pId = SafeValue.SafeString(txt_pId.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(RefProduct), "Id='" + pId + "'");
        RefProduct product = C2.Manager.ORManager.GetObject(query) as RefProduct;
        bool action = false;
        string code = "";

        if (product == null)
        {
            action = true;
            product = new RefProduct();
            string sql = "select Code from ref_product";
            DataTable dt = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    code = dt.Rows[i]["Code"].ToString();
                    if (txtCode.Text.ToUpper() == code.ToUpper())
                    {
                        throw new Exception("Please enter another value of the Code again!");
                        return "";
                    }
                }
            }
        }
        if (txtCode.Text == "")
        {
            code = C2Setup.GetNextNo("", "Product", DateTime.Now);
        }
        else
        {
            code = txtCode.Text;
        }
        product.Code = code;
        ASPxTextBox txtName = grid.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
        string name = txtName.Text.Trim();
        if (txtName.Text == "")
        {
            throw new Exception("Full Name not be null!!!");
            return "";
        }
        product.Name = name;
        ASPxButtonEdit txt_PartyId = grid.FindEditFormTemplateControl("btnParty") as ASPxButtonEdit;
        product.PartyId = txt_PartyId.Text;
        ASPxComboBox cbProductClass = grid.FindEditFormTemplateControl("cbProductClass") as ASPxComboBox;
        product.ProductClass = cbProductClass.Text.Trim();
        //ASPxComboBox cbCountry = grid.FindEditFormTemplateControl("cbCountry") as ASPxComboBox;
        //product.Country= cbCountry.Text;
        ASPxTextBox txt_BrandName = grid.FindEditFormTemplateControl("txt_BrandName") as ASPxTextBox;
        product.BrandName = txt_BrandName.Text;
        ASPxSpinEdit txt_QtyPackingWhole = grid.FindEditFormTemplateControl("txt_QtyPackingWhole") as ASPxSpinEdit;
        product.QtyPackingWhole = SafeValue.SafeInt(txt_QtyPackingWhole.Text, 0);
        ASPxMemo txt_Description = grid.FindEditFormTemplateControl("txt_Description") as ASPxMemo;
        product.Description = txt_Description.Text;
        ASPxSpinEdit txt_QtyWholeLoose = grid.FindEditFormTemplateControl("txt_QtyWholeLoose") as ASPxSpinEdit;
        product.QtyWholeLoose = SafeValue.SafeInt(txt_QtyWholeLoose.Text, 0);
        ASPxSpinEdit spin_LengthPacking = grid.FindEditFormTemplateControl("spin_LengthPacking") as ASPxSpinEdit;
        product.LengthPacking = SafeValue.SafeDecimal(spin_LengthPacking.Text);
        ASPxSpinEdit spin_WidthPacking = grid.FindEditFormTemplateControl("spin_WidthPacking") as ASPxSpinEdit;
        product.WidthPacking = SafeValue.SafeDecimal(spin_WidthPacking.Text);
        ASPxSpinEdit spin_HeightPacking = grid.FindEditFormTemplateControl("spin_HeightPacking") as ASPxSpinEdit;
        product.HeightPacking = SafeValue.SafeDecimal(spin_HeightPacking.Text);
        ASPxSpinEdit spin_VolumePacking = grid.FindEditFormTemplateControl("spin_VolumePacking") as ASPxSpinEdit;
        product.VolumePacking = SafeValue.SafeDecimal(spin_VolumePacking.Text);
        ASPxButtonEdit spin_UomPacking = grid.FindEditFormTemplateControl("spin_UomPacking") as ASPxButtonEdit;
        product.UomPacking = spin_UomPacking.Text;
        ASPxSpinEdit spin_LengthWhole = grid.FindEditFormTemplateControl("spin_LengthWhole") as ASPxSpinEdit;
        product.LengthWhole = SafeValue.SafeDecimal(spin_LengthWhole.Text);
        ASPxSpinEdit spin_WidthWhole = grid.FindEditFormTemplateControl("spin_WidthWhole") as ASPxSpinEdit;
        product.WidthWhole = SafeValue.SafeDecimal(spin_WidthWhole.Text);
        ASPxSpinEdit spin_HeightWhole = grid.FindEditFormTemplateControl("spin_HeightWhole") as ASPxSpinEdit;
        product.HeightWhole = SafeValue.SafeDecimal(spin_HeightWhole.Text);
        ASPxSpinEdit spin_VolumeWhole = grid.FindEditFormTemplateControl("spin_VolumeWhole") as ASPxSpinEdit;
        product.VolumeWhole = SafeValue.SafeDecimal(spin_VolumeWhole.Text);
        ASPxButtonEdit spin_UomWhole = grid.FindEditFormTemplateControl("spin_UomWhole") as ASPxButtonEdit;
        product.UomWhole = spin_UomWhole.Text;
        ASPxSpinEdit spin_VolumeLoose = grid.FindEditFormTemplateControl("spin_VolumeLoose") as ASPxSpinEdit;
        product.VolumeLoose = SafeValue.SafeDecimal(spin_VolumeLoose.Text);
        ASPxSpinEdit spin_LengthLoose = grid.FindEditFormTemplateControl("spin_LengthLoose") as ASPxSpinEdit;
        product.LengthLoose = SafeValue.SafeDecimal(spin_LengthLoose.Text);
        ASPxSpinEdit spin_WidthLoose = grid.FindEditFormTemplateControl("spin_WidthLoose") as ASPxSpinEdit;
        product.WidthLoose = SafeValue.SafeDecimal(spin_WidthLoose.Text);
        ASPxSpinEdit spin_HeightLoose = grid.FindEditFormTemplateControl("spin_HeightLoose") as ASPxSpinEdit;
        product.HeightLoose = SafeValue.SafeDecimal(spin_HeightLoose.Text);
        ASPxButtonEdit spin_UomLoose = grid.FindEditFormTemplateControl("spin_UomLoose") as ASPxButtonEdit;
        product.UomLoose = spin_UomLoose.Text;
        ASPxSpinEdit spin_CostPrice = grid.FindEditFormTemplateControl("spin_CostPrice") as ASPxSpinEdit;
        product.CostPrice = SafeValue.SafeDecimal(spin_CostPrice.Text);
        ASPxSpinEdit spin_Saleprice = grid.FindEditFormTemplateControl("spin_Saleprice") as ASPxSpinEdit;
        product.Saleprice = SafeValue.SafeDecimal(spin_Saleprice.Text);
        product.CreateBy = HttpContext.Current.User.Identity.Name;
        ASPxTextBox hsCode = grid.FindEditFormTemplateControl("txt_HsCode") as ASPxTextBox;
        product.HsCode = hsCode.Text;
        ASPxSpinEdit QtyLooseBase = grid.FindEditFormTemplateControl("txt_QtyLooseBase") as ASPxSpinEdit;
        product.QtyLooseBase = SafeValue.SafeInt(QtyLooseBase.Text, 0);

        ASPxButtonEdit txt_UomBase = grid.FindEditFormTemplateControl("txt_UomBase") as ASPxButtonEdit;
        product.UomBase = txt_UomBase.Text;
        product.CreateDateTime = DateTime.Now;

        ASPxComboBox cb_Att1 = grid.FindEditFormTemplateControl("cb_Att1") as ASPxComboBox;
        product.Att4 = SafeValue.SafeString(cb_Att1.Text);
        ASPxComboBox cb_Att2 = grid.FindEditFormTemplateControl("cb_Att2") as ASPxComboBox;
        product.Att5 = SafeValue.SafeString(cb_Att2.Text);
        ASPxComboBox cb_Att3 = grid.FindEditFormTemplateControl("cb_Att3") as ASPxComboBox;
        product.Att6 = SafeValue.SafeString(cb_Att3.Text);
        ASPxComboBox cb_Att4 = grid.FindEditFormTemplateControl("cb_Att4") as ASPxComboBox;
        product.Att7 = SafeValue.SafeString(cb_Att4.Text);
        ASPxComboBox cb_Att5 = grid.FindEditFormTemplateControl("cb_Att5") as ASPxComboBox;
        product.Att8 = SafeValue.SafeString(cb_Att5.Text);
        ASPxComboBox cb_Att6 = grid.FindEditFormTemplateControl("cb_Att6") as ASPxComboBox;
        product.Att9 = SafeValue.SafeString(cb_Att6.Text);
        ASPxTextBox txt_ArCode = grid.FindEditFormTemplateControl("txt_ArCode") as ASPxTextBox;
        product.ArCode = SafeValue.SafeString(txt_ArCode.Text.Trim());
        ASPxTextBox txt_ApCode = grid.FindEditFormTemplateControl("txt_ApCode") as ASPxTextBox;
        product.ApCode = SafeValue.SafeString(txt_ApCode.Text.Trim());
        if (txt_ArCode.Text.Trim() == "")
        {
            product.ArCode = System.Configuration.ConfigurationManager.AppSettings["ItemArCode"];
        }
        if (txt_ApCode.Text.Trim() == "")
        {
            product.ApCode = System.Configuration.ConfigurationManager.AppSettings["ItemApCode"];
        }
        ASPxSpinEdit MinOrderQty = grid.FindEditFormTemplateControl("spin_MinOrderQty") as ASPxSpinEdit;
        product.MinOrderQty = SafeValue.SafeInt(MinOrderQty.Text, 0);
        ASPxSpinEdit NewOrderQty = grid.FindEditFormTemplateControl("spin_NewOrderQty") as ASPxSpinEdit;
        product.NewOrderQty = SafeValue.SafeInt(NewOrderQty.Text, 0);
        //ASPxComboBox cb_Att7 = grid.FindEditFormTemplateControl("cb_Att7") as ASPxComboBox;
        //product.Att10 = SafeValue.SafeString(cb_Att7.Text);
        //ASPxComboBox cb_Att8 = grid.FindEditFormTemplateControl("cb_Att8") as ASPxComboBox;
        //product.Att11 = SafeValue.SafeString(cb_Att8.Text);
        //ASPxComboBox cb_Att9 = grid.FindEditFormTemplateControl("cb_Att9") as ASPxComboBox;
        //product.Att12 = SafeValue.SafeString(cb_Att9.Text);
        //ASPxComboBox cb_Att10 = grid.FindEditFormTemplateControl("cb_Att10") as ASPxComboBox;
        //product.Att13 = SafeValue.SafeString(cb_Att10.Text);
        product.Att1 = "1" + product.UomPacking;
        if (product.QtyPackingWhole > 0 && product.UomWhole.Length > 0)
            product.Att1 += string.Format("x{0}{1}", product.QtyPackingWhole, product.UomWhole);
        if (product.QtyWholeLoose > 0 && product.UomLoose.Length > 0)
            product.Att1 += string.Format("x{0}{1}", product.QtyWholeLoose, product.UomLoose);
        if (product.QtyLooseBase > 0 && product.UomBase.Length > 0)
            product.Att1 += string.Format("x{0}{1}", product.QtyLooseBase, product.UomBase);

        ASPxComboBox cmb_OptionType = grid.FindEditFormTemplateControl("cmb_OptionType") as ASPxComboBox;
        product.OptionType = SafeValue.SafeString(cmb_OptionType.Value);
        product.Att4 = SafeValue.SafeString(cb_Att1.Text);

        if (txt_UomBase.Text == "Packing")
            product.Att3 = spin_UomPacking.Text;
        else if (txt_UomBase.Text == "Whole")
            product.Att3 = spin_UomWhole.Text;
        else
            product.Att3 = spin_UomLoose.Text;
        if (cbProductClass.Text == "CIGR")
        {
            product.UomPacking = "PKG";
            product.UomWhole = "BOX";
            product.UomLoose = "PKT";
            product.UomBase = "STK";
        }
        if (action)
        {
            product.StatusCode = "USE";
            product.CreateBy = EzshipHelper.GetUserName();
            product.CreateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(product, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(product);
            txt_pId.Text = product.Id.ToString();
            C2Setup.SetNextNo("", "Product", code, DateTime.Now);
        }
        else
        {
            product.UpdateBy = EzshipHelper.GetUserName();
            product.UpdateDateTime = DateTime.Now;
            Manager.ORManager.StartTracking(product, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(product);
        }
        Session["SkuWhere"] = "Code='" + product.Code + "'";
        this.dsProduct.FilterExpression = Session["SkuWhere"].ToString();
        if (this.grid.GetRow(0) != null)
            this.grid.StartEdit(0);
        return product.Code;
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox txt_pId = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            txt_pId.Text = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            ASPxTextBox txt_Code = grid.FindEditFormTemplateControl("txt_ProductCode") as ASPxTextBox;
            txt_Code.ReadOnly = true;
            txt_Code.BackColor = System.Drawing.Color.LightGray;

            ASPxTextBox txt_PartyName = grid.FindEditFormTemplateControl("txt_PartyName") as ASPxTextBox;
            txt_PartyName.Text = EzshipHelper.GetPartyName(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "PartyId" }));
            string id = SafeValue.SafeString(this.grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }));
            if (id.Length > 0)
            {
                string sql = string.Format("select StatusCode from ref_product where Id={0}", id);
                string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");// 
                txt_Code.ReadOnly = true;
                txt_Code.BackColor = System.Drawing.Color.LightGray;
                //ASPxButton btn_Block = this.grid.FindEditFormTemplateControl("btn_Block") as ASPxButton;
                //if (status == "USE")
                //{
                //    btn_Block.Text = "Block";
                //}
                //else
                //{
                //    btn_Block.Text = "UnBlock";
                //}
            }

        }
    }

    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        ASPxTextBox id = grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox code = grid.FindEditFormTemplateControl("txt_ProductCode") as ASPxTextBox;
        if (s == "Block")
        {
            #region Block
            string sql = "select StatusCode from ref_product where Code='" + code.Text + "'";
            string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
            if (status == "InActive")
            {
                sql = string.Format("update ref_product set StatusCode='USE',UpdateBy='{1}',UpdateDateTime='{2}' where Code='{0}'", code.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            else
            {
                sql = string.Format("update ref_product set StatusCode='InActive',UpdateBy='{1}',UpdateDateTime='{2}' where Code='{0}'", code.Text, EzshipHelper.GetUserName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int res = Manager.ORManager.ExecuteCommand(sql);
                if (res > 0)
                {
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Fail";
                }
            }
            #endregion
        }
        else if (s == "Save")
        {
            e.Result = AddOrUpdate();
            this.grid.CancelEdit();
        }

    }

    #endregion
    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxTextBox refN = this.grid.FindEditFormTemplateControl("txt_ProductCode") as ASPxTextBox;
        this.dsPhoto.FilterExpression = "RefNo='" + refN.Text + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
    }
    #endregion
}