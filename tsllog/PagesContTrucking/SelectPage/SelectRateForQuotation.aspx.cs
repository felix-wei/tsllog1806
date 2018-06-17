using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;

public partial class PagesContTrucking_SelectPage_SelectRateForQuotation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["quoteNo"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            if (Request.QueryString["client"] != null)
            {
                ds1.FilterExpression = "ClientId='" + client + "'";
                btn_search_Click(null, null);
            }
        }
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string client = SafeValue.SafeString(Request.QueryString["client"]);
        ds1.FilterExpression = "ClientId='" + client + "' and JobNo='-1'";
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid1.AddNewRow();
    }
    #region quotation det
    protected void Grid1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.JobRate));
    }
    protected void Grid1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Price"] = (decimal)0;
        e.NewValues["Unit"] = "";
        e.NewValues["Remark"] = " ";
        e.NewValues["MinAmt"] = (decimal)0;
        e.NewValues["Qty"] = (decimal)1;
        e.NewValues["CurrencyId"] = "SGD";
        e.NewValues["ExRate"] = 1;
        e.NewValues["JobNo"] = "-1";
        e.NewValues["LineType"] = "RATE";
        e.NewValues["ClientId"] = SafeValue.SafeString(Request.QueryString["client"]);

        btn_search_Click(null, null);
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;
        e.NewValues["LineType"] = "RATE";
        e.NewValues["JobNo"] = "-1";
        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Value);
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        e.NewValues["RowCreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowCreateTime"] = DateTime.Now;
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        if (SafeValue.SafeString(e.NewValues["ChgCodeDe"]) == "")
        {
            e.NewValues["ChgCodeDe"] = chgcodeDes;
        }
        btn_search_Click(null, null);
    }
    protected void Grid1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
        if (Helper.Safe.SafeString(e.NewValues["ContType"]).Trim() != "")
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim().Substring(0, 2);
        }
        else
        {
            e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        }
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["BillScope"] = Helper.Safe.SafeString(e.NewValues["BillScope"]).Trim();
        e.NewValues["BillType"] = Helper.Safe.SafeString(e.NewValues["BillType"]).Trim();
        e.NewValues["BillClass"] = Helper.Safe.SafeString(e.NewValues["BillClass"]).Trim();
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;
        string sql = string.Format(@"select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", SafeValue.SafeString(txt_ChgCode.Value));
        string chgcodeDes = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
        e.NewValues["ChgCode"] = SafeValue.SafeString(txt_ChgCode.Value);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        btn_search_Click(null, null);
    }
    protected void Grid1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        btn_search_Click(null, null);
    }
    #endregion
    protected void grid1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (Request.QueryString["quoteNo"] != null)
        {
            try
            {
                if (list.Count > 0)
                {
                    string jobNo = SafeValue.SafeString(Request.QueryString["quoteNo"]);
                    for (int i = 0; i < list.Count; i++)
                    {

                        int id = list[i].id;
                        decimal price = list[i].price;
                        decimal qty = list[i].qty;
                        string unit = list[i].unit;

                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                        C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;

                        if (rate != null)
                        {
                            rate.JobNo = jobNo;
                            rate.Qty = qty;
                            rate.Price = price;
                            rate.Unit = unit;
                            rate.LineType = "QUOTED";
                            Manager.ORManager.StartTracking(rate, Wilson.ORMapper.InitialState.Inserted);
                            Manager.ORManager.PersistChanges(rate);
                        }

                    }
                    e.Result = "Success";
                }
                else
                {
                    e.Result = "Pls Select at least one Rate";
                }
            }
            catch { }
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public decimal price = 0;
        public decimal qty = 0;
        public string unit = "";
        public Record(int _id,decimal _price,decimal _qty,string _unit)
        {
            id = _id;
            price = _price;
            qty = _qty;
            unit = _unit;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = 1000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit spin_Price = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit spin_Qty = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxTextBox txt_Unit = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Unit"], "txt_Unit") as ASPxTextBox;
            
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), SafeValue.SafeDecimal(spin_Price.Value), SafeValue.SafeDecimal(spin_Qty.Value), txt_Unit.Text));
            }
            else if (id == null)
                break;
        }
    }

}