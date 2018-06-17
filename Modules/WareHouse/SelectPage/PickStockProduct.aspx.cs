using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class WareHouse_SelectPage_PickStockProduct : System.Web.UI.Page
{
    protected void Page_Init()
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            btn_Sch_Click(null, null);
            OnLoad();
        }
        else
        {
            OnLoad1();
        }
       
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select * from (SELECT distinct tab.Id,tab.ProductCode,tab_hand.LotNo,DoDate,tab_in.Location as LocationCode,Des1,Packing,QtyPackWhole,QtyWholeLoose,Price,Att1,Att2,Att3,Att4,Att5,Att6,Uom1,Uom2,Uom3,tab.Qty5 as Qty,tab_hand.HandQty-ISNULL(tab_out.ReservedQty,0) as AvaibleQty,tab_hand.HandQty,tab_out.ReservedQty,tab.Qty5 as Qty5,tab_Pick.PendingPickQty,tab.Remark,tab.PalletNo FROM 
 (select distinct Id,ProductCode,Des1,Packing,QtyPackWhole,QtyWholeLoose,Qty5,Price,Att1,Att2,Att3,Att4,Att5,Att6,Uom1,Uom2,Uom3,LocationCode,isnull(PalletNo,'') as PalletNo ,isnull(Remark,'') as Remark from wh_doDet where DoNo='{0}' and JobStatus='Pending' ) as tab
left join (select product,LotNo,max(mast.dodate) as DoDate,max(Location) as Location,sum(isnull(Case when det.DoType='IN' then Qty1 else -Qty1 end,0)) as HandQty,isnull(PalletNo,'') as PalletNo ,isnull(det.Remark,'') as Remark from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode='CLS' group by product,LotNo,det.PalletNo,det.Remark) as tab_hand on tab_hand.Product=tab.ProductCode and tab_hand.PalletNo=tab.PalletNo and tab_hand.Remark=tab.Remark
left join (select productCode as Product,LotNo,isnull(sum(Qty5),0) as ReservedQty,isnull(PalletNo,'') as PalletNo ,isnull(det.Remark,'') as Remark from wh_doDet det inner join wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo,PalletNo,det.Remark) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo and tab.PalletNo=tab_out.PalletNo and tab.Remark=tab_out.Remark
left join (select Location,Product,LotNo,DoType,PalletNo,Remark from Wh_DoDet2 ) as tab_in on tab_in.Product=tab.ProductCode and tab_in.LotNo=tab_hand.LotNo and DoType='IN'and tab_hand.Location=tab_in.Location and tab_hand.PalletNo=tab_out.PalletNo and tab_hand.Remark=tab_out.Remark
left join (select SUM(Qty1) as PendingPickQty,Product,LotNo,isnull(PalletNo,'') as PalletNo ,isnull(det.Remark,'') as Remark from Wh_DoDet2 det inner join wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode='USE' where det.DoType='OUT' and det.DoNo!='{0}'  group by Product,LotNo,PalletNo,det.Remark) as tab_Pick on tab_Pick.Product=tab_in.Product and tab_Pick.LotNo=tab_in.LotNo and tab_Pick.PalletNo=tab.PalletNo and tab_Pick.Remark=tab.Remark) 
as tab_stok where 1=1 and AvaibleQty>0 ", doNo);
        string where = "";
        if (name.Trim().Length > 0)
        {
            string value = name.Replace("'", "''");
            string filter=value.Replace(" ", "%") + "%";
            where = GetWhere(where, " where (Des1 Like '%" + filter + "' or tab.ProductCode like '%" + filter + "')");
            sql += where;
        }
        sql += " order by ProductCode,DoDate asc";
        //throw new Exception(sql);
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
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
        string type = SafeValue.SafeString(Request.QueryString["type"].ToString());
        string s = e.Parameters;
        bool action = false;
        decimal totalQty = 0;
        decimal value = 0;
        string product = "";
        int result = 0;
        if (Request.QueryString["Sn"] != null)
        {
            #region Vali
            if (s == "Vali")
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sku = list[i].sku;
                    string sql = string.Format(@"select count(*) from Wh_DoDet2 d1 inner join Wh_DoDet d on d.ProductCode=d1.Product and d.DoType=d1.DoType and d1.DoNo=d.DoNo where d1.DoNo='{0}' and d1.Product='{1}'", doNo, sku);
                    int cnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
                    if (cnt > 0)
                    {
                        e.Result = "Fail!Had Picked";
                        return;
                    }

                 }
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string sku = list[i].sku;
                        decimal balqty = list[i].avaibleQty;
                        decimal qty = list[i].qty;
                        decimal picking = list[i].pickQty;
                        if (qty <= 0)
                        {
                            e.Result = "No Qty";
                            return;
                        }
                        if (qty > balqty)
                        {
                            e.Result = "No Balance";
                            return;
                        }
                        if (balqty < (qty + picking))
                        {
                            e.Result = "Had Picking,No Enough Stock";
                            return;
                        }
                        //if(picking==balqty){
                        //    e.Result = "Had Picking,Can't Pick again";
                        //    return;
                        //}
                    
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        int id = list[i].id;
                        string sku = list[i].sku;
                        decimal qty = list[i].qty;
                        string lotNo = list[i].lotNo;
                        decimal balqty = list[i].avaibleQty;
                        string loc = list[i].loc;
                        decimal price = list[i].price;
                        decimal pendingpick = list[i].pickQty;
                        string remark = list[i].remark;
                        string palletNo = list[i].palletNo;
                        for (int j = 0; j < list.Count; j++)
                        {
                            string sku1 = list[j].sku;
                            decimal qty1 = list[j].qty;
                            if (sku1 == sku)
                            {
                                totalQty = qty1 + value;
                            }
                            value = qty1;
                        }
                        string sql_sum = string.Format(@"select sum(Qty1) as TotalQty from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, lotNo,remark,palletNo);
                        int total = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_sum), 0);
                        string sql = string.Format(@"select count(*) from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and LotNo='{2}' and Location='{3}' and Remark='{4}' and PalletNo='{5}'", doNo, sku, lotNo, loc,remark,palletNo);
                        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                        string sql_totalQty5 = string.Format(@"select sum(Qty5) as PickQty from Wh_DoDet where DoNo='{0}' and ProductCode='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku,remark,palletNo);
                        string sql_totalQty1 = string.Format(@"select sum(Qty1) as PickQty from Wh_DoDet where DoNo='{0}' and ProductCode='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku,remark,palletNo);
                        int total_qty5 = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_totalQty5), 0);
                        int total_qty1 = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_totalQty1), 0);
                        if (cnt > 0 && (total < total_qty1 || total < total_qty5))
                        {
                            sql = string.Format(@"update Wh_DoDet2 set Qty1=Qty1+{3} where DoNo='{0}' and Product='{1}' and LotNo='{2}' and Remark='{3}' and PalletNo='{4}'", doNo, sku, lotNo, qty, remark, palletNo);
                            C2.Manager.ORManager.ExecuteCommand(sql);
                            action = false;
                        }
                        else if (cnt > 0 && (total_qty1 == total || total_qty5 == total))
                        {
                            sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and LotNo='{2}' and Remark='{3}' and PalletNo='{4}'", doNo, sku, lotNo, remark, palletNo);
                            C2.Manager.ORManager.ExecuteCommand(sql);
                            action = true;
                        }
                        else if (cnt == 0)
                        {
                            if ((total_qty1 == total || total_qty5 == total) && totalQty < total)
                            {
                                sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Qty1={2} and Remark='{3}' and PalletNo='{4}'", doNo, sku, qty, remark, palletNo);
                                C2.Manager.ORManager.ExecuteCommand(sql);
                            }
                            if ((total_qty1 == total || total_qty5 == total) && totalQty >= total)
                            {
                                sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, remark, palletNo);
                                C2.Manager.ORManager.ExecuteCommand(sql);
                            }
                            action = true;
                        }
                        if (action)
                        {
                            TransferToDo(id,doNo, sku, lotNo, qty, loc, price);
                        }
                        sql = string.Format(@"select JobStatus from Wh_DoDet where Id={0}", id);
                        string jobstatus = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                        if (jobstatus == "Pending")
                        {
                            sql = string.Format(@"update Wh_DoDet set Qty1=Qty5,Qty5=0,JobStatus='Picked' where Id={0}", id);
                            result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                        }
                    }
                    EzshipLog.Log(doNo, "", "OUT", "Picked");
                    e.Result = "Success";
                }
                catch { }


            }
            #endregion

            #region OK
            if (s == "OK")
            {
                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string sku = list[i].sku;
                        decimal balqty = list[i].avaibleQty;
                        decimal qty = list[i].qty;
                        decimal picking = list[i].pickQty;
                        if (qty <= 0)
                        {
                            e.Result = "No Qty";
                            return;
                        }
                        if (qty > balqty)
                        {
                            e.Result = "No Balance";
                            return;
                        }
                        if (balqty < (qty + picking))
                        {
                            e.Result = "Had Picking,No Enough Stock";
                            return;
                        }
                        //if (picking >balqty)
                        //{
                        //    e.Result = "Had Picking,Can't Pick again";
                        //    return;
                        //}
                    }

                    for (int i = 0; i < list.Count; i++)
                    {

                        int id = list[i].id;
                        string sku = list[i].sku;
                        decimal qty = list[i].qty;
                        string lotNo = list[i].lotNo;
                        decimal balqty = list[i].avaibleQty;
                        string loc = list[i].loc;
                        decimal price = list[i].price;
                        string remark = list[i].remark;
                        string palletNo = list[i].palletNo;
                        for (int j = 0; j < list.Count;j++)
                        {
                            string sku1 = list[j].sku;
                            decimal qty1 = list[j].qty;
                            if (sku1 == sku)
                            {
                                totalQty = qty1 + value;
                            }
                            value = qty1;
                        }
                        string sql_sum = string.Format(@"select sum(Qty1) as TotalQty from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku,remark,palletNo);
                        int total = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_sum), 0);
                        string sql = string.Format(@"select count(*) from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and LotNo='{2}' and Location='{3}' and Remark='{4}' and PalletNo='{5}'", doNo, sku, lotNo, loc, remark, palletNo);
                        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

                        string sql_totalQty5 = string.Format(@"select sum(Qty5) as PickQty from Wh_DoDet where DoNo='{0}' and ProductCode='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, remark, palletNo);
                        string sql_totalQty1 = string.Format(@"select sum(Qty1) as PickQty from Wh_DoDet where DoNo='{0}' and ProductCode='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, remark, palletNo);
                        int total_qty5 = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_totalQty5), 0);
                        int total_qty1 = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql_totalQty1), 0);
                        if (cnt > 0 && (total < total_qty1 || total < total_qty5))
                        {
                            sql = string.Format(@"update Wh_DoDet2 set Qty1=Qty1+{3} where DoNo='{0}' and Product='{1}' and LotNo='{2}' and Remark='{3}' and PalletNo='{4}'", doNo, sku, lotNo, qty, remark, palletNo);
                            C2.Manager.ORManager.ExecuteCommand(sql);
                            action = false;
                        }
                        else if (cnt > 0 && (total_qty1 == total || total_qty5 == total))
                        {
                            sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, remark, palletNo);
                            C2.Manager.ORManager.ExecuteCommand(sql);
                            action = true;
                        }
                        else if (cnt == 0)
                        {
                            if ((total_qty1 == total || total_qty5 == total) && totalQty < total)
                            {
                                sql = string.Format(@"select count(*) from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Qty1={2} and Remark='{3}' and PalletNo='{4}'", doNo, sku, qty, remark, palletNo);
                                cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                                if (cnt > 0)
                                {
                                    sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Qty1={2} and Remark='{3}' and PalletNo='{4}'", doNo, sku, qty, remark, palletNo);
                                    C2.Manager.ORManager.ExecuteCommand(sql);
                                }
                            }
                            if ((total_qty1 == total || total_qty5 == total) && totalQty >= total)
                            {
                                sql = string.Format(@"delete from Wh_DoDet2 where DoNo='{0}' and Product='{1}' and Remark='{2}' and PalletNo='{3}'", doNo, sku, remark, palletNo);
                                C2.Manager.ORManager.ExecuteCommand(sql);
                            }
                            action = true;
                        }
                        if (action)
                        {
                            TransferToDo(id,doNo, sku, lotNo, qty, loc, price);
                        }
                        sql = string.Format(@"select JobStatus from Wh_DoDet where Id={0}", id);
                        string jobstatus = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                        if (jobstatus == "Pending")
                        {
                            sql = string.Format(@"update Wh_DoDet set Qty1=Qty5+Qty1,JobStatus='Picked' where Id={0}", id);
                            result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                            sql = string.Format(@"update Wh_DoDet set Qty5=0,JobStatus='Picked' where Id={0}", id);
                            result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
                        }
                        product = sku;
                    }

                    e.Result = "Success";
                    EzshipLog.Log(doNo, "", "OUT", "Picked");
                }
                catch { }
            }
            #endregion
        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    private void TransferToDo(int id,string doNo, string sku, string lotNo, decimal qty, string location, decimal price)
    {
        string sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus,PalletNo,ContainerNo,Remark)";
        sql += string.Format(@"select '{1}'as DoNo, 'OUT' as DoType,'{2}' as Sku
 ,'{3}' as Qty1
 ,Qty2
 ,Qty3
 ,'{4}' as Price
,'{5}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{6}' as CreateBy,getdate() as CreateDateTime,'{6}' as UpdateBy,getdate() as UpdateDateTime
,Att1,Att2,Att3,Att4,Att5,Att6,Des1,'' as Packing,'{7}' as Location,'Delivered' as ProcessStatus,PalletNo,ContainerNo,Remark
from Wh_DoDet where Id={0}", id,doNo, sku, qty, price, lotNo, EzshipHelper.GetUserName(), location);
       
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public string sku = "";
        public decimal qty = 0;
        public decimal avaibleQty = 0;
        public string lotNo = "";
        public string loc = "";
        public decimal price = 0;
        public decimal pickQty = 0;
        public string remark = "";
        public string palletNo = "";
        public Record(int _id, string _sku, string _lotNo, decimal _qty, decimal _avaibleQty, string _loc, decimal _price, decimal _pickQty,string _remark,string _palletNo)
        {
            id = _id;
            sku = _sku;
            lotNo = _lotNo;
            qty = _qty;
            avaibleQty = _avaibleQty;
            loc = _loc;
            price = _price;
            pickQty = _pickQty;
            remark = _remark;
            palletNo = _palletNo;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 5000;
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
        decimal qty5 = 0;
        decimal totalQty = 0;
        string produt = "";
        decimal value = 0;
        decimal value1 = 0;
        decimal bQty = 0;
        decimal qty = 0;
        decimal pendingPickQty = 0;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ProductCode"], "txt_Product") as ASPxTextBox;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit spin_Qty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxLabel spin_Picked = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty5"], "spin_Picked") as ASPxLabel;
            ASPxLabel balQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["AvaibleQty"], "spin_AvaibleQty") as ASPxLabel;
            ASPxLabel location = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Location"], "lbl_Location") as ASPxLabel;
            ASPxLabel price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxLabel;
            ASPxLabel spin_PengingPickQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PengingPickQty"], "spin_PengingPickQty") as ASPxLabel;
            ASPxLabel txt_Remark = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Remark"], "txt_Remark") as ASPxLabel;
            ASPxLabel txt_PalletNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PalletNo"], "txt_PalletNo") as ASPxLabel;

            if (sku != null)
            {
                string sql = string.Format(@"select sum(Qty5) as Qty5 from wh_dodet where ProductCode='{0}' and DoNo='{1}' and JobStatus='Pending' and Remark='{2}' and PalletNo='{3}'", SafeValue.SafeString(sku.Text), doNo,SafeValue.SafeString(txt_Remark.Text),SafeValue.SafeString(txt_PalletNo.Text));
                qty5 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
                sql = string.Format(" select sum(Qty1) as Qty from wh_dodet2 det inner join Wh_DO mast on det.DoNo=mast.DoNo  where Product='{0}' and det.DoType='IN' and det.Remark='{1}'and PalletNo='{2}'", SafeValue.SafeString(sku.Text), SafeValue.SafeString(txt_Remark.Text), SafeValue.SafeString(txt_PalletNo.Text));
                totalQty = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
                decimal picking = SafeValue.SafeDecimal(spin_Qty.Text);
                bQty = SafeValue.SafeInt(balQty.Text, 0);
                sql = string.Format(@"select Qty5 from wh_dodet where Id={0}",SafeValue.SafeInt(id.Text,0));
                decimal qty5_n = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
                sql = string.Format(@"select Count(*) from wh_dodet2 where Product='{0}' and LotNo='{1}' and DoNo='{2}' and Remark='{3}' and PalletNo='{4}'", SafeValue.SafeString(sku.Text), SafeValue.SafeString(lotNo.Text), doNo, SafeValue.SafeString(txt_Remark.Text), SafeValue.SafeString(txt_PalletNo.Text));
                decimal cnt = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql), 0);
                sql = string.Format(@"select Qty1 from wh_dodet2 where Product='{0}' and LotNo='{1}' and DoNo='{2}' and Remark='{3}' and PalletNo='{4}'", SafeValue.SafeString(sku.Text), SafeValue.SafeString(lotNo.Text), doNo, SafeValue.SafeString(txt_Remark.Text), SafeValue.SafeString(txt_PalletNo.Text));
                decimal qty1 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql));
                if (cnt == 0 && qty1 < bQty)
                {
                    if (totalQty >= qty5)
                    {
                        if (sku.Text != produt)
                        {
                            value = 0;
                            value1 = 0;
                            qty = 0;
                            if (bQty < qty5)
                            {
                                value = bQty;
                            }
                            else
                            {
                                value = qty5;
                            }
                            isPay.Checked = true;
                            spin_Picked.Text = value.ToString();
                        }
                        if (sku.Text == produt && qty5 > qty)
                        {
                            if ((bQty + qty) < qty5)
                            {
                                value = bQty;
                            }
                            else
                            {
                                value = qty5 - value1;
                            }

                            isPay.Checked = true;
                            spin_Picked.Text = value.ToString();

                        }
                        //if (qty5 == qty)
                        //{
                        //    spin_Picked.Text = "";
                        //    isPay.Checked = false;
                        //}
                        if (pendingPickQty == SafeValue.SafeInt(spin_PengingPickQty.Text, 0))
                        {
                            spin_PengingPickQty.Text = "";
                        }
                    }
                    pendingPickQty += SafeValue.SafeInt(spin_PengingPickQty.Text, 0);
                }
            }
            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text,0),sku.Text, lotNo.Text, SafeValue.SafeInt(spin_Qty.Value, 0), SafeValue.SafeInt(balQty.Text, 0), location.Text, SafeValue.SafeDecimal(price.Text, 0)
                    , SafeValue.SafeInt(spin_PengingPickQty.Text, 0), txt_Remark.Text, txt_PalletNo.Text));
               produt = sku.Text;
                spin_Qty.Text = value.ToString();

                value = qty5 - value - value1;
                value1 += bQty;
                qty += SafeValue.SafeInt(spin_Qty.Text, 0);
            }
            else if (sku == null)
                break; ;
        }
    }
     private void OnLoad1()
    {
        int start = 0;
        int end = 5000;
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());

        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ProductCode"], "txt_Product") as ASPxTextBox;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit spin_Qty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxLabel spin_Picked = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty5"], "spin_Picked") as ASPxLabel;
            ASPxLabel balQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["AvaibleQty"], "spin_AvaibleQty") as ASPxLabel;
            ASPxLabel location = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Location"], "lbl_Location") as ASPxLabel;
            ASPxLabel price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxLabel;
            ASPxLabel spin_PengingPickQty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PengingPickQty"], "spin_PengingPickQty") as ASPxLabel;
            ASPxLabel txt_Remark = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Remark"], "txt_Remark") as ASPxLabel;
            ASPxLabel txt_PalletNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PalletNo"], "txt_PalletNo") as ASPxLabel;
            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), sku.Text, lotNo.Text, SafeValue.SafeDecimal(spin_Qty.Value, 0), SafeValue.SafeDecimal(balQty.Text, 0), location.Text, SafeValue.SafeDecimal(price.Text, 0)
                     , SafeValue.SafeInt(spin_PengingPickQty.Text, 0), txt_Remark.Text,txt_PalletNo.Text));
            }
            else if (sku == null)
                break; ;
        }
    }
}