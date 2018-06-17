using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using C2;
 


public partial class Modules_EcOrderStaff : BasePage
{
    public bool _save_jump = true;
    public string _page = "FormEdit";
    public string _where = "Where";
    public string _key = "id";
    public string _type = "AUDIT";
    public string _table = "X2";
    public string _status = "LIST";
    public string _query = "RowType=''";
    public string _code = "code";

    protected void page_Init(object sender, EventArgs e)
    {


        



     //   DataTable dt = new DataTable();
     //   string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
      //              "localhost", "5432", "openpg", "openpgpwd", "oeRejo");
       // NpgsqlConnection conn = new NpgsqlConnection(connstring);
       //         conn.Open();
                // quite complex sql statement
        //        string sql = "SELECT * FROM product_product";
                // data adapter making request from our connection
        //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
         //       da.Fill(dt);
	//	conn.Close();

	//	throw new Exception(dt.Rows.Count.ToString());

    }
    protected void Page_Load(object sender, EventArgs e)
    {



    }

    protected void submit_order(object sender, EventArgs e)
    {

        C2.WhTrans o = new C2.WhTrans();
        o.DoNo = C2Setup.GetNextNo("", "SaleOrders", DateTime.Today);
		o.DoType = "SO";
        o.DoStatus = "Draft";
        o.PartyId = "";
        o.DoDate = DateTime.Today;
        o.Currency = "USD";
        o.ExRate = 1;
        o.PayTerm = "CASH";
        o.IncoTerm = "EXW";
        o.Remark = "QUICK ORDER";
        o.WareHouseId = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
                o.CreateBy = EzshipHelper.GetUserName();
                o.CreateDateTime = DateTime.Now;
                o.UpdateBy = "";
                o.UpdateDateTime = DateTime.Now;

        Manager.ORManager.StartTracking(o, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(o);
		C2Setup.SetNextNo("", "SaleOrders", o.DoNo, DateTime.Today);

        for(int i=0; i<50;i++) {
            string lineno = string.Format("line{0}",i+1);

            int q1 = Helper.Safe.SafeInt(Request[lineno]);
            int q2 = Helper.Safe.SafeInt(Request[lineno+"a"]);
            int pq = Helper.Safe.SafeInt(Request[lineno+"q"]);
            int whole = Helper.Safe.SafeInt(Request[lineno+"w"]);
            string code = Helper.Safe.SafeString(Request[lineno + "p"]);
            string lot = Helper.Safe.SafeString(Request[lineno + "b"]);
			
			string _sql = "select top 1 * from wh_dodet where dotype='IN' and lotno='"+lot+"'";
			if(lot == "")
				_sql = "select description as des1, att4 as att1, att5 as att2, att6 as att3, att7 as att4, att8 as att5, att9 as att6, uomPacking as uom1,uomWhole as uom2, uomLoose as uom3, uomBase as uom4 from ref_Product where Code='"+code+"'";
			//throw new Exception(_sql);
			DataTable dt1 = Helper.Sql.List(_sql);
			//DataRow dr = dt1.Rows[0];
			//string a = dt1.Rows[0]["LotNo"].ToString();
			//throw new Exception(a);
			//DataRow dr = dt1.Rows[0]; //new DataRow();
            if (q1 > 0)
            {
			
                C2.WhTransDet d = new C2.WhTransDet();
                d.DoNo = o.DoNo;
                d.DoType = "SO";
                //d.LineSNo = i + 1;
                d.ProductCode = code;
                d.LotNo = lot;
                d.Qty1 = q1;
                d.Qty2 = 0;
                d.Qty3 = 0; //pq * q1;
                d.QtyWholeLoose = 0;
		        d.QtyLooseBase = 0;
                d.LocationCode = "LWN365";
		        d.Des1 = Helper.Safe.SafeString(dt1.Rows[0]["Des1"]);
                d.Uom1 = Helper.Safe.SafeString(dt1.Rows[0]["Uom1"]);
                d.Uom2 = Helper.Safe.SafeString(dt1.Rows[0]["Uom2"]);
                d.Uom3 = Helper.Safe.SafeString(dt1.Rows[0]["Uom3"]);
                d.Uom4 = Helper.Safe.SafeString(dt1.Rows[0]["Uom4"]);
                d.QtyPackWhole = whole;
 				d.Att1 = Helper.Safe.SafeString(dt1.Rows[0]["Att1"]);
				d.Att2 = Helper.Safe.SafeString(dt1.Rows[0]["Att2"]);
				d.Att3 = Helper.Safe.SafeString(dt1.Rows[0]["Att3"]);
				d.Att4 = Helper.Safe.SafeString(dt1.Rows[0]["Att4"]);
				d.Att5 = Helper.Safe.SafeString(dt1.Rows[0]["Att5"]);
				d.Att6 = Helper.Safe.SafeString(dt1.Rows[0]["Att6"]);
                d.Price = 0;
				d.Packing = "";
                d.Gst = 0;
                d.Currency = "USD";
                d.ExRate = 1;

                Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(d);
            }
            if (q2 > 0)
            {
                C2.WhTransDet d = new C2.WhTransDet();
                               d.DoNo = o.DoNo;
                d.DoType = "SO";
                //d.LineSNo = i + 1;
                d.ProductCode = code;
                d.LotNo = "";
                d.Qty1 = q2;
                d.Qty2 = 0;
                d.Qty3 = 0; //pq * q1;
                d.QtyPackWhole = 0;
                d.QtyWholeLoose = 0;
				d.QtyLooseBase = 0;
                d.LocationCode = "LWN365";
		        d.Des1 = Helper.Safe.SafeString(dt1.Rows[0]["Des1"]);
                d.Uom1 = Helper.Safe.SafeString(dt1.Rows[0]["Uom1"]);
                d.Uom2 = Helper.Safe.SafeString(dt1.Rows[0]["Uom2"]);
                d.Uom3 = Helper.Safe.SafeString(dt1.Rows[0]["Uom3"]);
                d.Uom4 = Helper.Safe.SafeString(dt1.Rows[0]["Uom4"]);
                 d.QtyPackWhole = whole;
 				d.Att1 = Helper.Safe.SafeString(dt1.Rows[0]["Att1"]);
				d.Att2 = Helper.Safe.SafeString(dt1.Rows[0]["Att2"]);
				d.Att3 = Helper.Safe.SafeString(dt1.Rows[0]["Att3"]);
				d.Att4 = Helper.Safe.SafeString(dt1.Rows[0]["Att4"]);
				d.Att5 = Helper.Safe.SafeString(dt1.Rows[0]["Att5"]);
				d.Att6 = Helper.Safe.SafeString(dt1.Rows[0]["Att6"]);
                d.Price = 0;
				d.Packing = "";
                d.Gst = 0;
                d.Currency = "USD";
                d.ExRate = 1;

                Manager.ORManager.StartTracking(d, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(d);
            }
        }

        Response.Redirect("SoEdit.aspx?no=" + o.DoNo);

        //OpenErpService openErpService = new OpenErpService("http://rejo.ezconnect.asia:99", "oeRejo", "admin", "admin");

        //Partner p = new Partner();
        //p = openErpService.GetEntity<Partner>(a => a.Name.Equals("STARCAST"));

        //SalesOrder order = new SalesOrder();
        //order.Name = "STARCAST:" + DateTime.Now.ToString("MMddHHmmss");
        //order.PartnerId = p.id;
        //order.PartnerInvoiceId = p.id;
        //order.PartnerShippingId = p.id;
        //order.DateOrder = DateTime.Today;
        //order.PriceListId = 1;
        //order.ShopId = 1;
        //openErpService.AddEntity<SalesOrder>(order);

        //int[] prod = { 76, 77, 78, 79, 113,114 };
        //string[] lots = { "SBLA2012", "SBLA2013", "SBLA1675", "SBLA1676", "", "" };
        //double[] qty = { 0,0,0,0,0,0};
        //double[] qtya = { 0, 0, 0, 0, 0, 0 };
        //qty[0] = Helper.Safe.SafeDouble(line1.Text);
        //qty[1] = Helper.Safe.SafeDouble(line2.Text);
        //qty[2] = Helper.Safe.SafeDouble(line3.Text);
        //qty[3] = Helper.Safe.SafeDouble(line4.Text);
        //qty[4] = Helper.Safe.SafeDouble(line5.Text);
        //qty[5] = Helper.Safe.SafeDouble(line6.Text);

        //qtya[0] = Helper.Safe.SafeDouble(line1a.Text);
        //qtya[1] = Helper.Safe.SafeDouble(line2a.Text);
        //qtya[2] = Helper.Safe.SafeDouble(line3a.Text);
        //qtya[3] = Helper.Safe.SafeDouble(line4a.Text);
        //qtya[4] = Helper.Safe.SafeDouble(line5a.Text);
        //qtya[5] = Helper.Safe.SafeDouble(line6a.Text);


        //for (int i = 0; i < prod.Length; i++)
        //{
        //    if (qty[i] > 0)
        //    {
        //        SalesOrderLine line = new SalesOrderLine();
        //        line.OrderId = order.Id;
        //        line.ProductId = prod[i];
        //        Product pr = new Product();
        //        pr = openErpService.GetEntity<Product>(prd => prd.Id == line.ProductId);
        //        line.Name = "[" + lots[i] + "] " + pr.Name;
        //        line.Qantity = qty[i];
        //        openErpService.AddEntity<SalesOrderLine>(line);
        //    }
        //    if (qtya[i] > 0)
        //    {
        //        SalesOrderLine linea = new SalesOrderLine();
        //        linea.OrderId = order.Id;
        //        linea.ProductId = prod[i];
        //        Product pr = new Product();
        //        pr = openErpService.GetEntity<Product>(prd => prd.Id == linea.ProductId);
        //        linea.Name = "[NEW] " + pr.Name;
        //        linea.Qantity = qtya[i];
        //        openErpService.AddEntity<SalesOrderLine>(linea);
        //    }
        //}
        
        //string temp = "Order Submitted : {0} ";
        //msg.Text = string.Format(temp, order.Name);
        //Helper.Logic.AlertOrder(order.Name);
    }




}
