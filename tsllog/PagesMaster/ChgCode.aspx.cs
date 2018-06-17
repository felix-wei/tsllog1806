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
        e.NewValues["ChgTypeId"] = "TRUCKING";
        e.NewValues["Quotation_Ind"] = "";
        e.NewValues["GstP"] = 0;
        e.NewValues["ImpExpInd"] = "Import";
        e.NewValues["ArCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
        e.NewValues["ApCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string sql = string.Format(@"select count(*) from xxchgcode where ChgcodeId='{0}'", e.NewValues["ChgcodeId"]);
        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (n > 0)
        {
            throw new Exception("Pls enter another code,Already exist");
        }
        e.NewValues["Quotation_Ind"] = SafeValue.SafeString(e.NewValues["Quotation_Ind"]);
        e.NewValues["ChgcodeId"] = SafeValue.SafeString(e.NewValues["ChgcodeId"]).Replace("'", "");
        e.NewValues["ChgcodeDe"] = SafeValue.SafeString(e.NewValues["ChgcodeDe"]).Replace("'", "");
        e.NewValues["ChgUnit"] = SafeValue.SafeString(e.NewValues["ChgUnit"]);
        e.NewValues["GstTypeId"] = SafeValue.SafeString(e.NewValues["GstTypeId"]);
        e.NewValues["GstP"] = SafeValue.SafeDecimal(e.NewValues["GstP"]);
        e.NewValues["ArCode"] = SafeValue.SafeString(e.NewValues["ArCode"]);
        e.NewValues["ApCode"] = SafeValue.SafeString(e.NewValues["ApCode"]);
        e.NewValues["ChgTypeId"] = SafeValue.SafeString(e.NewValues["ChgTypeId"]);
        e.NewValues["ArShortcutCode"] = SafeValue.SafeString(e.NewValues["ArShortcutCode"]);
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("ChargeCode", true);
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgcodeId"] = SafeValue.SafeString(e.NewValues["ChgcodeId"]).Replace("'", "");
        e.NewValues["ChgcodeDe"] = SafeValue.SafeString(e.NewValues["ChgcodeDe"]).Replace("'", "");
        e.NewValues["ChgUnit"] = SafeValue.SafeString(e.NewValues["ChgUnit"]);
        e.NewValues["GstTypeId"] = SafeValue.SafeString(e.NewValues["GstTypeId"]);
        e.NewValues["GstP"] = SafeValue.SafeDecimal(e.NewValues["GstP"]);
        e.NewValues["ArCode"] = SafeValue.SafeString(e.NewValues["ArCode"]);
        e.NewValues["ApCode"] = SafeValue.SafeString(e.NewValues["ApCode"]);
        e.NewValues["ChgTypeId"] = SafeValue.SafeString(e.NewValues["ChgTypeId"]);
        e.NewValues["ArShortcutCode"] = SafeValue.SafeString(e.NewValues["ArShortcutCode"]);
        e.NewValues["Quotation_Ind"] = SafeValue.SafeString(e.NewValues["Quotation_Ind"]);
    }
}
