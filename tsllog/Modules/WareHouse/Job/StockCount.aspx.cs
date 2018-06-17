using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.Xml;
using System.IO;
using DevExpress.Web.ASPxHtmlEditor;

public partial class Modules_WareHouse_Job_StockCount : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Date.Date = DateTime.Today;
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // btn_Sch_Click(null, null);
        }
        OnLoad();
    }
    private void BindData()
    {
        string sql = string.Format(@"select det.*,p.HsCode,p.ProductClass,mast.WareHouseId,StockDate,DoDate from StockCountDet det  left join StockCount mast  on mast.Id=det.RefNo left join ref_product p on p.code=det.Product
");
        string dateTo = "";
        if (txt_Date.Value != null)
        {
            dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
            date.Date = txt_Date.Date;
        }
        if (dateTo.Length > 0)
        {
            sql += string.Format(" where CONVERT(varchar(100), StockDate, 23)='{0}'", date.Date.ToString("yyyy-MM-dd"));
        }
        sql += " order by StockDate";
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select det.*,StockDate from StockCountDet det  left join StockCount mast  on mast.StockNo=det.RefNo
");
        string dateTo = "";
        if (txt_Date.Value != null)
        {
            dateTo = txt_Date.Date.ToString("yyyy-MM-dd");
            date.Date = txt_Date.Date;
        }
        if (dateTo.Length > 0)
        {
            sql += string.Format(" where CONVERT(varchar(100), StockDate, 23)='{0}'", date.Date.ToString("yyyy-MM-dd"));
        }
        sql += " order by StockNo";
        DataTable tab= ConnectSql.GetTab(sql);
        if (tab.Rows.Count == 0)
        {
            InsertToStockCount(date.Date);
        }
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    protected void InsertToStockCount(DateTime date)
    {
        string sql = string.Format(@"select * from (select tab_in.No,tab_in.DoNo,tab_in.Product,tab_in.LotNo,tab_in.Id,
tab_in.PartyId,tab_in.PartyName,tab_in.PartyAdd,tab_in.DoDate,tab_in.PoNo,tab_in.Remark,tab_in.GrossWeight,tab_in.NettWeight
,tab_in.Qty1 as InQty,tab_in.PermitNo,tab_in.PartyInvNo,tab_in.PalletNo,tab_in.Remark1,
isnull(tab_in.Qty2,0) as BadQty,isnull(tab_in.Qty3,0) as InPallet,tab_hand.Qty1 as HandQty
,tab_in.DoDate as InDate,tab_in.Des1,tab_in.WareHouseId,tab_in.Location,tab_in.Price,tab_in.Packing,
tab_in.Att1,tab_in.Att2,tab_in.Att3,tab_in.Att4,tab_in.Att5,tab_in.Att6,tab_in.Qty2,tab_in.Qty3,
tab_in.Uom1,tab_in.ExpiredDate from 
(select distinct row_number() over (order by det.Id) as No, max(det.Id) as Id, max(mast.DoNo) as DoNo,max(det.Product) as Product,max(det.Qty1) as Qty1,max(det.Qty2) as Qty2,max(det.Qty3) as Qty3,max(det.LotNo) as LotNo,max(p.HsCode) as HsCode,max(mast.Remark1) as Remark1,
max(p.ProductClass) as ProductClass,max(mast.PartyId) as PartyId,max(mast.PartyName) as PartyName,max(mast.PartyAdd) as PartyAdd,max(mast.DoDate) as DoDate,max(det.QtyPackWhole) as Pkg,max(det.QtyWholeLoose) as Unit,max(mast.WareHouseId) as WareHouseId,max(mast.PoNo) as PoNo,max(det.Remark) as Remark,
max(det.GrossWeight) as GrossWeight,max(det.NettWeight) as NettWeight,max(det.PalletNo) as PalletNo,max(det.Location) as Location,max(mast.PermitNo) as PermitNo,max(mast.PartyInvNo) as PartyInvNo,max(det.Uom1) as Uom1,max(d.ExpiredDate) as ExpiredDate,max(det.Price) as Price,
max(det.Des1) as Des1,max(det.att1) as Att1,max(det.att2) as Att2,max(det.att3) as Att3,max(det.att4) as Att4,max(det.att5) as Att5,max(det.att6) as Att6,max(det.packing) as Packing from  wh_dodet2 det inner join Wh_DoDet d on d.DoNo=det.DoNo and d.ProductCode=det.Product and d.LotNo=det.LotNo inner join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 group by det.Id,Product,det.LotNo,det.Des1,det.PalletNo,det.Remark) as tab_in 
inner join (select product,LotNo, sum(isnull(Case when det.DoType='IN' then Qty1 else -Qty1 end,0)) as Qty1  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  
  group by Product,LotNo) as tab_hand on tab_hand.product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo 
) as tab where HandQty>0 
");
        if(txt_CustId.Text!=""){
            sql += " and PartyId='"+txt_CustId.Text+"'";
        }
        if (cmb_WareHouse.Value != null)
        {
            sql += " and WareHouseId='" + cmb_WareHouse.Value + "'";
        }
        if (cmb_loc.Value != null)
        {
            sql += " and Location='" + cmb_loc.Value + "'";
        }
        DataTable tab = ConnectSql.GetTab(sql);
        string partyId = "";
        string no = "";
        if (tab.Rows.Count > 0)
        {
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                string client = SafeValue.SafeString(tab.Rows[i]["PartyId"]);

                if (partyId != client)
                {
                    StockCount stock = new StockCount();

                    stock.StockDate = DateTime.Now;
                    stock.PartyId = client;
                    stock.PartyName = SafeValue.SafeString(tab.Rows[i]["PartyName"]);
                    stock.PartyAdd = SafeValue.SafeString(tab.Rows[i]["PartyAdd"]);
                    stock.WareHouseId = SafeValue.SafeString(tab.Rows[i]["WareHouseId"]);

                    stock.Remark = "";
                    stock.CreatedBy = EzshipHelper.GetUserName();
                    stock.CreateDateTime = DateTime.Now;

                    string stockNo = C2Setup.GetNextNo("", "StockTake", txt_Date.Date);
                    stock.StockNo = SafeValue.SafeString(stockNo);
                    C2Setup.SetNextNo("", "StockTake", stockNo, txt_Date.Date);
                    Manager.ORManager.StartTracking(stock, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(stock);
                    no = stockNo;
                    partyId = client;
                    InsertDet(tab, no, i);
                }
                else
                {
                    InsertDet(tab, no, i);
                }
            }
        }
    }

    private void InsertDet(DataTable tab, string no, int i)
    {
        string product = SafeValue.SafeString(tab.Rows[i]["Product"]);
        string lotNo = SafeValue.SafeString(tab.Rows[i]["LotNo"]);
        string des = SafeValue.SafeString(tab.Rows[i]["Des1"]);
        string remark = SafeValue.SafeString(tab.Rows[i]["Remark"]);
        string palletNo = SafeValue.SafeString(tab.Rows[i]["PalletNo"]);
        decimal qty1 = SafeValue.SafeDecimal(tab.Rows[i]["HandQty"], 0);
        decimal qty2 = SafeValue.SafeDecimal(tab.Rows[i]["Qty2"], 0);
        decimal qty3 = SafeValue.SafeDecimal(tab.Rows[i]["Qty3"], 0);
        decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"]);
        string packing = SafeValue.SafeString(tab.Rows[i]["Packing"]);
        string location = SafeValue.SafeString(tab.Rows[i]["Location"]);
        string uom = SafeValue.SafeString(tab.Rows[i]["Uom1"]);
        string att1 = SafeValue.SafeString(tab.Rows[i]["Att1"]);
        string Att2 = SafeValue.SafeString(tab.Rows[i]["Att2"]);
        string Att3 = SafeValue.SafeString(tab.Rows[i]["Att3"]);
        string Att4 = SafeValue.SafeString(tab.Rows[i]["Att4"]);
        string Att5 = SafeValue.SafeString(tab.Rows[i]["Att5"]);
        string Att6 = SafeValue.SafeString(tab.Rows[i]["Att6"]);
        DateTime expiryDate = SafeValue.SafeDate(tab.Rows[i]["ExpiredDate"], DateTime.Now);
        decimal grossWeight = SafeValue.SafeDecimal(tab.Rows[i]["GrossWeight"], 0);
        decimal nettWeight = SafeValue.SafeDecimal(tab.Rows[i]["NettWeight"], 0);
        string partyId = SafeValue.SafeString(tab.Rows[i]["PartyId"]);
        string partyName = SafeValue.SafeString(tab.Rows[i]["PartyName"]);
        string partyAdd = SafeValue.SafeString(tab.Rows[i]["PartyAdd"]);
        string doNo = SafeValue.SafeString(tab.Rows[i]["DoNo"]);
        DateTime doDate = SafeValue.SafeDate(tab.Rows[i]["DoDate"], DateTime.Now);
        string wareHouseId = SafeValue.SafeString(tab.Rows[i]["WareHouseId"]);
        string sql = string.Format(@"INSERT INTO [dbo].[StockCountDet] ([Product],[LotNo],[Description],[Qty1],[Qty2],[Qty3],[NewQty],[Price],[NewPrice],[Packing],[Location],[Uom]
 ,[Att1],[Att2],[Att3],[Att4],[Att5],[Att6],[ExpiryDate],[RefNo],[GrossWeight],[NettWeight],[PartyId],[PartyName],[PartyAdd],[DoNo],[DoDate],[WareHouseId]) 
VALUES ('{0}','{1}','{2}',{3},{4},{5},0,0,0,'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}','{21}','{22}','{23}','{24}')",
product, lotNo, des, qty1,qty2,qty3, packing,location, uom, att1, Att2, Att3, Att4, Att5, Att6, expiryDate, no, grossWeight, nettWeight,partyId,partyName,partyAdd,doNo,doDate,wareHouseId);
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int qty = 0;
        public string id = "";
        public string sku = "";
        public string lotNo = "";
        public Record(string _id, int _qty,string _sku,string _lotNo)
        {
            id = _id;
            qty = _qty;
            sku = _sku;
            lotNo = _lotNo;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int count = this.grid.VisibleRowCount;
        int end = count;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "ack_IsPay") as ASPxCheckBox;
            ASPxLabel sku = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "txt_Product") as ASPxLabel;
            ASPxLabel lotNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["LotNo"], "txt_LotNo") as ASPxLabel;
            ASPxTextBox txt_Id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit spin_NewQty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["NewQty"], "spin_NewQty") as ASPxSpinEdit;
            if (spin_NewQty != null)
            {
                int qty = SafeValue.SafeInt(spin_NewQty.Value, 0);
                if (qty > 0)
                {
                    isPay.Checked = true;
                    if (txt_Id != null && isPay != null && isPay.Checked && qty > 0)
                    {
                        list.Add(new Record(txt_Id.Text, qty, sku.Text, lotNo.Text));
                    }
                }

                else if (txt_Id == null)
                    break; ;
            }
        }
    }
    private void TransferToDo(string doNo, string sku, string lotNo, int qty, decimal price, string wh, string type, int id, string loc)
    {
        string sql = "";

        #region DOOUT
        sql = @"Insert Into Wh_DoDet(JobStatus,DoNo, DoType,ProductCode,Qty4,Qty5,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,DocAmt,LocationCode)";
        sql += string.Format(@"select 'Picked','{0}'as DoNo, 'OUT' as DoType,'{1}' as ProductCode
,{2} as Qty4,0 as Qty5
,{2} as Qty1
,0 as Qty2
,0 as Qty3
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
from Wh_DoDet where Id={0}", res, doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(), loc, id);

        ConnectSql.ExecuteSql(sql);
        #endregion
        
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if(s=="Save"){
            if (list.Count > 0)
            {
                string value = "";
                string variable = "";
                for (int i = 0; i < list.Count; i++)
                {
                    int newQty = list[i].qty;
                    string id = list[i].id;
                    string product = list[i].sku;
                    string lot = list[i].lotNo;
                    string sql = string.Format(@"update StockCountDet set NewQty=-{0} where Id='{1}'", newQty, id);
                    ConnectSql.ExecuteSql(sql);
                    ASPxTextBox txt_DoNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "txt_DoNo") as ASPxTextBox;

                    string doNo = SafeValue.SafeString(txt_DoNo.Text);
                    string sql_do = string.Format(@"select distinct det2.Id,det2.Product,det2.LotNo,det2.Price,det2.Location,mast.PartyId,mast.PartyName,mast.PartyAdd,mast.WarehouseId 
from wh_dodet2 det2 inner join wh_do mast on mast.DoNo=det2.DoNo where mast.DoNo='{0}' and Product='{1}' and LotNo='{2}'", doNo,product,lot);
                    DataTable dt = ConnectSql.GetTab(sql_do);
                   
                    if (variable!=doNo)
                    {
                        WhDo whDo = new WhDo();
                        string issueN = C2Setup.GetNextNo("", "DOOUT", DateTime.Today);
                        whDo.DoDate = DateTime.Now;
                        whDo.DoType = "OUT";
                        whDo.PartyId = SafeValue.SafeString(dt.Rows[i]["PartyId"]);
                        whDo.PartyName = SafeValue.SafeString(dt.Rows[i]["PartyName"]);
                        whDo.PartyAdd = SafeValue.SafeString(dt.Rows[i]["PartyAdd"]);
                        whDo.DeliveryTo = SafeValue.SafeString(dt.Rows[i]["PartyAdd"]);
                        whDo.WareHouseId = SafeValue.SafeString(dt.Rows[i]["WareHouseId"]);
                        string userId = HttpContext.Current.User.Identity.Name;
                        whDo.CreateBy = userId;
                        whDo.CreateDateTime = DateTime.Now;
                        whDo.UpdateBy = userId;
                        whDo.UpdateDateTime = DateTime.Now;
                        whDo.DoNo = issueN;
                        whDo.StatusCode = "CLS";

                        C2Setup.SetNextNo("", "DOOUT", issueN, whDo.DoDate);
                        Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(whDo);

                        value = issueN;
                    }

                    for (int j = 0; j < dt.Rows.Count;j++ )
                    {
                        string sku = SafeValue.SafeString(dt.Rows[j]["Product"]);
                        string lotNo = SafeValue.SafeString(dt.Rows[j]["LotNo"]);
                        decimal price = SafeValue.SafeDecimal(dt.Rows[j]["Price"]);
                        string loc = SafeValue.SafeString(dt.Rows[j]["Location"]);
                        int detId = SafeValue.SafeInt(dt.Rows[j]["Id"], 0);
                        TransferToDo(value, sku, lotNo, newQty, price, loc, "", detId, loc);
                    }
                    variable = doNo;

                }

                e.Result = "Success";
                btn_Sch_Click(null, null);
            }
            else
            {
                //e.Result = "Please keyin select product";
            }
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null,null);
        gridExport.WriteXlsToResponse("StockCount");
    }
    protected void cmb_loc_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        string s = cmb_WareHouse.SelectedItem.Value.ToString();

        string Sql = "SELECT Id,Code,Name from ref_location where Loclevel='Unit' and WarehouseCode='" + s + "'";

        DataSet ds = ConnectSql.GetDataSet(Sql);

        cmb_loc.DataSource = ds.Tables[0];

        cmb_loc.TextField = "Code";

        cmb_loc.ValueField = "Code";

        cmb_loc.DataBind();
    }
}