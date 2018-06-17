using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class WareHouse_SelectPage_CreateSalesOrder : System.Web.UI.Page
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
            
        }
        btn_Sch_Click(null, null);
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"select * from Wh_TransDet det inner join Wh_Trans mast on det.DoNo=mast.DoNo where mast.DoNo='{0}' and mast.DoType='BS' and ISNULL(LotNo,'')!=''", doNo);
        string where = "";
        if (name.Trim().Length > 0)
        {
            string value = name.Replace("'", "''");
            string filter = value.Replace(" ", "%") + "%";
            where = GetWhere(where, " where (Des1 Like '%" + filter + "' or tab.ProductCode like '%" + filter + "')");
            sql += where;
        }
        sql += " order by ProductCode";
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
        string whId = SafeValue.SafeString(Request.QueryString["WhId"].ToString());
        string partyId = SafeValue.SafeString(Request.QueryString["partyId"].ToString());
        string s = e.Parameters;
        if(s=="Save"){

            #region Create SO
            string sql = string.Format(@"select Count(*) from Wh_TransDet where DoNo='{0}' and DoType='BS' and ISNULL(LotNo,'')!=''", doNo);
            int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (cnt > 0)
            {
                bool action = false;
                for (int i = 0; i < list.Count; i++)
                {
                    int totalQty = list[i].pickQty;
                    int qty = list[i].qty;
                    if (totalQty < qty)
                    {
                        action = false;
                        e.Result = "Fail! Pls enter again";
                        return;
                    }
                    else
                    {
                        action = true;
                    }
                }
                if (action)
                {
                    string pId = "";
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhTrans), "Id='" + pId + "'");
                    WhTrans whTrans = C2.Manager.ORManager.GetObject(query) as WhTrans;

                    whTrans = new WhTrans();
                    string poNo = C2Setup.GetNextNo("", "SalesOrders", DateTime.Now);
                    whTrans.DoNo = poNo;
                    whTrans.DoType = "SO";
                    whTrans.PartyId = partyId;
                    whTrans.PartyName = SafeValue.SafeString(partyId);
                    whTrans.CreateBy = EzshipHelper.GetUserName();
                    whTrans.CreateDateTime = DateTime.Now;
                    whTrans.DoDate = DateTime.Now;
                    whTrans.ExpectedDate = DateTime.Today.AddDays(14);
                    whTrans.Currency = "SGD";
                    whTrans.DoStatus = "Draft";
                    whTrans.ExRate = SafeValue.SafeDecimal(1.000000);
                    whTrans.WareHouseId = whId;
                    Manager.ORManager.StartTracking(whTrans, Wilson.ORMapper.InitialState.Inserted);
                    Manager.ORManager.PersistChanges(whTrans);
                    C2Setup.SetNextNo("", "SalesOrders", poNo, DateTime.Now);


                    for (int i = 0; i <list.Count; i++)
                    {
                        string sku = list[i].sku;
                        string lotNo = list[i].lotNo;
                        int qty = list[i].qty;
                        decimal price = list[i].price;
                        string location = list[i].loc;
                        sql = @"Insert Into Wh_TransDet(DoNo,ProductCode,DoType,Qty1,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,LocationCode)";
                        sql += string.Format(@"select '{0}'as DoNo, '{1}' as Sku,'SO','{2}' as Qty1,'{3}','{5}',p.UomPacking,p.UomWhole,p.UomLoose,p.UomBase,p.QtyPackingWhole,p.QtyWholeLoose,p.QtyLooseBase,'{4}' as CreateBy,getdate() as CreateDateTime,
'{4}' as UpdateBy,getdate() as UpdateDateTime
,P.att4,P.att5,P.att6,P.att7,P.att8,P.att9,p.Description,p.Att1,'{6}'
from (select '{1}' as Sku) as tab
left join ref_product p on tab.Sku=p.Code", poNo, sku, qty, price, EzshipHelper.GetUserName(), lotNo,location);
                        ConnectSql.ExecuteSql(sql);

                    }
                    e.Result = "Success! " +"Sales Order No is"+ whTrans.DoNo;
                }
            }
            else
            {
                e.Result = "Fail";
            }
            #endregion
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public string sku = "";
        public int qty = 0;
        public int avaibleQty = 0;
        public string lotNo = "";
        public string loc = "";
        public decimal price = 0;
        public int pickQty = 0;
        public Record(int _id, string _sku, string _lotNo, int _qty, int _avaibleQty, string _loc, decimal _price, int _pickQty)
        {
            id = _id;
            sku = _sku;
            lotNo = _lotNo;
            qty = _qty;
            avaibleQty = _avaibleQty;
            loc = _loc;
            price = _price;
            pickQty = _pickQty;
        }

    }
    private void OnLoad()
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
            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), sku.Text, lotNo.Text, SafeValue.SafeInt(spin_Qty.Value, 0), 0, location.Text, SafeValue.SafeDecimal(price.Text, 0)
                     , SafeValue.SafeInt(spin_Picked.Value,0)));
            }
            else if (sku == null)
                break; ;
        }
    }
}