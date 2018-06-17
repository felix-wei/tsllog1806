using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;

public partial class Modules_Warehouse_SelectPage_SelectProduct : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            Session["ProductWhere"] = null;
            if (Request.QueryString["PartyId"] != null)
            {
                this.dsProduct.FilterExpression = "1=0";
                // btn_Sch_Click(null, null);
            }
            
        }
        if (Session["ProductWhere"] != null)
        {
            this.dsProduct.FilterExpression = Session["ProductWhere"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string partyId = SafeValue.SafeString(Request.QueryString["PartyId"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        string sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12,REPLACE(Att13,char(39),'\&#39;') as Att13 from ref_product ";
        string typ = SafeValue.SafeString(Request.QueryString["type"]).ToLower();
        string type = SafeValue.SafeString(cbProductClass.Text);
        if (typ == "SO")
        {
            sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12
,isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='SO' and det.ProductCode=ref_product.code order by mast.DoDate desc,det.Price desc),0) as Att13
from ref_product";
        }
        else if (typ == "PO")
        {
            
            sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12
,isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='PO' and det.ProductCode=ref_product.code order by mast.DoDate desc,det.Price desc),0) as Att13
from ref_product";

        }
        else if (typ == "bonded" || typ == "licenced")
        {
            where = GetWhere(where, " OptionType = '" + typ + "'");

        }
        if (txt_CustId.Text.Trim() != "")
        {
            where = GetWhere(where, "  PartyId Like '%" + txt_CustId.Text.Replace("'", "''") + "%'");
        }
        
        else
        {
            if (partyId != "undefined" || Request.QueryString["PartyId"]!=null)
            {
                where = GetWhere(where, "  PartyId Like '%" + partyId.Replace("'", "''") + "%'");
            }
            
            if (name.Length > 0)
            {
                where = GetWhere(where, "  Name Like '%" + name.Replace("'", "''") + "%'");
            }
            if (type.Length > 0)
            {
                where = GetWhere(where, " ltrim(rtrim(ProductClass))='" + type + "'");
            }
        }     
        //if (where.Length > 0)
        //{
        //    where = " where " + where;
        //}
        sql += where + " ORDER BY Id ";

        this.ASPxGridView1.Visible = true;
        //this.ASPxGridView1.Visible = false;
        //DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        //this.ASPxGridView1.DataSource = tab;
        //this.ASPxGridView1.DataBind();
        dsProduct.FilterExpression = where;

        //if (type != "CIGR")
        //{
        //    this.ASPxGridView1.Visible = true;
        //    this.ASPxGridView1.Visible = false;
        //    //DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        //    //this.ASPxGridView1.DataSource = tab;
        //    //this.ASPxGridView1.DataBind();
        //    dsProduct.FilterExpression = where;

        //}
        //else
        //{
        //    this.ASPxGridView1.Visible = false;
        //    this.ASPxGridView1.Visible = true;
        //    //DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        //    dsProduct.FilterExpression = where;
        //}

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            AddOrUpdate();
            this.ASPxGridView1.CancelEdit();
        }
        else if (s == "Cancle")
            this.ASPxGridView1.CancelEdit();
    }
    protected void AddOrUpdate()
    {
        ASPxTextBox txt_pId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        ASPxTextBox txtCode = ASPxGridView1.FindEditFormTemplateControl("txt_ProductCode") as ASPxTextBox;
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
        ASPxTextBox txtName = ASPxGridView1.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
        string name = txtName.Text.Trim();
        if (txtName.Text == "")
        {
            throw new Exception("Full Name not be null!!!");
        }
        product.Name = name;
        ASPxButtonEdit txt_PartyId = ASPxGridView1.FindEditFormTemplateControl("btnParty") as ASPxButtonEdit;
        product.PartyId = txt_PartyId.Text;
        ASPxComboBox cbProductClass = ASPxGridView1.FindEditFormTemplateControl("cbProductClass") as ASPxComboBox;
        product.ProductClass = cbProductClass.Text.Trim();
        //ASPxComboBox cbCountry = grid.FindEditFormTemplateControl("cbCountry") as ASPxComboBox;
        //product.Country= cbCountry.Text;
        ASPxTextBox txt_BrandName = ASPxGridView1.FindEditFormTemplateControl("txt_BrandName") as ASPxTextBox;
        product.BrandName = txt_BrandName.Text;
        ASPxSpinEdit txt_QtyPackingWhole = ASPxGridView1.FindEditFormTemplateControl("txt_QtyPackingWhole") as ASPxSpinEdit;
        product.QtyPackingWhole = SafeValue.SafeInt(txt_QtyPackingWhole.Text, 0);
        ASPxMemo txt_Description = ASPxGridView1.FindEditFormTemplateControl("txt_Description") as ASPxMemo;
        product.Description = txt_Description.Text;
        ASPxSpinEdit txt_QtyWholeLoose = ASPxGridView1.FindEditFormTemplateControl("txt_QtyWholeLoose") as ASPxSpinEdit;
        product.QtyWholeLoose = SafeValue.SafeInt(txt_QtyWholeLoose.Text, 0);
        ASPxSpinEdit spin_LengthPacking = ASPxGridView1.FindEditFormTemplateControl("spin_LengthPacking") as ASPxSpinEdit;
        product.LengthPacking = SafeValue.SafeDecimal(spin_LengthPacking.Text);
        ASPxSpinEdit spin_WidthPacking = ASPxGridView1.FindEditFormTemplateControl("spin_WidthPacking") as ASPxSpinEdit;
        product.WidthPacking = SafeValue.SafeDecimal(spin_WidthPacking.Text);
        ASPxSpinEdit spin_HeightPacking = ASPxGridView1.FindEditFormTemplateControl("spin_HeightPacking") as ASPxSpinEdit;
        product.HeightPacking = SafeValue.SafeDecimal(spin_HeightPacking.Text);
        ASPxSpinEdit spin_VolumePacking = ASPxGridView1.FindEditFormTemplateControl("spin_VolumePacking") as ASPxSpinEdit;
        product.VolumePacking = SafeValue.SafeDecimal(spin_VolumePacking.Text);
        ASPxButtonEdit spin_UomPacking = ASPxGridView1.FindEditFormTemplateControl("spin_UomPacking") as ASPxButtonEdit;
        product.UomPacking = spin_UomPacking.Text;
        ASPxSpinEdit spin_LengthWhole = ASPxGridView1.FindEditFormTemplateControl("spin_LengthWhole") as ASPxSpinEdit;
        product.LengthWhole = SafeValue.SafeDecimal(spin_LengthWhole.Text);
        ASPxSpinEdit spin_WidthWhole = ASPxGridView1.FindEditFormTemplateControl("spin_WidthWhole") as ASPxSpinEdit;
        product.WidthWhole = SafeValue.SafeDecimal(spin_WidthWhole.Text);
        ASPxSpinEdit spin_HeightWhole = ASPxGridView1.FindEditFormTemplateControl("spin_HeightWhole") as ASPxSpinEdit;
        product.HeightWhole = SafeValue.SafeDecimal(spin_HeightWhole.Text);
        ASPxSpinEdit spin_VolumeWhole = ASPxGridView1.FindEditFormTemplateControl("spin_VolumeWhole") as ASPxSpinEdit;
        product.VolumeWhole = SafeValue.SafeDecimal(spin_VolumeWhole.Text);
        ASPxButtonEdit spin_UomWhole = ASPxGridView1.FindEditFormTemplateControl("spin_UomWhole") as ASPxButtonEdit;
        product.UomWhole = spin_UomWhole.Text;
        ASPxSpinEdit spin_VolumeLoose = ASPxGridView1.FindEditFormTemplateControl("spin_VolumeLoose") as ASPxSpinEdit;
        product.VolumeLoose = SafeValue.SafeDecimal(spin_VolumeLoose.Text);
        ASPxSpinEdit spin_LengthLoose = ASPxGridView1.FindEditFormTemplateControl("spin_LengthLoose") as ASPxSpinEdit;
        product.LengthLoose = SafeValue.SafeDecimal(spin_LengthLoose.Text);
        ASPxSpinEdit spin_WidthLoose = ASPxGridView1.FindEditFormTemplateControl("spin_WidthLoose") as ASPxSpinEdit;
        product.WidthLoose = SafeValue.SafeDecimal(spin_WidthLoose.Text);
        ASPxSpinEdit spin_HeightLoose = ASPxGridView1.FindEditFormTemplateControl("spin_HeightLoose") as ASPxSpinEdit;
        product.HeightLoose = SafeValue.SafeDecimal(spin_HeightLoose.Text);
        ASPxButtonEdit spin_UomLoose = ASPxGridView1.FindEditFormTemplateControl("spin_UomLoose") as ASPxButtonEdit;
        product.UomLoose = spin_UomLoose.Text;
        ASPxSpinEdit spin_CostPrice = ASPxGridView1.FindEditFormTemplateControl("spin_CostPrice") as ASPxSpinEdit;
        product.CostPrice = SafeValue.SafeDecimal(spin_CostPrice.Text);
        ASPxSpinEdit spin_Saleprice = ASPxGridView1.FindEditFormTemplateControl("spin_Saleprice") as ASPxSpinEdit;
        product.Saleprice = SafeValue.SafeDecimal(spin_Saleprice.Text);
        product.CreateBy = HttpContext.Current.User.Identity.Name;
        ASPxTextBox hsCode = ASPxGridView1.FindEditFormTemplateControl("txt_HsCode") as ASPxTextBox;
        product.HsCode = hsCode.Text;
        ASPxSpinEdit QtyLooseBase = ASPxGridView1.FindEditFormTemplateControl("txt_QtyLooseBase") as ASPxSpinEdit;
        product.QtyLooseBase = SafeValue.SafeInt(QtyLooseBase.Text, 0);

        ASPxButtonEdit txt_UomBase = ASPxGridView1.FindEditFormTemplateControl("txt_UomBase") as ASPxButtonEdit;
        product.UomBase = txt_UomBase.Text;
        product.CreateDateTime = DateTime.Now;

        ASPxComboBox cb_Att1 = ASPxGridView1.FindEditFormTemplateControl("cb_Att1") as ASPxComboBox;
        product.Att4 = SafeValue.SafeString(cb_Att1.Text);
        ASPxComboBox cb_Att2 = ASPxGridView1.FindEditFormTemplateControl("cb_Att2") as ASPxComboBox;
        product.Att5 = SafeValue.SafeString(cb_Att2.Text);
        ASPxComboBox cb_Att3 = ASPxGridView1.FindEditFormTemplateControl("cb_Att3") as ASPxComboBox;
        product.Att6 = SafeValue.SafeString(cb_Att3.Text);
        ASPxComboBox cb_Att4 = ASPxGridView1.FindEditFormTemplateControl("cb_Att4") as ASPxComboBox;
        product.Att7 = SafeValue.SafeString(cb_Att4.Text);
        ASPxComboBox cb_Att5 = ASPxGridView1.FindEditFormTemplateControl("cb_Att5") as ASPxComboBox;
        product.Att8 = SafeValue.SafeString(cb_Att5.Text);
        ASPxComboBox cb_Att6 = ASPxGridView1.FindEditFormTemplateControl("cb_Att6") as ASPxComboBox;
        product.Att9 = SafeValue.SafeString(cb_Att6.Text);
        ASPxTextBox txt_ArCode = ASPxGridView1.FindEditFormTemplateControl("txt_ArCode") as ASPxTextBox;
        product.ArCode = SafeValue.SafeString(txt_ArCode.Text.Trim());
        ASPxTextBox txt_ApCode = ASPxGridView1.FindEditFormTemplateControl("txt_ApCode") as ASPxTextBox;
        product.ApCode = SafeValue.SafeString(txt_ApCode.Text.Trim());
        if (txt_ArCode.Text.Trim() == "")
        {
            product.ArCode = System.Configuration.ConfigurationManager.AppSettings["ItemArCode"];
        }
        if (txt_ApCode.Text.Trim() == "")
        {
            product.ApCode = System.Configuration.ConfigurationManager.AppSettings["ItemApCode"];
        }
        ASPxSpinEdit MinOrderQty = ASPxGridView1.FindEditFormTemplateControl("spin_MinOrderQty") as ASPxSpinEdit;
        product.MinOrderQty = SafeValue.SafeInt(MinOrderQty.Text, 0);
        ASPxSpinEdit NewOrderQty = ASPxGridView1.FindEditFormTemplateControl("spin_NewOrderQty") as ASPxSpinEdit;
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

        ASPxComboBox cmb_OptionType = ASPxGridView1.FindEditFormTemplateControl("cmb_OptionType") as ASPxComboBox;
        product.OptionType =SafeValue.SafeString(cmb_OptionType.Value);
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
        Session["ProductWhere"] = "Code='" + product.Code + "'";
        this.dsProduct.FilterExpression = Session["ProductWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
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
}