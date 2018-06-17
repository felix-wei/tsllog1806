using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SelectPoductFromStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string lotNo = this.txt_LotNo.Text.Trim().ToUpper();
		string whId=SafeValue.SafeString(Request.QueryString["WhId"]);
        string partyId = SafeValue.SafeString(Request.QueryString["partyId"]);
        string dateFrom = "";
        string dateTo = "";
        string sql = string.Format(@"Select *  from (
select distinct tab_hand.Product,ltrim(rtrim(tab_hand.LotNo)) as LotNo,tab_in.Packing,WareHouseId
,0 as Qty1,0 as Qty2,0 as Qty3,tab_in.Description
,tab_hand.HandQty
,isnull(tab_Reserved.ReservedQty,0) as ReservedQty
,tab_hand.HandQty-isnull(tab_Reserved.ReservedQty,0) as AvaibleQty
,tab_Incoming.InQty_0
,tab_Incoming.InQty_1
,tab_Incoming.InQty_2
,tab_Incoming.InQty_3
,isnull((select top(1) price from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where mast.DoType='SO' and mast.DoStatus!='Canceled' and det.Productcode=tab_in.Product order by mast.DoDate desc,det.Price Desc ),0) as Price
 from (select max(det.Product) as Product,max(det.LotNo) as LotNo,max(mast.WareHouseId) as WareHouseId,max(p.Description) as Description,
 max(p.Att1) as Packing,max(mast.PartyId) as PartyId  from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
inner join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 group by Product,LotNo,Des1,QtyPackWhole,mast.PartyId) as tab_in 
inner join (select product,LotNo,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode='CLS' group by product,LotNo) as tab_hand on tab_in.Product=tab_hand.Product and tab_in.LotNo=tab_hand.LotNo
left join (select productCode as Product,LotNo,sum(Qty5) as ReservedQty from wh_doDet det inner join  wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo) as tab_Reserved on tab_Reserved.product=tab_hand.product and tab_hand.LotNo=tab_Reserved.LotNo
inner join (
select ProductCode,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)<0 then Qty5 else 0 end) as InQty_0
,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)>=0 and DATEDIFF(week,getdate(),mast.ExpectedDate)<2 then Qty5 else 0 end) as InQty_1
,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)>0 and DATEDIFF(week,getdate(),mast.ExpectedDate)<4 then Qty5 else 0 end) as InQty_2
,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)>0 and DATEDIFF(week,getdate(),mast.ExpectedDate)>4 then Qty5 else 0 end) as InQty_3
 from wh_doDet det inner join Wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where det.DoType='IN' and mast.StatusCode='CLS'  group by productCode
,LotNo,mast.PartyId) as tab_Incoming 
on tab_Incoming.ProductCode=tab_hand.product  
)  as tab
 where 1=1 and AvaibleQty>0");
        if (name.Length > 0)
        {
            sql += string.Format(" and Product like '%{0}%'", name.Replace("'", ""));
        }
        if (lotNo.Length > 0)
        {
            sql += string.Format(" and tab.LotNo like '%{0}%'", lotNo);
        }
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            sql += string.Format(" and DoDate >= '{0}' and DoDate < '{1}'", dateFrom, dateTo);
        }
		if(whId.Length>0){
		    sql+=string.Format(" and tab.WareHouseId='{0}'",whId);
		}
        sql += " order by LotNo asc";
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
            string wh = Request.QueryString["WhId"].ToString();
            string type = Request.QueryString["Type"].ToString();
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
                    TransferToDo(soNo, sku, lotNo, qty1, price, wh, type);

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
    private void TransferToDo(string doNo, string sku, string lotNo, int qty, decimal price,string wh,string type)
    {
        string sql = "";
        if(type=="SO")
        {
            #region DOOUT
            sql = @"Insert Into Wh_TransDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
        sql += string.Format(@"select '{0}'as DoNo, 'SO' as DoType,'{1}' as Product
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,'{3}' as Price
,'{4}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,att1,att2,att3,att4,att5,att6,Des1,Packing,{2}*{3} as DocAmt , '{6}' LocationCode
from wh_dodet det inner join Wh_DO mast on det.DoNo=mast.DoNo where ProductCode='{1}' and LotNo='{4}' and mast.dotype='IN'", doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(),wh);
            #endregion
        }
        #region DOOUT
        if (type=="OUT"){
            sql = @"Insert Into Wh_DoDet(JobStatus,DoNo, DoType,ProductCode,Qty4,Qty5,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
            sql += string.Format(@"select 'Pending','{0}'as DoNo, 'OUT' as DoType,'{1}' as Product
,'{2}' as Qty4,'{2}' as Qty5
 ,0 as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,'{3}' as Price
,'{4}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,att1,att2,att3,att4,att5,att6,Des1,Packing,{2}*{3} as DocAmt , '{6}' LocationCode
from (select max(DoType) as DoType, max(LotNo) as LotNo,max(Product) as Product,max(Uom1) as Uom1,max(Uom2) as Uom2,max(Uom3) as Uom3,max(Uom4) as Uom4,
max(QtyPackWhole) as QtyPackWhole,max(QtyWholeLoose) as QtyWholeLoose,max(QtyLooseBase) QtyLooseBase
,max(Att1) att1,max(Att2) att2,max(Att3) att3,max(Att4) att4,max(Att5) att5,max(Att6) att6,max(Des1) as Des1,max(Packing) as Packing from Wh_DoDet2 group by LotNo ) as tab where Product='{1}' and LotNo='{4}' and dotype='IN'", doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(), wh);
        #endregion
        }

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
            ASPxLabel sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Product") as ASPxLabel;
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