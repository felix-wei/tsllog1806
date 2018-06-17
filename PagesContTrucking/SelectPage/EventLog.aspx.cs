using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_DriverList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["JobNo"]);
            txt_Name.Text = jobNo;
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select * from CTM_JobEventLog where JobNo=@JobNo and Platform<>'BACKEND'");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", txt_Name.Text.Trim(), SqlDbType.NVarChar, 100));

        DataTable tab = ConnectSql_mb.GetDataTable(sql, list);
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}