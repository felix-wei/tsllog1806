using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
public partial class WareHouse_Job_MaterialMovement : System.Web.UI.Page
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
        string company = cmb_CompanyName.Text.Trim();
        where += "CONVERT(varchar(100), DoDate, 23) <= '" + txt_from.Date.ToString("yyyy-MM-dd") + "'";
        string sql = string.Format(@"
		select * from(
		select tab_out.RefNo,tab_out.JobDate,tab_hand.Code,tab_hand.Description,tab_hand.New as Inwards,
		(tab_hand.New-ISNULL(tab_out.Used,0)) as Closing,ISNULL(tab_out.Used,0) as Outwards,tab_hand.Unit,
		Job.CustomerName,Particulars from JobSchedule job inner join 
(select max(Code) as Code,max(Description) as Description,max(Unit) as Unit,MAX(m.RefNo) as RefNo,sum(TotalNew) as New from Materials m inner join JobSchedule sch  on  DoType='IN3' and sch.RefNo=m.RefNo and CustomerName='{0}' group by Code,CustomerName) as tab_hand on  tab_hand.RefNo=job.RefNo
left join (select Code,sum(TotalNew) as Used,MAX(m.RefNo) as RefNo,max(OriginAdd) as Particulars,max(JobDate) as JobDate from Materials m inner join JobSchedule sch  on  DoType='OUT3' and sch.RefNo=m.RefNo  group by Code,OriginAdd) as tab_out on tab_hand.Code=tab_out.Code) as tab where Inwards>0 
", company);

		sql = string.Format(@"select * from (
select m1.Code, m1.Description, m1.Unit, m1.DoType, 0 as BalQty, totalnew as InQty, 0 as OutQty, o1.OriginAdd as PartyRef, o1.Value1 as DoStatus, o1.Value2 as BillNo, DateTIme2 as BillDate,o1.Value3 as DoNo, DateTime2 as DoDate from materials m1, jobschedule o1 where m1.refno=o1.refno and o1.jobtype='IN3' and o1.CustomerName='{0}' and o1.DateTime2 <= '{1}' and m1.totalnew > 0		
union all		
select m2.Code, m2.Description, m2.Unit, m2.DoType, 0 as BalQty, 0 as InQty, totalnew as OutQty, o2.OriginAdd as PartyRef, o2.Value1 as DoStatus, o2.Value2 as BillNo, DateTIme2 as BillDate,o2.Value3 as DoNo, DateTime2 as DoDate from materials m2, jobschedule o2 where m2.refno=o2.refno and o2.jobtype='OUT3' and o2.CustomerName='{0}'  and o2.DateTime2 <= '{1}'	and m2.totalnew > 0	
		) as tmpTable ",company, txt_from.Date.ToString("yyyy-MM-dd"));

          sql += " order by Code, Dodate, DoType";
        if (where.Length > 0)
        {
        }
        //throw new Exception(sql);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        this.gridExport.WriteXlsToResponse("MaterialStockMovement");
    }
}