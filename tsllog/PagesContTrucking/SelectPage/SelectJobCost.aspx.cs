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
        string quoteNo = SafeValue.SafeString(Request.QueryString["quoteNo"]);
        if(!IsPostBack){

            txt_DocDt.Date = DateTime.Today;

            string type = SafeValue.SafeString(Request.QueryString["type"]);
            string client = SafeValue.SafeString(Request.QueryString["client"]);
            string contType = SafeValue.SafeString(Request.QueryString["contType"]);
            lbl_JobNo.Text = no;
            lbl_Client.Text = client;
            lbl_Type.Text = type;
            lbl_QuotedNo.Text = quoteNo;
            cmb_CurrencyId.Text =SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["Currency"],"SGD");
            spin_ExRate.Value = 1;
            string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}'", no);
            string docNo = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
            if (docNo.Length > 0)
            {
                lbl_DocNo.Text = docNo;
            }
        }
        if (no.Length > 0)
        {
            dsCosting.FilterExpression = "(JobNo='" + no + "'or JobNo='" + quoteNo + "') and LineType!='DP' ";
        }
        count = this.grid.VisibleRowCount;
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
        string quoteNo = SafeValue.SafeString(Request.QueryString["quoteNo"]);
        //string sql = string.Format(@"seelct Id,LineType,LineStatus,ChgCode,ChgCodeDe,Remark,ContNo,ContType,Price from job_cost c inner join ctm_job j");
        if (no.Length > 0)
        {
            dsCosting.FilterExpression = "JobNo='" + no + "' or JobNo='" + quoteNo + "'";
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
        decimal qty = SafeValue.SafeDecimal(e.NewValues["Qty"]);
        decimal amt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
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
            if (Request.QueryString["no"] != null)
            {
                try
                {
                    bool action = false;
                    string res = "";
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    string type = SafeValue.SafeString(Request.QueryString["type"]);
                    if (type == "IMP" || type == "EXP")
                    {
                        string sql_c = string.Format(@"select count(*) from ctm_jobdet1 where JobNo='{0}' and StatusCode!='Completed'", no);
                        int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_c), 0);
                        n = 0;
						if (n == 0)
                            action = true;
                        else
                            res= "Action Error!Not Completed Container,Can not Create Invoice";
                    }
                    else {
                        action = true;
                    }
                    if (action)
                    {
                        #region Create Inv
                        string docId = "";
                        if (list.Count > 0)
                        {
                            string billType = SafeValue.SafeString(cbb_BillType.Value);
                            string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
                            string client = SafeValue.SafeString(Request.QueryString["client"]);
                            string contType = SafeValue.SafeString(Request.QueryString["contType"]);
                            string user = HttpContext.Current.User.Identity.Name;
                            string acCode = EzshipHelper.GetAccArCode("", "SGD");
                            DateTime dtime = txt_DocDt.Date;
                            string invN = C2Setup.GetNextNo("", "AR-IV", dtime);
                            string termId = EzshipHelper.GetTerm(client);
                            string term = EzshipHelper.GetTermCode(termId);
                            string currency = cmb_CurrencyId.Text;
                            decimal exrate =SafeValue.SafeDecimal(spin_ExRate.Value);
                            string sql_att = string.Format(@"select ClientContact from ctm_job where JobNo='{0}'",jobNo);
                            string contact = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_att));
							
        string[] currentPeriod = EzshipHelper.GetAccPeriod(dtime);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];
							
                            string sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo,Contact)
values('IV','{5:yyyy-MM-dd}','{4}','{0}','{6}','{7}','{8}','{5:yyyy-MM-dd}','',
'{9}','CTM',{10},'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}','{11}')
select @@IDENTITY", invN, user, acCode, jobNo, client, dtime, acYear, acPeriod, term,currency,exrate, contact);
                            docId = ConnectSql_mb.ExecuteScalar(sql);
                            C2Setup.SetNextNo("", "AR-IV", invN, dtime);
                            string code = "";
                            for (int i = 0; i < list.Count; i++)
                            {
                                int id = list[i].id;
                                string chgCode = list[i].chgCode;
                                string chgcodeDes = list[i].chgCodedes;
                                if (IsCostCreated(id))
                                   C2.ComMethod.CreateInv(invN, id, docId, i, billType,currency,exrate);
                                else
                                {
                                    if (list.Count - i > 1)
                                        code += chgcodeDes + " / ";
                                    else
                                        code += chgcodeDes;
                                }
                            }
                            for (int i = 0; i < list1.Count; i++)
                            {
                                int id = list1[i].id;
                                string chgCode = list[i].chgCode;
                                if (IsCostCreated(id))
                                    C2.ComMethod.CreateWhInv(invN, id, docId);
                                else
                                {
                                    if (list.Count - i > 1)
                                        code += chgCode + " / ";
                                    else
                                        code += chgCode;
                                }
                            }
                            C2.XAArInvoice.update_invoice_mast(SafeValue.SafeInt(docId, 0));
                            if (code.Length == 0)
                                e.Result = invN;
                            else
                                e.Result = "Action Error!" + code + " already exist !";
                        }
                        else
                        {
                            e.Result = "Action Error!Please keyin select cost ";
                        }
                        #endregion

                        string userId = HttpContext.Current.User.Identity.Name;
                        C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                        elog.Platform_isWeb();
                        elog.Controller = userId;
                        elog.ActionLevel_isINVOICE(SafeValue.SafeInt(docId, 0));
                        elog.setActionLevel(SafeValue.SafeInt(docId, 0), CtmJobEventLogRemark.Level.Invoice, 3);
                        elog.log();
                    }
                   else {
                       e.Result = res;
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
                        string currency = cmb_CurrencyId.Text;
                        decimal exrate = SafeValue.SafeDecimal(spin_ExRate.Value);
                        string code = "";
                        for (int i = 0; i < list.Count; i++)
                        {
                            int id = list[i].id;
                            string chgCode=list[i].chgCode;
                            string chgcodeDes = list[i].chgCodedes;
                            if (IsCostCreated(id))
                                C2.ComMethod.CreateInv(invN, id, docId,i,"",currency,exrate);
                            else
                            {
                                if(list.Count-i>1)
                                  code += chgcodeDes + " / ";
                                else
                                    code += chgcodeDes;
                            }
                        }
                        if (code.Length == 0)
                            e.Result =invN;
                        else
                            e.Result = "Action Error!" + code + " already exist !";
                    }
                    else
                    {
                        e.Result = "Action Error!Please keyin select cost ";
                    }
                }
                catch(Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
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
                    string unit = list[i].unit;
                    string code = list[i].chgCode;
                    string des = list[i].chgCodedes;
                    int lineIndex = list[i].lineIndex;
                    string remark = list[i].remark;
                    string groupBy = list[i].groupBy;
                    decimal locAmt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
                    string sql = string.Format(@"update job_cost set ContNo='{0}',ContType='{1}',Price={2},Qty={3},LocAmt={4},Unit='{6}',ChgCode='{7}',ChgCodeDes='{8}',LineIndex={9},Remark='{10}',GroupBy='{11}' where Id={5}", 
                        contNo, contType, price, qty, locAmt, id, unit,code,des,lineIndex,remark,groupBy);
                    ConnectSql.ExecuteSql(sql);

                }
                e.Result = "Save Success";
            }
            else
            {
                e.Result = "Action Error!Please keyin select cost ";
            }
            #endregion
        }
        if(par=="Delete")
        {
            #region 
            if (list.Count > 0)
            {
                bool action = false;
                string no = SafeValue.SafeString(Request.QueryString["no"]);
                for (int i = 0; i < list.Count; i++)
                {
                    int id = list[i].id;
                    string sql = string.Format(@"select LineSource from job_cost where Id={0}", id);
                    string status=SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
                    if (status != "S")
                    {
                        action = true;
                        sql = string.Format(@"delete from job_cost where Id={0}", id);
                        ConnectSql.ExecuteSql(sql);
                    }
                    else {
                        e.Result = "this can not delete ";
                    }
                }
                if(action)
                  e.Result = "Action Success";
            }
            else
            {
                e.Result = "Please keyin select cost ";
            }
            #endregion
        }
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("ChargeUpdateline"))
            {
                Update_Inline(e);
            }
        }
    }
    private void Update_Inline(DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string billType = SafeValue.SafeString(cbb_BillType.Value);
        string client = SafeValue.SafeString(Request.QueryString["client"]);
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        string sql = string.Format(@"select Price,ChgCode,ContType from job_rate where BillType='{0}' and ClientId='{1}'",billType,client);
        DataTable tab_dt = ConnectSql_mb.GetDataTable(sql);
        for (int i = 0; i < tab_dt.Rows.Count; i++)
        {
            #region
            string chgCode=SafeValue.SafeString(tab_dt.Rows[i]["ChgCode"]);
            string contType = SafeValue.SafeString(tab_dt.Rows[i]["ContType"]);
            decimal price = SafeValue.SafeDecimal(tab_dt.Rows[i]["Price"]);
            string sql_cost="";
            if (contType == "20HD" || contType == "40HD")
            {
                sql_cost = string.Format(@"select Id from job_cost where JobNo='{0}' and ChgCode='{1}' and ContType='{2}'", jobNo, chgCode,contType);
            }
            else if ((contType != "20HD" || contType != "40HD") && contType.Length > 0)
            {
                if (contType.Substring(0, 2) == "20")
                {
                    sql_cost = string.Format(@"select Id from job_cost where JobNo='{0}' and ChgCode='{1}' and ContType!='20HD'", jobNo, chgCode);
                }
                if (contType.Substring(0, 2) == "40")
                {
                    sql_cost = string.Format(@"select Id from job_cost where JobNo='{0}' and ChgCode='{1}' and ContType!='40HD'", jobNo, chgCode);
                }
            }
            else {
                sql_cost = string.Format(@"select Id from job_cost where JobNo='{0}' and ChgCode='{1}'", jobNo, chgCode);
            }
            DataTable dt_cost = ConnectSql.GetTab(sql_cost);
            for (int a = 0; a < dt_cost.Rows.Count; a++)
            {
                int id = SafeValue.SafeInt(dt_cost.Rows[a]["Id"],0);

                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                sql = string.Format(@"update job_cost set Price=@Price,LocAmt=1*@Price where Id=@Id");
                #region list
                list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Id", id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@Price", price, SqlDbType.Decimal));
                if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
                {
                    e.Result = "Save Success";
                }
                else
                {
                    e.Result = "Save Error";
                }
                #endregion
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
        public string chgCode = "";
        public string chgCodedes = "";
        public string contType = "";
        public decimal price = 0;
        public decimal qty = 0;
        public string unit = "";
        public int lineIndex = 0;
        public string remark = "";
        public string groupBy = "";
        public Record(int _id,string _contNo,string _contType,decimal _price,decimal _qty,string _chgCode,string _chgCodeDes,string _unit,int _lineIndex,string _remark,string _groupBy)
        {
            id = _id;
            contNo = _contNo;
            contType = _contType;
            price = _price;
            qty = _qty;
            chgCode = _chgCode;
            chgCodedes = _chgCodeDes;
            unit = _unit;
            lineIndex = _lineIndex;
            remark = _remark;
            groupBy = _groupBy;
        }
    }
    internal class Record1
    {
        public int id = 0;
        public string chgCode = "";
        public Record1(int _id,string _chgCode)
        {
            id = _id;
            chgCode = _chgCode;
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
            ASPxTextBox txt_Unit = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Unit"], "txt_Unit") as ASPxTextBox;
            ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
            ASPxSpinEdit spin_LineIndex = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["LineIndex"], "spin_LineIndex") as ASPxSpinEdit;
            ASPxComboBox cbb_ChgCode = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgCode"], "cbb_ChgCode") as ASPxComboBox;
            ASPxTextBox txt_ChgCodeDe = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgCodeDe"], "txt_ChgCodeDe") as ASPxTextBox;
            ASPxTextBox txt_Remark = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Remark"], "txt_Remark") as ASPxTextBox;
            ASPxTextBox txt_GroupBy = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GroupBy"], "txt_GroupBy") as ASPxTextBox;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), txt_ContNo.Text, SafeValue.SafeString(cbbContType.Value), SafeValue.SafeDecimal(spin_Price.Value), SafeValue.SafeDecimal(spin_Qty.Value), 
                    SafeValue.SafeString(cbb_ChgCode.Value), txt_ChgCodeDe.Text, txt_Unit.Text,SafeValue.SafeInt(spin_LineIndex.Value,0),txt_Remark.Text,txt_GroupBy.Text));

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
            ASPxLabel lbl_ChgCode = this.grid_Cost.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Cost.Columns["ChgCodeDes"], "lbl_ChgCode") as ASPxLabel;

            if (id != null && isOk != null && isOk.Checked)
            {
                list1.Add(new Record1(SafeValue.SafeInt(id.Text, 0),lbl_ChgCode.Text));
            }
            else if (id == null)
                break;
        }
    }
    
    public DataTable GetCont() {
        DataTable tab = null;
        if (Request.QueryString["no"] != null)
        {
            string no = Request.QueryString["no"].ToString();
            string sql = string.Format(@"select ContainerNo,ContainerType,Weight,ScheduleDate,StatusCode from ctm_jobdet1 where JobNo='{0}'",no);
            tab = ConnectSql.GetTab(sql);
        }
        return tab;
    }
    public DataTable GetTrip()
    {
        DataTable tab = null;
        if (Request.QueryString["no"] != null)
        {
            string no = Request.QueryString["no"].ToString();
            string sql = string.Format(@"select TripIndex,TripCode,BillingRemark,ContainerNo,Statuscode,RequestVehicleType,FromCode,ToCode from ctm_jobdet2 where JobNo='{0}'", no);
            tab = ConnectSql.GetTab(sql);
        }
        return tab;
    }
    public string VilaStatus(string status)
    {
        string strStatus = "";
        if (status == "P")
        {
            strStatus = "Pending";
        }
        if (status == "C")
        {
            strStatus = "Completed";
        }
        if (status == "X")
        {
            strStatus = "Cancel";
        }
        return strStatus;
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
    public bool IsCostCreated(object id)
    {
        bool res = false;
        string sql = string.Format(@"select count(*) from XAArInvoiceDet where JobCostId={0}",id);
        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql),0);
        if (n == 0) {
            res = true;
        }
        return res;
    }
    public string DocNo(object id)
    {
        string sql = string.Format(@"select DocNo from XAArInvoiceDet where JobCostId={0}", id);
        return SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
    }
}