using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_Tpt_SelectPage_TallySheet : System.Web.UI.Page
{
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        if (!IsPostBack) {
            txt_DocDt.Date = DateTime.Now;
            string sql = string.Format(@"select * from ");
            this.dsArInvoice.FilterExpression = "MastRefNo='"+ jobNo + "' and MastType='STORAGE'";
            cmb_CurrencyId.Text = SafeValue.SafeString(System.Configuration.ConfigurationManager.AppSettings["Currency"], "SGD");
            spin_ExRate.Value = 1;
            lbl_JobNo.Text = jobNo;
        }
        if (jobNo.Length > 0)
        {
            dsCosting.FilterExpression = "JobNo='" + jobNo + "' and LineType='STORAGE'";
        }
        count = this.grid.VisibleRowCount;
        OnLoad();
    }
    public DataTable GetData(string client, string date1, string date2, string hblNo, string lotNo, string sku, string warehouse, string location, string contNo)
    {
        string jobNo = SafeValue.SafeString(Request.QueryString["no"]);
        client = ConnectSql_mb.ExecuteScalar(string.Format(@"select PartyId from ctm_job where JobNo='{0}'", jobNo));
        DataTable tab = C2.JobHouse.getStockMove_cost(jobNo,client, date1, date2, hblNo, lotNo, sku, warehouse, location, contNo);
        return tab;
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
                    bool action = true;
                    string res = "";
                    string no = SafeValue.SafeString(Request.QueryString["no"]);
                    string type = SafeValue.SafeString(Request.QueryString["type"]);
                    if (action)
                    {
                        #region Create Inv
                        string docId = "";
                        if (list.Count > 0)
                        {
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
                            decimal exrate = SafeValue.SafeDecimal(spin_ExRate.Value);
                            string sql_att = string.Format(@"select ClientContact from ctm_job where JobNo='{0}'", jobNo);
                            string contact = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql_att));
                            string sql = string.Format(@"insert into XAArInvoice (DocType,DocDate,PartyTo,DocNo,AcYear,AcPeriod,Term,DocDueDate,Description,
CurrencyId,MastType,ExRate,ExportInd,CancelDate,CancelInd,UserId,EntryDate,Eta,AcCode,AcSource,MastRefNo,Contact)
values('IV','{5:yyyy-MM-dd}','{4}','{0}','{6}','{7}','{8}','{5:yyyy-MM-dd}','',
'{9}','STORAGE',{10},'N','19000101','N','{1}',getdate(),'17530101','{2}','DB','{3}','{11}')
select @@IDENTITY", invN, user, acCode, jobNo, client, dtime, dtime.Year, dtime.Month, term, currency, exrate, contact);
                            docId = ConnectSql_mb.ExecuteScalar(sql);
                            C2Setup.SetNextNo("", "AR-IV", invN, dtime);
                            string code = "";
                            for (int i = 0; i < list.Count; i++)
                            {
                                int id = list[i].id;
                                string chgCode = list[i].chgCode;
                                string chgcodeDes = list[i].chgCodedes;
                                if (IsCostCreated(id))
                                    C2.ComMethod.CreateInv(invN, id, docId, i,"",currency,exrate);
                                else
                                {
                                    if (list.Count - i > 1)
                                        code += chgcodeDes + " / ";
                                    else
                                        code += chgcodeDes;
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
                    else
                    {
                        e.Result = res;
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
                    string code = list[i].chgCode;
                    string des = list[i].chgCodedes;
                    int lineIndex = list[i].lineIndex;
                    string remark = list[i].remark;
                    string groupBy = list[i].groupBy;
                    decimal locAmt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
                    string sql = string.Format(@"update job_cost set ContNo='{0}',ContType='{1}',Price={2},Qty={3},LocAmt={4},Unit='{6}',ChgCode='{7}',ChgCodeDes='{8}',LineIndex={9},Remark='{10}',GroupBy='{11}' where Id={5}",
                        contNo, contType, price, qty, locAmt, id, unit, code, des, lineIndex, remark,groupBy);
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
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("SaveInvline"))
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);

               e.Result=Save_Inline(rowIndex,e);
            }
        }
    }
    private string Save_Inline(int i,DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox txt_Id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Id"], "txt_Id") as ASPxTextBox;
        ASPxTextBox txt_ContNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContNo"], "txt_ContNo") as ASPxTextBox;
        ASPxComboBox cbbContType = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ContType"], "cbbContType") as ASPxComboBox;
        ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
        ASPxTextBox txt_Unit = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Unit"], "txt_Unit") as ASPxTextBox;
        ASPxSpinEdit spin_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "spin_Qty") as ASPxSpinEdit;
        ASPxSpinEdit spin_LineIndex = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["LineIndex"], "spin_LineIndex") as ASPxSpinEdit;
        ASPxComboBox cbb_ChgCode = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgCode"], "cbb_ChgCode") as ASPxComboBox;
        ASPxTextBox txt_ChgCodeDe = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChgCodeDe"], "txt_ChgCodeDe") as ASPxTextBox;
        ASPxMemo txt_Remark = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Remark"], "txt_Remark") as ASPxMemo;
        ASPxTextBox txt_GroupBy = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GroupBy"], "txt_GroupBy") as ASPxTextBox;
        int id =SafeValue.SafeInt(txt_Id.Text,0);
        string contNo = "";
        string contType = "";
        decimal price = SafeValue.SafeDecimal(spin_Price.Value);
        decimal qty = SafeValue.SafeDecimal(spin_Qty.Value);
        string unit =SafeValue.SafeString(txt_Unit.Text);
        string code = SafeValue.SafeString(cbb_ChgCode.Text);
        string des = SafeValue.SafeString(txt_ChgCodeDe.Text);
        int lineIndex = SafeValue.SafeInt(spin_LineIndex.Value, 0);
        string remark = SafeValue.SafeString(txt_Remark.Text);
        decimal locAmt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
        string groupBy= SafeValue.SafeString(txt_GroupBy.Text);
        string sql = string.Format(@"update job_cost set ContNo='{0}',ContType='{1}',Price={2},Qty={3},LocAmt={4},Unit='{6}',ChgCode='{7}',ChgCodeDes='{8}',LineIndex={9},Remark='{10}',GroupBy='{11}' where Id={5}",
            contNo, contType, price, qty, locAmt, id, unit, code, des, lineIndex, remark,groupBy);
        ConnectSql.ExecuteSql(sql);
        return "Success";
    }
    
    #endregion
    #region Create Inv
    List<Record> list = new List<Record>();
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
        public Record(int _id, string _contNo, string _contType, decimal _price, decimal _qty, string _chgCode, string _chgCodeDes, string _unit, int _lineIndex, string _remark, string _groupBy)
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
            ASPxMemo txt_Remark = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Remark"], "txt_Remark") as ASPxMemo;
            ASPxTextBox txt_GroupBy = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["GroupBy"], "txt_GroupBy") as ASPxTextBox;
            if (id != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(SafeValue.SafeInt(id.Text, 0), "", "", SafeValue.SafeDecimal(spin_Price.Value), SafeValue.SafeDecimal(spin_Qty.Value),
                    SafeValue.SafeString(cbb_ChgCode.Value), txt_ChgCodeDe.Text, txt_Unit.Text, SafeValue.SafeInt(spin_LineIndex.Value, 0), txt_Remark.Text, txt_GroupBy.Text));

            }
            else if (id == null)
                break;
        }
    }
    public DataTable GetCont()
    {
        DataTable tab = null;
        if (Request.QueryString["no"] != null)
        {
            string no = Request.QueryString["no"].ToString();
            string sql = string.Format(@"select ContainerNo,ContainerType,ScheduleDate,StatusCode from ctm_jobdet1 where JobNo='{0}'", no);
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
            string sql = string.Format(@"select TripIndex,TripCode,ContainerNo,Statuscode,RequestVehicleType,FromCode,ToCode from ctm_jobdet2 where JobNo='{0}'", no);
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
    public bool IsCostCreated(object id)
    {
        bool res = false;
        string sql = string.Format(@"select count(*) from XAArInvoiceDet where JobCostId={0}", id);
        int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
        if (n == 0)
        {
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