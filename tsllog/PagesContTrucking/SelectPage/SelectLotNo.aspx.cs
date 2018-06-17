using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class PagesContTrucking_SelectPage_SelectLotNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string where = "";
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select h.Id,h.LineId,BookingNo,job.JobNo,job.JobDate From job_house h inner join ctm_job job on h.JobNo=job.JobNo");
        if (name != "")
        {
            where = GetWhere(where, " and BookingNo like '%" + name + "%'");
        }
        if (where.Length > 0)
        {
            sql += " and " + where;
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}