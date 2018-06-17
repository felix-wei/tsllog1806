using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SelectPage_ContainerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select ContainerNo,ContainerType,Id from Ref_Container where ContainerNo like '%{0}%'  order by ContainerNo", name);

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}