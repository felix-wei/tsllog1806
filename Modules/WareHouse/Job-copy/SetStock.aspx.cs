using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class WareHouse_Job_SetStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
      string  sql = string.Format(@"select * from (
select loc.Code,loc.MaxCount,Convert(int,(loc.MaxCount-isnull(tab_set.Qty1,0))) as AvaibleQty,loc.WarehouseCode from ref_location loc
left join (select sum(Qty1) Qty1,ToLocId from wh_TransferDet group by ToLocId) det on  loc.Code=det.ToLocId
left join (select sum(Qty1) as Qty1,Location from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Location) as tab_set on tab_set.Location=det.ToLocId
 where loc.Loclevel='Unit') as tab ");

        if (cmb_Location.Text.Trim() != "")
        {
            sql += string.Format(" where tab.Code='{0}'", cmb_Location.Text);
        }

        DataTable tab1 = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView2.DataSource = tab1;
        this.ASPxGridView2.DataBind();
    }
}