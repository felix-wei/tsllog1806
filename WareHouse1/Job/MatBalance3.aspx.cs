using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Data;

public partial class WareHouse_Job_MatBalance3 : System.Web.UI.Page
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
        where += "CONVERT(varchar(100), JobDate, 23) <= '" + txt_from.Date.ToString("yyyy-MM-dd") + "'";
        string sql = string.Format(@"select tab_hand.Code,tab_hand.Description,(tab_hand.New-ISNULL(tab_out.Used,0)) as OnHand,0 as Qty,tab_hand.Unit,Job.CustomerName from JobSchedule job inner join 
(select max(Code) as Code,max(Description) as Description,max(Unit) as Unit,MAX(m.RefNo) as RefNo,sum(TotalNew) as New from Materials m inner join JobSchedule sch  on  DoType='IN3' and sch.RefNo=m.RefNo and CustomerName='{0}' group by Code,CustomerName) as tab_hand on  tab_hand.RefNo=job.RefNo
left join (select Code,sum(TotalNew) as Used from Materials where DoType='OUT3' group by Code) as tab_out on tab_hand.Code=tab_out.Code
", company);

        if (where.Length > 0)
        {
            sql += " where " + where + "order by Code";
        }
        //throw new Exception(sql);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
}