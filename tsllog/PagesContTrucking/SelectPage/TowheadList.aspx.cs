using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_TowheadList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null, null);
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string type = SafeValue.SafeString(Request.QueryString["typ"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select Id,VehicleCode,VehicleType,VehicleStatus from ref_Vehicle where VehicleStatus='Active' and (VehicleType='Towhead' or VehicleType='Lorry') and VehicleCode like '%{0}%'", name);
        if (type.Length > 0)
            sql += string.Format(" and VehicleType='{0}'", type);

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}