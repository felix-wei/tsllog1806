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

public partial class PagesMaster_XXPartyGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXPartyGroup));
        }
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Code"] = SafeValue.SafeString(e.Values["Code"]);
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Code"] = "";
        e.NewValues["Description"] = "";
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        
    }
    protected void Save()
    {
        ASPxComboBox com_Code = grid.FindEditFormTemplateControl("cbPort") as ASPxComboBox;
        ASPxMemo me_Description = grid.FindEditFormTemplateControl("me_Description") as ASPxMemo;
        string code = SafeValue.SafeString(com_Code.Text, "");
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XXPartyGroup), "Code='" + code + "'");
        XXPartyGroup group = C2.Manager.ORManager.GetObject(query) as XXPartyGroup;
        bool action = false;
        if(group==null){
            group = new XXPartyGroup();
            action = true;
        }
        group.Code = code;
        group.Description = me_Description.Text;
        if (action)
        {
            Manager.ORManager.StartTracking(group, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(group);
        }
        else
        {
            Manager.ORManager.StartTracking(group, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(group);
        }
    }
}