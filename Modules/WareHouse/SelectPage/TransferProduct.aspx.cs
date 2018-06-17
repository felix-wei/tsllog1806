using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class Modules_WareHouse_SelectPage_TransferProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_Name.Focus();
            //btn_Sch_Click(null,null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string name = this.txt_Name.Text.Trim().ToUpper();
        string lotNo = this.txt_LotNo.Text.Trim().ToUpper();
        string partyId = SafeValue.SafeString(Request.QueryString["PartyId"]);
        string where="";
        string sql = @"select tab.Id,Product,LotNo,p.Name,p.att1 as Packing,Qty1,Qty2,Qty3,Price
 ,FromWh, Location as FromLoc,FromWh as ToWh,'' as ToLoc,p.PartyId
 from ( 
   select tab_in.Id,tab_in.Product,tab_in.FromWh,tab_in.LotNo,tab_in.Qty1-isnull(tab_out.Qty1,0) as Qty1,tab_in.Qty2-isnull(tab_out.Qty2,0) as Qty2,tab_in.Qty3-isnull(tab_out.Qty3,0) as Qty3,Price,tab_in.Location from 
  ( select det.Id,Product,LotNo,sum(Qty1) Qty1,sum(Qty2) Qty2,sum(Qty3) Qty3,Location,max(price) price,max(mast.WareHouseId) as FromWh from wh_dodet2 det inner join Wh_DO mast on mast.DoNo=det.DoNo where det.DoType='In' group by det.Id, Product,LotNo,Location) as tab_in
  left join (select Product,LotNo,sum(Qty1) Qty1,sum(Qty2) Qty2,sum(Qty3) Qty3,Location from wh_dodet2 where DoType='Out' group by Product,LotNo,Location) as tab_out on tab_in.Product=tab_out.Product and tab_in.LotNo=tab_out.LotNo and tab_in.Location=tab_out.Location
 ) as tab
  left join ref_product p on tab.Product=p.code
  where Qty1+Qty2+Qty3>0  ";
        if (name.Length > 0)
        {
            sql += string.Format("  and p.Name Like '{0}%'", name);
        }
        if (partyId.Length>0)
        {
            sql += string.Format("  and p.PartyId Like '{0}%'", partyId);
        }
        if (lotNo.Length > 0)
        {
            sql += string.Format("  and LotNo Like '{0}%'", lotNo);
        }
        sql += " ORDER BY p.Name ";

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
        if (Request.QueryString["no"] != null)
        {
            string soNo = Request.QueryString["no"].ToString();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string doDetId = list[i].doDetId;
                    int qty1 = list[i].qty1;
                    int qty2 = list[i].qty2;
                    int qty3 = list[i].qty3;
                    string toLoc = list[i].toLoc;
                    string fromWh=list[i].fromWh;
                    string toWh=list[i].toWh;
                    Transfer(soNo, doDetId, qty1, qty2, qty3,toLoc,fromWh,toWh);
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
    private void Transfer(string no,string doDetId, int qty1, int qty2, int qty3,string toLoc,string fromWh,string toWh)
    {
        string sql = @"Insert Into [wh_TransferDet]([TransferNo],[FromLocId],[ToLocId],[Product],[Des1],[LotNo],[Qty1],[Qty2],[Qty3],[Uom1],[Uom2],[Uom3],[Uom4],[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[Price],[Packing],Att1,Att2,Att3,Att4,Att5,Att6,[CreateBy],[CreateDateTime],[UpdateBy],[UpdateDateTime],[Weight],[FromWarehouseId],[ToWarehouseId])";
        sql += string.Format(@"
  select '{4}',Location,'{5}' as ToLoc,[Product],[Des1],[LotNo],'{1}' as [Qty1],'{2}' [Qty2],'{3}' [Qty3],[Uom1],[Uom2],[Uom3],[Uom4],[QtyPackWhole],[QtyWholeLoose],[QtyLooseBase],[Price],[Packing],Att1,Att2,Att3,Att4,Att5,Att6,'{6}',getdate(),'{6}',getDate(),NettWeight,'{7}','{8}'
  from wh_dodet2 where Id='{0}'", doDetId,qty1,qty2,qty3,no,toLoc, EzshipHelper.GetUserName(),fromWh,toWh);
        ConnectSql.ExecuteSql(sql);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string doDetId = "";
        public int qty1 = 0;
        public int qty2 = 0;
        public int qty3 = 0;
        public string toLoc = "";
        public string fromWh = "";
        public string toWh = "";
        public Record(string _doDetId, int _qty1, int _qty2, int _qty3,string _toLoc,string _fromWh,string _toWh)
        {
            doDetId = _doDetId;
            qty1 = _qty1;
            qty2 = _qty2;
            qty3 = _qty3;
            toLoc = _toLoc;
            fromWh = _fromWh;
            toWh = _toWh;
        }

    }
    private void OnLoad()
    {
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.ASPxGridView1.FindRowCellTemplateControl(i,(DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Id"],"ack_IsPay") as ASPxCheckBox;
            ASPxTextBox doDetId = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Product"], "txt_DoDetId") as ASPxTextBox;
            ASPxSpinEdit qty1 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty1"], "spin_Qty1") as ASPxSpinEdit;
            ASPxSpinEdit qty2 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty2"], "spin_Qty2") as ASPxSpinEdit;
            ASPxSpinEdit qty3 = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["Qty3"], "spin_Qty3") as ASPxSpinEdit;
            ASPxLabel fromWh = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["FromWh"], "lbl_FromWh") as ASPxLabel;
            ASPxComboBox toWh = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ToWh"], "txt_ToWh") as ASPxComboBox;
            ASPxButtonEdit toLoc = this.ASPxGridView1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.ASPxGridView1.Columns["ToLoc"], "txt_ToLoc") as ASPxButtonEdit;

            if (doDetId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(doDetId.Text, SafeValue.SafeInt(qty1.Value, 0), SafeValue.SafeInt(qty2.Value, 0), SafeValue.SafeInt(qty3.Value, 0), toLoc.Text
                    ,SafeValue.SafeString(fromWh.Text),SafeValue.SafeString(toWh.Text)));
            }
            else if (doDetId == null)
                break; ;
        }
    }
    protected void ASPxGridView1_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex < 0) return;
        if (e.DataColumn.FieldName == "ToLoc")
        {
            ASPxButtonEdit toLoc = this.ASPxGridView1.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "txt_ToLoc") as ASPxButtonEdit;
            if (toLoc != null)
            {
                toLoc.ClientInstanceName = toLoc.ClientInstanceName + e.VisibleIndex;
                toLoc.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupLocation({0},null);", toLoc.ClientInstanceName) + "}";
            }
        }
    }
    protected void txt_ToLoc_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        ASPxComboBox txt_ToWh = this.ASPxGridView1.FindRowCellTemplateControl(0, (GridViewDataTextColumn)ASPxGridView1.Columns["ToWh"], "txt_ToWh") as ASPxComboBox;
        ASPxComboBox txt_ToLoc = this.ASPxGridView1.FindRowCellTemplateControl(0, (GridViewDataTextColumn)ASPxGridView1.Columns["ToLoc"], "txt_ToLoc") as ASPxComboBox;
        string s = txt_ToWh.SelectedItem.Value.ToString();

        string Sql = "SELECT Id,Code,Name from ref_location where Loclevel='Unit' and WarehouseCode='" + s + "'";

        DataSet ds = ConnectSql.GetDataSet(Sql);

        txt_ToLoc.DataSource = ds.Tables[0];

        txt_ToLoc.TextField = "Code";

        txt_ToLoc.ValueField = "Code";

        txt_ToLoc.DataBind();
    }
}