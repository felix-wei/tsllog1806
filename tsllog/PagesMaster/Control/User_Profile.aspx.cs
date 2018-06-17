using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class User_Profile : BasePage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        dsUser.FilterExpression = "name='"+HttpContext.Current.User.Identity.Name+"'";
    }

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
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox pwd = grd.FindEditFormTemplateControl("txtPwd") as ASPxTextBox;
        if (pwd.Text == "")
        {
            e.Cancel = true;
            throw new Exception("Please enter the password .");
        }else
        {
            //e.NewValues["Pwd"] = pwd.Text;
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
            e.Cancel = true;
            throw new Exception("Please enter the password .");
        }
        else
        {
 ASPxTextBox userId = grd.FindEditFormTemplateControl("txtCode") as ASPxTextBox;

            Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
            e.NewValues["Pwd"] = encrypt.DESEnCode(userId.Text, pwd.Text);
		//            e.NewValues["Pwd"] = pwd.Text;
        }
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
    #endregion

}
