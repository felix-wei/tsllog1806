﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;


public partial class Modules_Hr_Master_Letter : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Item"] = "AttachType='LETTER'";
        }
        if (Session["Item"] != null)
            this.dsCtmAttachment.FilterExpression = Session["Item"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (this.txtSchName.Text.Trim() != "")
        {
            where = "Employee= '" + this.txtSchName.Value + "' AND AttachType='LETTER'";
        }
        else
        {
            where = "AttachType='LETTER'";
        }
        this.dsCtmAttachment.FilterExpression = where;
        Session["Item"] = where;

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
        gridExport.WriteXlsToResponse("Letter", true);
    }
    #region Pass Certficate
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.CtmAttachment));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["AttachType"] = "LETTER";
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        if (SafeValue.SafeString(e.NewValues["Employee"], "") == "")
            throw new Exception("Employee can not be null!!!");
        e.NewValues["AttachType"] = "LETTER";
        e.NewValues["JobType"] = "";
        e.NewValues["FilePath"] = SafeValue.SafeString(e.NewValues["FilePath"]);
        e.NewValues["FileNote"] = SafeValue.SafeString(e.NewValues["FileNote"]);
        e.NewValues["TypeCode"] = SafeValue.SafeString(e.NewValues["TypeCode"]);
        e.NewValues["CreateBy"] = user;
        e.NewValues["CreateDateTime"] = DateTime.Now;
        e.NewValues["UpdateBy"] = user;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
        btn_search_Click(null, null);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        e.NewValues["FilePath"] = SafeValue.SafeString(e.NewValues["FilePath"]);
        e.NewValues["FileNote"] = SafeValue.SafeString(e.NewValues["FileNote"]);
        e.NewValues["TypeCode"] = SafeValue.SafeString(e.NewValues["TypeCode"]);
        e.NewValues["UpdateBy"] = user;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {

    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("UploadLine"))
            {
                #region 
                ASPxGridView grid = sender as ASPxGridView;
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_Id = grid.FindRowCellTemplateControl(rowIndex, null, "txt_Id") as ASPxTextBox;
                e.Result = txt_Id.Text;

                #endregion
            }
        }
    }
}