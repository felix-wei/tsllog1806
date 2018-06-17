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

public partial class WareHouse_Job_Mo_CLM : System.Web.UI.Page
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
            where += "CONVERT(varchar(100), DocDate, 23) >= '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and CONVERT(varchar(100), DocDate, 23) <= '" + txt_date2.Date.ToString("yyyy-MM-dd") + "'";
            this.dsStock.FilterExpression = where + " and DocOwner='CLM' and DoType='OUT3'";
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
            where += " and DocOwner='CLM' and DoType='OUT3' ";
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
		try {
		string[] sp = s.Split(new char[] {'#'});
		if(sp[0] == "Add")
		{
			string lineid = sp[1];
			string code = sp[2];
			int qty = S.Int(sp[3]);
			DataTable dt = D.List("select * from Materials where id=" + lineid);
			DataRow dr = dt.Rows[0];
			
			C2.Material mt = new C2.Material();
			
			mt.DocDate = S.Date(dr["DocDate"]);
			mt.DocOwner = "CLM";
			mt.DocNo = S.Text(dr["DocNo"]);
			mt.DoType = S.Text(dr["DoType"]);
			mt.DocType = S.Text(dr["DocType"]);
			mt.BillNo = S.Text(dr["BillNo"]);
			mt.BillTerm = S.Text(dr["BillTerm"]);
			mt.PartyTo = S.Text(dr["PartyTo"]);
			mt.GstType = S.Text(dr["GstType"]);
			mt.Code = code;
			mt.Qty = S.Decimal(qty);
			mt.Price = D.Dec("select top 1 price from Materials where DoType='RT3' and PartyTo='"+mt.PartyTo+"' and Code='"+mt.Code+"'");
			decimal tot = SafeValue.ChinaRound(mt.Qty * mt.Price,2);
			mt.GstAmt = mt.GstType != "S" ? 0 : SafeValue.ChinaRound(tot * S.Decimal(0.07),2);
			mt.DocAmt = tot + mt.GstAmt;
			
                     C2.Manager.ORManager.StartTracking(mt, Wilson.ORMapper.InitialState.Inserted);
                     C2.Manager.ORManager.PersistChanges(mt);			

					//grid.Refresh();
					e.Result = "Success";
		}
		} catch(Exception ex)
		{
			e.Result = ex.Message + ex.StackTrace;
		}
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
        e.NewValues["DocOwner"] = "CLM";
        e.NewValues["DoType"] = "OUT3";
        e.NewValues["DocStatus"] = "Pending";
        e.NewValues["DocDate"] = SafeValue.SafeDate(e.NewValues["DocDate"], DateTime.Now);
		e.NewValues["DocNo"] = S.Text(e.NewValues["DocNo"]);

		
        int n1 = S.Int(e.NewValues["RequisitionNew"]);
        int n2 = S.Int(e.NewValues["RequisitionUsed"]);
        int a1 = S.Int(e.NewValues["AdditionallNew"]);
        int a2 = S.Int(e.NewValues["AdditionalUsed"]);
        int r1 = S.Int(e.NewValues["ReturnedNew"]);
        int r2 = S.Int(e.NewValues["ReturnedUsed"]);
		int t1 = n1+a1-r1;
		int t2 = n2+a2-r2;
        
		e.NewValues["RequisitionNew"] = n1;
		e.NewValues["RequisitionUsed"] = n2;
		e.NewValues["AdditionalNew"] = a1;
        e.NewValues["AdditionalUsed"] = a2;
        e.NewValues["ReturnedNew"] = r1;
        e.NewValues["ReturnedUsed"] = r2;
        e.NewValues["TotalNew"] = t1;
        e.NewValues["TotalUsed"] = t2;
		
      
	  
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocDate"] = DateTime.Now;
        e.NewValues["DocOwner"] = "CLM";
        e.NewValues["DoType"] = "OUT3";
        e.NewValues["BillTerm"] = "IMMEDIATE";
        e.NewValues["GstType"] = "S";
        e.NewValues["Qty"] = 0;
        e.NewValues["Unit"] = "Pcs";
        e.NewValues["Price"] = 0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
			e.NewValues["RequisitionNew"] = 0;
		e.NewValues["RequisitionUsed"] = 0;
		e.NewValues["AdditionalNew"] = 0;
        e.NewValues["AdditionalUsed"] = 0;
        e.NewValues["ReturnedNew"] = 0;
        e.NewValues["ReturnedUsed"] = 0;
        e.NewValues["TotalNew"] = 0;
        e.NewValues["TotalUsed"] = 0;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
 
        //e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
   		
        int n1 = S.Int(e.NewValues["RequisitionNew"]);
        int n2 = S.Int(e.NewValues["RequisitionUsed"]);
        int a1 = S.Int(e.NewValues["AdditionallNew"]);
        int a2 = S.Int(e.NewValues["AdditionalUsed"]);
        int r1 = S.Int(e.NewValues["ReturnedNew"]);
        int r2 = S.Int(e.NewValues["ReturnedUsed"]);
		int t1 = n1+a1-r1;
		int t2 = n2+a2-r2;
        
		e.NewValues["RequisitionNew"] = n1;
		e.NewValues["RequisitionUsed"] = n2;
		e.NewValues["AdditionalNew"] = a1;
        e.NewValues["AdditionalUsed"] = a2;
        e.NewValues["ReturnedNew"] = r1;
        e.NewValues["ReturnedUsed"] = r2;
        e.NewValues["TotalNew"] = t1;
        e.NewValues["TotalUsed"] = t2;
	
	
		
       
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}