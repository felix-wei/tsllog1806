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
public partial class PagesContTrucking_SelectPage_SelectBillRate : System.Web.UI.Page
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

        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobType = SafeValue.SafeString(Request.QueryString["type"]);
        string billClass="";
        string where = "";
        if (jobType == "IMP" || jobType == "EXP" || jobType == "LOC")
            billClass = "TRUCKING";
        if (jobType == "GR" || jobType == "DO" || jobType == "TR")
            billClass = "WAREHOUSE";
        if (jobType == "TP")
            billClass = "TRANSPORT";
        if (jobType == "CRA")
            billClass = "CRANE";

        ds1.FilterExpression = "ClientId='" + client + "' and JobNo='-1' and BillClass='" + billClass + "'";
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
        btn_search_Click(null,null);
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;

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
        if (Request.QueryString["no"] != null)
        {
            try
            {
                if (list.Count > 0)
                {
                    string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
                    string jobType = SafeValue.SafeString(Request.QueryString["type"]);
                    string result = "";
                    string code = "";
                    for (int i = 0; i < list.Count; i++)
                    {
                        int id = list[i].id;
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                        C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;

                        if (rate != null)
                        {
                            #region Container
                            if (rate.BillScope.ToUpper() == "CONT")
                            {
                                string sql_cont = string.Format(@"select ContainerNo,ContainerType from CTM_JobDet1 where JobNo='{0}'", jobNo);
                                DataTable dt = ConnectSql.GetTab(sql_cont);
                                for (int a = 0; a < dt.Rows.Count; a++)
                                {
                                    string contNo = SafeValue.SafeString(dt.Rows[a]["ContainerNo"]);
                                    string contType = SafeValue.SafeString(dt.Rows[a]["ContainerType"]);
                                    string str = contType.Substring(0,2);
                                    decimal price = 0;
                                    string sql = string.Format(@"select count(*) from job_cost where ContNo='{0}' and ChgCode='{1}'",contNo,rate.ChgCode);
                                    int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
                                    if (n == 0)
                                    {
                                        C2.Job_Cost cost = new Job_Cost();
                                        cost.JobNo = jobNo;
                                        cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                                        cost.ExRate = new decimal(1.0);
                                        cost.ContNo = contNo;
                                        cost.ContType = contType;
                                        cost.ChgCode = rate.ChgCode;
                                        if (rate.ChgCode.ToUpper().Equals("TRUCKING"))
                                        {
                                            cost.ChgCodeDe = rate.ChgCode;
                                        }
                                        else
                                        {
                                            cost.ChgCodeDe = rate.ChgCodeDe;
                                        }
                                        cost.BillClass = "TRUCKING";
                                        cost.BillScope = "CONT";
                                        cost.LineSource = "M";
                                        cost.JobType = jobType;
                                        cost.Qty = 1;
                                        cost.Unit = "";
                                        cost.LineType = "CONT";
                                        cost.LocAmt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(rate.Price, 0), 2);
                                        if (rate.ContSize == str)
                                        {
                                            cost.Price = rate.Price;
                                            Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                                            Manager.ORManager.PersistChanges(cost);
                                        }
                                        else if (rate.ContSize.Length == 0)
                                        {
                                            cost.Price = rate.Price;
                                            Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                                            Manager.ORManager.PersistChanges(cost);
                                        }
                                        result = "Success";
                                    }
                                    else {
                                        code +=contNo+" Had "+ rate.ChgCodeDe + " / ";
                                    }
                                }
                            }
                            #endregion
                            #region Other
                            else
                            {
                                string sql = string.Format(@"select count(*) from job_cost where JobNo='{0}' and ChgCode='{1}'", jobNo, rate.ChgCode);
                                int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
                                if (n == 0)
                                {
                                    C2.Job_Cost cost = new Job_Cost();
                                    cost.JobNo = jobNo;
                                    cost.JobType = jobType;
                                    cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                                    cost.ExRate = new decimal(1.0);
                                    cost.ContNo = "";
                                    cost.ContType = "";
                                    cost.ChgCode = rate.ChgCode;
                                    cost.ChgCodeDe = rate.ChgCodeDe;
                                    cost.Qty = 1;
                                    cost.Unit = "";
                                    cost.Price = rate.Price;
                                    cost.LineType ="JOB";
                                    cost.BillClass = rate.BillClass;
                                    cost.BillScope = rate.BillScope;
                                    cost.LineSource = "M";
                                    cost.LocAmt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(rate.Price, 0), 2);
                                    Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                                    Manager.ORManager.PersistChanges(cost);
                                }
                                else {
                                    code += rate.ChgCodeDe + " ";
                                }
                                result = "Success";
                            }
                            #endregion
                        }
                    }
                    if(code.Length>0)
                        e.Result =code;
                    else
                      e.Result = result;
                }
                else {
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
        public Record(int _id)
        {
            id = _id;
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

            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0)));
            }
            else if (id == null)
                break;
        }
    }
}