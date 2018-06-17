using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_MastData_Product : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_Sch_Click(null,null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string where = "";
            string party = "";
            Session["SkuWhere"] = null;
            string user = HttpContext.Current.User.Identity.Name;
            string sql = string.Format(@"select OptionType from [dbo].[User] where Name='{0}'", user);
            string type = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql)).ToLower();
            if (type == "bonded" || type == "licenced")
            {
                where = GetWhere(where, string.Format(" OptionType = '" + type + "'"));

            }
            else {
                where = "1=1";
            }
            if (Request.QueryString["party"] != null)
            {
                party = Request.QueryString["party"].ToString();
                where = "PartyId='" + party + "'";
            }
            this.dsProduct.FilterExpression = where;
        }
        if (Session["SkuWhere"] != null)
        {
            this.dsProduct.FilterExpression = Session["SkuWhere"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string party = "";

        string name = this.txt_Name.Text.Trim().ToUpper();
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format(@"select OptionType from [dbo].[User] where Name='{0}'",user);
        string type = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql)).ToLower();
        
        string where = "";
       
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("Code like '%{0}%' or NAME LIKE '%{0}%' or Description LIKE '%{0}%'", name.Replace("'", "''")));
        }
        else {
            where = "1=1";
        }
        if (type == "bonded" || type == "licenced")
        {
            where =GetWhere(where, string.Format(" OptionType = '" + type + "'"));

        }
        if (Request.QueryString["party"] != null)
        {
            party = Request.QueryString["party"].ToString();
            where = "PartyId='" + party + "'";
        }
        this.dsProduct.FilterExpression = where;
        Session["SkuWhere"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region product
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefProduct));
        }
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Delete")
        {
            
           // grid.DeleteRow();
            e.Result = "";
        }
    }
    #endregion

    public string GetPartyName(string partyId) {
        return EzshipHelper.GetPartyName(partyId);
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
    }
}