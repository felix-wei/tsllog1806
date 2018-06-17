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
using DevExpress.Web.ASPxDataView;
using Wilson.ORMapper;

public partial class WareHouse_Job_PoRequest : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Date_To.Date = DateTime.Today.AddDays(1);
            this.Date_From.Date = DateTime.Today.AddMonths(-6);
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name =SafeValue.SafeString(Request.QueryString["sku"]);
            if(name.Length>0){

            }
            Session["PoRequest"] = null;
            this.dsPoRequest.FilterExpression ="1=0";
        }
        if (Session["PoRequest"] != null)
            this.dsPoRequest.FilterExpression = Session["PoRequest"].ToString();
    }

    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sku = SafeValue.SafeString(txt_Product.Text, "");
        string where = " ";
        string dateFrom = "";
        string dateTo = "";
        if (sku.Length > 0)
            where = String.Format("Product like '{0}%'", sku);
        else
        {
            where = "1=1";
        }
        if (Date_From.Value != null && Date_To.Value != null)
        {
            dateFrom = Date_From.Date.ToString("yyyy-MM-dd");
            dateTo = Date_To.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format(" CreateDateTime >= '{0}' and CreateDateTime <= '{1}'", dateFrom, dateTo));
            //string.Format(" ref.Eta >= '{0}' and ref.Eta <= '{1}'", dateFrom, dateTo));
        }
        if (this.ckb_ShowAll.Checked)
        {
        }
        else
        {
            where = GetWhere(where, "isnull(PoNo, '')= ''");
        }
        Session["PoRequest"] = where;
        this.dsPoRequest.FilterExpression = Session["PoRequest"].ToString();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.WhPORequest));
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Qty1"] = 1;
        e.NewValues["Qty2"] = 0;
        e.NewValues["Qty3"] = 0;
        e.NewValues["Qty4"] = 0;
        e.NewValues["Qty5"] = 0;
        e.NewValues["QtyPackWhole"] = 1;
        e.NewValues["QtyWholeLoose"] = 1;
        e.NewValues["QtyLooseBase"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["RequestDateTime"] = DateTime.Today;
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["RequestDateTime"] = SafeValue.SafeDate(e.NewValues["RequestDateTime"],DateTime.Today);
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
        //e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;

        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Product"], "").Length < 1)
        {
            e.Cancel = true;
            throw new Exception("Pls select the Product");
        }
        e.NewValues["Qty1"] = SafeValue.SafeInt(e.NewValues["Qty1"], 0);
        e.NewValues["Qty2"] = SafeValue.SafeInt(e.NewValues["Qty2"], 0);
        e.NewValues["Qty3"] = SafeValue.SafeInt(e.NewValues["Qty3"], 0);
        e.NewValues["Price"] = SafeValue.SafeDecimal(e.NewValues["Price"], 0);
        e.NewValues["QtyPackWhole"] = SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0);
        e.NewValues["QtyWholeLoose"] = SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0);
        e.NewValues["QtyLooseBase"] = SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0);


        e.NewValues["Uom1"] = SafeValue.SafeString(e.NewValues["Uom1"]);
        e.NewValues["Uom2"] = SafeValue.SafeString(e.NewValues["Uom2"]);
        e.NewValues["Uom3"] = SafeValue.SafeString(e.NewValues["Uom3"]);
        e.NewValues["Uom4"] = SafeValue.SafeString(e.NewValues["Uom4"]);
        //e.NewValues["LotNo"] = SafeValue.SafeString(e.NewValues["LotNo"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox cb_Att1 = grid.FindEditRowCellTemplateControl(null, "cb_Att1") as ASPxComboBox;
        ASPxComboBox cb_Att2 = grid.FindEditRowCellTemplateControl(null, "cb_Att2") as ASPxComboBox;
        ASPxComboBox cb_Att3 = grid.FindEditRowCellTemplateControl(null, "cb_Att3") as ASPxComboBox;
        ASPxComboBox cb_Att4 = grid.FindEditRowCellTemplateControl(null, "cb_Att4") as ASPxComboBox;
        ASPxComboBox cb_Att5 = grid.FindEditRowCellTemplateControl(null, "cb_Att5") as ASPxComboBox;
        ASPxComboBox cb_Att6 = grid.FindEditRowCellTemplateControl(null, "cb_Att6") as ASPxComboBox;
        e.NewValues["Att1"] = SafeValue.SafeString(cb_Att1.Text);
        e.NewValues["Att2"] = SafeValue.SafeString(cb_Att2.Text);
        e.NewValues["Att3"] = SafeValue.SafeString(cb_Att3.Text);
        e.NewValues["Att4"] = SafeValue.SafeString(cb_Att4.Text);
        e.NewValues["Att5"] = SafeValue.SafeString(cb_Att5.Text);
        e.NewValues["Att6"] = SafeValue.SafeString(cb_Att6.Text);
        //e.NewValues["Att7"] = SafeValue.SafeString(e.NewValues["Att7"]);
        //e.NewValues["Att8"] = SafeValue.SafeString(e.NewValues["Att8"]);
        //e.NewValues["Att9"] = SafeValue.SafeString(e.NewValues["Att9"]);
        //e.NewValues["Att10"] = SafeValue.SafeString(e.NewValues["Att10"]);

        e.NewValues["Des1"] = SafeValue.SafeString(e.NewValues["Des1"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;

        string packing = "1" + SafeValue.SafeString(e.NewValues["Uom1"]); ;
        if (SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom2"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyPackWhole"], 0), SafeValue.SafeString(e.NewValues["Uom2"]));
        if (SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom3"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyWholeLoose"], 0), SafeValue.SafeString(e.NewValues["Uom3"]));
        if (SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0) > 0 && SafeValue.SafeString(e.NewValues["Uom4"]).Length > 0)
            packing += string.Format("x{0}{1}", SafeValue.SafeInt(e.NewValues["QtyLooseBase"], 0), SafeValue.SafeString(e.NewValues["Uom4"]));

        e.NewValues["Packing"] = packing;
        for (int i = 0; i < e.NewValues.Count; i++)
        {
            if (e.NewValues[i] == null)
            {
                e.NewValues[i] = "";
            }
        }
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}