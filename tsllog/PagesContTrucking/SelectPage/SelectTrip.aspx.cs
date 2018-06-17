using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_SelectPage_SelectTrip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string no = "";
        string type = "";
        string where="";
        if (Request.QueryString["no"] != null)
        {
            no = Request.QueryString["no"].ToString();

        }
        if (Request.QueryString["tripType"] != null)
        {
            type = Request.QueryString["tripType"].ToString();
            if (type == "WGR")
                where = GetWhere(where, "(TripCode='WGR' or TripCode='TPT')");
            else
                where = GetWhere(where, "TripCode='"+type+"'");

        }
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select Id,TripIndex,BookingDate,BookingTime,RequestTrailerType,FromCode,ToCode From ctm_jobdet2 where JobNo='{0}'",no);
        if (name != "")
        {
            where=GetWhere(where," and TripIndex like '%"+name+"%'");
        }
        if (where.Length > 0)
        {
            sql +=" and " +where;
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