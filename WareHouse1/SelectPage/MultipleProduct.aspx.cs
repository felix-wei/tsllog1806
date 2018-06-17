using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_MultipleProduct : System.Web.UI.Page
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
        string sql = string.Format(@"select *,0 as Qty,0.00 as Price,isnull(tab_hand.HandQty,0) as HandQty,
isnull((select top(1) price from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='SO' and mast.DoStatus!='Canceled' and det.Productcode=ref.Code order by mast.DoDate desc,det.Price Desc ),0) as LastSell,
isnull((select top(1) price from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='PO' and mast.DoStatus!='Canceled' and det.Productcode=ref.Code order by mast.DoDate desc,det.Price Desc ),0) as LastBuy,
tab_sq.Price as SellingPrice,tab_pq.Price as BuyingPrice
from ref_product ref 
left join (select PartyId,ProductCode,Price from wh_trans mast inner join  Wh_TransDet det on det.DoType='SQ' and mast.DoNo=det.DoNo) as tab_sq on tab_sq.ProductCode=ref.Code
left join (select PartyId,ProductCode,Price from wh_trans mast inner join  Wh_TransDet det on det.DoType='PQ' and mast.DoNo=det.DoNo) as tab_pq on tab_sq.ProductCode=tab_pq.ProductCode
left join (select product,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') group by product) as tab_hand on tab_hand.Product=ref.Code ");
        if (name.Length > 0)
        {
            sql += string.Format(" where Name like '%{0}%' ", name.Replace("'", ""));
        }
        sql += " order by ref.Id";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string soNo =SafeValue.SafeString(Request.QueryString["Sn"].ToString());
            string type =SafeValue.SafeString(Request.QueryString["Type"].ToString());
            string whId = SafeValue.SafeString(Request.QueryString["WhId"].ToString());
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string sku = list[i].sku;
                    int qty1 = list[i].qty1;
                    //int qty2 = list[i].qty2;
                    //int qty3 = list[i].qty3;
                    decimal price = list[i].price;
                    decimal amt = SafeValue.SafeDecimal(qty1 * price);
                    TransferToDo(soNo, sku, type, qty1, price, amt,whId);
					if(whId.ToUpper()=="DROPSHIP"){					
                      TransferToPoRequest(soNo, sku, qty1, price);
					}
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
    private void TransferToDo(string doNo, string sku,string type, int qty, decimal price,decimal amt,string wh)
    {
        string sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,DocAmt,Des1,Packing,LocationCode)";
        sql += string.Format(@"select '{0}'as DoNo, '{4}' as DoType,'{1}' as Sku
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,0 as Qty4,0 as Qty5
 ,'{3}'
,p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase
,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,'{6}',p.Description,p.Att1,'{7}'
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", doNo, sku, qty, price, type, EzshipHelper.GetUserName(),amt,wh);
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
        public string type = "";
        public Record(string _sku, int _qty1, decimal _price)
        {
            sku = _sku;
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
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Product") as ASPxTextBox;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            //ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            //ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;

            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(sku.Text, SafeValue.SafeInt(qty1.Value, 0), SafeValue.SafeDecimal(price.Value, 0)
                    ));
            }
            else if (sku == null)
                break; ;
        }
    }
	    private void TransferToPoRequest(string doNo, string sku, int qty, decimal price)
    {
        string sql = @"Insert Into Wh_PoRequest(SoNo,Product,Qty1,Qty2,Qty3,Price,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing)";
        sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,'{3}'
,p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase
,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase
,'{4}' as CreateBy,getdate() as CreateDateTime,'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1 
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", doNo, sku, qty, price, EzshipHelper.GetUserName());
        ConnectSql.ExecuteSql(sql);
    }
}