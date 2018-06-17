using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_SelectProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
        btn_Sch_Click(null,null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string where = "";
        string sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12,REPLACE(Att13,char(39),'\&#39;') as Att13 from ref_product ";
        string typ = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
        string type = SafeValue.SafeString(cbProductClass.Text);
        if (typ == "SO")
        {
            sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12
,isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='SO' and det.ProductCode=ref_product.code order by mast.DoDate desc,det.Price desc),0) as Att13
from ref_product";
        }
        else //if (typ == "PO")
        {
            
            sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12
,isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='PO' and det.ProductCode=ref_product.code order by mast.DoDate desc,det.Price desc),0) as Att13
from ref_product";

        }

        if (name.Length > 0)
        {
            where = GetWhere(where, "  Name Like '%" + name.Replace("'", "''") + "%'");
        }
        if (type.Length > 0)
        {
            where = GetWhere(where, " ltrim(rtrim(ProductClass))='" + type + "'");
        }
        if(where.Length>0){
            where = " where "+where;
        }
        sql += where + " ORDER BY Id ";
        if (type != "CIGR")
        {
            this.ASPxGridView1.Visible = true;
            this.ASPxGridView2.Visible = false;
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView1.DataSource = tab;
            this.ASPxGridView1.DataBind();

        }
        else
        {
            this.ASPxGridView1.Visible = false;
            this.ASPxGridView2.Visible = true;
            DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
            this.ASPxGridView2.DataSource = tab;
            this.ASPxGridView2.DataBind();
        }

    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
}