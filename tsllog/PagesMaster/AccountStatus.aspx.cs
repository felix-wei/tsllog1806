using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class MastData_AccountStatus : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.spin_Period.Number = Convert.ToDecimal(DateTime.Today.Month);
            this.spin_Year.Number = Convert.ToDecimal(DateTime.Today.Year);
            Session["AcPeriodWhere"] = "1=0";
        }
        if (Session["AcPeriodWhere"] != null)
            this.dsAcStatus.FilterExpression = Session["AcPeriodWhere"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_filter_Click(object sender, EventArgs e)
    {
        this.dsAcStatus.FilterExpression = "AcPeriod='"+this.spin_Period.Text+"' and Year='" + this.spin_Year.Text+"'";
        Session["AcPeriodWhere"] = "AcPeriod='" + this.spin_Period.Text + "' and Year='" + this.spin_Year.Text + "'"; 
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXAccStatus));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Year"] = DateTime.Today.Year;
        e.NewValues["AcPeriod"] = 0;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
 
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

}
