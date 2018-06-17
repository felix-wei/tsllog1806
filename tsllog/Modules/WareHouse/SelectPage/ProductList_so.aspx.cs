using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_ProductList_so : System.Web.UI.Page
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
        string doType = SafeValue.SafeString(Request.QueryString["typ"]);//so or po
        string sql = string.Format(@"select * from(select Id,ProductCode as Code
,'1'+Uom1
	+case when len(uom2)>0 then 'x'+convert(nvarchar(10),QtyPackWhole)+Uom2 else '' end
	+case when len(uom3)>0 then 'x'+convert(nvarchar(10),QtyWholeLoose)+Uom3 else '' end
	+case when len(uom4)>0 then 'x'+convert(nvarchar(10),QtyLooseBase)+Uom4 else '' end
	as  Packing,Qty1-isnull((select sum(qty1+qty5) from wh_DoDet where ProductCode=wh_transDet.ProductCode and lotNo=wh_transDet.lotNo),0) as Qty1
,0 as Qty2,0 as Qty3,Price,LotNo,Des1 from wh_transDet where DoNo='{0}' and DoType='{1}') as tab where tab.Qty1>0", soNo,doType);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string doNo = Request.QueryString["Sn"].ToString();
            string doType = SafeValue.SafeString(Request.QueryString["typ"]);//so or po
            if (doType.ToLower() == "so")
                doType = "OUT";
            else
                doType = "IN";
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string skuId = list[i].skuId;
                    int qty1 = list[i].qty1;
                    int qty2 = list[i].qty2;
                    int qty3 = list[i].qty3;
                    InsertDoDet(skuId, doNo,doType,qty1,qty2,qty3);
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
    private void InsertDoDet(string soDetId, string doNo, string doType, int qty1, int qty2, int qty3)
    {
        string sql = @"Insert Into wh_DoDet(DoNo, DoType,ProductCode,Qty1,Qty2,Qty3,Qty4,Qty5,Des1,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],DoInId)";
        sql += string.Format(@"select '{1}', '{2}',ProductCode,0 as Qty1,0 as Qty2,0 as Qty3,'{4}' as Qty4,'{4}' as Qty5,Des1,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,'{3}',getdate(),'{3}',getdate(),'{0}' from Wh_TransDet where Id='{0}'", 
            soDetId, doNo,doType, EzshipHelper.GetUserName(), qty1,qty2,qty3);

        int cnt = C2.Manager.ORManager.ExecuteCommand(sql);
    }    
    //private void UpdatePoDetBalQty(int transId)
    //{
    //    string sql = string.Format(@"update Wh_TransDet set BalQty=Qty-isnull((select sum(Qty) from Wh_DoDet where DoInId={0}),0) where Id='{0}'", transId);
    //    C2.Manager.ORManager.ExecuteScalar(sql);
    //}
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string skuId = "";
        public int qty1 = 0;
        public int qty2 = 0;
        public int qty3 = 0;
        public decimal price = 0;
        string lotNo = "";
        public Record(string _skuId, int _qty1, int _qty2, int _qty3)
        {
            skuId = _skuId;
            qty1 = _qty1;
            qty2 = _qty2;
            qty3 = _qty3;
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
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;
            //ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            //ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;


            if (skuId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(skuId.Text, SafeValue.SafeInt(qty1.Value, 0), 0,0
                    ));
            }
            else if (skuId == null)
                break; ;
        }
    }
}