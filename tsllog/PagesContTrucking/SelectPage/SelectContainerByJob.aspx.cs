using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PagesContTrucking_SelectPage_SelectContainerByJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string name = this.txt_Name.Text.Trim().ToUpper();

        string sql = string.Format(@"select ContainerNo,ContainerType,Id from ctm_jobdet1 where JobNo='{0}' and  ContainerNo like '%{1}%'  order by ContainerNo",no, name);

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}