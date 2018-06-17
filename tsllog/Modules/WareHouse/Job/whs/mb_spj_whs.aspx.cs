using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_Job_Mb_SPJ_WHS: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            cmb_CompanyName.SelectedIndex = 0;
            btn_search_Click(null, null);
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string where = "";
        string company =cmb_CompanyName.Text.Trim();
        where += " docdate <= '" + txt_from.Date.ToString("yyyy-MM-dd") + "'";
        string sql = string.Format(@"select * from (select 
m.Code, m.Unit, sum(case m.dotype when 'OUT3' then -1 * qty else qty end) as Bal from Materials m  
where   m.DocOwner='SPJ' and DoType in ('IN3','OUT3') and m.docdate<='{0:yyyy-MM-dd}'   group by m.Code,m.Unit ) as tmp 
where bal <> 0 order by Code, Unit
", txt_from.Date);

        if (where.Length > 0)
        {
          //  sql += " where " + where + "order by Code";
        }
        //throw new Exception(sql);
        DataTable tab = D.List(sql);
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}