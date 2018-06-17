using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_GenCodeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            Session["GenCode"] = "1=1";
        }
        if (Session["GenCode"] != null)
            this.dsGenCode.FilterExpression = Session["GenCode"].ToString();
    }
    #region GenCode
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.XXGenCode));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {


    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
            Session["GenCode"] = "Code='" + e.NewValues["Code"] + "'";
            this.dsGenCode.FilterExpression = "Code='" + e.NewValues["Code"] + "'";
        }
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
    }

    #endregion

}
