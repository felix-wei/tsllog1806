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

public partial class PagesContTrucking_SelectPage_SelectChgCode : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.form1.Focus();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btn_Retrieve_Click(null, null);
        }
        OnLoad();
    }
    protected void btn_Retrieve_Click(object sender, EventArgs e)
    {
        string where = "";
        string type = SafeValue.SafeString(Request.QueryString["type"]);
        string sql = string.Format(@"select  SequenceId,REPLACE(REPLACE(ChgcodeId,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeId,REPLACE(REPLACE(ChgcodeDes,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeDes,ChgUnit,GstTypeId,GstP,ArCode,ApCode,ImpExpInd,0 as Price,1 as Qty from XXChgCode");
        if(txt_Code.Text.Trim()!=""){
            where =GetWhere(where, " ChgcodeDes like '%" + txt_Code.Text.Trim() + "%'");
        }
        if (SafeValue.SafeString(cbb_GroupBy.Value) != "")
        {
            where = GetWhere(where, " ChgTypeId='" + SafeValue.SafeString(cbb_GroupBy.Value) + "'");
        }
        if (type=="quoted")
        {
            where = GetWhere(where, " Quotation_Ind='Y'");
        }
        if (where.Length > 0)
        {
            sql += " where " + where;
        }
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
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
        string par = e.Parameters;
        if (par == "OK")
        {
            string no = SafeValue.SafeString(Request.QueryString["no"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            string type= SafeValue.SafeString(Request.QueryString["type"]);
            if (Request.QueryString["no"]!=null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string code = list[i].code;
                    string des = list[i].des;
                    decimal price=list[i].price;
                    decimal qty = list[i].qty;
                    string unit = list[i].unit;
                    if (type == "Quoted")
                    {
                        #region Quotation
                        JobRate rate = new JobRate();
                        rate.ChgCode = code;
                        rate.ChgCodeDe = des;
                        rate.Price = price;
                        rate.JobNo = no;
                        rate.Qty = qty;
                        rate.ClientId = client;
                        rate.Unit = unit;
                        rate.RowCreateUser = EzshipHelper.GetUserName();
                        rate.RowCreateTime = DateTime.Now;
                        rate.RowUpdateUser = EzshipHelper.GetUserName();
                        rate.RowUpdateTime = DateTime.Now;
                        rate.ContSize = " ";
                        rate.ContType = "";
                        rate.Remark = " ";
                        rate.BillType = " ";
                        rate.BillScope = " ";
    
                        rate.LineType = "QUOTED";
                        Manager.ORManager.StartTracking(rate, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(rate);
                        #endregion
                    }
                    if (type == "Cost")
                    {
                        #region Job_Cost
                        Job_Cost cost = new Job_Cost();
                        cost.ChgCode = code;
                        cost.ChgCodeDe = des;
                        cost.LineSource = "S";
                        cost.Price = price;
                        cost.JobNo = no;
                        cost.Qty = qty;
                        cost.BillClass = " ";
                        cost.Unit = unit;
                        cost.RowCreateUser = EzshipHelper.GetUserName();
                        cost.RowCreateTime = DateTime.Now;
                        cost.RowUpdateUser = EzshipHelper.GetUserName();
                        cost.RowUpdateTime = DateTime.Now;

                        Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                        Manager.ORManager.PersistChanges(cost);
                        #endregion
                    }
                }
                e.Result = "Success";
            }
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string code = "";
        public string des = "";
        public decimal price = 0;
        public decimal qty = 0;
        public string unit = "";
        public Record(string _code, string _des,decimal _price,decimal _qty,string _unit)
        {
            code = _code;
            des = _des;
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
            ASPxCheckBox isOk = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["SequenceId"], "ack_IsOk") as ASPxCheckBox;

            ASPxTextBox id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["SequenceId"], "txt_Id") as ASPxTextBox;
            ASPxLabel lbl_ChgcodeId = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgcodeId"], "lbl_ChgcodeId") as ASPxLabel;
            ASPxLabel lbl_ChgcodeDes = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgcodeDes"], "lbl_ChgcodeDes") as ASPxLabel;
            ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxTextBox txt_Unit = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgUnit"], "txt_Unit") as ASPxTextBox;
            if (id != null && isOk != null && isOk.Checked)
            {
                list.Add(new Record(lbl_ChgcodeId.Text,lbl_ChgcodeDes.Text,SafeValue.SafeDecimal(spin_Price.Value),SafeValue.SafeDecimal(spin_Qty.Value),txt_Unit.Text));

            }
            else if (id == null)
                break;
        }
    }
    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        btn_Retrieve_Click(null,null);
    }
}