using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class WareHouse_SelectPage_AddProducts : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            btn_Sch_Click(null,null);
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string code = this.txt_No.Text.Trim();
        string where = "";
        string partyId = SafeValue.SafeString(Request.QueryString["party"]);
        string warehouse = SafeValue.SafeString(Request.QueryString["wh"]);
        string sql = "select Id,Code as Product,CostPrice as Price from ref_product";
        if (code.Length > 0)
        {
            where = " and Code='" + code + "'";
        }
        if(partyId.Length>0)
           where = " where PartyId='" + partyId + "' or PartyId=''";
        if(warehouse.Length>0)
            where = " where WareHouseId='" + warehouse + "'";
        if (where.Length > 0)
        {
            sql += where;
        }
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["party"] != null && Request.QueryString["no"] != null)
        {
            int start = 0;// 
            int end = 10000;//
            int qty = 0;
            for (int a = start; a < end; a++)
            {
                string sql = "";
                ASPxTextBox txt_Id = this.grid.FindRowTemplateControl(a, "txt_Id") as ASPxTextBox;
                ASPxCheckBox isPay = this.grid.FindRowTemplateControl(a, "ack_IsPay") as ASPxCheckBox;
                if (isPay != null && isPay.Checked)
                {
                    int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    string s = e.Parameters;
                    if (s == "OK")
                    {
                        #region PO Det
                        try
                        {
                            sql = string.Format("select Id,Code from ref_product where Id={0}", SafeValue.SafeInt(txt_Id.Text, 0));
                            WhPODet det = null;
                            DataTable tab = ConnectSql.GetTab(sql);
                            for (int i = 0; i < tab.Rows.Count; i++)
                            {
                                string product = tab.Rows[i]["Code"].ToString();

                                det = new WhPODet();
                                ASPxSpinEdit spin_Qty1 = this.grid.FindRowTemplateControl(a, "spin_Qty1") as ASPxSpinEdit;
                                ASPxSpinEdit spin_Qty2 = this.grid.FindRowTemplateControl(a, "spin_Qty2") as ASPxSpinEdit;
                                ASPxSpinEdit spin_Price = this.grid.FindRowTemplateControl(a, "spin_Price") as ASPxSpinEdit;
                                ASPxSpinEdit spin_ExRate = this.grid.FindRowTemplateControl(a, "spin_ExRate") as ASPxSpinEdit;
                                ASPxSpinEdit spin_GstP = this.grid.FindRowTemplateControl(a, "spin_GstP") as ASPxSpinEdit;
                                ASPxTextBox txt_Currency = this.grid.FindRowTemplateControl(a, "txt_Currency") as ASPxTextBox;
                                ASPxComboBox cmb_GstType = this.grid.FindRowTemplateControl(a, "cmb_GstType") as ASPxComboBox;
                                decimal price = SafeValue.SafeDecimal(spin_Price.Value, 0);
                                string currency = SafeValue.SafeString(txt_Currency.Text);
                                decimal exRate = SafeValue.SafeDecimal(spin_ExRate.Value, 0);
                                decimal gst = SafeValue.SafeDecimal(spin_GstP.Value, 0);
                                string gstType = SafeValue.SafeString(cmb_GstType.Text);
                                if (gstType == "S")
                                {
                                    gst = SafeValue.SafeDecimal(0.07, 0);
                                }
                                int packQty = SafeValue.SafeInt(spin_Qty1.Value, 0);
                                int unitQty = SafeValue.SafeInt(spin_Qty2.Value, 0);
                                qty =SafeValue.SafeInt(packQty * unitQty,0);
                                det.Product = product;
                                det.Currency = currency;
                                det.ExRate = exRate;
                                det.Price = price;
                                det.Qty = qty;
                                det.PoNo = no;
                                det.Gst = gst;
                                det.GstType = gstType;
                                det.BalQty = qty;
                                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                                decimal docAmt = amt + gstAmt;
                                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);
                                det.GstAmt = gstAmt;
                                det.DocAmt = docAmt;
                                det.LocAmt = locAmt;
                                det.CreateBy = EzshipHelper.GetUserName();
                                det.CreateDateTime = DateTime.Now;
                                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(det);

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion
                    }

                }
            }
        }
        else
        {
            e.Result = "Error, Pls refresh your Det";
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public bool isPay = false;
        public Record(int _Id)
        {
            id = _Id;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.grid.FindRowTemplateControl(i, "txt_Id") as ASPxTextBox;
            ASPxCheckBox isPay = this.grid.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0)
                    ));
            }
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
}