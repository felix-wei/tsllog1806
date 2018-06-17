using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class WareHouse_Job_Mo_SPJ_WHS : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             this.txt_from.Date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
           this.txt_end.Date = DateTime.Today;
            string d = Request.QueryString["d"] ?? "";
            if (d != "")
            {
                this.txt_from.Date = DateTime.Parse(d);
                this.txt_end.Date = DateTime.Parse(d);
            }
            btn_search_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
        else
        {
            string where = "";
            where += "CONVERT(varchar(100), DocDate, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "'";
            this.dsStock.FilterExpression = where + " and DocOwner='SPJ' and DoType='OUT3'";
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string where = "";
        //string sql = string.Format(@"select * from  JobSchedule ");
        string dateFrom = "";
        string dateTo = "";
        string no = txt_no.Text.Trim();
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(0).ToString("yyyy-MM-dd");
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_end.Date;
        }
		if(no.Length > 0) {
            where += " BillNo='"+no+"' ";
		} else {
			if (dateFrom.Length > 0)
			{
				where = GetWhere(where, " DocDate>= '" + dateFrom + "' and DocDate<= '" + dateTo + "' ");
			}
		}
        if (where.Length > 0)
        {
            where += " and DocOwner='SPJ' and DoType='OUT3' ";
        }
        //throw new Exception(sql);
        dsStock.FilterExpression = where;
        //DataTable tab = ConnectSql.GetTab(sql);
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
	
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string where = "";
        //string sql = string.Format(@"select * from  Materials ");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " DocDate>= '" + dateFrom + "' and DocDate<= '" + dateTo + "'");
        }
        if (where.Length > 0)
        {
            where += " Order by JobNo";
        }
        //throw new Exception(sql);
        dsStock.FilterExpression = where;
        //btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("JobSchedule", true);
    }	
	
	
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string id = "";
        string sql = "";
        string s = e.Parameters;
    }

  


    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Material));
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["DocOwner"] = "SPJ";
        e.NewValues["DoType"] = "OUT3";
        e.NewValues["DocStatus"] = "Pending";
        e.NewValues["DocDate"] = SafeValue.SafeDate(e.NewValues["DocDate"], DateTime.Now);
		if(S.Text(e.NewValues["DoType"]) == "OUT3") {	
		
			if(S.Text(e.NewValues["BillNo"]).Length < 5) {
				string refNo = C2Setup.GetNextNo("","SPJ", S.Date(e.NewValues["DocDate"])); //SafeValue.SafeString(e.NewValues["RefNo"]);
				e.NewValues["BillNo"] = refNo;
				e.NewValues["DocNo"] = refNo;
				C2Setup.SetNextNo("","SPJ", refNo, S.Date(e.NewValues["DocDate"]));
			} else {
				e.NewValues["BillNo"] = S.Text(e.NewValues["BillNo"]);
				e.NewValues["DocNo"] = S.Text(e.NewValues["DocNo"]);
			}
			
		} else {
        e.NewValues["BillNo"] = S.Text(e.NewValues["BillNo"]);
        e.NewValues["DocNo"] = S.Text(e.NewValues["DocNo"]);

		}
		
		string gst = S.Text(e.NewValues["GstType"]);
		decimal qty = S.Decimal(e.NewValues["Qty"]);
		string party = S.Text(e.NewValues["PartyTo"]);		
		string code = S.Text(e.NewValues["Code"]);		
        decimal price = D.Dec("select top 1 price from Materials where PartyTo='"+party+"' and Code='"+code+"'");
		decimal tot = SafeValue.ChinaRound(qty * price,2);
        decimal gstamt = 0;
		if(gst == "S")
			gstamt = SafeValue.ChinaRound(tot * S.Decimal(0.07),2);
		tot += gstamt;	
        e.NewValues["Qty"] = qty;
        e.NewValues["Price"] = price;
        e.NewValues["GstAmt"] = gstamt;
        e.NewValues["DocAmt"] = tot;
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocDate"] = DateTime.Now;
        e.NewValues["DocOwner"] = "SPJ";
        e.NewValues["DoType"] = "OUT3";
        e.NewValues["BillTerm"] = "IMMEDIATE";
        e.NewValues["GstType"] = "S";
        e.NewValues["Qty"] = 0;
        e.NewValues["Unit"] = "Pcs";
        e.NewValues["Price"] = 0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
 
        //e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
       		string gst = S.Text(e.NewValues["GstType"]);
		decimal qty = S.Decimal(e.NewValues["Qty"]);
		string party = S.Text(e.NewValues["PartyTo"]);		
		string code = S.Text(e.NewValues["Code"]);		
        decimal price = D.Dec("select top 1 price from Materials where PartyTo='"+party+"' and Code='"+code+"'");
		decimal tot = SafeValue.ChinaRound(qty * price,2);
        decimal gstamt = 0;
		if(gst == "S")
			gstamt = SafeValue.ChinaRound(tot * S.Decimal(0.07),2);
		tot += gstamt;	
        e.NewValues["Qty"] = qty;
        e.NewValues["Price"] = price;
        e.NewValues["GstAmt"] = gstamt;
        e.NewValues["DocAmt"] = tot;
		
       
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}