using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SelectPoductMore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null, null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
		
		string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12,REPLACE(Att13,char(39),'\&#39;') as Att13 from ref_product ";
        string typ = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
            sql = @"SELECT REPLACE(Code,char(39),'\&#39;') as Code,REPLACE(REPLACE(NAME,char(34),'\&#34;'),char(39),'\&#39;') as Name,REPLACE(Description,char(39),'\&#39;') as Description,QtyPackingWhole,QtyWholeLoose,QtyLooseBase,UomPacking as Uom1,UomWhole as Uom2,UomLoose as Uom3,UomBase as Uom4,REPLACE(Att1,char(39),'\&#39;') as  Att1,
Name as Name1,Description as Description1,
REPLACE(Att4,char(39),'\&#39;') as Att4,REPLACE(Att5,char(39),'\&#39;') as Att5,REPLACE(Att6,char(39),'\&#39;') as Att6,REPLACE(Att7,char(39),'\&#39;') as Att7,REPLACE(Att8,char(39),'\&#39;') as Att8,REPLACE(Att9,char(39),'\&#39;') as Att9,REPLACE(Att10,char(39),'\&#39;') as Att10,REPLACE(Att11,char(39),'\&#39;') as Att11,REPLACE(Att12,char(39),'\&#39;') as Att12
,case when isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='PO' and det.ProductCode=ref_product.code order by mast.DoDate desc),0)=0 then isnull(CostPrice,0) else isnull((select top(1) price from wh_transdet det inner join wh_trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='PO' and det.ProductCode=ref_product.code order by mast.DoDate desc),0) end as Att13
from ref_product";
        string where = "";
        if (name.Length > 0)
        {
            where = GetWhere(where, " where Name Like '" + name.Replace("'", "''") + "%'");
        }
        sql+=where+ " ORDER BY Name ";
		
       DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string soNo = Request.QueryString["Sn"].ToString();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string sku = list[i].sku;
                    string lotNo = list[i].lotNo;
                    int qty1 = list[i].qty1;
                    //int qty2 = list[i].qty2;
                    //int qty3 = list[i].qty3;
                    decimal price = list[i].price;
                    TransferToDo(soNo, sku, lotNo, qty1, price);
                }
                e.Result = "Success";
            }
            catch { }

        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    private void TransferToDo(string doNo, string sku, string lotNo, int qty, decimal price)
    {
        string sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6)";
        sql += string.Format(@"select '{0}'as DoNo, 'SO' as DoType,'{1}' as Sku
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,0 as Qty4,0 as Qty5
 ,'{3}'
,'{4}' as LotNo
,p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase
,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName());
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string sku = "";
        public int qty1 = 0;
        //public int qty2 = 0;
        //public int qty3 = 0;
        public decimal price = 0;
        public string lotNo = "";
        public Record(string _sku, string _lotNo,int _qty1, decimal _price)
        {
            sku = _sku;
            lotNo = _lotNo;
            qty1 = _qty1;
            //qty2 = _qty2;
            //qty3 = _qty3;
            price = _price;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i,(DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"],"ack_IsPay") as ASPxCheckBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Product") as ASPxTextBox;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;
            //ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            //ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;

            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(sku.Text, lotNo.Text, SafeValue.SafeInt(qty1.Value, 0), SafeValue.SafeDecimal(price.Value, 0)
                    ));
            }
            else if (sku == null)
                break; ;
        }
    }
}