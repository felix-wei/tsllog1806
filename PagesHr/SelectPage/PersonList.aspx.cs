using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_PersonList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            string type = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
            if (type.Length>0)
                this.lab_Type.Text = type;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = "";
        string type = this.lab_Type.Text.Trim();
        if (SafeValue.SafeString(type).Length < 1)
            sql = string.Format("Select Id,Name from Hr_Person where Name like '{0}%'", name.Replace("'", "''"));
        else
            sql = string.Format("Select Id,Name from Hr_Person where Name like '{0}%' and Status like '{1}%'", name.Replace("'", "''"), type);
        
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}
