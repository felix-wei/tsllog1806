using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesMaster_Control_Menu1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ASPxComboBox1.Value = EzshipHelper.GetUseRole();
            this.dsMenuMast.SelectParameters.Clear();
            this.dsMenuMast.SelectParameters.Add("RoleName", TypeCode.String, SafeValue.SafeString(this.ASPxComboBox1.Value));
        }
    }
    protected void grid_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        if (e.Exception == null)
        {
            string masterId = e.Values["MasterId"].ToString();
            string sql = string.Format("Delete from Menu1 where MasterId='{0}'", masterId);
            C2.Manager.ORManager.ExecuteCommand(sql);

        }

    }
    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
        this.dsMenuMast.SelectParameters.Clear();
        this.dsMenuMast.SelectParameters.Add("RoleName", TypeCode.String, this.ASPxComboBox1.Value.ToString());
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["RoleName"] = this.ASPxComboBox1.Value;
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["RoleName"] = this.ASPxComboBox1.Value;
    }
}