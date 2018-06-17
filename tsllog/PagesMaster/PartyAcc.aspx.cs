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

public partial class PagesMaster_PartyAcc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXPartyAcc));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ArCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalArCode"];
        e.NewValues["ApCode"] = System.Configuration.ConfigurationManager.AppSettings["LocalApCode"];
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string sId = e.Values["SequenceId"].ToString();
        if (sId.Length > 0)
        {
            string sql = string.Format("delete from XXPartyAcc where SequenceId='{0}'", sId);
            int res = Manager.ORManager.ExecuteCommand(sql);
            e.Cancel = true;
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Currency can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["ArCode"], "") == "")
            throw new Exception("ArCode can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["ApCode"], "") == "")
            throw new Exception("ApCode can not be null!!!");
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["CurrencyId"], "") == "")
            throw new Exception("Currency can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["ArCode"], "") == "")
            throw new Exception("ArCode can not be null!!!");
        if (SafeValue.SafeString(e.NewValues["ApCode"], "") == "")
            throw new Exception("ApCode can not be null!!!");
        e.NewValues["PartyId"] = SafeValue.SafeString(e.NewValues["PartyId"]);
    }
}