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

public partial class WareHouse_SelectPage_AddDoOut : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.form1.Focus();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            btn_Sch_Click(null, null);
        OnLoad();
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string productCode = this.txt_No.Text.Trim();
        string doNo = this.txt_DoNo.Text.Trim();
        string partyId = SafeValue.SafeString(Request.QueryString["party"]);
        string wh = SafeValue.SafeString(Request.QueryString["wh"]);
        string sql = "select top(100) * from Wh_DoDet det inner join Wh_Do d on det.DoNo=d.DoNo where";
        string where = " d.DoType='IN' and StatusCode='CLS' and det.BalQty>0";
        if (doNo.Length > 0)
        {
            where = GetWhere(where, " det.DoNo='" + doNo + "'");
        }
        if (productCode.Length > 0)
        {
            where =GetWhere(where, " det.ProductCode='" + productCode + "'");
        }
        if (partyId.Length > 0)
            where =GetWhere(where," PartyId='" + partyId + "'");
        if (wh.Length > 0)
            where = GetWhere(where, " WareHouseId='" + wh + "'");
        if (where.Length > 0)
        {
            sql += where;
        }
        DataTable tab = ConnectSql.GetTab(sql);
        this.grid.DataSource = tab;
        this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "OK")
        {
            if (Request.QueryString["party"] != null && Request.QueryString["no"] != null)
            {
                string no = SafeValue.SafeString(Request.QueryString["no"]);
                string whCode = SafeValue.SafeString(Request.QueryString["wh"]);
                for (int a = 0; a < list.Count; a++)
                {
                    string sql = "";
                    #region Do Out Det
                    int id = list[a].id;
                    int qty = list[a].qty;
                    try
                    {
                        sql = string.Format("select * from Wh_DoDet where Id={0} ", id);
                        ASPxSpinEdit spin_Qty1 = this.grid.FindRowTemplateControl(a, "spin_Qty1") as ASPxSpinEdit;
                        ASPxSpinEdit spin_Qty2 = this.grid.FindRowTemplateControl(a, "spin_Qty2") as ASPxSpinEdit;
                        int packQty = SafeValue.SafeInt(spin_Qty1.Text,0);
                        int locaQty = SafeValue.SafeInt(spin_Qty2.Text, 0);
                        DataTable tab = ConnectSql.GetTab(sql);
                        if (tab.Rows.Count == 1)
                        {
                            int i = 0;
                            string product = tab.Rows[i]["ProductCode"].ToString();
                            string DoInNo = tab.Rows[i]["DoNo"].ToString();
                            decimal price = SafeValue.SafeDecimal(tab.Rows[i]["Price"], 0);
                            string currency = SafeValue.SafeString(tab.Rows[i]["Currency"]);
                            decimal exRate = SafeValue.SafeDecimal(tab.Rows[i]["ExRate"], 1);
                            decimal gst = SafeValue.SafeDecimal(tab.Rows[i]["Gst"], 0);
                            string gstType = SafeValue.SafeString(tab.Rows[i]["GstType"]);
                            string batchNo = SafeValue.SafeString(tab.Rows[i]["BatchNo"]);
                            DateTime expiredDate = SafeValue.SafeDate(tab.Rows[i]["ExpiredDate"], DateTime.Today);
                            string color = SafeValue.SafeString(tab.Rows[i]["Color"]);
                            decimal size = SafeValue.SafeDecimal(tab.Rows[i]["Size"], 0);
                            string packCode = SafeValue.SafeString(tab.Rows[i]["PackCode"]);
                            string unit = SafeValue.SafeString(tab.Rows[i]["Unit"]);
                            int balQty = SafeValue.SafeInt(tab.Rows[i]["BalQty"],0);

                            WhDoDet det = new WhDoDet();
                            if (gstType == "S")
                            {
                                gst = SafeValue.SafeDecimal(0.07, 0);
                            }
                            det.ProductCode = product;
                            det.Currency = currency;
                            det.ExRate = exRate;
                            det.Price = price;
                            det.Qty1 = packQty;
                            det.Qty2 = locaQty;
                            det.Qty = packQty*locaQty;
                            det.DoInNo = DoInNo;
                            det.DoInId = id.ToString();
                            det.Gst = gst;
                            det.GstType = gstType;
                            det.BalQty = 0;
                            det.PreQty = qty;
                            det.DoNo = no;
                            det.BatchNo = batchNo;
                            det.ExpiredDate = expiredDate;
                            det.Color = color;
                            det.Size = size;
                            det.DoType = "OUT";
                            det.JobStatus = "Draft";
                            det.PackCode = packCode;
                            det.ContractNo = SafeValue.SafeString(tab.Rows[i]["ContractNo"]);
                            det.DocAmt = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                            det.GstAmt = SafeValue.SafeDecimal(tab.Rows[i]["GstAmt"], 0);
                            det.DocAmt = SafeValue.SafeDecimal(tab.Rows[i]["DocAmt"], 0);
                            det.LocAmt = SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                            if (balQty >= qty&&packQty>0&&locaQty>0)
                            {
                                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                                C2.Manager.ORManager.PersistChanges(det);
                                UpdateBalQty(id, 0);
                            }
                            else
                            {
                                e.Result = "Error, Pls refresh your Det";
                            }
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
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public int qty = 0;
        public Record(int _Id,int _qty)
        {
            id = _Id;
            qty = _qty;
        }

    }
    private void OnLoad()
    {
        int start = 0;//
        int end = 10000;// 
        for (int i = start; i < end; i++)
        {
            ASPxTextBox docId = this.grid.FindRowTemplateControl(i, "txt_Id") as ASPxTextBox;
            ASPxCheckBox isPay = this.grid.FindRowTemplateControl(i, "ack_IsPay") as ASPxCheckBox;
            ASPxSpinEdit qty = this.grid.FindRowTemplateControl(i, "spin_Qty") as ASPxSpinEdit;
            ASPxLabel balQty = this.grid.FindRowTemplateControl(i, "lab_BalQty") as ASPxLabel;

            if (docId != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(docId.Text, 0),SafeValue.SafeInt(qty.Value,0)
                    ));
            }
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    private void UpdateBalQty(int id, int qty)
    {
        string sql = string.Format(@"update Wh_DoDet set BalQty=Qty-(select sum(Qty) from Wh_DoDet where DoType='OUT' and DoInId={0})+{1} from Wh_DoDet  where Id={0}", id, qty);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
}