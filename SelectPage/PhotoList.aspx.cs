using DevExpress.Web.ASPxDataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_PhotoList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = Request.QueryString["JobNo"].ToString();
            txt_JobNo.Text = jobNo;
            this.dsJobPhoto.FilterExpression = "RefNo='" + txt_JobNo.Text + "'";// 
        }
    }
    #region photo
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {

    }
    protected void grd_Photo_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string str = e.Parameter;
        string sql = "";
        string jobNo = txt_JobNo.Text;
        if (str == "DeleteAll")
        {
            sql = "delete FROM CTM_Attachment  WHERE RefNo='" + jobNo + "'";
        }
        else
        {
            sql = "delete FROM CTM_Attachment  WHERE Id='" + SafeValue.SafeInt(str, 0) + "'";
        }
        if (sql.Length > 0)
        {
            C2.Manager.ORManager.ExecuteCommand(sql);
            this.dsJobPhoto.FilterExpression = "RefNo='" + jobNo + "'";//
            (sender as ASPxDataView).DataBind();
        }
    }

    #endregion
}