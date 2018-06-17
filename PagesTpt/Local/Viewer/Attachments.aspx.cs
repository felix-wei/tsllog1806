using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_Viewer_Attachments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["JobNo"] != null)
            {
                txt_JobNo.Text = Request.QueryString["JobNo"].ToString();
                this.dsJobPhoto.FilterExpression = string.Format("JobType='Tpt' and RefNo='{0}'", txt_JobNo.Text);
                string sql = "select statuscode from tpt_job where jobno='" + txt_JobNo.Text + "'";
                DataTable dt=ConnectSql.GetTab(sql);
                if (dt.Rows.Count > 0 && SafeValue.SafeString(dt.Rows[0][0]).Equals("USE"))
                {

                }
                else
                {
                    ASPxButton12.Enabled = false;
                }
            }
        }
    }

    #region photo
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.TptAttachment));
        }
    }
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        this.dsJobPhoto.FilterExpression = "RefNo='" + txt_JobNo.Text + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

    }
    protected void grd_Photo_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["FileNote"] = " ";
    }

    #endregion


}