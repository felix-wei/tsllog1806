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

public partial class PagesContTrucking_SelectPage_SelectJobCost : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        if(!IsPostBack){

            txt_DocDt.Date = DateTime.Today.AddDays(-7);
            
            string type = SafeValue.SafeString(Request.QueryString["type"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            string contType = SafeValue.SafeString(Request.QueryString["contType"]);
            lbl_JobNo.Text = no;
            lbl_Client.Text = client;
            lbl_Type.Text = type;
            string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}'", no);
            string docNo = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
            if (docNo.Length > 0)
            {
                btn_CreateInv.Enabled = false;
                lbl_DocNo.Text = docNo;
            }
            else {
                btn_UpdateInv.Enabled = false;
            }
        }
        if (no.Length > 0)
        {
            dsCosting.FilterExpression = "JobNo='" + no + "' and LineType!='DP'";
            count = this.grid.VisibleRowCount;
        }
        OnLoad();
        OnLoad1();
        Bind();
    }
    private void Bind() {
        string no = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = "select * from Wh_Costing c inner join Wh_DO mast on c.RefNo=mast.DoNo inner join CTM_Job j on j.JobNo=mast.JobNo where j.JobNo='" + no + "'";
        DataTable dt=ConnectSql.GetTab(sql);
        int count = dt.Rows.Count;
        grid_Cost.DataSource = dt;
        grid_Cost.DataBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        this.grid.AddNewRow();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string no = SafeValue.SafeString(Request.QueryString["no"]);
        //string sql = string.Format(@"seelct Id,LineType,LineStatus,ChgCode,ChgCodeDe,Remark,ContNo,ContType,Price from job_cost c inner join ctm_job j");
        if (no.Length > 0)
        {
            dsCosting.FilterExpression = "JobNo='" + no + "'";
        }
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
        if (par == "OK")
        {
            #region Create Inv
            if (Request.QueryString["no"] != null)
            {
                try
                {
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    if (list.Count > 0)
                    {
                        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
                        string type = SafeValue.SafeString(Request.QueryString["type"]);
                        string client = SafeValue.SafeString(Request.QueryString["client"]);
                        string contType = SafeValue.SafeString(Request.QueryString["contType"]);
                        string user = HttpContext.Current.User.Identity.Name;
                        string acCode = EzshipHelper.GetAccArCode("", "SGD");
                        DateTime dtime = txt_DocDt.Date;
                        string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
                        string termId = EzshipHelper.GetTerm(client);
                        string term = EzshipHelper.GetTermCode(termId);
                        string sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo)
values('IV','{5:yyyy-MM-dd}','{4}','{0}','{6}','{7}','{8}','{5:yyyy-MM-dd}','',
'SGD','CTM',1,'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}')
select @@IDENTITY", invN, user, acCode, jobNo, client, dtime, dtime.Year, dtime.Month, term);
                        string docId = ConnectSql_mb.ExecuteScalar(sql);
                        C2Setup.SetNextNo("", "AR-IV", invN, dtime);

                        for (int i = 0; i < list.Count; i++)
                        {
                            int id = list[i].id;
                            
                            CreateInv(invN, id, docId);
                           ;
                        }
                        for (int i = 0; i < list1.Count;i++ )
                        {
                            int id = list1[i].id;
                            CreateWhInv(invN, id, docId);
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
        if(par=="Save")
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
                    decimal locAmt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                    string sql = string.Format(@"update job_cost set ContNo='{0}',ContType='{1}',Price={2},Qty={3},LocAmt={4} where Id={5}",contNo,contType,price,qty,locAmt,id);
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
        if(par=="Delete")
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
    }
    #endregion
    #region Create Inv
    List<Record> list = new List<Record>();
    List<Record1> list1 = new List<Record1>();
    internal class Record
    {
        public int id = 0;
        public string contNo = "";
        public string contType = "";
        public decimal price = 0;
        public decimal qty = 0;
        public Record(int _id,string _contNo,string _contType,decimal _price,decimal _qty)
        {
            id = _id;
            contNo = _contNo;
            contType = _contType;
            price = _price;
            qty = _qty;
        }
    }
    internal class Record1
    {
        public int id = 0;
        public Record1(int _id)
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
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "ack_IsPay") as ASPxCheckBox;

            ASPxTextBox id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "txt_Id") as ASPxTextBox;
            ASPxTextBox txt_ContNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContNo"], "txt_ContNo") as ASPxTextBox;
            ASPxComboBox cbbContType = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContType"], "cbbContType") as ASPxComboBox;
            ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;

            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), txt_ContNo.Text, SafeValue.SafeString(cbbContType.Value), SafeValue.SafeDecimal(spin_Price.Value), SafeValue.SafeDecimal(spin_Qty.Value)));

            }
            else if (id == null)
                break;
        }
    }
    private void OnLoad1() {
        int start = 0;
        int end = 100;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isOk= this.grid_Cost.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Cost.Columns["Id"], "ack_IsOk") as ASPxCheckBox;
            ASPxTextBox id = this.grid_Cost.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Cost.Columns["Id"], "txt_wh_Id") as ASPxTextBox;
            if (id != null && isOk != null && isOk.Checked)
            {
                list1.Add(new Record1(SafeValue.SafeInt(id.Text, 0)));
            }
            else if (id == null)
                break;
        }
    }
    private void CreateInv(string invN,int id,string docId) {
        string sql = string.Format(@"select * from job_cost where Id={0}", id);
        DataTable dt = ConnectSql.GetRemoteTab(sql);
        string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType,JobCostId)
values");
        for (int i = 0; i < dt.Rows.Count;i++ )
        {
            sql = "";
            string chgCodeId = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
            string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
            decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
            string cntNo = SafeValue.SafeString(dt.Rows[i]["ContNo"]);
            string cntType = SafeValue.SafeString(dt.Rows[i]["ContType"]);
            string jobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
            string billType =SafeValue.SafeString(cbb_BillType.Text,"");
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
            decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
            if (chgCodeDes.ToUpper().Equals("TRUCKING"))
            {
                chgCodeDes += " " + billType.ToUpper();
            }
            string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','{14}',1,{5},'{9}','SGD',1,{10},{11},{12},{13},0,'{6}','{7}','{8}',{15}) select @@IDENTITY ", docId, invN, i + 1, chgCodeId, chgCodeDes, price, jobNo, cntNo, chgTypeId, cntType, gst, gstAmt, docAmt, locAmt, gstType, id);
            sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
        }
        if (sql.Length > 0)
        {
            sql = sql_part1 + sql;
            int re =SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);

            UpdateMaster(SafeValue.SafeInt(docId, 0));
            UpdateLineId(id,re);
        }
    }
    private void UpdateLineId(int id,int lineId) {
        string sql = string.Format(@"update job_cost set LineId={1} where Id={0}",id,lineId);
        ConnectSql.ExecuteSql(sql);
    }
    private void CreateWhInv(string invN, int id, string docId)
    {
        string sql = string.Format(@"select c.*,det3.ContainerNo,det3.ContainerType from Wh_Costing c left join wh_dodet3 det3 on c.RefNo=det3.DoNo where c.Id={0}", id);
        DataTable dt = ConnectSql.GetRemoteTab(sql);
        string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sql = "";
            string chgCodeId = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
            string cheCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
            decimal price = SafeValue.SafeDecimal(dt.Rows[i]["CostLocAmt"]);
            string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
            string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);
            string jobNo = SafeValue.SafeString(dt.Rows[i]["RefNo"]);
            string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes='{0}'", chgCodeId);
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
            decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
            decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
            string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','{14}',1,{5},'{9}','SGD',1,{10},{11},{12},{13},0,'{6}','{7}','{8}')", docId, invN, i + 1, chgCodeId, cheCodeDes, price, jobNo, cntNo, chgTypeId, cntType, gst, gstAmt, docAmt, locAmt, gstType);
            sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
        }
        if (sql.Length > 0)
        {
            sql = sql_part1 + sql;
            int re = ConnectSql.ExecuteSql(sql);

            UpdateMaster(SafeValue.SafeInt(docId, 0));

        }
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
    protected void grid_Cost_DataSelect(object sender, EventArgs e)
    {
        //string no = SafeValue.SafeString(Request.QueryString["no"]);
        //string sql = "select * from Wh_Costing c inner join Wh_DO mast on c.RefNo=mast.DoNo inner join CTM_Job j on j.JobNo=mast.JobNo where j.JobNo='" + no + "'";
        //grid_Cost.DataSource = ConnectSql_mb.GetDataTable(sql);
        //grid_Cost.DataBind();
        //this.dsWhCosting.FilterExpression = "RefNo='" + SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "0") + "'";
    }
    protected void grid_Cost_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.WhCosting));
        }
    }
}