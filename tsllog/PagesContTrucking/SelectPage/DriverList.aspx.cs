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
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sub_Ind = SafeValue.SafeString(Request.QueryString["SubInd"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string tel = txt_Tel.Text.Trim();
        string primeMover = txt_PrimeMover.Text.Trim();
        string sql = string.Format(@"select Id,Code,Name,TowheaderCode,Tel From CTM_Driver where statuscode='Active' and (Code like '%{0}%' or Name like '%{0}%') and Tel like '%{1}%' and  TowheaderCode like '%{2}%' order by Code", name,tel,primeMover);
        if (sub_Ind == "Y")
        {
            sql = string.Format(@"select Id,Code,Name,TowheaderCode,Tel From CTM_Driver where statuscode='Active' and (Code like '%{0}%' or Name like '%{0}%') and SubContract_Ind='{1}' and Tel like '%{2}%' and  TowheaderCode like '%{3}%'  order by Code", name, sub_Ind, tel, primeMover);
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}