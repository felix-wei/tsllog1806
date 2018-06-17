using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_MastData_LotNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){

        }
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhLotNo));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string lotNo = C2Setup.GetNextNo("", "LOTNO", DateTime.Today);

        e.NewValues["Description"] = "";
        e.NewValues["Code"] = lotNo;
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string lotNo = SafeValue.SafeString(e.NewValues["Code"], "");
        C2Setup.SetNextNo("", "LOTNO", lotNo, DateTime.Today);
        e.NewValues["CreateBy"] = EzshipHelper.GetUserName();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid.FindEditRowCellTemplateControl(null, "txt_Code") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if(s=="Auto"){
            for (int i = 0; i < 10;i++ )
            {
                WhLotNo lot = new WhLotNo();
                string lotNo = C2Setup.GetNextNo("", "LOTNO", DateTime.Today);
                lot.Code = lotNo;
                lot.Description = lotNo;
                lot.CreateBy = EzshipHelper.GetUserName();
                lot.CreateDateTime = DateTime.Now;

                C2Setup.SetNextNo("", "LOTNO", lotNo, DateTime.Today);
                Manager.ORManager.StartTracking(lot, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(lot);

            }
            e.Result = "Success";
        }
    }
}