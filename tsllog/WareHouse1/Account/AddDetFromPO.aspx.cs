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

public partial class WareHouse_Account_AddDetFromPO : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string typ = Request.QueryString["typ"].ToString();
            this.cmb_PoSo.Text = typ;
            if (typ == "POR")
            {
                lbl_No.Text = "Po No";
            }
        }
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string invNo = this.txt_No.Text.Trim();
        string where = "";
        string sql = "";
        if (this.cmb_PoSo.Text == "POR")
        {
            sql = "select Id,Product,BalQty,Price,Currency,GstType from wh_PODet";
            if (invNo.Length > 0)
            {
                where = " and PoNo='" + invNo + "'";
            }
            where = " where Qty>0";
        }
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
        if (Request.QueryString["po"] != null && Request.QueryString["no"] != null && Request.QueryString["typ"] != null)
        {
            int start = 0;// 
            int end = 10000;//
            int qty = 0;
            int id = SafeValue.SafeInt(Request.QueryString["id"], 0);
            for (int a = start; a < end; a++)
            {
                ASPxSpinEdit spin_det_Qty1 = this.grid.FindRowTemplateControl(a, "spin_det_Qty1") as ASPxSpinEdit;
                ASPxSpinEdit spin_det_Qty2 = this.grid.FindRowTemplateControl(a, "spin_det_Qty2") as ASPxSpinEdit;
                string sql = "";
                ASPxTextBox txt_docId = this.grid.FindRowTemplateControl(a, "txt_docId") as ASPxTextBox;
                ASPxCheckBox isPay = this.grid.FindRowTemplateControl(a, "ack_IsPay") as ASPxCheckBox;

                if (isPay != null && isPay.Checked)
                {
                    int packQty = SafeValue.SafeInt(spin_det_Qty1.Text,0);
                    int unitQty = SafeValue.SafeInt(spin_det_Qty2.Text, 0);
                    string invNo = Request.QueryString["no"].ToString();
                    string typ = Request.QueryString["typ"].ToString().ToUpper();
                    int invId = SafeValue.SafeInt(Request.QueryString["id"], 0);
                    string invId_old = e.Parameters;
                    string arApInd_sch = this.cmb_PoSo.Text.ToUpper();
                    int sequenceId = 0;
                    if (typ == "POR")
                    {

                        #region PO Det
                        sql = string.Format("select BalQty from wh_PODet where Id={0}", SafeValue.SafeInt(txt_docId.Text, 0));
                        int balQty = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                        qty = SafeValue.SafeInt(packQty*unitQty,0);
                        if (balQty > qty || balQty == qty)
                        {
                            for (int m = 0; m < list.Count; m++)
                            {
                                try
                                {
                                    sequenceId = list[m].docId;
                                    sql = string.Format("select * from wh_PODet where Id={0}", sequenceId);
                                    WhPOReceiptDet det = null;
                                    DataTable tab = ConnectSql.GetTab(sql);
                                    for (int i = 0; i < tab.Rows.Count; i++)
                                    {
                                        string product = tab.Rows[i]["Product"].ToString();

                                        det = new WhPOReceiptDet();
                                        decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                                        string unit = tab.Rows[i]["Unit"].ToString().ToUpper();
                                        string currency = tab.Rows[i]["Currency"].ToString();
                                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 0);
                                        decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                                        string gstType = tab.Rows[i]["GstType"].ToString();
                                        det.Product = product;
                                        det.Currency = currency;
                                        det.ExRate = exRate;
                                        det.ReceiptNo = invNo;
                                        det.Price = price;
                                        det.Qty = qty;
                                        det.PoNo = tab.Rows[i]["PoNo"].ToString();
                                        det.Gst = gst;
                                        det.GstType = gstType;

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
                                    UpdatePoMaster(invId, sequenceId, det.Qty);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        else
                        {
                            e.Result = "Error, Pls try again";
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
    #region chgcode update master

    private void UpdatePoMaster(int lineNo, int sequenceId, int qty)
    {

        string sql = string.Format("update wh_POReceiptDet set LineLocAmt=locAmt* (select ExRate from wh_POReceipt where Id=wh_POReceiptDet.LinePNo) where LinePNo='{0}'", lineNo);
        C2.Manager.ORManager.ExecuteCommand(sql);
        sql = string.Format("Update wh_PODet set BalQty=Qty-{1} where Id={0}", sequenceId, qty);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion

    List<Record> list = new List<Record>();
    internal class Record
    {
        public int docId = 0;
        public bool isPay = false;
        public Record(int _docId)
        {
            docId = _docId;
        }

    }
    private void OnLoad()
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 100;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.grid.FindRowTemplateControl(i, "txt_docId") as ASPxTextBox;
            ASPxCheckBox isPay = this.grid.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0)
                    ));
            }
        }
    }
}