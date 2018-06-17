using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class WareHouse_SelectPage_ProductList_do : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
            //if (SafeValue.SafeString(Request.QueryString["typ"]).ToLower() == "out")
            //    this.ASPxGridView1.Columns["Loc"].Visible = false;
            btn_Sch_Click(null, null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string doNo = SafeValue.SafeString(Request.QueryString["Sn"]);
        string doType = SafeValue.SafeString(Request.QueryString["typ"]);
        string sql = string.Format(@"select Id,ProductCode as Code,Price,Des1,Qty1,Qty2,Qty3
,Packing
,LotNo,'' as Loc,Att1,Att2,Att3,Att4,Att5,Att6 from wh_DoDet where DoNo='{0}' and DoType='{1}'  and Qty1>0", doNo, doType);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.ASPxGridView1.DataSource = tab;
        this.ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["Sn"] != null)
        {
            string doNo = Request.QueryString["Sn"].ToString();
            string type = Request.QueryString["typ"].ToString().ToLower();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string skuId = list[i].skuId;
                    int qty1 = list[i].qty1;
                    int qty2 = list[i].qty2;
                    int qty3 = list[i].qty3;
                    string loc = list[i].loc;
                    if(type=="in"){
                        InsertDoDet2(skuId, doNo, qty1, qty2, qty3, loc);//picklist
                    }
                    else
                        InsertDoDet2_out(skuId, doNo, qty1, qty2, qty3, loc);
                }
                UpdateDoDet2(doNo, "");
                e.Result = "";
            }
            catch { }
        }
        else
        {
            e.Result = "Please keyin select party ";
        }
    }
    private void UpdateDoDet2(string orderNo, string orderType)
    {
        string sql = string.Format(@"update wh_dodet2 set wh_dodet2.PartyId=mast.PartyId,wh_dodet2.DoDate=mast.DoDate from wh_do mast  where mast.DoNo=wh_dodet2.DoNo and mast.Dotype=wh_dodet2.DoType
and wh_dodet2.doNo='{0}'", orderNo);
        ConnectSql.ExecuteSql(sql);
    }
    private void InsertDoDet2(string soDetId, string doNo, int qty1, int qty2, int qty3,string loc)
    {
       string sql = @"Insert Into wh_DoDet2(DoNo, DoType,Product,Des1,Price,Packing,Qty1,Qty2,Qty3,Location,LotNo,BatchNo,CustomsLot,ProcessStatus,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime])";
        sql += string.Format(@"select DoNo, DoType,ProductCode,Des1,Price,Packing,'{2}','{3}','{4}','{5}',LotNo,BatchNo,CustomsLot,'',Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,'{1}',getdate(),'{1}',getdate() from Wh_DoDet where Id='{0}'"
            , soDetId, EzshipHelper.GetUserName(), qty1,qty2,qty3,loc);
        int cnt = C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void InsertDoDet2_out(string soDetId, string doNo, int qty1, int qty2, int qty3, string loc)
    {
        string sql = @"Insert Into wh_DoDet2(DoNo, DoType,Product,Des1,Price,Packing,Qty1,Qty2,Qty3,Location,LotNo,BatchNo,CustomsLot,ProcessStatus,Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],[PreQty1],[PreQty2],[PreQty3])";
        sql += string.Format(@"select DoNo, DoType,ProductCode,Des1,Price,Packing,'{2}','{3}','{4}','{5}',LotNo,BatchNo,CustomsLot,'',Uom1,Uom2,Uom3,Uom4,[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],Att1,Att2,Att3,Att4,Att5,Att6,Att7,Att8,Att9,Att10,'{1}',getdate(),'{1}',getdate(),'{2}','{3}','{4}' from Wh_DoDet where Id='{0}'"
            , soDetId, EzshipHelper.GetUserName(), qty1, qty2, qty3, loc);
        int cnt = C2.Manager.ORManager.ExecuteCommand(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string skuId = "";
        public int qty1 = 0;
        public int qty2 = 0;
        public int qty3 = 0;
        public decimal price = 0;
        public string loc = "";
        public Record(string _skuId, int _qty1, int _qty2, int _qty3,string _loc)
        {
            skuId = _skuId;
            qty1 = _qty1;
            qty2 = _qty2;
            qty3 = _qty3;
            loc = _loc;
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
            ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;
            ASPxButtonEdit loc = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Loc"], "txt_Loc") as ASPxButtonEdit;

            if (skuId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(skuId.Text, SafeValue.SafeInt(qty1.Value, 0), SafeValue.SafeInt(qty2.Value, 0), SafeValue.SafeInt(qty3.Value, 0),loc.Text
                    ));
            }
            else if (skuId == null)
                break;
        }
    }
    protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        ASPxButtonEdit loc = this.ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Loc"], "txt_Loc") as ASPxButtonEdit;
        if (loc != null)
        {
            loc.ClientInstanceName = loc.ClientInstanceName + e.VisibleIndex;
            loc.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupLo({0},null);", loc.ClientInstanceName) + "}";
            string location = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select top 1 Code from  ref_location where Loclevel='Unit'")));
            loc.Text = location;
        }
    }
}