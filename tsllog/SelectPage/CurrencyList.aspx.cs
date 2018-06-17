using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_CurrencyList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            btn_Sch_Click(null,null);
        }
        if (Session["Currency"] != null)
            this.dsCurrencyMast.FilterExpression = Session["Currency"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        if (name.Length > 0)
        {
            where= string.Format(" currencyId='{0}'", name);
        }
        this.dsCurrencyMast.FilterExpression = where;

    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXCurrency));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CurrencyExRate"] = 0;
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Id can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["CurrencyName"], "") == "")
            throw new Exception("Name can not be null!!!");
        e.NewValues["CurrencyId"] = SafeValue.SafeString(e.NewValues["CurrencyId"]);
        e.NewValues["CurrencyName"] = SafeValue.SafeString(e.NewValues["CurrencyName"]);
        e.NewValues["CurrencyExRate"] = SafeValue.SafeDecimal(e.NewValues["CurrencyExRate"], 1);
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Id can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["CurrencyName"], "") == "")
            throw new Exception("Name can not be null!!!");
        e.NewValues["CurrencyId"] = SafeValue.SafeString(e.NewValues["CurrencyId"]);
        e.NewValues["CurrencyName"] = SafeValue.SafeString(e.NewValues["CurrencyName"]);
        e.NewValues["CurrencyExRate"] = SafeValue.SafeDecimal(e.NewValues["CurrencyExRate"], 1);
    }
}
