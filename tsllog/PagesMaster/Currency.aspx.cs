using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using Wilson.ORMapper;

public partial class MastData_Currency : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Currency"] = "1=1";
        }
        if (Session["Currency"] != null)
            this.dsCurrencyMast.FilterExpression = Session["Currency"].ToString();
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
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
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Currency", true);
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Id can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["CurrencyName"], "") == "")
            throw new Exception("Name can not be null!!!");
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Id can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["CurrencyName"], "") == "")
            throw new Exception("Name can not be null!!!");
        e.NewValues["CurrencyExRate"] = SafeValue.SafeDecimal(e.NewValues["CurrencyExRate"], 1);
    }
}
