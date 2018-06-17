using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_MastData_Product : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SkuWhere"] = null;
            this.dsProduct.FilterExpression = "1=0";
        }
        if (Session["SkuWhere"] != null)
        {
            this.dsProduct.FilterExpression = Session["SkuWhere"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("Code like '%{0}%' or NAME LIKE '%{0}%' or Description LIKE '%{0}%'", name.Replace("'", "''"));
        }
        else
        {
            where = "1=1";
        }
        this.dsProduct.FilterExpression = where;
        Session["SkuWhere"] = where;
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
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Delete")
        {
            
           // grid.DeleteRow();
            e.Result = "";
        }
    }
    #endregion
}