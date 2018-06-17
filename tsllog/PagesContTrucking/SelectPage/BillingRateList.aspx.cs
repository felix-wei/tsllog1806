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

public partial class PagesContTrucking_SelectPage_BillingRateList : System.Web.UI.Page
{
    public int count = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
            string type = SafeValue.SafeString(Request.QueryString["type"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);

            if (Request.QueryString["client"] != null)
            {
                ds1.FilterExpression = "ClientId='" + client + "'";
                btn_search_Click(null, null);
            }

        }
        count = this.grid1.VisibleRowCount;
        OnLoad();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        string status = SafeValue.SafeString(Request.QueryString["status"]);
        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobType = SafeValue.SafeString(Request.QueryString["type"]);
        string billClass = "";
        string where = "";
        if (jobType == "IMP" || jobType == "EXP" || jobType == "LOC")
            billClass = "TRUCKING";
        if (jobType == "GR" || jobType == "DO" || jobType == "TR")
            billClass = "WAREHOUSE";
        if (jobType == "TP")
            billClass = "TRANSPORT";
        if (jobType == "CRA")
            billClass = "CRANE";
        if (status == "Rate")
        {
            ds1.FilterExpression = "ClientId='" + client + "' and JobNo='-1' and BillClass='" + billClass + "'";
        }
        else if (status == "Quoted")
        {
            ds1.FilterExpression = "ClientId='" + client + "' and JobNo='" + jobNo + "'";
        }
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
        e.NewValues["ClientId"] = SafeValue.SafeString(Request.QueryString["client"]);
        e.NewValues["JobNo"] = "-1";
        e.NewValues["LineType"] = "RATE";
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
        e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
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
        e.NewValues["LineType"] = "RATE";
        e.NewValues["JobNo"] = "-1";
        e.NewValues["Remark"] = Helper.Safe.SafeString(e.NewValues["Remark"]).Trim();
        e.NewValues["RowUpdateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["RowUpdateTime"] = DateTime.Now;
        e.NewValues["LineStatus"] = Helper.Safe.SafeString(e.NewValues["LineStatus"]).Trim();
        e.NewValues["Unit"] = Helper.Safe.SafeString(e.NewValues["Unit"]).Trim();
        e.NewValues["ContSize"] = Helper.Safe.SafeString(e.NewValues["ContSize"]).Trim();
        e.NewValues["ContType"] = Helper.Safe.SafeString(e.NewValues["ContType"]).Trim();
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
    }
    #endregion
    protected void grid1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        try
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                    C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;

                    if (rate != null)
                    {
                        rate.ClientId = btn_ClientId.Text;
						rate.Price = list[i].amt;
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
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
		public decimal amt = 0;
        public Record(int _id, decimal _amt)
        {
            id = _id;
			amt = _amt;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = count;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Id"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox id = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxSpinEdit amt = this.grid1.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid1.Columns["Price"], "txt_Amt") as ASPxSpinEdit;

            if (id != null && isPay != null && isPay.Checked)
            {
				//throw new Exception(amt.Text);
                list.Add(new Record(S.Int(id.Text), S.Dec(amt.Text)));
            }
            else if (id == null)
                break;
        }
    }
}