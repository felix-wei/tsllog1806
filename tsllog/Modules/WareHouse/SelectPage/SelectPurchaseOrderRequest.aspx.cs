using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;

public partial class WareHouse_SelectPage_SelectPurchaseOrderRequest : System.Web.UI.Page
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
        string sql = string.Format(@"select Id,Product,Des1,LotNo,Qty1,Qty2,Qty3,Price,
Packing,Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10 from  Wh_PORequest pr where isnull(PoNo, '')= ''");
        string where="";
        if (txt_LotNo.Text.Length > 0)
        {
            where = GetWhere(where, string.Format(" LotNo like '%{0}%'", txt_LotNo.Text));
        }
        else if (name.Length > 0)
        {
            where = GetWhere(where, " and  Des1 Like '%" + name.Replace("'", "") + "%'");
        }
        sql += where + " ORDER BY Product ";
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
            string poNo = Request.QueryString["Sn"].ToString();
            
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string poRequestId = list[i].sku;
                    int qty1 = list[i].qty1;
                    int qty2 = list[i].qty2;
                    int qty3 = list[i].qty3;
                    decimal price = list[i].price;
                    string lotNo = list[i].lotNo;
                    decimal amt = SafeValue.SafeDecimal(qty1*price);
                    TransferToPo(poNo, qty1, qty2, qty3, price, lotNo, poRequestId,amt);
                }
                e.Result = "";
            }
            catch { }

        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    private void TransferToPo(string poNo, int qty1, int qty2, int qty3, decimal price, string lotNo, string poRequestId,decimal amt)
    {
        string sql = @"Insert Into wh_TransDet(DoNo, DoType,ProductCode,Price,Packing,Qty1,Qty2,Qty3,Qty4,Qty5,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId,DocAmt)";
        sql += string.Format(@" select '{1}' as DoNo, 'Po' as DoType,Product,'{2}' as Price,Packing
 ,'{3}' as Qty1
 ,'{4}' as Qty2
,'{5}' as Qty3
 ,0 as Qty4,0 as Qty5,'{6}' as LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,Des1,'{7}' as CreateBy,getdate() as CreateDateTime,'{7}'  as UpdateBy,getdate() as UpdateDateTime,Id as DoInId,'{8}' from Wh_PORequest where Id='{0}'
 ", poRequestId,poNo,price,qty1,qty2,qty3,lotNo, EzshipHelper.GetUserName(),amt);
        if (ConnectSql.ExecuteSql(sql) > 0)
        {
            sql = string.Format("Update wh_poRequest set PoNo='{1}' where Id='{0}'",poRequestId,poNo);
            ConnectSql.ExecuteSql(sql);
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string sku = "";
        public int qty1 = 0;
        public int qty2 = 0;
        public int qty3 = 0;
        public decimal price = 0;
        public string lotNo = "";
        public Record(string _sku, int _qty1, int _qty2, int _qty3, decimal _price, string _lotNo)
        {
            sku = _sku;
            qty1 = _qty1;
            qty2 = _qty2;
            qty3 = _qty3;
            price = _price;
            lotNo = _lotNo;
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
            ASPxTextBox skuId = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit price = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;
            //ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            //ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;

            if (skuId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(skuId.Text, SafeValue.SafeInt(qty1.Value, 0), 0, 0, SafeValue.SafeDecimal(price.Value, 0), ""
                    ));
            }
            else if (skuId == null)
                break; ;
        }
    }
}