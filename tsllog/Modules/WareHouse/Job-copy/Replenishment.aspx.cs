using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxCallbackPanel;
using C2;
using DevExpress.Web.ASPxClasses;

public partial class WareHouse_Job_Replenishment : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        OnLoad();

        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string lotNo = this.txt_LotNo.Text.Trim().ToUpper();
        string whId = SafeValue.SafeString(Request.QueryString["Wh"]);
        string partyId = SafeValue.SafeString(Request.QueryString["partyId"]);
        string sql = string.Format(@"select * from (select distinct tab_in.DoNo, tab_in.Product,tab_in.LotNo,tab_in.HsCode,tab_in.ProductClass,tab_in.PartyId,tab_in.PartyName,tab_in.DoDate
,tab_hand.Qty1-ISNULL(tab_out.Qty1,0) as OnHand
,tab_in.Pkg,tab_in.Unit,Uom1,tab_hand.Location,WarehouseId,0 as Qty1,'' as ToWarehouseId,'' as ToLocId
,tab_in.Des1 as Description,tab_in.Att1,tab_in.Att2,tab_in.Packing from 
(select max(mast.DoNo) as DoNo, max(det.Product) as Product,max(det.LotNo) as LotNo,max(p.HsCode) as HsCode,max(p.ProductClass) as ProductClass,max(mast.PartyId) as PartyId,max(mast.PartyName) as PartyName,max(mast.DoDate) as DoDate,max(det.QtyPackWhole) as Pkg,max(det.QtyWholeLoose) as Unit,max(mast.WareHouseId) as WareHouseId,
max(det.Des1) as Des1,max(det.att1) as att1,max(det.att2) as att2,max(det.packing) as packing,max(det.Location) as Location,max(det.Uom1) as Uom1 from  wh_dodet2 det 
left join wh_do mast on mast.DoNo=det.DoNo and mast.DoType=det.DoType and mast.StatusCode='CLS' 
left join ref_product p on p.code=det.Product
where det.Dotype='IN' and len(det.doNo)>0 group by Product,LotNo,mast.PartyId,Location) as tab_in 
left join (select product,LotNo,MAX(Location) as Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,max(mast.PartyId) as PartyId from wh_dodet2 det 
inner join wh_do mast on mast.DoNo=det.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') and det.DoType='IN'   where  len(det.DoNo)>0  group by product,LotNo,mast.PartyId,Location) as tab_hand on  tab_hand.Product=tab_in.Product and tab_hand.LotNo=tab_in.LotNo and tab_hand.Location=tab_in.Location
left join (select product,LotNo,MAX(Location) as Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det 
inner join wh_do mast on mast.DoNo=det.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') and det.DoType='OUT'  where  len(det.DoNo)>0  group by product,LotNo,Location) as tab_out on  tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab_out.Location=tab_hand.Location
left join ref_location loc on loc.Loclevel='Unit' and loc.WarehouseCode=WareHouseId
) as tab where  OnHand>0", partyId);
        if (name.Length > 0)
        {
            sql += string.Format(" and Product like '%{0}%'", name.Replace("'", ""));
        }
        if (lotNo.Length > 0)
        {
            sql += string.Format(" and tab.LotNo like '%{0}%'", lotNo);
        }
        if (cmb_WareHouse.Text.Trim() != "" && cmb_Location.Text.Trim() == "")
        {
            sql += string.Format(" and tab.WarehouseId='{0}'", cmb_WareHouse.Text);
        }
        if (cmb_Location.Text.Trim() != "")
        {
            sql += string.Format(" and tab.Location='{0}'", cmb_Location.Text);
        }

        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();


    }
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "OK")
        {
            try
            {
                string sql=string.Format(@"select code,MaxCount,isnull((tab_hand.Qty1-isnull(tab_out.Qty1,0)),0) as nowN from ref_location lc
left join (select product,LotNo,MAX(Location) as Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,max(mast.PartyId) as PartyId from wh_dodet2 det 
inner join wh_do mast on mast.DoNo=det.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') and det.DoType='IN'   where  len(det.DoNo)>0  group by product,LotNo,mast.PartyId,Location) as tab_hand on  tab_hand.Location=lc.Code
left join (select product,LotNo,MAX(Location) as Location,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3 from wh_dodet2 det 
inner join wh_do mast on mast.DoNo=det.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') and det.DoType='OUT'  where  len(det.DoNo)>0  group by product,LotNo,Location) as tab_out on  tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab_out.Location=tab_hand.Location");
                DataTable dt = ConnectSql.GetTab(sql);
                for (int i = 0; i < list.Count; i++)
                {
                    int qty = list[i].qty;
                    string loc = list[i].toLoc;

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["code"].Equals(loc))
                        {
                            dt.Rows[j]["nowN"] = SafeValue.SafeInt(dt.Rows[j]["nowN"], 0) + qty;
                            break;
                        }
                    }
                    
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (SafeValue.SafeInt(dr["nowN"], 1) > SafeValue.SafeInt(dr["MaxCount"], 0))
                    {
                        e.Result = "Fail! (" + dr["code"] + ") No enough space";
                        return;
                    }
                    
                }

                for (int i = 0; i < list.Count; i++)
                {
                    string sku = list[i].sku;
                    string lotNo = list[i].lotNo;
                    string des = list[i].des;
                    string wh = list[i].wh;
                    string loc = list[i].loc;
                    int qty = list[i].qty;
                    string doNo = list[i].doNo;
                    int id = 0;
                    string toWh = list[i].toWh;
                    string toLoc = list[i].toLoc;

                   TransferProduct(sku, lotNo, des, wh, qty, loc, doNo, id, toWh, toLoc);

                }
                e.Result = "Success";
            }
            catch
            {
            }
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string sku = "";
        public int qty = 0;
        public string des = "";
        public string lotNo = "";
        public string loc = "";
        public string wh = "";
        public string doNo = "";
        public string toWh = "";
        public string toLoc = "";
        public Record(string _sku, string _lotNo, int _qty, string _loc, string _wh, string _des, string _doNo, string _toWh, string _toLoc)
        {
            sku = _sku;
            lotNo = _lotNo;
            qty = _qty;
            loc = _loc;
            wh = _wh;
            des = _des;
            doNo = _doNo;
            toWh = _toWh;
            toLoc = _toLoc;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 5000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ProductCode"], "txt_Product") as ASPxTextBox;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit qty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;

            ASPxLabel location = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Location"], "lbl_Location") as ASPxLabel;
            ASPxLabel des = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Description"], "txt_Description") as ASPxLabel;
            ASPxLabel wh = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["WareHouse"], "txt_WareHouse") as ASPxLabel;
            ASPxLabel doNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["DoNo"], "txt_DoNo") as ASPxLabel;
            ASPxComboBox fromWh = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ToWarehouseId"], "cmb_WareHouse") as ASPxComboBox;
            ASPxComboBox toLoc = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ToLocId"], "cmb_Location") as ASPxComboBox;
            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(sku.Text, lotNo.Text, SafeValue.SafeInt(qty.Value, 0), location.Text, wh.Text, des.Text, doNo.Text
                   , fromWh.Text, toLoc.Text));
            }
            else if (sku == null)
                break; ;
        }
    }
    protected int TransferProduct(string sku, string lotNo, string des, string wh, int qty, string loc, string doNo, int tId, string toWh, string toLoc)
    {
        int pId = 0;
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTransfer), "Id='" + tId + "'");
        WhTransfer transfer = C2.Manager.ORManager.GetObject(query) as WhTransfer;
        bool action = false;
        string transferNo = "";
        if (transfer == null)
        {
            action = true;
            transfer = new WhTransfer();
            transferNo = C2Setup.GetNextNo("Transfer");
        }
        string userId = HttpContext.Current.User.Identity.Name;
        transfer.TransferNo = transferNo;
        transfer.RequestPerson = userId;
        transfer.RequestDate = DateTime.Now;
        transfer.TransferDate = DateTime.Now;
        transfer.Pic = "";
        transfer.ConfirmPerson = "";
        transfer.ConfirmDate = DateTime.Now;
        transfer.StatusCode = "CLS";
        if (action)
        {
            transfer.TransferNo = transferNo;
            transfer.CreateBy = HttpContext.Current.User.Identity.Name;
            transfer.CreateDateTime = DateTime.Now;
            C2Setup.SetNextNo("Transfer", transferNo);
            Manager.ORManager.StartTracking(transfer, Wilson.ORMapper.InitialState.Inserted);
            Manager.ORManager.PersistChanges(transfer);
            pId = transfer.Id;
        }
        int id = 0;
        Wilson.ORMapper.OPathQuery query1 = new Wilson.ORMapper.OPathQuery(typeof(WhTransferDet), "Id='" + id + "'");
        WhTransferDet det = C2.Manager.ORManager.GetObject(query1) as WhTransferDet;
        if (det == null)
        {
            det = new WhTransferDet();
        }
        det.TransferNo = transferNo;
        det.Product = sku;
        det.LotNo = lotNo;
        det.Qty1 = qty;
        det.FromWarehouseId = wh;
        det.FromLocId = loc;
        det.ToWarehouseId = toWh;
        det.ToLocId = toLoc;
        det.Des1 = des;
        det.CreateBy = userId;
        det.CreateDateTime = DateTime.Now;
        det.UpdateBy = userId;
        det.UpdateDateTime = DateTime.Now;
        Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(det);
        id = det.Id;

        string sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus)";
        sql += string.Format(@"select DoNo, 'OUT' as DoType,Product
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,Price
,'{3}' as LotNo
, Uom1,Uom2,Uom3, Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{4}' as CreateBy,getdate() as CreateDateTime,'{4}' as UpdateBy,getdate() as UpdateDateTime
,Att1,Att2,Att3,Att4,Att5,Att6,'{5}' as Des1,'' as Packing,'{6}' as Location,'Delivered' as ProcessStatus
from wh_dodet2 det where DoType='IN' and DoNo='{0}' and Product='{1}' and LotNo='{3}' and Location='{6}' select @@identity", doNo, sku, qty, lotNo, EzshipHelper.GetUserName(), des, loc);
        int doOutId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);

        sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus)";
        sql += string.Format(@"select DoNo, 'IN' as DoType,Product
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 ,Price
,'{3}' as LotNo
, Uom1,Uom2,Uom3, Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{4}' as CreateBy,getdate() as CreateDateTime,'{4}' as UpdateBy,getdate() as UpdateDateTime
,Att1,Att2,Att3,Att4,Att5,Att6,'{5}' as Des1,'' as Packing,'{6}' as Location,'Delivered' as ProcessStatus
from wh_dodet2 det where DoType='IN' and DoNo='{0}' and Product='{1}' and LotNo='{3}' and Location='{7}' select @@identity", doNo, sku, qty, lotNo, EzshipHelper.GetUserName(), des, toLoc, wh);
        int doInId = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);


        sql = string.Format(@"update wh_TransferDet set DoInId='{1}',DoOutId='{2}' where Id={0}", id, doInId, doOutId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        return pId;
    }
    private bool VilaSetStock(string loc, int qty)
    {
        bool result = false;
        string sql = string.Format(@"select * from (select tab_hand.Qty1-isnull(tab_out.Qty1,0) as HandQty,tab_hand.Location
from (select product ,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,Location,max(mast.DoNo) DoNo,max(mast.DoDate) DoDate from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS'  where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo,Location) as tab_hand 
left join (select Product,LotNo,sum(Qty1) as Qty1,sum(Qty2) as Qty2,sum(Qty3) as Qty3,Location from  wh_dodet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='OUT' and len(det.DoNo)>0 group by Product,LotNo,Location) 
as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab_out.Location=tab_hand.Location) as tab where Location='{0}'", loc);
        int HandQty = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

        sql = string.Format(@"select MaxCount from  ref_location where Code='{0}' and Loclevel='Unit'", loc);
        int maxCount = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

        if (HandQty == maxCount)
        {
            result = true;
        }
        if ((HandQty + qty) - maxCount > 0)
        {
            result = true;
        }
        return result;
    }
    protected void FillLocationCombo(string wh)
    {
        if (string.IsNullOrEmpty(wh)) return;
        cmb_Location.Items.Clear();
        string sql = string.Format(@"select Code,WarehouseCode from ref_location");
        DataTable tab = ConnectSql.GetTab(sql);
        dsRefLocation.FilterExpression = string.Format("WarehouseCode = '{0}'", wh);
        cmb_Location.DataBind();
        //DataRow[] foundRows = tab.Select(string.Format("WarehouseCode = '{0}'", wh)); 
        //for (int i=0;i<foundRows.Length;i++)
        //    cmb_Location.Text = (string)foundRows[i]["Code"]; 
    }
    protected void cmbFromLoc_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillLocationCombo(e.Parameter);
    }
    protected void FillToLocationCombo(ASPxComboBox cmb, string wh)
    {
        if (string.IsNullOrEmpty(wh)) return;
        cmb.Items.Clear();
        string sql = string.Format(@"select Code,WarehouseCode from ref_location");
        DataTable tab = ConnectSql.GetTab(sql);
        dsRefLocation.FilterExpression = string.Format("WarehouseCode = '{0}'", wh);
        cmb.DataBind();
    }
    protected void cmbToFromLoc_OnCallback(object source, CallbackEventArgsBase e)
    {
        FillToLocationCombo(source as ASPxComboBox, e.Parameter);
    }
    protected void cmb_Location_Init(object sender, EventArgs e)
    {
        ASPxComboBox cmb_Location = sender as ASPxComboBox;
        GridViewDataItemTemplateContainer container = cmb_Location.NamingContainer as GridViewDataItemTemplateContainer;

        cmb_Location.ClientInstanceName = String.Format("cmb_Location{0}", container.VisibleIndex);

    }

    protected void cmb_WareHouse_Init(object sender, EventArgs e)
    {
        ASPxComboBox cmb_WareHouse = sender as ASPxComboBox;
        GridViewDataItemTemplateContainer container = cmb_WareHouse.NamingContainer as GridViewDataItemTemplateContainer;

        cmb_WareHouse.ClientInstanceName = String.Format("cmb_WareHouse{0}", container.VisibleIndex);
        cmb_WareHouse.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnSelectedIndexChanged(s, e, cmb_Location{0}); }}", container.VisibleIndex);
    }
}