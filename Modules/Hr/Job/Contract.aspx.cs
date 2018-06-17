using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Modules_Hr_Job_Contract : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Contract"] = null;
            this.dsContract.FilterExpression = "1=0";
        }
        if (Session["Contract"] != null)
        {
            this.dsContract.FilterExpression = Session["Contract"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string no = SafeValue.SafeString(txtSchNo.Value, "");
        string where = "";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        else if (no.Length > 0)
            where = string.Format("No='{0}'", no);
        else
        {
            where = "1=1";
        }
        this.dsContract.FilterExpression = where;
        Session["Contract"] = where;
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrContract));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Date"] = DateTime.Now;
        e.NewValues["StatusCode"] = "USE";
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
        e.NewValues["No"] = SafeValue.SafeString(e.NewValues["No"]);
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);
        e.NewValues["Remark3"] = SafeValue.SafeString(e.NewValues["Remark3"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}
