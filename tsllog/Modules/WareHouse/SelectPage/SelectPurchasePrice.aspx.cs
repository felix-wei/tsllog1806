using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SelectProductPrice : System.Web.UI.Page
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
        string sql = string.Format(@"select ref.PartyId,ref.Code as Product,ref.Description from ref_product ref ");
        if (name.Length > 0)
        {
            sql += string.Format(" where Name like '%{0}%' ", name.Replace("'", ""));
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s=="OK")
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string partyId = list[i].partyId;
                    string sku = list[i].sku;
                    string des = list[i].des;
                    decimal price1 = list[i].price1;
                    DateTime fromDate = list[i].fromDate;
                    TransferToDo(partyId, sku, des, price1, fromDate);
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
    private void TransferToDo(string partyId, string sku, string des, decimal price1, DateTime fromDate)
    {
        string sql = @"Insert Into Ref_ProductPrice(PartyId, Product,Description,Price1,Price2,StatusCode,FromDate,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime])";
        sql += string.Format(@"select '{0}'as PartyId, '{1}' as Product,'{2}' as Description
 ,'{3}' as Price1
 ,0 as Price2,'Use' as StatusCode
 ,'{4}' as FromDate
,'{5}' as CreateBy,getdate() as CreateDateTime,'{5}' as UpdateBy,getdate() as UpdateDateTime
from ref_product where Code='{1}'", partyId, sku, des, price1, fromDate, EzshipHelper.GetUserName());
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string partyId = "";
        public string sku = "";
        public string des = "";
        public decimal price1 = 0;
        public DateTime fromDate;
        public Record(string _partyId, string _sku, string _des, decimal _price1,DateTime _fromDate)
        {
            partyId = _partyId;
            sku = _sku;
            des = _des;
            price1 = _price1;
            fromDate = _fromDate;
        }

    }
    public decimal totPayAmt = 0;
    private void OnLoad()
    {
        int start = 0;
        int end = 200;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox partyId = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["PartyId"], "txt_PartyId") as ASPxTextBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Product"], "txt_Product") as ASPxTextBox;
            ASPxTextBox des = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Description"], "txt_Description") as ASPxTextBox;
            ASPxSpinEdit price1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Price1"], "spin_Price1") as ASPxSpinEdit;
            ASPxDateEdit fromDate = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["FromDate"], "txt_FromDate") as ASPxDateEdit;

            if (partyId != null && sku != null && des != null && price1 != null && fromDate != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(partyId.Text, sku.Text, des.Text, SafeValue.SafeDecimal(price1.Value, 0), fromDate.Date
                    ));
            }
            else if (sku == null)
                break; ;
        }
    }
}