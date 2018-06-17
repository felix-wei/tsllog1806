using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_Transhipment : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
            if (type.Length > 0)
                txt_Type.Text = type;
            this.txt_from.Date = DateTime.Today.AddDays(-15);
            this.txt_end.Date = DateTime.Today.AddDays(8);
            Session["Trans_" + this.txt_Type.Text] = null;
            this.dsTrans.FilterExpression = "1=0";
        }
        if (Session["Trans_" + this.txt_Type.Text] != null)
        {
            this.dsTrans.FilterExpression = Session["Trans_" + this.txt_Type.Text].ToString();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value, "");
        string dateFrom = "";
        string dateTo = "";
        string where = "Type='" + txt_Type.Text+"'";
        if (id.Length > 0)
            where = GetWhere(where,String.Format("Person='{0}'", id));
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format(" Date2 >= '{0}' and Date1 <= '{1}'", dateFrom, dateTo));
        }
        this.dsTrans.FilterExpression = where;
        Session["Trans_" + this.txt_Type.Text] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse(SafeValue.SafeString(txt_Type.Text,"Transshipment"), true);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonTran));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = 0;
        e.NewValues["Type"] = txt_Type.Text;
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["Date1"] = DateTime.Today;
        e.NewValues["Date2"] = DateTime.Today;
        e.NewValues["Time1"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        e.NewValues["Time2"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
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
        if (SafeValue.SafeString(e.NewValues["Person"],"0")=="0")
            throw new Exception("Name not be null !!!");
        e.NewValues["Type"] = SafeValue.SafeString(txt_Type.Text, "OT");
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], DateTime.Now);
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], DateTime.Now);
        e.NewValues["Time1"] = SafeValue.SafeString(e.NewValues["Time1"]);
        e.NewValues["Time2"] = SafeValue.SafeString(e.NewValues["Time2"]);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox id = ASPxGridView1.FindEditFormTemplateControl("cmb_Person") as ASPxComboBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Value, 0);
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], DateTime.Now);
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], DateTime.Now);
        e.NewValues["Hrs"] = SafeValue.SafeString(e.NewValues["Hrs"]);
        e.NewValues["Pic"] = SafeValue.SafeInt(e.NewValues["Pic"],0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
}
