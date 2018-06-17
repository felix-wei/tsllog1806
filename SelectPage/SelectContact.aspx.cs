using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class SelectPage_SelectContact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["partyId"] != null)
            {
                string partyId = Request.QueryString["partyId"].ToString();
                //string sql = string.Format(@"select * from ref_contact where PartyId='{0}'",partyId);
                dsContact.FilterExpression = "PartyId='" + partyId + "' and Department in ('Operation','Finance')";
            }
        }
    }
    #region Contact
    protected void grid_contact_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.RefContact));
        }
    }
    protected void grid_contact_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string partyId = Request.QueryString["partyId"].ToString();
        e.NewValues["Address"] = "";
        e.NewValues["PartyId"] = partyId;
        e.NewValues["PartyName"] =EzshipHelper.GetPartyName(partyId);
    }
    protected void grid_contact_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string partyId = Request.QueryString["partyId"].ToString();
        e.NewValues["PartyId"] = partyId;
        e.NewValues["PartyName"] = EzshipHelper.GetPartyName(partyId);
        e.NewValues["IsDefault"] = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        e.NewValues["Department"] = SafeValue.SafeString(e.NewValues["Department"]);
    }
    protected void grid_contact_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string partyId = Request.QueryString["partyId"].ToString();
        e.NewValues["PartyId"] = partyId;
        e.NewValues["PartyName"] = EzshipHelper.GetPartyName(partyId);
        e.NewValues["IsDefault"] = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["Tel"] = SafeValue.SafeString(e.NewValues["Tel"]);
        e.NewValues["Fax"] = SafeValue.SafeString(e.NewValues["Fax"]);
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Email"] = SafeValue.SafeString(e.NewValues["Email"]);
        e.NewValues["Mobile"] = SafeValue.SafeString(e.NewValues["Mobile"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Department"] = SafeValue.SafeString(e.NewValues["Department"]);
    }
    protected void grid_contact_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_contact_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string partyId = Request.QueryString["partyId"].ToString();
        this.dsContact.FilterExpression = "PartyId='" + partyId + "' and len(PartyId)>0";
    }
    public string IsDefault(bool isDefault)
    {
        string res = "NO";
        if (isDefault)
            res = "YES";
        return res;
    }
    protected void grid_contact_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        string partyId = SafeValue.SafeString(e.NewValues["PartyId"]);
        string id = SafeValue.SafeString(e.Keys["Id"]);
        bool isDefault = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        if (isDefault == true)
        {
            string sql = string.Format(@"update ref_contact set IsDefault=0 where PartyId='{0}' and Id!={1}", partyId, id);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        else
        {
            string sql = string.Format(@"update ref_contact set IsDefault=1 where PartyId='{0}' and Id=(select top 1 Id from ref_contact where PartyId='{0}')", partyId);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    protected void grid_contact_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string partyId = SafeValue.SafeString(e.NewValues["PartyId"]);
        string sql = string.Format(@"select top 1 Id from ref_contact where PartyId='{0}' order by Id desc", partyId);
        string id = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql), "0");
        bool isDefault = SafeValue.SafeBool(e.NewValues["IsDefault"], true);
        if (isDefault == true)
        {
            sql = string.Format(@"update ref_contact set IsDefault=0 where PartyId='{0}' and Id!={1}", partyId, id);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
        else
        {
            sql = string.Format(@"update ref_contact set IsDefault=1 where PartyId='{0}' and Id=(select top 1 Id from ref_contact where PartyId='{0}')", partyId);
            ConnectSql_mb.ExecuteNonQuery(sql);
        }
    }
    #endregion
}