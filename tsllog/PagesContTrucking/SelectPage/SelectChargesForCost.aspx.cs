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
public partial class PagesContTrucking_SelectPage_SelectChargesForCost : System.Web.UI.Page
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
        string sql = string.Format(@"select  SequenceId,REPLACE(REPLACE(ChgcodeId,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeId,REPLACE(REPLACE(ChgcodeDes,char(34),'\&#34;'),char(39),'\&#39;') as ChgcodeDes,ChgUnit,GstTypeId,GstP,ArCode,ApCode,ImpExpInd,0 as Price from XXChgCode");
        if (txt_Code.Text.Trim() != "")
        {
            where = GetWhere(where, " (ChgcodeDes like '%" + txt_Code.Text.Trim() + "%' or ChgcodeId like '%" + txt_Code.Text.Trim() + "%')");
        }
        if (SafeValue.SafeString(cbb_GroupBy.Value) != "")
        {
            where = GetWhere(where, " ChgTypeId='" + SafeValue.SafeString(cbb_GroupBy.Value) + "'");
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
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string quoteNo = SafeValue.SafeString(Request.QueryString["quoteNo"]);

        if (par == "Cont")
        {
            #region Container
            if (Request.QueryString["no"] != null)
            {
                string sql = string.Format(@"select ContainerNo,ContainerType,(case when isnull(BillingRefNo,'')<>''then job.JobNo else '' end) as SubJobNo from CTM_JobDet1 det1 inner join CTM_Job job on det1.JobNo=job.JobNo where job.JobNo='{0}' or job.BillingRefNo='{0}'", no);
                DataTable dt = ConnectSql_mb.GetDataTable(sql);
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    string contNo = SafeValue.SafeString(dt.Rows[a]["ContainerNo"]);
                    string contType = SafeValue.SafeString(dt.Rows[a]["ContainerType"]);
                    string subJobNo = SafeValue.SafeString(dt.Rows[a]["SubJobNo"]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where (JobNo='{0}' or JobNo='{1}') and LineType!='DP'", no, quoteNo)), 0);
                        n = n + 1;
                        string code = list[i].code;
                        string des = list[i].des;
                        decimal price = list[i].price;
                        string gstType = list[i].gstType;
                        decimal gst = list[i].gstP;
                        string unit = "";
                        insert_cost(n+i, no,subJobNo, "CONT", contNo, contType, "", code, des, price, gst, gstType, unit);
                        if(subJobNo.Length>0)
                            insert_cost(n + i, subJobNo, "", "CONT", contNo, contType, "", code, des, price, gst, gstType, unit);
                    }
                }
                e.Result = "Success";
            }
            #endregion
        }
        if (par == "Job")
        {
            #region Job
            if (Request.QueryString["no"] != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where (JobNo='{0}' or JobNo='{1}') and LineType!='DP'", no, quoteNo)), 0);
                    n = n + 1;
                    string code = list[i].code;
                    string des = list[i].des;
                    decimal price = list[i].price;
                    string gstType = list[i].gstType;
                    decimal gst = list[i].gstP;
                    string unit = list[i].unit;
                    insert_cost(n + i, no,"", "JOB", "", "", "", code, des, price, gst, gstType, unit);
                }
                e.Result = "Success";
            }
            #endregion
        }
        if (par == "Trip")
        {
            #region Trip
            if (Request.QueryString["no"] != null)
            {
                string sql = string.Format(@"select det2.Id,ChessisCode,job.JobNo,BillingRefNo,(case when isnull(BillingRefNo,'')<>''then job.JobNo else '' end) as SubJobNo from CTM_JobDet2 det2 inner join CTM_Job job on det2.JobNo=job.JobNo where job.JobNo='{0}' or job.BillingRefNo='{0}' and (Self_Ind='No' or isnull(Self_Ind,'')='')", no);
                DataTable dt = ConnectSql_mb.GetDataTable(sql);
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where (JobNo='{0}' or JobNo='{1}') and LineType!='DP'", no, quoteNo)), 0);
                    n = n + 1;
                    string tripId = SafeValue.SafeString(dt.Rows[a]["Id"]);
                    string contNo = SafeValue.SafeString(dt.Rows[a]["ChessisCode"]);
                    string subJobNo = SafeValue.SafeString(dt.Rows[a]["SubJobNo"]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string code = list[i].code;
                        string des = list[i].des;
                        decimal price = list[i].price;
                        string gstType = list[i].gstType;
                        decimal gst = list[i].gstP;
                        string unit = list[i].unit;
                        insert_cost(n + i, no,subJobNo, "CL", contNo, "", tripId, code, des, price, gst, gstType, unit);
                        if(subJobNo.Length>0)
                            insert_cost(n + i, subJobNo, "", "CL", contNo, "", tripId, code, des, price, gst, gstType, unit);
                    }
                }
                e.Result = "Success";
            }
            #endregion
        }
    }
    private void insert_cost(int i,string no, string subJobNo, string type, string contNo, string contType, string tripId, string code, string des, decimal price, decimal gst,string gstType,string unit)
    {
        Job_Cost c = new Job_Cost();
        c.LineIndex = i;
        c.TripNo = tripId;
        c.ChgCode = code;
        c.ChgCodeDe = des;
        c.ContNo = contNo;
        c.ContType = contType;
        c.Price = price;
        c.JobNo = no;
        c.Qty = 1;
        c.LineType = type;
        c.GstType = gstType;
        c.LineSource = "M";
        c.LineStatus = "";
        c.Unit = unit;
        c.CurrencyId = "SGD";
        c.SubJobNo = subJobNo;
        decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
        decimal docAmt = amt ;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
        c.DocAmt = docAmt;
        c.LocAmt = locAmt;
        c.RowCreateUser = EzshipHelper.GetUserName();
        c.RowCreateTime = DateTime.Now;
        c.RowUpdateUser = EzshipHelper.GetUserName();
        c.RowUpdateTime = DateTime.Now;

        Manager.ORManager.StartTracking(c, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(c);
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string code = "";
        public string des = "";
        public decimal price = 0;
        public string gstType = "";
        public decimal gstP = 0;
        public string unit = "";
        public Record(string _code, string _des, decimal _price, string _gstType, decimal _gstP,string _unit)
        {
            code = _code;
            des = _des;
            price = _price;
            gstType = _gstType;
            gstP = _gstP;
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
            ASPxLabel lbl_GstTypeId = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GstTypeId"], "lbl_GstTypeId") as ASPxLabel;
            ASPxLabel lbl_GstP = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GstP"], "lbl_GstP") as ASPxLabel;
            ASPxTextBox txt_Unit = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgUnit"], "txt_Unit") as ASPxTextBox;

            if (id != null && isOk != null && isOk.Checked)
            {
                list.Add(new Record(lbl_ChgcodeId.Text, lbl_ChgcodeDes.Text, SafeValue.SafeDecimal(spin_Price.Value), lbl_GstTypeId.Text, SafeValue.SafeDecimal(lbl_GstP.Text),txt_Unit.Text));

            }
            else if (id == null)
                break;
        }
    }
}