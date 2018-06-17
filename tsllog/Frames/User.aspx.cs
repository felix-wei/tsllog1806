﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class User : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    #region user
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.User));
        }
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["IsActive"] = true;
        e.NewValues["Tel"] = " ";
        e.NewValues["Email"] = " ";
        e.NewValues["Role"] = "Admin";
        e.NewValues["CustId"] = "";
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        int totCnt = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["License"], 0);
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar("SELECT COUNT(SequenceId) FROM [User]"), 0);
        if (cnt - 1 >= totCnt)
        {
            throw new Exception("Error, pls contact your IT");
        }
        else
        {
            ASPxGridView grd = sender as ASPxGridView;
            ASPxTextBox pwd = grd.FindEditFormTemplateControl("txtPwd") as ASPxTextBox;
            if (pwd.Text == "")
            {
                e.Cancel = true;
                throw new Exception("Please enter the password .");
            }
            else
            {
                ASPxTextBox userId = grd.FindEditFormTemplateControl("txtCode") as ASPxTextBox;

                Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
                e.NewValues["Pwd"] = encrypt.DESEnCode(userId.Text, pwd.Text);
            }
            e.NewValues["IsActive"] = true;
        }
    }
    protected void ASPxGridView1_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        if (e.Exception == null)
        {
        }
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox pwd = grd.FindEditFormTemplateControl("txtPwd") as ASPxTextBox;
        if (pwd.Text == "")
        {
            //e.Cancel = true;
            //throw new Exception("Please enter the password .");
        }
        else
        {
            ASPxTextBox userId = grd.FindEditFormTemplateControl("txtCode") as ASPxTextBox;

            Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
            e.NewValues["Pwd"] = encrypt.DESEnCode(userId.Text, pwd.Text);
        }
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    #endregion

}