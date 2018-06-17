using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;
public partial class ReportWarehouse_Report_HandlingReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_CutOffDate.Date = DateTime.Today;
        }
        else
        {
            OnLoad();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select distinct * from(select distinct mast.DoNo as No,mast.DoNo,CONVERT(VARCHAR(10),mast.DoDate,110) as DoDate,CONVERT(int,tab_Qty.TotalQty) as Qty,StatusCode,
isnull(tab_Amt.TotalAmt,0) as TotalAmt,dbo.fun_GetPartyName(mast.PartyId) AS PartyName,mast.PartyId,
0 as Surcharge,0 as SurchageAmt,JobType as DoType,(case when  mast.DoType='OUT' then '/Modules/WareHouse/Job/DoOutEdit.aspx?no='+mast.DoNo else '/Modules/WareHouse/Job/DoInEdit.aspx?no='+mast.DoNo end) as Pagelink,
ISNULL((select count(SequenceId) from XAArInvoiceDet det where det.ChgCode=cost.ChgCode and det.MastRefNo=cost.RefNo ),0) as CostCnt,
isnull((select count(Id) from Wh_Costing where RefNo=mast.DoNo and JobType=mast.DoType),0) as CostDoCnt,
ISNULL(tab_Amt.TotalAmt-isnull((select sum(LocAmt) from XAArInvoiceDet where MastRefNo=cost.RefNo),0),0) as UnBilledAmt
from Wh_Costing cost inner join Wh_DO mast on RefNo=mast.DoNo and JobType=mast.DoType and StatusCode='CLS' 
left join (select SUM(CostLocAmt) as TotalAmt,RefNo from Wh_Costing where CostLocAmt>0  group by RefNo) as tab_Amt on tab_Amt.RefNo=mast.DoNo
left join (select sum(Qty1) as TotalQty,DoNo,DoType from Wh_DoDet2 group by DoNo,DoType) tab_Qty on tab_Qty.DoNo=cost.RefNo and tab_Qty.DoType=cost.JobType
where  (cost.CostQty*cost.CostPrice)>0) as tab where CostCnt=0 and CostDoCnt>0  ");
        string where = " ";
        string cutoffdate = "";
        if (txt_CutOffDate.Value != null)
        {
            cutoffdate = txt_CutOffDate.Date.ToString("yyyy-MM-dd");
        }
        if (this.txt_CustName.Text.Length > 0)
        {
            where += string.Format(" and PartyName like '%{0}%'", this.txt_CustName.Text);
        }
        if (cutoffdate.Length > 0)
        {
            where += string.Format(" and DoDate<'{0}'", cutoffdate);
        }
        if(this.cmb_DoType.Text.Length>0){
            where += string.Format(@" and DoType='{0}'", this.cmb_DoType.Text);
        }
        if (where.Length > 0)
        {
            sql += " " + where + " order by DoDate asc";
        }
        //throw new Exception(sql);
        this.grid_handle.DataSource = ConnectSql.GetTab(sql);
        this.grid_handle.DataBind();
        if (this.grid_handle.PageCount > 0)
        {
            btn_CreateInv.Enabled = true;
            btn_CreateGstInv.Enabled = true;
        }
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
        this.gridExport.WriteXlsToResponse("HandlingReport");
    }
    protected void btn_CreateInv_Click(object sender, EventArgs e)
    {
        string invNo = "";
        string mastType = "WH";
        DateTime dt = DateTime.Today;
        bool isNew = false;
        int docId = 0;
        if (list.Count > 0)
        {
            XAArInvoice iv = null;
            for (int i = 0; i < list.Count; i++)
            {

                string counterType = "AR-IV";
                string partyId = list[i].partyId;
                string product = list[i].doNo;
                string doNo = list[i].doNo;
                int qty = list[i].qty;
                decimal surcharge = list[i].surcharge;
                string sql_cost = string.Format(@"select * from(select mast.DoNo,CONVERT(VARCHAR(10),mast.DoDate,110) as DoDate,cost.ChgCode as ChargeCode, 
ChgCodeDes as Description,CONVERT(int,CostQty) as Qty,CostPrice as Price,StatusCode,
isnull(((CostQty)*(isnull((CostPrice),0) )),0) as TotalAmt,dbo.fun_GetPartyName(mast.PartyId) AS PartyName,mast.PartyId,
0 as Surcharge,0 as SurchageAmt,JobType as DoType,CONVERT(decimal(10,6),(isnull((CostQty*CostPrice),0)*CostGst)) as GstAmt,
ISNULL((select count(SequenceId) from XAArInvoiceDet det where det.ChgCode=cost.ChgCode and det.MastRefNo=RefNo),0) as CostCnt,
isnull((select count(Id) from Wh_Costing where RefNo=mast.DoNo and JobType=mast.DoType),0) as CostDoCnt
 from Wh_Costing cost left join Wh_DO mast on RefNo=mast.DoNo and JobType=mast.DoType and StatusCode='CLS'
 ) as tab where CostCnt=0 and CostDoCnt>0 and TotalAmt>0 and DoNo='{0}'",doNo);
                string sql = string.Format(@"select top 1 det.DocNo from XAArInvoiceDet det inner join XAArInvoice mast on det.DocNo=mast.DocNo where det.MastRefNo='{0}' and InvType='HANDLING' and CancelInd='N' order by det.DocNo desc", doNo);
               invNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                
                if (invNo.Length ==0)
                {
                    iv = new XAArInvoice();
                    invNo = C2Setup.GetNextNo("", counterType, dt);
                    isNew = true;
                }
                else
                {
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XAArInvoice), "DocNo='" + invNo + "'");
                    iv = C2.Manager.ORManager.GetObject(query) as XAArInvoice;
                    isNew = false;
                }
                iv.DocType = "IV";
                iv.DocDate = dt;
                iv.DocNo = invNo;
                iv.PartyTo = partyId;
                iv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                iv.ExRate = 1;
                iv.AcCode = EzshipHelper.GetAccApCode(iv.PartyTo, iv.CurrencyId);
                iv.AcSource = "DB";
                iv.Description = "";
                iv.Term = "CASH";

                string[] currentPeriod = EzshipHelper.GetAccPeriod(iv.DocDate);
                iv.AcYear = SafeValue.SafeInt(currentPeriod[1], iv.DocDate.Year);
                iv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], iv.DocDate.Month);

                iv.MastRefNo = "";
                iv.JobRefNo = "";
                iv.MastType = mastType;
                iv.DocAmt = 0;
                iv.LocAmt = 0;
                iv.BalanceAmt = 0;
                iv.CancelDate = new DateTime(1900, 1, 1);
                iv.CancelInd = "N";
                iv.DocDueDate = dt;
                iv.ExportInd = "N";
                iv.SpecialNote = "";
                iv.UserId = EzshipHelper.GetUserName();
                iv.EntryDate = DateTime.Now;
                iv.InvType = "HANDLING";
                if (isNew)
                {
                    C2.Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(iv);
                    C2Setup.SetNextNo(iv.DocType, counterType, invNo, iv.DocDate);
                }
                else
                {
                    Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(iv);
                }

                try
                {
                    DataTable tab_Cost = ConnectSql.GetTab(sql_cost);
                    for (int a = 0; a < tab_Cost.Rows.Count; a++)
                    {
                        C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
                        det.DocId = iv.SequenceId;
                        det.DocLineNo = i + 1;
                        det.DocNo = invNo;
                        det.DocType = "IV";
                        det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", product)), System.Configuration.ConfigurationManager.AppSettings["ItemArCode"]);
                        det.AcSource = "CR";
                        det.MastRefNo = doNo;
                        det.JobRefNo = "";
                        det.MastType = mastType;
                        det.SplitType = "";


                        det.ChgCode = SafeValue.SafeString(tab_Cost.Rows[a]["ChargeCode"]); ;
                        det.ChgDes1 =SafeValue.SafeString(tab_Cost.Rows[a]["Description"]);
                        det.ChgDes2 = "";
                        det.ChgDes3 = "";

                        det.Price = SafeValue.SafeDecimal(tab_Cost.Rows[a]["Price"]);
                        det.Qty = SafeValue.SafeInt(tab_Cost.Rows[a]["Qty"],0);
                        det.Unit = "";
                        det.Currency = iv.CurrencyId;
                        det.ExRate = 1;
                        det.Gst = 0;
                        if (det.ExRate == 0)
                            det.ExRate = 1;
                        if (det.Gst > 0)
                            det.GstType = "S";
                        else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                            det.GstType = "E";
                        else
                            det.GstType = "Z";
                        decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
                        decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                        det.GstAmt = gstAmt;
                        det.DocAmt = docAmt;
                        det.LocAmt = locAmt;
                        det.OtherAmt = 0;

                        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(det);
                        if (surcharge != 0)
                        {
                            det.ChgCode = "HANDLE";
                            det.Qty = 1;
                            det.Price = surcharge;
                            sql = string.Format(@"select * from XXChgCode where ChgcodeId='{0}'", det.ChgCode);
                            DataTable tab = ConnectSql.GetTab(sql);
                            for (int j = 0; j < tab.Rows.Count; j++)
                            {
                                det.AcCode = SafeValue.SafeString(tab.Rows[j]["ArCode"]);
                                det.ChgDes1 = SafeValue.SafeString(tab.Rows[j]["ChgcodeDes"]);
                                det.GstType = SafeValue.SafeString(tab.Rows[j]["GstTypeId"]);
                                det.Gst = SafeValue.SafeDecimal(tab.Rows[j]["GstP"]);
                            }


                            amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
                            gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);

                            docAmt = amt + gstAmt;

                            locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                            det.GstAmt = gstAmt;
                            det.DocAmt = docAmt;
                            det.LocAmt = locAmt;
                            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(det);
                        }
                        docId = iv.SequenceId;
                    }
                }
                catch
                {
                }
                UpdateMaster(docId);
            }
           // string script = string.Format('<script type="text/javascript" > parent.navTab.openTab("{0}","/opsAccount/ArInvoiceEdit.aspx?no="{0}"",{title:'', fresh:false, external:true});</script>", invNo);
            //string script = string.Format("<script type='text/javascript' >alert('{0}');</script>", refNo);
            //Response.Clear();
            //Response.Write(script);
            //<a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/Modules/WareHouse/Job/DoOutEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
            Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public int qty = 0;
        public string doNo = "";
        public string partyId = "";

        public decimal surcharge = 0;
        public Record(string _doNo, string _partyId, decimal _surcharge)
        {
            doNo = _doNo;
            partyId = _partyId;
            surcharge = _surcharge;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end =5000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_handle.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_handle.Columns["No"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox txt_Id = this.grid_handle.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_handle.Columns["No"], "txt_Id") as ASPxTextBox;
            //ASPxLabel lbl_DoNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["DoNo"], "lbl_DoNo") as ASPxLabel;
            //ASPxLabel lbl_Description = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Description"], "lbl_Description") as ASPxLabel;
            ASPxLabel lbl_PartyId = this.grid_handle.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_handle.Columns["PartyName"], "lbl_PartyId") as ASPxLabel;
            //ASPxLabel lbl_LotNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["LotNo"], "lbl_LotNo") as ASPxLabel;
            ASPxLabel lbl_Qty = this.grid_handle.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_handle.Columns["Qty"], "lbl_Qty") as ASPxLabel;
            //ASPxLabel lbl_ChargeCode = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChargeCode"], "lbl_ChargeCode") as ASPxLabel;
            //ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            //ASPxSpinEdit spin_Surcharge = this.grid_handle.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_handle.Columns["Surcharge"], "spin_Surcharge") as ASPxSpinEdit;

            if (isPay != null && isPay.Checked)
            {
                list.Add(new Record(txt_Id.Text, lbl_PartyId.Text ,0));
            }
            else if (txt_Id == null)
                break; ;
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
    protected void btn_CreateGstInv_Click(object sender, EventArgs e)
    {
        string invNo = "";
        string mastType = "WH";
        DateTime dt = DateTime.Today;
        bool isNew = false;
        int docId = 0;
        if (list.Count > 0)
        {
            XAArInvoice iv = null;
            for (int i = 0; i < list.Count; i++)
            {

                string counterType = "AR-IV";
                string partyId = list[i].partyId;
                string product = list[i].doNo;
                string doNo = list[i].doNo;
                int qty = list[i].qty;
                decimal surcharge = list[i].surcharge;
                string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}'", doNo);
                string sql_cost = string.Format(@"select * from(select mast.DoNo,CONVERT(VARCHAR(10),mast.DoDate,110) as DoDate,cost.ChgCode as ChargeCode, 
ChgCodeDes as Description,CONVERT(int,CostQty) as Qty,CostPrice as Price,StatusCode,
isnull(((CostQty)*(isnull((CostPrice),0) )),0) as TotalAmt,dbo.fun_GetPartyName(mast.PartyId) AS PartyName,mast.PartyId,
0 as Surcharge,0 as SurchageAmt,JobType as DoType,CONVERT(decimal(10,6),(isnull((CostQty*CostPrice),0)*CostGst)) as GstAmt,
ISNULL((select count(SequenceId) from XAArInvoiceDet det where det.ChgCode=cost.ChgCode and det.MastRefNo=RefNo),0) as CostCnt,
isnull((select count(Id) from Wh_Costing where RefNo=mast.DoNo and JobType=mast.DoType),0) as CostDoCnt
 from Wh_Costing cost left join Wh_DO mast on RefNo=mast.DoNo and JobType=mast.DoType and StatusCode='CLS'
 ) as tab where CostCnt=0 and CostDoCnt>0 and TotalAmt>0 and DoNo='{0}'", doNo);
                sql = string.Format(@"select top 1 det.DocNo from XAArInvoiceDet det inner join XAArInvoice mast on det.DocNo=mast.DocNo where det.MastRefNo='{0}' and InvType='HANDLING' and CancelInd='N' order by det.DocNo desc", doNo);
                invNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                if (invNo.Length == 0)
                {
                    iv = new XAArInvoice();
                    invNo = C2Setup.GetNextNo("", counterType, dt);
                    isNew = true;
                }
                else
                {
                    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XAArInvoice), "DocNo='" + invNo + "'");
                    iv = C2.Manager.ORManager.GetObject(query) as XAArInvoice;
                    isNew = false;
                }
                iv.DocType = "IV";
                iv.DocDate = dt;
                iv.DocNo = invNo;
                iv.PartyTo = partyId;
                iv.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                iv.ExRate = 1;
                iv.AcCode = EzshipHelper.GetAccApCode(iv.PartyTo, iv.CurrencyId);
                iv.AcSource = "DB";
                iv.Description = "";
                iv.Term = "CASH";

                string[] currentPeriod = EzshipHelper.GetAccPeriod(iv.DocDate);
                iv.AcYear = SafeValue.SafeInt(currentPeriod[1], iv.DocDate.Year);
                iv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], iv.DocDate.Month);

                iv.MastRefNo = "";
                iv.JobRefNo = "";
                iv.MastType = mastType;
                iv.DocAmt = 0;
                iv.LocAmt = 0;
                iv.BalanceAmt = 0;
                iv.CancelDate = new DateTime(1900, 1, 1);
                iv.CancelInd = "N";
                iv.DocDueDate = dt;
                iv.ExportInd = "N";
                iv.SpecialNote = "";
                iv.UserId = EzshipHelper.GetUserName();
                iv.EntryDate = DateTime.Now;
                iv.InvType = "HANDLING";
                if (isNew)
                {
                    C2.Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(iv);
                    C2Setup.SetNextNo(iv.DocType, counterType, invNo, iv.DocDate);
                }
                else
                {
                    Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(iv);
                }

                try
                {
                    
                    DataTable tab_Cost = ConnectSql.GetTab(sql_cost);
                    for (int a = 0; a < tab_Cost.Rows.Count; a++)
                    {
                        C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
                        det.DocId = iv.SequenceId;
                        det.DocLineNo = i + 1;
                        det.DocNo = invNo;
                        det.DocType = "IV";
                        det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", product)), System.Configuration.ConfigurationManager.AppSettings["ItemArCode"]);
                        det.AcSource = "CR";
                        det.MastRefNo = doNo;
                        //det.JobRefNo = lotNo;
                        det.MastType = mastType;
                        det.SplitType = "";


                        det.ChgCode = SafeValue.SafeString(tab_Cost.Rows[a]["ChargeCode"]);
                        det.ChgDes1 = SafeValue.SafeString(tab_Cost.Rows[a]["Description"]);
                        det.ChgDes2 = "";
                        det.ChgDes3 = "";

                        det.Price = SafeValue.SafeDecimal(tab_Cost.Rows[a]["Price"]);
                        det.Qty = SafeValue.SafeInt(tab_Cost.Rows[a]["Qty"], 0);
                        det.Unit = "";
                        det.Currency = iv.CurrencyId;
                        det.ExRate = 1;
                        det.Gst = SafeValue.SafeDecimal(0.07);
                        if (det.ExRate == 0)
                            det.ExRate = 1;
                        if (det.Gst > 0)
                            det.GstType = "S";
                        else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                            det.GstType = "E";
                        else
                            det.GstType = "Z";
                        decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
                        decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                        det.GstAmt = gstAmt;
                        det.DocAmt = docAmt;
                        det.LocAmt = locAmt;
                        det.OtherAmt = 0;

                        C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(det);
                        if (surcharge != 0)
                        {
                            det.ChgCode = "HANDLE";
                            det.Qty = 1;
                            det.Price = surcharge;
                            sql = string.Format(@"select * from XXChgCode where ChgcodeId='{0}'", det.ChgCode);
                            DataTable tab = ConnectSql.GetTab(sql);
                            for (int j = 0; j < tab.Rows.Count; j++)
                            {
                                det.AcCode = SafeValue.SafeString(tab.Rows[j]["ArCode"]);
                                det.ChgDes1 = SafeValue.SafeString(tab.Rows[j]["ChgcodeDes"]);
                                det.GstType = SafeValue.SafeString(tab.Rows[j]["GstTypeId"]);
                                det.Gst = SafeValue.SafeDecimal(tab.Rows[j]["GstP"]);
                            }


                            amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
                            gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);

                            docAmt = amt + gstAmt;

                            locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                            det.GstAmt = gstAmt;
                            det.DocAmt = docAmt;
                            det.LocAmt = locAmt;
                            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                            C2.Manager.ORManager.PersistChanges(det);
                        }
                        docId = iv.SequenceId;
                    }
                }
                catch
                {
                }
                UpdateMaster(docId);
            }
            Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
        }
    }
}