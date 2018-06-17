using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WareHouse_SelectPage_ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string type =SafeValue.SafeString(Request.QueryString["type"]);
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = "";
        if(type=="PO"){
            sql = @"SELECT pr.Id,Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PName,po.PoNo as MastNo FROM ref_product pr inner join wh_PODet as po on pr.Code=po.Product where ";

        }
        if(type=="SO"){
            sql = @"SELECT pr.Id,Code,Name,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as PName,po.PoNo as MastNo FROM ref_product pr inner join wh_POReceiptDet as po on pr.Code=po.Product where ";
        }
        if (name.Length > 0)
        {
            sql += "()" + string.Format("  and Name Like '{0}%'", name.Replace("'", "''"));
        }
        else
        {
            sql += "1=1";
        }
        sql += " ORDER BY code ";

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();

    }
}