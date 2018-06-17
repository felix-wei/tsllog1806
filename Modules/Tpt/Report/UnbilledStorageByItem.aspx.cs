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

public partial class Modules_Tpt_Report_UnbilledStorageByItem : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        if (!IsPostBack)
        {

            txt_DocDt.Date = DateTime.Today;
            string type = SafeValue.SafeString(Request.QueryString["type"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            string contType = SafeValue.SafeString(Request.QueryString["contType"]);
            lbl_JobNo.Text = no;
            lbl_Client.Text = client;
            lbl_Type.Text = type;
            if (this.grid.VisibleRowCount > 0)
            {
                btn_CreateInv.Enabled = false;
            }
            txt_search_dateFrom.Date = DateTime.Now.AddDays(-7);
            txt_search_dateTo.Date = DateTime.Now;
        }
        count = this.grid.VisibleRowCount;
        OnLoad();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid.AddNewRow();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        string sql = string.Format(@"select c.*,(select top 1 Name from xxparty where PartyId=job.ClientId) as Name from job_cost c inner join CTM_Job job on c.JobNo=job.JobNo where LineType='STORAGE' and LineId=0");
        string From = txt_search_dateFrom.Date.ToString("yyyy-MM-dd");
        string To = txt_search_dateTo.Date.AddDays(1).ToString("yyyy-MM-dd");
        string jobNo = txt_JobNo.Text;
        string client = btn_ClientId.Text;
        if (jobNo.Length > 0)
            where = GetWhere(where, string.Format(" and c.JobNo like '%{0}%'", jobNo));
       if(client.Length>0)
            where = GetWhere(where, string.Format(" and job.ClientId like '%{0}%'", client));
        else
            where =GetWhere(where,string.Format(" and c.RowCreateTime between '{0}' and '{1}'",From,To));

        if (where.Length > 0)
            sql = sql + where;
       
        this.grid.DataSource = ConnectSql.GetTab(sql);
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
    #region Costing
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.Job_Cost));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["VendorId"] = " ";
        e.NewValues["ChgCode"] = " ";
        e.NewValues["ChgCodeDe"] = " ";
        e.NewValues["Remark"] = " ";
        e.NewValues["CurrencyId"] = "SGD";
        e.NewValues["ExRate"] = 1;
        e.NewValues["JobNo"] = SafeValue.SafeString(Request.QueryString["no"]);
        e.NewValues["Price"] = 0;
        e.NewValues["Qty"] = 0;
        e.NewValues["LineType"] = "CONT";

    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string code = SafeValue.SafeString(e.NewValues["ChgCode"]);
        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes like '%{0}%'", code);
        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
        decimal gst = 0;
        string gstType = "";
        string chgTypeId = "";
        if (dt_chgCode.Rows.Count > 0)
        {
            gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
            gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
            chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
        }
        e.NewValues["JobNo"] = SafeValue.SafeString(Request.QueryString["no"]);
        decimal price = SafeValue.SafeDecimal(e.NewValues["Price"]);
        decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["LineType"] = SafeValue.SafeString(e.NewValues["LineType"]);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["ContType"] = SafeValue.SafeString(e.NewValues["ContType"]);
    }
    protected void grid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {


    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string code = SafeValue.SafeString(e.NewValues["ChgCode"]);
        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes like '%{0}%'", code);
        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
        decimal gst = 0;
        string gstType = "";
        string chgTypeId = "";
        if (dt_chgCode.Rows.Count > 0)
        {
            gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
            gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
            chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
        }
        decimal price = SafeValue.SafeDecimal(e.NewValues["Price"]);
        decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
        e.NewValues["LocAmt"] = locAmt;
        e.NewValues["LineType"] = SafeValue.SafeString(e.NewValues["LineType"]);
        e.NewValues["ChgCodeDe"] = SafeValue.SafeString(e.NewValues["ChgCodeDe"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["ContNo"] = SafeValue.SafeString(e.NewValues["ContNo"]);
        e.NewValues["ContType"] = SafeValue.SafeString(e.NewValues["ContType"]);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        //ASPxPageControl pageControl = this.grid_job.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxTextBox refNo = pageControl.FindControl("txt_RefN") as ASPxTextBox;

    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string par = e.Parameters;
        string[] ar = par.Split('_');
        if (par == "OK")
        {
            #region Create Inv

            try
            {
                if (list.Count > 0)
                {
                    string invN = "";
                    for (int i = 0; i < list.Count; i++)
                    {
                        string jobNo = list[i].jobNo;
                        string client = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select PartyId from ctm_job where JobNo='{0}'",jobNo)));
                        string user = HttpContext.Current.User.Identity.Name;
                        string acCode = EzshipHelper.GetAccArCode("", "SGD");
                        DateTime dtime = txt_DocDt.Date;
                        string currency = "SGD";
                        decimal exrate = 1;
                        string termId = EzshipHelper.GetTerm(client);
                        string term = EzshipHelper.GetTermCode(termId);
                        string sql = string.Format(@"select SequenceId,DocNo from XAArInvoice where MastRefNo='{0}' and datediff(d,'{1}',DocDate)>=0 and PartyTo='{2}'",jobNo,dtime,client);
                        DataTable tab =ConnectSql.GetTab(sql);
                        string docId = "";
                        if (tab.Rows.Count == 0)
                        {
                            invN = C2Setup.GetNextNo("", "AR-IV", dtime);
                            sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV','{5:yyyy-MM-dd}','{4}','{0}','{6}','{7}','{8}','{5:yyyy-MM-dd}','',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, jobNo, client, dtime, dtime.Year, dtime.Month, term);
                            docId = ConnectSql_mb.ExecuteScalar(sql);
                            C2Setup.SetNextNo("", "AR-IV", invN, dtime);
                        }
                        else {
                            invN = tab.Rows[0]["DocNo"].ToString();
                            docId = tab.Rows[0]["SequenceId"].ToString();
                        }
                        int id = list[i].id;
                        

                       C2.ComMethod.CreateInv(invN, id, docId,0,"",currency,exrate);

                    }
                    e.Result = invN;
                }
                else
                {
                    e.Result = "Action Error!Please keyin select cost ";
                }


            }
            catch { }
            
            #endregion
        }
        if (par == "Update")
        {
            #region Update Inv
            if (Request.QueryString["no"] != null)
            {
                try
                {
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    if (list.Count > 0)
                    {
                        string invN = lbl_DocNo.Text;
                        string sql = string.Format(@"select SequenceId from  XAArInvoice where DocNo='{0}'", invN);
                        string docId = ConnectSql_mb.ExecuteScalar(sql);
                        for (int i = 0; i < list.Count; i++)
                        {
                            int id = list[i].id;
                            CreateInv(invN, id, docId);
                        }
                        e.Result = "Success";
                    }
                    else
                    {
                        e.Result = "Please keyin select cost ";
                    }
                }
                catch { }
            }
            #endregion
        }
        if (par == "Save")
        {
            #region Save All
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string contNo = list[i].contNo;
                    string contType = list[i].contType;
                    decimal price = list[i].price;
                    decimal qty = list[i].qty;
                    string unit = list[i].unit;
                    string groupBy = list[i].groupBy;
                    decimal locAmt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                    string sql = string.Format(@"update job_cost set ContNo='{0}',ContType='{1}',Price={2},Qty={3},LocAmt={4},Unit='{6}',GroupBy='{7}' where Id={5}", contNo, contType, price, qty, locAmt, id,unit,groupBy);
                    ConnectSql.ExecuteSql(sql);
                }
                e.Result = "Save Success";
            }
            else
            {
                e.Result = "Please keyin select cost ";
            }
            #endregion
        }
        if (par == "Delete")
        {
            #region
            if (list.Count > 0)
            {
                string no = SafeValue.SafeString(Request.QueryString["no"]);
                string sql = string.Format(@"delete from job_cost where JobNo='{0}'", no);
                ConnectSql.ExecuteSql(sql);
                e.Result = "Action Success";
            }
            else
            {
                e.Result = "Please keyin select cost ";
            }
            #endregion
        }
        if (par.Length >= 2) {
            if (ar[0].Equals("Tallysheet"))
            {
                ASPxGridView grid = sender as ASPxGridView;

                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxTextBox txt_JobNo = grid.FindRowCellTemplateControl(rowIndex, null, "txt_JobNo") as ASPxTextBox;
                e.Result = txt_JobNo.Text+"_"+ rowIndex;
            }
        }
    }
    #endregion
    #region Create Inv
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int id = 0;
        public string jobNo = "";
        public string contNo = "";
        public string contType = "";
        public decimal price = 0;
        public decimal qty = 0;
        public string unit = "";
        public string groupBy = "";
        public Record(int _id,string _jobNo, string _contNo, string _contType, decimal _price, decimal _qty,string _unit,string _groupBy)
        {
            id = _id;
            jobNo = _jobNo;
            contNo = _contNo;
            contType = _contType;
            price = _price;
            qty = _qty;
            unit = _unit;
            groupBy = _groupBy;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = count;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "ack_IsPay") as ASPxCheckBox;

            ASPxTextBox id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxLabel txt_ContNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContNo"], "lbl_ContNo") as ASPxLabel;
            ASPxLabel lbl_JobNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["JobNo"], "lbl_JobNo") as ASPxLabel;
            ASPxLabel lbl_ContType = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContType"], "lbl_ContType") as ASPxLabel;
            ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxTextBox txt_Unit = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Unit"], "txt_Unit") as ASPxTextBox;
            ASPxTextBox txt_GroupBy = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GroupBy"], "txt_GroupBy") as ASPxTextBox;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0),lbl_JobNo.Text,"", "", SafeValue.SafeDecimal(spin_Price.Value), SafeValue.SafeDecimal(spin_Qty.Value),txt_Unit.Text,txt_GroupBy.Text));

            }
            else if (id == null)
                break;
        }
    }
    private void CreateInv(string invN, int id, string docId)
    {
        string sql = string.Format(@"select * from job_cost where Id={0}", id);
        DataTable dt = ConnectSql.GetTab(sql);
        string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType,JobCostId,ChgDes4)
values");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sql = "";
            decimal qty = SafeValue.SafeDecimal(dt.Rows[i]["Qty"]);
            string chgCodeId = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
            string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
            decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
            string cntNo = SafeValue.SafeString(dt.Rows[i]["ContNo"]);
            string cntType = SafeValue.SafeString(dt.Rows[i]["ContType"]);
            string jobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
            string billType = "";
            string remark = SafeValue.SafeString(dt.Rows[i]["Remark"]);
            string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where chgCodeId='{0}'", chgCodeId);
            DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
            decimal gst = 0;
            string gstType = "";
            string chgTypeId = "";
            if (dt_chgCode.Rows.Count > 0)
            {
                gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                chgCodeDes = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgCodeDes"]);
            }
            decimal amt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
            if (chgCodeDes.ToUpper().Equals("TRUCKING"))
            {
                chgCodeDes += " " + billType.ToUpper();
            }
            string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','{14}',1,{5},'{9}','SGD',1,{10},{11},{12},{13},0,'{6}','{7}','{8}',{15},'{16}') select @@IDENTITY ", docId, invN, i + 1, chgCodeId, chgCodeDes, price, jobNo, cntNo, chgTypeId, cntType, gst, gstAmt, docAmt, locAmt, gstType, id,remark);
            sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
        }
        if (sql.Length > 0)
        {
            sql = sql_part1 + sql;
            int re = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);

            UpdateMaster(SafeValue.SafeInt(docId, 0));
            UpdateLineId(id, re);
        }
    }
    private void UpdateLineId(int id, int lineId)
    {
        string sql = string.Format(@"update job_cost set LineId={1} where Id={0}", id, lineId);
        ConnectSql.ExecuteSql(sql);
    }
    private void UpdateMaster(int docId)
    {
        string sql = string.Format("update XaArInvoiceDet set LineLocAmt=locAmt* (select ExRate from XAArInvoice where SequenceId=XaArInvoiceDet.docid) where DocId='{0}'", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAArInvoiceDet where DocId='{0}'", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "CR")
            {
                docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
            else
            {
                docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
        }


        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT sum(det.DocAmt)
FROM  XAArReceiptDet AS det INNER JOIN XAArReceipt AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        balAmt += SafeValue.SafeDecimal(Manager.ORManager.GetDataSet(string.Format(@"SELECT sum(det.DocAmt) 
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='IV' or det.DocType='DN')", docId)), 0);

        sql = string.Format("Update XAArInvoice set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion
}