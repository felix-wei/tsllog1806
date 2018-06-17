using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Report_ProductPL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string prodectClass = this.cb_ProductClss.Text.Trim().ToUpper();
        string where = "";
        if (prodectClass.Length > 0)
        {
            where = string.Format(" ProductClass='{0}'", prodectClass);
        }
        else
        {
            where = "1=1";
        }
        this.dsProduct.FilterExpression = where;
        Session["NameWhere"] = where;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("ProductPL", true);
    }
}