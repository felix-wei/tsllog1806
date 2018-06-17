using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectProdutFromReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"select * from Wh_TransDet det inner join Wh_Trans d on det.DoNo=d.DoNo where d.DoType='PO' and d.StatusCode='CLS'";
        if (name.Length > 0)
        {
            sql += "()" + string.Format("  and ProductCode Like '{0}%'", name);
        }
        sql += " ORDER BY det.Id ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}