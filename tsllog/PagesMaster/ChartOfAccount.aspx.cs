using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class MastData_ChartOfAccount : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXChartAcc));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["AcType"] = "B";
        e.NewValues["AcSubType"] = "FA";
        e.NewValues["AcDorc"] = "DB";
        e.NewValues["AcCurrency"] = "SGD";
        e.NewValues["AcBank"] = "N";
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string acCode = e.NewValues["Code"].ToString();
        if (acCode.Trim().Length == 0)
        {
            e.Cancel = true;
            throw new Exception("Please key in code");
        }
        else
        {
            string sql = "select count(*) from XXChartAcc where Code='"+acCode+"'";
            int cnt=SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql),0);
            if (cnt > 0)
            {
                e.Cancel = true;
                throw new Exception("double code");
            }
            else
            {
                ASPxComboBox subType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_AcSubType") as ASPxComboBox;
                e.NewValues["AcSubType"] = SafeValue.SafeString(subType.Value, "");
                ASPxComboBox gNo = this.ASPxGridView1.FindEditFormTemplateControl("cbo_AcGroup") as ASPxComboBox;
                e.NewValues["GNo"] = SafeValue.SafeString(gNo.Value, "");
            }
        }
        e.NewValues["AcDesc"] = SafeValue.SafeString(e.NewValues["AcDesc"]).Replace("'", "");
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox subType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_AcSubType") as ASPxComboBox;
        ASPxComboBox gNo = this.ASPxGridView1.FindEditFormTemplateControl("cbo_AcGroup") as ASPxComboBox;
        e.NewValues["GNo"] = SafeValue.SafeString(gNo.Value, "");
        e.NewValues["AcDesc"] = SafeValue.SafeString(e.NewValues["AcDesc"]).Replace("'", "");
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string sql = string.Format(@"select (select count(SequenceId) from XaArInvoice Where AcCode='{0}')+(select count(SequenceId) from XaArInvoiceDet Where AcCode='{0}')
+(select count(SequenceId) from XaArReceipt Where AcCode='{0}')+(select count(SequenceId) from XaArReceiptDet Where AcCode='{0}')
+(select count(SequenceId) from XaApPayable Where AcCode='{0}')+(select count(SequenceId) from XaApPayableDet Where AcCode='{0}')
+(select count(SequenceId) from XaApPayment Where AcCode='{0}')+(select count(SequenceId) from XaApPaymentDet Where AcCode='{0}')
+(select count(SequenceId) from XaGlEntryDet Where AcCode='{0}')", SafeValue.SafeString(e.Values["Code"]));
        int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        if (cnt > 0)
            throw new Exception("Have transactions, can't delete!");
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
            FillCityCombo(SafeValue.SafeString(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "AcType" })));
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("ChartOfAcc", true);
    }
    protected void cbo_AcSubType_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        FillCityCombo(e.Parameter);
    }
    // internal
    protected void FillCityCombo(string acType)
    {
        ASPxComboBox subType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_AcSubType") as ASPxComboBox;
        subType.Items.Clear();
        if (acType == "B")
        {
            subType.Items.Add("FIXED ASSETS", "FA");
            subType.Items.Add("CURRENT ASSETS", "CA");
            subType.Items.Add("OTHER ASSETS", "OA");
            subType.Items.Add("LONG TERM LIABILITIES", "LL");
            subType.Items.Add("CURRENT LIABILITIES", "CL");
            subType.Items.Add("OTHER LIABILITIES", "OL");
            subType.Items.Add("SHARE CAPITAL", "SC");
            subType.Items.Add("NON CURRENT ASSETS", "NCA");
        }
        else if (acType == "P")
        {
            subType.Items.Add("SALES", "S");
            subType.Items.Add("COST OF SALES", "C");
            subType.Items.Add("OPERATING EXPENSE", "E");
            subType.Items.Add("TAXATION", "T");
            subType.Items.Add("OTHER INCOME", "I");
            subType.Items.Add("OTHER EXPENSE", "O");
        }
        else
        {
        }
    }
}
