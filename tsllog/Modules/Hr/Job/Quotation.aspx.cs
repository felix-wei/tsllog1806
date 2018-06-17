using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Modules_Hr_Job_Quote : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Quote"] = null;
            this.dsQuote.FilterExpression = "1=0";
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Quote"] = null;
            this.dsQuote.FilterExpression = "1=0";
            btn_Sch_Click(null,null);
        }
        if (Session["Quote"] != null)
        {
            this.dsQuote.FilterExpression = Session["Quote"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string where = "";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        if (txtPayItem.Text.Trim() != "")
            where = GetWhere(where, string.Format("PayItem='{0}'", SafeValue.SafeString(txtPayItem.Value)));
        if(where.Length==0)
        {
            where = "1=1";
        }
        this.dsQuote.FilterExpression = where;
        Session["Quote"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Quote_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrQuote));
    }
    protected void grid_Quote_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = 0;
        e.NewValues["Person"] = 0;
        e.NewValues["IsCal"] = "YES";
    }
    protected void grid_Quote_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

    }
    protected void grid_Quote_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

    }
    protected void grid_Quote_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
    protected void grid_Quote_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_Quote_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"], "").Length < 1)
            throw new Exception("Item not be null !!!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["PayItem"] = SafeValue.SafeString(e.NewValues["PayItem"]).Replace(",","");
        string sql = string.Format(@"select count(*) from Hr_Quote where Person={0} and PayItem='{1}'", SafeValue.SafeInt(e.NewValues["Person"], 0), SafeValue.SafeString(e.NewValues["PayItem"]));
        int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
        if(cnt>0){
            throw new Exception("This already exists !!!");
        }
        e.NewValues["IsCal"] = SafeValue.SafeString(e.NewValues["IsCal"]);
        //e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["CreateDateTime"] = DateTime.Today;
    }
    protected void grid_Quote_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"], "").Length < 1)
            throw new Exception("Item not be null !!!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
        e.NewValues["PayItem"] = SafeValue.SafeString(e.NewValues["PayItem"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["IsCal"] = SafeValue.SafeString(e.NewValues["IsCal"]);
        //string sql = string.Format(@"select count(*) from Hr_Quote where Person={0} and PayItem='{1}'", SafeValue.SafeInt(e.NewValues["Person"], 0), SafeValue.SafeString(e.NewValues["PayItem"]));
        //int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
        //if (cnt > 0)
        //{
        //    throw new Exception("This already exists !!!");
        //}
        //e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["UpdateDateTime"] = DateTime.Today;
    }
}
