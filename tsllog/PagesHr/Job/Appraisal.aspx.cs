using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_Appraisal : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Appraisal"] = null;
            this.dsAppraisal.FilterExpression = "1=0";
        }
        if (Session["Appraisal"] != null)
        {
            this.dsAppraisal.FilterExpression = Session["Appraisal"].ToString();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string where = "";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        else
        {
            where = "1=1";
        }
        this.dsAppraisal.FilterExpression = where;
        Session["Appraisal"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Appraisal", true);
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonComment));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Date"] = DateTime.Now ;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {


    }
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox id = ASPxGridView1.FindEditFormTemplateControl("cmb_Person") as ASPxComboBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Value, 0);
        if (SafeValue.SafeString(e.NewValues["Person"], "0") == "0")
            throw new Exception("Name not be null !!!");
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox id = ASPxGridView1.FindEditFormTemplateControl("cmb_Person") as ASPxComboBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Value, 0);
        if (SafeValue.SafeString(e.NewValues["Person"], "0") == "0")
            throw new Exception("Name not be null !!!");
        e.NewValues["Manager"] = SafeValue.SafeString(e.NewValues["Manager"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["Rating"] = SafeValue.SafeString(e.NewValues["Rating"]);
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
}
