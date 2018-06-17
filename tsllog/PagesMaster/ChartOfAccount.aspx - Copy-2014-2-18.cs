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
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {

    }
    protected void ASPxGridView1_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {

    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

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
            subType.Items.Add("LONG TERM LIABILITIES", "LL");
            subType.Items.Add("CURRENT LIABILITIES", "CL");
            subType.Items.Add("SHARE CAPITAL", "SC");
            subType.Items.Add("NON CURRENT ASSETS", "NCA");
        }
        else if (acType == "P")
        {
            subType.Items.Add("SALES", "S");
            subType.Items.Add("COST OF SALES", "C");
            subType.Items.Add("OPERATING EXPENSE", "O");
            subType.Items.Add("TAXATION", "T");
            subType.Items.Add("INCOME", "I");
        }
        else
        {
        }
    }
}
