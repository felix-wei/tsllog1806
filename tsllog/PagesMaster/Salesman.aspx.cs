using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class XX_Salesman : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Salesman"] = "1=1";
        }
        if (Session["Salesman"] != null)
            this.dsSalesman.FilterExpression = Session["Salesman"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txtName.Text.Trim() != "")
        {
            where = "Name like '" + this.txtName.Text.Trim() + "%'";
        }

        Session["Salesman"] = where;
        this.dsSalesman.FilterExpression = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Salesman", true);
    }
    #region Salesman
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXSalesman));
        }


            
        
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
 

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        //e.NewValues["UserId"] = HttpContext.Current.User.Identity.Name;
        //e.NewValues["EntryDate"] = DateTime.Now;
        e.NewValues["Tel"] = " ";
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            Session["Salesman"] = "Code='"+e.NewValues["Code"]+"'";
            this.dsSalesman.FilterExpression = "Code='" + e.NewValues["Code"] + "'";
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Code"], "") == "")
            throw new Exception("Code can not be null!!!");
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["Tel"] = SafeValue.SafeString(e.NewValues["Tel"]);
    }

    #endregion
    
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
}
