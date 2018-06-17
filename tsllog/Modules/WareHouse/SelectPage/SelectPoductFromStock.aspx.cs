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
		btn_Sch_Click(null, null);
	}

	protected void btn_Sch_Click(object sender, EventArgs e)
	{
		string name = this.txt_Name.Text.Trim().ToUpper();
		string lotNo = this.txt_LotNo.Text.Trim().ToUpper();
		string whId = SafeValue.SafeString(Request.QueryString["WhId"]);
		string partyId = SafeValue.SafeString(Request.QueryString["partyId"]);
		string dateFrom = "";
		string dateTo = "";
        string sql = string.Format(@"Select *  from (
select distinct tab_in.Id,PartyId,tab_hand.Product,ltrim(rtrim(tab_hand.LotNo)) as LotNo,tab_in.Packing,WareHouseId,tab_in.Location,0 as Qty
,tab_in.Qty1,tab_in.Qty2,tab_in.Qty3,tab_in.Description,isnull(tab_out.Qty1,0) as OutQty,tab_in.PalletNo,tab_in.Remark,tab_in.Des1
,tab_hand.Qty1 as HandQty,QtyPackWhole,QtyWholeLoose,Uom1,Uom2,Uom3,Att1,Att2,Att3,Att4,Att5,Att6,tab_in.DoDate
from (select max(det.Id) as Id,max(det.Product) as Product,max(Qty1) as Qty1,max(Qty2) as Qty2,max(Qty3) as Qty3,LotNo,PalletNo,Des1,det.Remark,max(mast.WareHouseId) as WareHouseId,max(p.Description) as Description,max(mast.DoDate) as DoDate,
max(p.Att1) as Packing,max(mast.PartyId) as PartyId,max(Location) as Location,max(Uom1) as Uom1,max(Uom2) as Uom2,max(Uom3) as Uom3,max(det.QtyPackWhole) as QtyPackWhole,max(det.QtyWholeLoose) as QtyWholeLoose,max(det.Att1) as Att1,max(det.Att2) as Att2,max(det.Att3) as Att3,
max(det.Att4) as Att4,max(det.Att5) as Att5,max(det.Att6) as Att6  from  wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
left join ref_product p on p.code=det.Product where det.Dotype='IN' and len(det.doNo)>0 group by det.Id, Product,LotNo,Des1,mast.PartyId,PalletNo,det.Remark,Des1) as tab_in 
left join(select max(Qty1) as PickQty,ProductCode,LotNo,PalletNo,det.Remark,Des1 from Wh_DoDet det inner join Wh_DO mast on det.DoNo=mast.DoNo and mast.StatusCode='CLS' where mast.DoType='OUT' and JobStatus='Picked' group by ProductCode,LotNo,PalletNo,det.Remark,Des1) as tab_pick 
on isnull(tab_pick.ProductCode,'')=isnull(tab_in.Product,'') and isnull(tab_pick.LotNo,'')=isnull(tab_in.LotNo,'') and isnull(tab_pick.PalletNo,'')=isnull(tab_in.PalletNo,'') and isnull(tab_pick.Des1,'')=isnull(tab_in.Des1,'') and isnull(tab_pick.Remark,'')=isnull(tab_in.Remark,'')
inner join (select product,LotNo, sum(isnull(Case when det.DoType='IN' then Qty1 else -Qty1 end,0)) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
  group by Product,LotNo) as tab_hand on tab_hand.product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo 
left join (select product,LotNo, sum(isnull(Case when det.DoType='OUT' then Qty1 else 0 end,0)) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
  group by Product,LotNo) as tab_out on tab_out.product=tab_in.Product and tab_out.LotNo=tab_in.LotNo) as tab where 1=1 ");
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
        if (partyId.Length > 0)
        {
            sql += string.Format(" and tab.PartyId= '{0}'", partyId);
        }
		if(whId.Length>0) {
			sql+= string.Format(" and tab.WareHouseId='{0}'", whId);
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
			string doNo = Request.QueryString["Sn"].ToString();
			string wh = Request.QueryString["WhId"].ToString();
			string type = Request.QueryString["Type"].ToString();
            bool action = false;
            decimal totalQty = 0;
			try
			{
				for (int i = 0; i < list.Count; i++)
				{
					int id=list[i].id;
                    string sku = list[i].sku;
                    string lotNo = list[i].lotNo;
                    int qty1 = list[i].qty1;
                    //int qty2 = list[i].qty2;
                    //int qty3 = list[i].qty3;
                    decimal price = list[i].price;
                    string loc = list[i].loc;
                    string remark = list[i].remark;
                    string palletNo = list[i].pallet;
                    string des = list[i].des;
                    int handQty = list[i].handQty;
                    string sql_sum = string.Format(@"select sum(Qty1) as TotalQty from Wh_DoDet2 where DoNo='{0}' and isnull(Product,'')='{1}' and isnull(LotNo,'')='{2}' and isnull(Remark,'')='{3}' and isnull(PalletNo,'')='{4}' and isnull(Des1,'')='{5}'", doNo, sku, lotNo, remark, palletNo, des);
                    int total = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_sum), 0);
                    string sql = string.Format(@"select count(*) from Wh_DoDet2 where DoNo='{0}' and isnull(Product,'')='{1}' and isnull(LotNo,'')='{2}' and Location='{3}' and isnull(Remark,'')='{4}' and isnull(PalletNo,'')='{5}' and isnull(Des1,'')='{6}'", doNo, sku, lotNo, loc, remark, palletNo, des);
                    int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                    string sql_totalQty1 = string.Format(@"select sum(Qty1) as HandQty from Wh_DoDet2 det inner join wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode='CLS'  where mast.DoType='IN' and isnull(Product,'')='{0}' and isnull(LotNo,'')='{1}' and isnull(det.Remark,'')='{2}' and isnull(PalletNo,'')='{3}' and isnull(Des1,'')='{4}'",  sku, lotNo,remark, palletNo, des);

                    int total_qty1 = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_totalQty1), 0);
                    if (cnt > 0 && total < total_qty1)
                    {
                        sql = string.Format(@"update Wh_DoDet2 set Qty1=Qty1+{3} where DoNo='{0}' and isnull(Product,'')='{1}' and isnull(LotNo,'')='{2}' and isnull(Remark,'')='{4}' and isnull(PalletNo,'')='{5}' and isnull(Des1,'')='{6}'", doNo, sku, lotNo, qty1, remark, palletNo, des);
                        C2.Manager.ORManager.ExecuteCommand(sql);

                        sql = string.Format(@"update Wh_DoDet set Qty1=Qty1+{3},Qty4=Qty1+{3} where DoNo='{0}' and isnull(ProductCode,'')='{1}' and isnull(LotNo,'')='{2}' and isnull(Remark,'')='{4}' and isnull(PalletNo,'')='{5}' and isnull(Des1,'')='{6}'", doNo, sku, lotNo, qty1, remark, palletNo, des);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                        action = false;
                    }
                    else if (cnt > 0 && (total_qty1 == total || total > total_qty1))
                    {
                        sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and isnull(Product,'')='{1}' and isnull(LotNo,'')='{2}' and isnull(Remark,'')='{3}' and isnull(PalletNo,'')='{4}' and isnull(Des1,'')='{5}'", doNo, sku, lotNo, remark, palletNo, des);
                        C2.Manager.ORManager.ExecuteCommand(sql);

                        sql = string.Format(@"delete from Wh_DoDet where DoNo='{0}' and isnull(ProductCode,'')='{1}' and isnull(LotNo,'')='{2}' and isnull(Remark,'')='{3}' and isnull(PalletNo,'')='{4}' and isnull(Des1,'')='{5}'", doNo, sku, lotNo, remark, palletNo, des);
                        C2.Manager.ORManager.ExecuteCommand(sql);
                        action = true;
                    }
                    else if (cnt == 0)
                    {
                        if (total_qty1 == total)
                        {
                            sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and isnull(Product,'')='{1}' and Qty1={2} and isnull(Remark,'')='{3}' and isnull(PalletNo,'')='{4}' and isnull(Des1,'')='{5}' and isnull(LotNo,'')='{6}'", doNo, sku, qty1, remark, palletNo, des,lotNo);
                            C2.Manager.ORManager.ExecuteCommand(sql);
                        }
                        action = true;
                    }
                    if (action)
                    {
                        TransferToDo(doNo, sku, lotNo, qty1, price, wh, type, id, loc);
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
    private void TransferToDo(string doNo, string sku, string lotNo, int qty, decimal price,string wh,string type,int id,string loc)
    {
        string sql = "";
        if(type=="SO")
        {
            #region SO
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

        ConnectSql.ExecuteSql(sql);
            #endregion
        }
        
        if (type=="OUT")
        {
            #region DOOUT
            sql = @"Insert Into Wh_DoDet(JobStatus,DoNo, DoType,ProductCode,Qty4,Qty5,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
            sql += string.Format(@"select 'Picked','{0}'as DoNo, 'OUT' as DoType,'{1}' as ProductCode
,{2} as Qty4,0 as Qty5
,{2} as Qty1
,Qty2 as Qty2
,Qty3 as Qty3
,{3} as Price
,'{4}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
,att1,att2,att3,att4,att5,att6,Des1,Packing,{2}*{3} as DocAmt , '{6}' LocationCode
from (select * from Wh_DoDet2 where Id={7} ) as tab select @@identity", doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(), wh, id);
            int res = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);


          sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus,PalletNo,ContainerNo,Remark,RelaId)";
          sql += string.Format(@"select '{1}'as DoNo, 'OUT' as DoType,'{2}' as Sku
 ,'{3}' as Qty1
 ,Qty2
 ,Qty3
 ,'{4}' as Price
,'{5}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{6}' as CreateBy,getdate() as CreateDateTime,'{6}' as UpdateBy,getdate() as UpdateDateTime
,Att1,Att2,Att3,Att4,Att5,Att6,Des1,'' as Packing,'{7}' as Location,'Delivered' as ProcessStatus,PalletNo,ContainerNo,Remark,{8}
from Wh_DoDet where Id={0}", res, doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(), loc,id);

          ConnectSql.ExecuteSql(sql);
        #endregion
        }
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
        public int id=0;
        public string loc = "";
        public string des = "";
        public string pallet = "";
        public string remark = "";
        public int handQty = 0;
        public Record(int _id,string _sku, string _lotNo,int _qty1, decimal _price,string _loc,string _des,string _pallet,string _remark,int _handQty)
        {
            id=_id;
            sku = _sku;
            lotNo = _lotNo;
            qty1 = _qty1;
            //qty2 = _qty2;
            //qty3 = _qty3;
            price = _price;
            loc = _loc;
            des = _des;
            pallet = _pallet;
            remark = _remark;
            handQty = _handQty;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox Id = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i,(DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"],"ack_IsPay") as ASPxCheckBox;
            ASPxLabel sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Product") as ASPxLabel;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;
            ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;
            ASPxLabel location = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Location"], "lbl_Location") as ASPxLabel;
            ASPxLabel des = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Des1"], "lbl_Description") as ASPxLabel;
            ASPxLabel palletNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PalletNo"], "lbl_PalletNo") as ASPxLabel;
            ASPxLabel remark = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Remark"], "lbl_Remark") as ASPxLabel;
            ASPxLabel handQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["HandQty"], "txt_HandQty") as ASPxLabel;

            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(Id.Text, 0),sku.Text, lotNo.Text, SafeValue.SafeInt(qty1.Value, 0),0
                    , location.Text, SafeValue.SafeString(des.Text), SafeValue.SafeString(palletNo.Text), SafeValue.SafeString(remark.Text), SafeValue.SafeInt(handQty.Text, 0)));
            }
            else if (sku == null)
                break; ;
        }
    }
}