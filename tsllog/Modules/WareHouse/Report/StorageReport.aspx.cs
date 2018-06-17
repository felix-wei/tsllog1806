using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;
using C2;
public partial class ReportWarehouse_Report_StorageReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_CutOffDate.Date = DateTime.Today.AddDays(1);
        }
        OnLoad();
        
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string cutoffdate = "";
        if (txt_CutOffDate.Value != null)
        {
            cutoffdate = txt_CutOffDate.Date.ToString("yyyy-MM-dd");
        }
        string sql = string.Format(@"select * from(select distinct det.DoNo,mast.DoDate,det.Product,tab_in.LotNo,det.Des1,tab_Bal.BalQty as Qty,tab_day.ProductClass,'STORAGE' as ChargeCode,dbo.fun_GetPartyName(mast.PartyId) AS PartyName,mast.PartyId,
tab_day.Price1 as Price,isnull(tab_day.Days,0) as Days,(tab_Bal.BalQty*isnull(tab_day.Price1,0)*tab_day.Days) as TotalAmt,0 as Surcharge,0 as SurchageAmt,((tab_Bal.BalQty*isnull(tab_day.Price1,0)*tab_day.Days)*0.07) as GstAmt,
isnull((select COUNT(SequenceId) as cnt from XAArInvoiceDet where MastType='WH' and (ChgCode=det.Product and JobRefNo=det.LotNo)),0) as IvCnt,tab_day.StartDate
from Wh_DoDet2 det  inner join Wh_DO mast on det.DoNo=mast.DoNo and det.DoType='IN'  and mast.StatusCode='CLS'
left join (select Product,LotNo from Wh_DoDet2 where DoType='IN' and LEN(LotNo)>0  group by Product,LotNo) as tab_in on tab_in.Product=det.Product and tab_in.LotNo=det.LotNo
left join (select (case when det.StorageType='Monthly' then datediff(month,mast.StartDate,'{0}') when det.StorageType='Weekly' then datediff(week,mast.StartDate,'{0}') when det.StorageType='Biweekly' then datediff(WEEK,mast.StartDate,'{0}')/2 else datediff(Day,mast.StartDate,'{0}') end) as Days,StartDate,mast.ContractNo,
ProductCode,ChgCode,PartyId,Price1,det.StorageType,det.ProductClass from wh_ContractDet det inner join  wh_Contract mast
 on det.ContractNo=mast.ContractNo) as tab_day on  tab_day.ProductCode=det.Product and tab_day.PartyId=mast.PartyId
left join (select Product,SUM(case when det.DoType='IN' then Qty1 else isnull(-Qty1,0) end) as BalQty,LotNo  from wh_dodet2 det inner join wh_do mast on mast.DoNo=det.DoNo and mast.StatusCode='CLS' where det.DoType='IN' and len(det.DoNo)>0  group by product,LotNo) as tab_Bal on tab_Bal.Product=det.Product and tab_Bal.LotNo=tab_in.LotNo
) as tab where IvCnt=0", cutoffdate);
        string where = " ";

        if (this.txt_CustName.Text.Length > 0)
        {
            where += string.Format(" and PartyName like '%{0}%'", this.txt_CustName.Text);
        }
        if(cutoffdate.Length>0){
            where += string.Format(" and DoDate<'{0}'",cutoffdate);
        }
        if (where.Length > 0)
        {
            sql += " " + where;
        }
        //throw new Exception(sql);
        this.grid.DataSource = ConnectSql.GetTab(sql);
        this.grid.DataBind();
        if (this.grid.PageCount > 0)
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
        this.gridExport.WriteXlsToResponse("StorageReport");
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
                string product = list[i].code;
                string doNo = list[i].doNo;
                string des = list[i].des;
                string lotNo = list[i].lotNo;
                decimal qty = list[i].qty;
                decimal price = list[i].price;
                int days = list[i].days;
                decimal surcharge = list[i].surcharge;
                //string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}' and InvType='STORAGE'", doNo);
                //invNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                //if (invNo.Length==0)
                //{
                //    iv = new XAArInvoice();
                //    invNo = C2Setup.GetNextNo("", counterType, dt);
                //    isNew = true;
                //}
                //else
                //{
                //    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XAArInvoice), "DocNo='" + invNo + "'");
                //    iv = C2.Manager.ORManager.GetObject(query) as XAArInvoice;
                //    isNew = false;
                //}
                iv = new XAArInvoice();
                invNo = C2Setup.GetNextNo("", counterType, dt);
                isNew = true;
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

                iv.MastRefNo = doNo;
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
                iv.InvType = "STORAGE";
                //if (isNew)
                //{
                    C2.Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(iv);
                    C2Setup.SetNextNo(iv.DocType, counterType, invNo, iv.DocDate);
                //}
                //else
                //{
                    Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(iv);
                //}

                try
                {
                    C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
                    det.DocId = iv.SequenceId;
                    det.DocLineNo = i+1;
                    det.DocNo = invNo;
                    det.DocType = "IV";
                    det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", product)), System.Configuration.ConfigurationManager.AppSettings["ItemArCode"]);
                    det.AcSource = "CR";
                    det.MastRefNo = doNo;
                    det.JobRefNo = lotNo;
                    det.MastType = mastType;
                    det.SplitType = "";

                    //sql = string.Format(@"select * from XXChgCode where ChgcodeId='{0}'", product);
                    //DataTable tab_chg = ConnectSql.GetTab(sql);
                    //for (int j = 0; j < tab_chg.Rows.Count; j++)
                    //{
                    //    det.Gst = SafeValue.SafeDecimal(tab_chg.Rows[j]["GstP"]);
                    //}

                    det.ChgCode = product;
                    det.ChgDes1 = des;
                    det.ChgDes2 = "";
                    det.ChgDes3 = "";

                    det.Price = price;
                    det.Qty = qty;
                    det.Unit = "";
                    det.Currency = iv.CurrencyId;
                    det.ExRate = 1;
                    det.Gst =0;
                    if (det.ExRate == 0)
                        det.ExRate = 1;
                    if (det.Gst > 0)
                        det.GstType = "S";
                    else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                        det.GstType = "E";
                    else
                        det.GstType = "Z";
                    decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);//*days
                    decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
                    decimal docAmt = amt + gstAmt;
                    decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                    decimal surchageAmt = SafeValue.ChinaRound(surcharge * qty, 2);
                    det.GstAmt = gstAmt;
                    det.DocAmt = docAmt;
                    det.LocAmt = locAmt;
                    det.OtherAmt = surchageAmt;
                    C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(det);
                    if (surcharge != 0)
                    {
                        det.ChgCode = "STORE";

                        det.Qty = 1;
                        det.Price = surcharge;
                        string  sql = string.Format(@"select * from XXChgCode where ChgcodeId='{0}'", det.ChgCode);
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
                catch
                {
                }
                UpdateMaster(docId);
            }

            Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
        }
    }
    List<Record> list = new List<Record>();
    internal class Record
    {
        public string code = "";
        public decimal qty = 0;
        public decimal price = 0;
        public string doNo = "";
        public string partyId = "";
        public string lotNo = "";
        public string chargeCode = "";
        public int days = 0;
        public string des = "";
        public decimal surcharge = 0;
        public Record(string _code, string _doNo, string _des, string _partyId, string _lotNo, decimal _qty, decimal _price, string _chargeCode, int _days, decimal _surcharge)
        {
            code = _code;
            doNo = _doNo;
            partyId = _partyId;
            lotNo = _lotNo;
            qty = _qty;
            price = _price;
            chargeCode = _chargeCode;
            des = _des;
            days = _days;
            surcharge = _surcharge;
        }
    }
    private void OnLoad()
    {
        int start = 0;
        int end = 5000;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "ack_IsPay") as ASPxCheckBox;
            ASPxTextBox txt_Id = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Product"], "txt_Id") as ASPxTextBox;
            ASPxLabel lbl_DoNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["DoNo"], "lbl_DoNo") as ASPxLabel;
            ASPxLabel lbl_Description = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Description"], "lbl_Description") as ASPxLabel;
            ASPxLabel lbl_PartyId = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["PartyName"], "lbl_PartyId") as ASPxLabel;
            ASPxLabel lbl_LotNo = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["LotNo"], "lbl_LotNo") as ASPxLabel;
            ASPxLabel lbl_Qty = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Qty"], "lbl_Qty") as ASPxLabel;
            ASPxLabel lbl_ChargeCode = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["ChargeCode"], "lbl_ChargeCode") as ASPxLabel;
            ASPxSpinEdit spin_Price = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Price"], "spin_Price") as ASPxSpinEdit;
            ASPxSpinEdit spin_Surcharge = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Surcharge"], "spin_Surcharge") as ASPxSpinEdit;
            ASPxLabel lbl_Days = this.grid.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid.Columns["Days"], "lbl_Days") as ASPxLabel;
            if (isPay != null && isPay.Checked)
            {
                list.Add(new Record(txt_Id.Text, lbl_DoNo.Text, lbl_Description.Text, lbl_PartyId.Text, lbl_LotNo.Text, SafeValue.SafeDecimal(lbl_Qty.Text, 0), SafeValue.SafeDecimal(spin_Price.Text), lbl_ChargeCode.Text, SafeValue.SafeInt(lbl_Days.Text, 0), SafeValue.SafeDecimal(spin_Surcharge.Text)));
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
                string product = list[i].code;
                string doNo = list[i].doNo;
                string des = list[i].des;
                string lotNo = list[i].lotNo;
                decimal qty = list[i].qty;
                decimal price = list[i].price;
                int days = list[i].days;
                decimal surcharge = list[i].surcharge;
                iv = new XAArInvoice();
                invNo = C2Setup.GetNextNo("", counterType, dt);
                //string sql = string.Format(@"select DocNo from XAArInvoice where MastRefNo='{0}' and InvType='STORAGE'", doNo);
                //invNo = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
                //if (invNo.Length == 0)
                //{

                //    isNew = true;
                //}
                //else
                //{
                //    Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(XAArInvoice), "DocNo='" + invNo + "'");
                //    iv = C2.Manager.ORManager.GetObject(query) as XAArInvoice;
                //    isNew = false;
                //}
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

                iv.MastRefNo = doNo;
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
                iv.InvType = "STORAGE";
                //if (isNew)
                //{
                    C2.Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(iv);
                    C2Setup.SetNextNo(iv.DocType, counterType, invNo, iv.DocDate);
                //}
                //else
                //{
                //    Manager.ORManager.StartTracking(iv, Wilson.ORMapper.InitialState.Updated);
                //    Manager.ORManager.PersistChanges(iv);
                //}

                try
                {
                    C2.XAArInvoiceDet det = new C2.XAArInvoiceDet();
                    det.DocId = iv.SequenceId;
                    det.DocLineNo = i + 1;
                    det.DocNo = invNo;
                    det.DocType = "IV";
                    det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ArCode from ref_product where Code='{0}'", product)), System.Configuration.ConfigurationManager.AppSettings["ItemArCode"]);
                    det.AcSource = "CR";
                    det.MastRefNo = doNo;
                    det.JobRefNo = lotNo;
                    det.MastType = mastType;
                    det.SplitType = "";


                    det.Gst = SafeValue.SafeDecimal(0.07);
                    det.ChgCode = product;
                    det.ChgDes1 = des;
                    det.ChgDes2 = "";
                    det.ChgDes3 = "";

                    det.Price = price;
                    det.Qty = qty;
                    det.Unit = "";
                    det.Currency = iv.CurrencyId;
                    det.ExRate = 1;
                    if (det.ExRate == 0)
                        det.ExRate = 1;
                    if (det.Gst > 0)
                        det.GstType = "S";
                    else if (det.Currency == System.Configuration.ConfigurationManager.AppSettings["Currency"])
                        det.GstType = "E";
                    else
                        det.GstType = "Z";
                    decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);//* days
                    decimal gstAmt = SafeValue.ChinaRound((amt * det.Gst), 2);
                    decimal docAmt = amt + gstAmt;
                    decimal locAmt = SafeValue.ChinaRound(docAmt * det.ExRate, 2);
                    decimal surchageAmt = SafeValue.ChinaRound(surcharge * qty, 2);
                    det.GstAmt = gstAmt;
                    det.DocAmt = docAmt;
                    det.LocAmt = locAmt;
                    det.OtherAmt = 0;
                    C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(det);
                    if (surcharge != 0)
                    {
                        det.ChgCode = "STORE";

                        det.Qty = 1;
                        det.Price = surcharge;
                        string sql = string.Format(@"select * from XXChgCode where ChgcodeId='{0}'", det.ChgCode);
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
                catch
                {
                }
                UpdateMaster(docId);
            }

            Response.Redirect("/opsAccount/ArInvoiceEdit.aspx?no=" + invNo);
        }
    }
}