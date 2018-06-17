using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WareHouse_Job_CostTest : System.Web.UI.Page
{
	
    protected void Page_Init(object sender, EventArgs e)
    {
	
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
		string allkey = Request.Params["allkey"] ?? "";
		
		
		//string id = Request.Params["id"] ?? "";
		
		string[] keys = allkey.Split(new char[] {'#'});
		
		for(int i=0; i<keys.Length-1; i++)
		{
			decimal qty = Helper.Safe.SafeDecimal(Request.Params["qty-" + keys[i]]);
			decimal price = Helper.Safe.SafeDecimal(Request.Params["price-" + keys[i]]);
			decimal min = Helper.Safe.SafeDecimal(Request.Params["min-" + keys[i]]);
			decimal total = Helper.Safe.SafeDecimal(Request.Params["total-" + keys[i]]);
		
			string update_sql = string.Format(@"Update CostDet Set SaleQty='{1}', SalePrice='{2}', SaleDocAmt='{3}', CostPrice='{4}' where SequenceId={0}",
				keys[i], qty, price, total, min );
				
			//	throw new Exception(update_sql);
			Helper.Sql.Exec(update_sql);
		}
        this.lab.Text = "Success";
		
	
	
		

    }
}