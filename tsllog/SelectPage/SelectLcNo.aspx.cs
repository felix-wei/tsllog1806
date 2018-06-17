using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Data;

public partial class SelectPage_SelectLcNo : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string code = this.txt_Code.Text.Trim().ToUpper();
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        string sql = "select * from Ref_LC where LcExpirtDate>getdate()";
        if (code.Length > 0)
        {
            sql += string.Format(" and  LcNo Like '{0}%'", code);
        }
        if(type.Length>0){
            sql += string.Format(" and  StatusCode='{0}'", type);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}