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
public partial class WareHouse_MastData_SellingPrice : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {

        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PriceWhere"] = null;
            this.dsPrice.FilterExpression = "DoType='SQ'";
        }
        if (Session["PriceWhere"] != null)
        {
            this.dsPrice.FilterExpression = Session["PriceWhere"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_SupplierName.Text.Trim().ToUpper();
        string where = " DoType='SQ'";
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("PartyName LIKE '{0}%'", name.Replace("'", "''")));
        }
        this.dsPrice.FilterExpression = where;
        Session["PriceWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region SO
    protected void grid_Price_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhTrans));
        }
    }


    #endregion

    
   
}