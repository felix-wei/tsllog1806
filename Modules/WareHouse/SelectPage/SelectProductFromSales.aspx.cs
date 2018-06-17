using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_SelectProductFromSales : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            btn_Sch_Click(null, null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string soNo = SafeValue.SafeString(Request.QueryString["so"]);
        string sql = string.Format("select Id,ProductCode as Code,Packing,BalQty as Qty,Price,LotNo,Unit,Des1 from wh_transDet where DoNo='{0}' and DoType='SO'", soNo);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string doNo = Request.QueryString["Sn"].ToString();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string skuId = list[i].skuId;
                    int qty = list[i].qty;
                    InsertDoDet(skuId, doNo, qty);
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
    private void InsertDoDet(string soDetId,string doNo,int qty)
    {
        string sql = @"Insert Into wh_DoDet(DoNo, DoType,ProductCode,Packing,Qty,Des1,LotNo,Unit,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
        sql += string.Format(@"select '{1}', 'OUT',ProductCode,Packing,'{3}',Des1,LotNo,Unit,'{2}',getdate(),'{2}',getdate(),'{0}' from Wh_TransDet where Id='{0}'", soDetId, doNo, EzshipHelper.GetUserName(),qty);

        int cnt = C2.Manager.ORManager.ExecuteCommand(sql);
        if (cnt > 0)
            UpdatePoDetBalQty(SafeValue.SafeInt(soDetId,0));
    }    
    private void UpdatePoDetBalQty(int transId)
    {
        string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId={0}),0) where Id='{0}'", transId);
        C2.Manager.ORManager.ExecuteScalar(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string skuId = "";
        public int qty = 0;
        public decimal price = 0;
        string lotNo = "";
        public Record(string _skuId, int _qty)
        {
            skuId = _skuId;
            qty = _qty;
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
            ASPxTextBox skuId = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit qty = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;


            if (skuId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(skuId.Text, SafeValue.SafeInt(qty.Value, 0)
                    ));
            }
            else if (skuId == null)
                break; ;
        }
    }
}