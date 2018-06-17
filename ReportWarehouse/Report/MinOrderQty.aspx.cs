using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;
public partial class ReportWarehouse_Report_MinOrderQty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        else
        {
            OnLoad();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"Select DISTINCT *  from (
select p.Code,WareHouseId,p.HsCode,p.ProductClass,tab_in.PartyId
,p.Description,ISNULL(tab_hand.Qty1-isnull(tab_out.Qty1,0),0) as HandQty,p.UomPacking as Uom1,p.MinOrderQty,p.Att4,p.Att5,p.Att6,p.Att7,p.Att8,p.Att9,0 as Qty,0 as Price
 from ref_product p
left join (select det.Product,mast.WareHouseId,det.Des1,det.packing,p.HsCode,p.ProductClass,Uom1,p.PartyId from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode!='CNL' 
left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in on p.Code=tab_in.Product
left join (select product ,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product) as tab_hand on  tab_hand.Product=tab_in.Product 
left join (select Product,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product) as tab_out on tab_out.Product=tab_hand.Product 
left join (select ProductCode,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)<0 then Qty5 else 0 end) as InQty_0
 from wh_doDet det inner join Wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where det.DoType='IN' and mast.StatusCode!='CNL'  group by productCode
) as tab_Incoming on tab_Incoming.ProductCode=tab_hand.product) as tab
left join (select Product,SUM(case when det.DoType='IN' then Qty1 else isnull(-Qty1,0) end) as BalQty  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS' where len(det.DoNo)>0  group by product) as tab_Bal on tab_Bal.Product=tab.Code 
where  MinOrderQty>ISNULL(BalQty,0) and MinOrderQty>0");
        string where = " ";
        if (this.txt_SKULine_Product.Text.Length > 0)
        {
            string product = this.txt_SKULine_Product.Text.Trim();
            string filter = product.Replace(" ", "%") + "%";
            where += string.Format("and (Code like '%{0}' or Description like '%{0}')", filter);
        }

        if (this.cmb_WareHouse.Text.Trim().Length > 0)
        {
            where += string.Format(" and WareHouseId='{0}'", this.cmb_WareHouse.Text.Trim());
        }
        if (this.txt_CustId.Text.Length > 0)
        {
            where += string.Format(" and PartyId like '%{0}%'", this.txt_CustId.Text);
        }
        if (where.Length > 0)
        {
            sql += " " + where;
        }
        this.grid.DataSource =  ConnectSql.GetTab(sql);
        this.grid.DataBind();
        if(this.grid.PageCount>0){
            btn_CreatePo.Enabled = true;
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("StockBalance");
    }
    protected void btn_CreatePo_Click(object sender, EventArgs e)
    {
        string poNo = "";
//        string sql = string.Format(@"Select DISTINCT tab.Product,tab.ProductClass,tab.HandQty,tab.HsCode,tab.Description,tab.Att4,tab.Att5,tab.Att6,tab.Att7,tab.Att8,tab.Att9,Uom1,MinOrderQty,WareHouseId,NewOrderQty,0 as Qty,isnull(tab_Po.Price,0) as Price   from (
//select tab_hand.Product,tab_in.Packing,WareHouseId,tab_in.HsCode,tab_in.ProductClass
//,tab_in.Description,tab_hand.Qty1-isnull(tab_out.Qty1,0) as HandQty,UomPacking as Uom1,Att4,Att5,Att6,Att7,Att8,Att9,0 as Qty
// from (select DISTINCT p.Code,mast.WareHouseId,p.Description,p.QtyPackingWhole as packing,p.HsCode,p.ProductClass,p.Att4,p.Att5,p.Att6,p.Att7,p.Att8,p.Att9,p.UomPacking from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
//left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in 
//left join (select product ,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product) as tab_hand on  tab_hand.Product=tab_in.Code 
//left join (select Product,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product) as tab_out on tab_out.Product=tab_hand.Product 
//left join (select ProductCode,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)<0 then Qty5 else 0 end) as InQty_0
// from wh_doDet det inner join Wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where det.DoType='IN' and mast.StatusCode!='CNL'  group by productCode
//) as tab_Incoming on tab_Incoming.ProductCode=tab_hand.product)  as tab left join ref_product p on p.Code=tab.Product
//left join (select top(1) price,ProductCode from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType and mast.DoType='PO' and mast.DoStatus='Closed' order by mast.DoDate,det.Price Desc) tab_Po on tab_Po.ProductCode=tab.Product
//left join (select Product,SUM(case when det.DoType='IN' then Qty1 else isnull(-Qty1,0) end) as BalQty  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS' where det.DoType='IN' and len(det.DoNo)>0  group by product) as tab_Bal on tab_Bal.Product=p.Code 
//where 1=1 and HandQty>0 and p.MinOrderQty>tab_Bal.BalQty");
//        string sql = string.Format(@"Select DISTINCT *  from (
//select p.Code,WareHouseId,p.HsCode,p.ProductClass,tab_in.PartyId
//,p.Description,ISNULL(tab_hand.Qty1-isnull(tab_out.Qty1,0),0) as HandQty,p.UomPacking as Uom1,p.MinOrderQty,p.Att4,p.Att5,p.Att6,p.Att7,p.Att8,p.Att9,0 as Qty,0 as Price
// from ref_product p
//left join (select det.Product,mast.WareHouseId,det.Des1,det.packing,p.HsCode,p.ProductClass,Uom1,p.PartyId from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode!='CNL' 
//left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 ) as tab_in on p.Code=tab_in.Product
//left join (select product ,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product) as tab_hand on  tab_hand.Product=tab_in.Product 
//left join (select Product,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' and len(det.DoNo)>0 group by Product) as tab_out on tab_out.Product=tab_hand.Product 
//left join (select ProductCode,sum(Case when DATEDIFF(week,getdate(),mast.ExpectedDate)<0 then Qty5 else 0 end) as InQty_0
// from wh_doDet det inner join Wh_do mast on det.DoNo=mast.DoNo and det.DoType=mast.DoType where det.DoType='IN' and mast.StatusCode!='CNL'  group by productCode
//) as tab_Incoming on tab_Incoming.ProductCode=tab_hand.product) as tab
//left join (select Product,SUM(case when det.DoType='IN' then Qty1 else isnull(-Qty1,0) end) as BalQty  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS' where det.DoType='IN' and len(det.DoNo)>0  group by product) as tab_Bal on tab_Bal.Product=tab.Code 
//where  MinOrderQty>ISNULL(BalQty,0) and MinOrderQty>0");
        //DataTable tab = new DataTable();
        if(list.Count>0){

            WhTrans whTrans = new WhTrans();
            poNo = C2Setup.GetNextNo("", "PurchaseOrders", DateTime.Now);

            whTrans.DoDate = DateTime.Now;
            whTrans.DoNo = poNo;
            whTrans.DoType = "PO";
            whTrans.DoStatus = "Draft";
            whTrans.ExpectedDate = DateTime.Today.AddDays(14);
            whTrans.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
            whTrans.ExRate =SafeValue.SafeDecimal(1.000000);
            whTrans.WareHouseId = System.Configuration.ConfigurationManager.AppSettings["WareHouse"];
            whTrans.CreateBy = EzshipHelper.GetUserName();
            whTrans.CreateDateTime = DateTime.Now;

            Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(whTrans);
            C2Setup.SetNextNo("", "PurchaseOrders", poNo, DateTime.Now);

            for (int i = 0; i < list.Count; i++)
            {
                string sku = list[i].code;
                string des = list[i].des;
                string att4 = list[i].att4;
                string att5 = list[i].att5;
                string att6 = list[i].att6;
                string att7 = list[i].att7;
                string att8 = list[i].att8;
                string att9 = list[i].att9;
                string uom1 = list[i].uom1;
                //sql += string.Format(@" and Code='{0}' ", sku);
                //tab = ConnectSql.GetTab(sql);
                WhTransDet whTransDet = new WhTransDet();
                whTransDet.DoNo = poNo;
                whTransDet.DoType = "PO";
                whTransDet.ProductCode = sku;
                whTransDet.Qty1 = list[i].qty;
                whTransDet.Des1 = des;
                whTransDet.Att1 = att4;
                whTransDet.Att2 = att5;
                whTransDet.Att3 = att6;
                whTransDet.Att4 = att7;
                whTransDet.Att5 = att8;
                whTransDet.Att6 = att9;
                whTransDet.Uom1 = uom1;
                whTransDet.Price = list[i].price;
                whTransDet.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                whTransDet.ExRate = SafeValue.SafeDecimal(1.000000);
                whTransDet.DocAmt = SafeValue.SafeDecimal(whTransDet.Qty1 * whTransDet.Price);
                
                Manager.ORManager.StartTracking(whTransDet, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(whTransDet);
            }
            Response.Redirect("/WareHouse/Job/PoEdit.aspx?no=" + poNo);
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string code = "";
        public int qty = 0;
        public decimal price = 0;
        public string des = "";
        public string att4 = "";
        public string att5 = "";
        public string att6 = "";
        public string att7 = "";
        public string att8 = "";
        public string att9 = "";
        public string uom1 = "";
        public Record(string _code,int _qty,decimal _price,string _des,string _att4,string _att5,string _att6,string _att7,string _att8,string _att9,string _uom1)
        {
            code = _code;
            qty = _qty;
            price = _price;
            des = _des;
            att4 = _att4;
            att5 = _att5;
            att6 = _att6;
            att7 = _att7;
            att8 = _att8;
            att9 = _att9;
            uom1 = _uom1;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = 5000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Code"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox code = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Code"], "txt_Id") as ASPxTextBox;
            ASPxLabel lbl_Description = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Description"], "lbl_Description") as ASPxLabel;
            ASPxLabel lbl_Att4 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att4"], "lbl_Att4") as ASPxLabel;
            ASPxLabel lbl_Att5 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att5"], "lbl_Att5") as ASPxLabel;
            ASPxLabel lbl_Att6 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att6"], "lbl_Att6") as ASPxLabel;
            ASPxLabel lbl_Att7 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att7"], "lbl_Att7") as ASPxLabel;
            ASPxLabel lbl_Att8= this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att8"], "lbl_Att8") as ASPxLabel;
            ASPxLabel lbl_Att9 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Att9"], "lbl_Att9") as ASPxLabel;
            ASPxLabel lbl_Uom1 = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Uom1"], "lbl_Uom1") as ASPxLabel;
            ASPxLabel lbl_PartyId = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["PartyName"], "lbl_PartyId") as ASPxLabel;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            if (isPay != null && isPay.Checked)
            {
                list.Add(new Record(code.Text, SafeValue.SafeInt(spin_Qty.Text, 0), SafeValue.SafeDecimal(spin_Price.Text),lbl_Description.Text,lbl_Att4.Text,lbl_Att5.Text,lbl_Att6.Text,
                    lbl_Att7.Text,lbl_Att8.Text,lbl_Att9.Text,lbl_Uom1.Text));
            }
            else if (code == null)
                break; ;
        }
    }
}