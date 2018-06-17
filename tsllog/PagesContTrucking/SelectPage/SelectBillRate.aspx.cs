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
            string status = SafeValue.SafeString(Request.QueryString["status"]);
            if (Request.QueryString["client"] != null)
            {
               // ds1.FilterExpression = "ClientId='" + client + "'";
                btn_search_Click(null, null);
            }
            if (status == "Quoted")
            {
                lbl.Style.Add("display","none");
                cbb.Style.Add("display", "none");
                btn_search.Visible = false;
            }
        }
        count = this.grid1.VisibleRowCount; 
        OnLoad();
    }
    protected void BindRate() { 
      
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        string status = SafeValue.SafeString(Request.QueryString["status"]);
        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobType = SafeValue.SafeString(Request.QueryString["type"]);
        string billClass = "";
        string chg = txt_Charge.Text.Trim();
        string where = "";
        if (jobType == "IMP" || jobType == "EXP" || jobType == "LOC")
            billClass = "TRUCKING";
        if (jobType == "WGR" || jobType == "WDO" || jobType == "FRT")
            billClass = "WAREHOUSE";
        if (jobType == "TPT")
            billClass = "TRANSPORT";
        if (jobType == "CRA")
            billClass = "CRANE";

        if (chg != "")
            where =GetWhere(where,"ChgCodeDes like '%" + chg + "%'");

			//throw new Exception(where);

		if (cbb_BillScope.Value != null)
            where =GetWhere(where,"BillScope='" + SafeValue.SafeString(cbb_BillScope.Value) + "'");
        if (status == "Rate")
        {
            where = GetWhere(where, "(ClientId='" + client + "') and JobNo='-1'");
            ds1.FilterExpression = where;
        }
        else if (status == "Quoted")
        {
            where = GetWhere(where, "ClientId='" + client + "' and JobNo='" + jobNo + "'");
            ds1.FilterExpression = where;
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid1.AddNewRow();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
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
        
    }
    protected void Grid1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string status = SafeValue.SafeString(Request.QueryString["status"]);
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        ASPxGridView grid = (sender as ASPxGridView) as ASPxGridView;
        ASPxComboBox txt_ChgCode = grid.FindEditRowCellTemplateControl(null, "txt_ChgCode") as ASPxComboBox;
        if (status == "Rate")
        {
            e.NewValues["LineType"] = "RATE";
            e.NewValues["JobNo"] = "-1";
        }
        else {
            e.NewValues["LineType"] = "QUOTED";
            e.NewValues["JobNo"] =jobNo;
        }
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
        string par = e.Parameters;
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string jobNo = SafeValue.SafeString(Request.QueryString["jobNo"]);
        string status = SafeValue.SafeString(Request.QueryString["status"]);
        if (Request.QueryString["no"] != null)
        {
            try
            {
                if (par == "Cont")
                {
                    #region Container
                    if (status == "Quoted")
                        no = jobNo;
                    string sql = string.Format(@"select ContainerNo,ContainerType,(case when isnull(BillingRefNo,'')<>''then job.JobNo else '' end) as SubJobNo from CTM_JobDet1 det1 inner join CTM_Job job on det1.JobNo=job.JobNo where job.JobNo='{0}' or job.BillingRefNo='{0}'", no);
                    DataTable dt = ConnectSql_mb.GetDataTable(sql);
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {
                        string contNo = SafeValue.SafeString(dt.Rows[a]["ContainerNo"]);
                        string contType = SafeValue.SafeString(dt.Rows[a]["ContainerType"]);
                        string subJobNo= SafeValue.SafeString(dt.Rows[a]["SubJobNo"]);

                        for (int i = 0; i < list.Count; i++)
                        {
                            int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where (JobNo='{0}' or JobNo='{1}') and LineType!='DP'", jobNo, no)), 0);
                            n = n + 1;
                            int id = list[i].id;
                            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                            C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;
                            string scope = SafeValue.SafeString(rate.BillScope, "");
                            string contSize = SafeValue.SafeString(rate.ContSize, "");
                            string str = "";
                            if (contType.Length > 2)
                                str = contType.Substring(0, 2);
                            decimal price = 0;
                            if (status == "Rate")
                            {
                                if (rate.BillScope == "CONT")
                                {
                                    insert_job_cost(rate, no,subJobNo, contNo, "", contType, contSize, str, par.ToUpper(), n);
                                }
                            }
                            else
                            {
                                insert_job_cost(rate, jobNo, subJobNo, contNo, "", contType, contSize, str, par.ToUpper(), n);
                            }
                            if (subJobNo.Length > 0)
                            {
                                insert_job_cost(rate, subJobNo, "", contNo, "", contType, contSize, str, par.ToUpper(), n);
                            }
                        }
                    }
                    e.Result = "Success";
                    
                    #endregion
                }
                if (par == "Job")
                {
                    #region Job
                    for (int i = 0; i < list.Count; i++)
                    {
                        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where JobNo='{0}' and LineType!='DP'", jobNo)), 0);
                        n = n + 1;
                        int id = list[i].id;
                        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                        C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;
                        if (status == "Rate")
                        {
                            //if (rate.BillScope == "JOB")
                            //{
                            insert_job_cost(rate, no, "", "", "", "", "", "", par.ToUpper(), n);
                            //}
                        }
                        else
                        {
                            insert_job_cost(rate, jobNo,"", "", "", "", "", "", par.ToUpper(), n);
                        }
                    }
                        e.Result = "Success";
                    
                    #endregion
                }
                if (par == "Trip")
                {
                    #region Trip
                    if (status == "Quoted")
                        no = jobNo;
                    string sql = string.Format(@"select det2.Id,ChessisCode,job.JobNo,BillingRefNo,(case when isnull(BillingRefNo,'')<>''then job.JobNo else '' end) as SubJobNo from CTM_JobDet2 det2 inner join CTM_Job job on det2.JobNo=job.JobNo where job.JobNo='{0}' or job.BillingRefNo='{0}' and (Self_Ind='No' or isnull(Self_Ind,'')='')", no);
                    DataTable dt = ConnectSql_mb.GetDataTable(sql);
                    for (int a = 0; a < dt.Rows.Count; a++)
                    {

                        string tripId = SafeValue.SafeString(dt.Rows[a]["Id"]);
                        string contNo = SafeValue.SafeString(dt.Rows[a]["ChessisCode"]);
                        string subJobNo= SafeValue.SafeString(dt.Rows[a]["SubJobNo"]);
                        for (int i = 0; i < list.Count; i++)
                        {
                            int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(string.Format(@"select count(*) from job_cost where (JobNo='{0}' or JobNo='{1}') and LineType!='DP'", jobNo,no)), 0);
                            n = n + 1;
                            int id = list[i].id;
                            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(C2.JobRate), "Id=" + id + "");
                            C2.JobRate rate = C2.Manager.ORManager.GetObject(query) as C2.JobRate;
                            if (status == "Rate")
                            {
                                if (rate.BillScope.ToUpper() == "TRIP")
                                {
                                    insert_job_cost(rate, no,subJobNo, contNo, tripId, "", "", "", par.ToUpper(), n);
                                }
                            }
                            else
                            {
                                insert_job_cost(rate, jobNo,subJobNo, contNo, tripId, "", "", "", par.ToUpper(), n);
                            }
                            if (subJobNo.Length > 0)
                            {
                                insert_job_cost(rate, subJobNo, "", contNo, tripId, "", "", "", par.ToUpper(), n);
                            }
                        }
                    }
                    e.Result = "Success";
                    
                    #endregion
                }
            }
            catch { }
        }
    }
    private void insert_job_cost(C2.JobRate rate, string jobNo,string subJobNo, string contNo,string tripId, string contType, string contSize, string str,string type,int lineIndex)
    {
        C2.Job_Cost cost = new Job_Cost();
        cost.JobNo = jobNo;
        cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        cost.ExRate = new decimal(1.0);
        cost.ContNo = contNo;
        cost.GstType = rate.GstType;
        cost.ContType = contType;
        cost.ChgCode = rate.ChgCode;
        cost.TripNo = tripId;
		cost.LineSource = "M";
        cost.SubJobNo = subJobNo;
        if (rate.ChgCode.ToUpper().Equals("TRUCKING"))
        {
            cost.ChgCodeDe = rate.ChgCode;
        }
        else
        {
            cost.ChgCodeDe = rate.ChgCodeDe;
        }
        cost.Qty = rate.Qty;
        cost.Unit = rate.Unit;
        cost.LineType =type;
        cost.LocAmt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(rate.Price, 0), 2);
        cost.LineIndex = lineIndex;
        //if (contSize == str)
        // {
        cost.Price = rate.Price;
        Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
        Manager.ORManager.PersistChanges(cost);
       // }
        //if (contSize.Length == 0)
        //{
        //    cost.Price = rate.Price;
        //    Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
        //    Manager.ORManager.PersistChanges(cost);
        //}
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