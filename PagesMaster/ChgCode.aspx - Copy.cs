using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class XX_ChgCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
  
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXChgCode));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ChgcodeDe"] = " ";
        e.NewValues["ChgUnit"] = "SET";
        e.NewValues["GstTypeId"] = "S";
        e.NewValues["ChgTypeId"] = "L";
        e.NewValues["GstP"] = 0;
        e.NewValues["ImpExpInd"] = "Import";
        e.NewValues["ArCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
        e.NewValues["ApCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["ArCodeExp"] = " ";
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("ChargeCode", true);
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
}
