using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Printing;

public partial class WareHouse_Report_BalListing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_end.Date = DateTime.Today;
            this.cmb_DoType.Text = "All";
        }

    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select mast.DoNo,mast.DoType,mast.DoDate,xxparty.Name as PartyName,det.BalQty,det.Price,det.ProductCode,det.Packing,det.LotNo  from wh_transDet det
 inner join wh_trans mast on mast.doNo=det.DoNo and mast.DoType=det.DoType
left join xxparty on mast.partyId=xxparty.partyId
where mast.DoStatus!='Draft' and mast.DoStatus!='Canceled' ");
        string where = "";
        string custId = this.btn_CustId.Text.Trim();
        //string dateEnd= "";
        string product=btn_Product.Text.Trim();
        //if (txt_end.Value != null)
        //{
        //    dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        //}
        if (custId.Length > 0)
            where = GetWhere(where, " mast.PartyId='" + custId + "'");
        //if (dateEnd.Length > 0)
        //    where = GetWhere(where, string.Format("DoDate <= '{0}'", dateEnd));
        if (product.Length > 0)
        {
            where = GetWhere(where, "det.ProductCode= '" + product + "'");
        }
        if (cmb_DoType.Text == "All")
        { }
        else {
            sql += string.Format(" and mast.DoType='{0}'",cmb_DoType.Text);
        }
        sql += " order by mast.DoType,mast.DoDate,mast.DoNo";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.grid_Import.DataSource = tab;
        this.grid_Import.DataBind();

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btnRetrieve_Click(null, null);
        this.gridExport.WriteXlsToResponse("BalListing", true);
    }
}