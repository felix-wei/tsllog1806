using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System.Configuration;
using System.Data;

public partial class SelectPage_AirQuoteTitle : BasePage
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
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT Title FROM AirQuoteTitle ";
        if (name.Length > 0)
        {
            sql += string.Format(" where title LIKE '{0}%'", name.Replace("'", "''"));
        }
        DataTable tab =C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Customer", true);
    }

}
