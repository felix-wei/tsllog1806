using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_Job_Mb_Spj: System.Web.UI.Page
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
        string sql = string.Format(@"select Code,Sum(BalNew) as BalNew, Sum(BalUsed) as BalUsed from (
		select m.Code, r.Name,  sum(case m.dotype when 'OUT3' then -1 * totalnew else totalnew end) as BalNew,sum(case m.dotype when 'OUT3' then -1 * totalused else totalused end) as BalUsed from Materials m  
, Ref_material r  
where  m.Code=r.Code and r.Note2='Mat' and  m.DocOwner='CLM' and DoType in ('OUT3') and m.docdate>='2015-03-01' and m.docdate<='{0:yyyy-MM-dd}'   group by m.Code  , r.Name
union
		select m.Code, r.Name,   sum(Qty * r.WholeLoose) as BalNew,0 as BalUsed from Materials m, Ref_material r  
where  m.Code=r.Code and r.Note2='Mat' and m.DocOwner='SPJ' and m.PartyTo='8001' and DoType in ('OUT3') and m.docdate>='2015-03-01' and m.docdate<='{0:yyyy-MM-dd}'   group by m.Code, r.Name  

) as tmp 
where balnew + balused  <> 0  group by Code, Name order by   Name
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