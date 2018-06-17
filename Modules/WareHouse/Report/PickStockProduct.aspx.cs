using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_PickStockProduct : System.Web.UI.Page
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
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
        string name = this.txt_Name.Text.Trim().ToUpper();
        string sql = string.Format(@"
select det.Id, productCode,tab_hand.LotNo,Des1,Qty1 as Qty,det.Price,det.QtyPackWhole,det.QtyWholeLoose,det.Uom1,det.Uom2,det.Uom3,Att1,Att2,Att3,Att4,Att5,Att6,Qty5,tab_hand.HandQty-isnull(tab_out.ReservedQty,0) as AvaibleQty from Wh_DoDet det 
left join (select product,LotNo,sum(isnull(Case when det.DoType='In' then Qty1 else -Qty1 end,0)) as HandQty from wh_dodet2 det inner join  wh_do mast on det.DoNo=mast.DoNo and mast.StatusCode!='CNL' group by product,LotNo) as tab_hand on tab_hand.Product=det.ProductCode 
left join (select productCode as Product,LotNo,isnull(sum(Qty5),0) as ReservedQty from wh_doDet det inner join wh_do mast on det.DoNo=mast.DoNo and (mast.StatusCode='CLS' or mast.StatusCode='RETURN') where det.DoType='Out' group by productCode,LotNo) as tab_out on tab_out.Product=tab_hand.Product and tab_out.LotNo=tab_hand.LotNo  
where DoNo='{0}'", doNo);
        if (name.Length > 0)
        {
            sql += string.Format(" and ProductCode like '%{0}%' ", name.Replace("'", ""));
        }
        sql += " ";
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string doNo = SafeValue.SafeString(Request.QueryString["Sn"].ToString());
            string type = SafeValue.SafeString(Request.QueryString["type"].ToString());
            string whId = SafeValue.SafeString(Request.QueryString["WhId"].ToString());
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = SafeValue.SafeInt(list[i].id,0);
                    string sku = list[i].sku;
                    int qty1 = list[i].qty1;
                    string lotNo = list[i].lotNo;
                    TransferToDo(doNo, sku, lotNo, qty1, whId,id);

                    string sql = string.Format(@"update Wh_DoDet set Qty1=Qty5,Qty5=0,JobStatus='Picked' where DoNo='{0}' and Qty5<>0", doNo);
                    ConnectSql.ExecuteSql(sql);
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
    private void TransferToDo(string doNo, string sku, string lotNo, int qty, string wh,int id)
    {
        string sql = @"Insert Into Wh_DoDet2(DoNo,DoType,Product,Qty1,Qty2,Qty3,Price,LotNo,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],Att1,Att2,Att3,Att4,Att5,Att6,Des1,Packing,Location,ProcessStatus)";
        sql += string.Format(@"select '{0}'as DoNo, 'OUT' as DoType,'{1}' as Sku
 ,'{2}' as Qty1
 ,0 as Qty2
 ,0 as Qty3
 , Price
,'{3}' as LotNo
,Uom1,Uom2,Uom3,Uom4
,QtyPackWhole,QtyWholeLoose,QtyLooseBase
,'{4}' as CreateBy,getdate() as CreateDateTime,'{4}' as UpdateBy,getdate() as UpdateDateTime
,att1,att2,att3,att4,att5,att6,Des1,Packing,'{5}' as Location,'Delivered' as ProcessStatus
from wh_dodet where Id='{6}'", doNo, sku, qty, lotNo, EzshipHelper.GetUserName(), wh,id);
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string id = "";
        public string sku = "";
        public int qty1 = 0;

        public string lotNo = "";
        public Record(string _id,string _sku, string _lotNo, int _qty1)
        {
            id = _id;
            sku = _sku;
            lotNo = _lotNo;
            qty1 = _qty1;

        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxTextBox sku = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Code"], "txt_Product") as ASPxTextBox;
            ASPxTextBox lotNo = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["LotNo"], "txt_LotNo") as ASPxTextBox;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty") as ASPxSpinEdit;
            //ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            //ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;

            if (sku != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(id.Text,sku.Text, lotNo.Text, SafeValue.SafeInt(qty1.Value, 0)
                    ));
            }
            else if (sku == null)
                break; ;
        }
    }
}