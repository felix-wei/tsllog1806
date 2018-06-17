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

 
public partial class Account_ArReceiptEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["RecEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text=Request.QueryString["no"].ToString();
                Session["RecEditWhere"] = "DocNo='" + Request.QueryString["no"] + "'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["RecEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsArReceipt.FilterExpression = "1=0";
        }
        if (Session["RecEditWhere"] != null)
        {
            this.dsArReceipt.FilterExpression = Session["RecEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region Receipt
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArReceipt));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["AcYear"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["DocDate"] = DateTime.Today;
        e.NewValues["DocType"] = "RE";
        e.NewValues["DocType1"] = "Job";
        e.NewValues["DocDueDate"] = DateTime.Today;
        e.NewValues["ChqDate"] = DateTime.Today;
        e.NewValues["AcSource"] = "DB";
        e.NewValues["AcCode"] = System.Configuration.ConfigurationManager.AppSettings["DefaultBankCode"];
        e.NewValues["DocCurrency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["DocExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";
        e.NewValues["LocAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (true)
        {
            #region save
            ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
            ASPxTextBox otherPartyName = this.ASPxGridView1.FindEditFormTemplateControl("txt_OtherPartyName") as ASPxTextBox;
            ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
            ASPxComboBox docType1 = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType1") as ASPxComboBox;

            ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
            ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
            ASPxMemo remark = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;

            ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
            ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
            ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;

            ASPxTextBox bankName = this.ASPxGridView1.FindEditFormTemplateControl("txt_BankName") as ASPxTextBox;
            ASPxTextBox acDorc = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcDorc") as ASPxTextBox;
            ASPxTextBox chqNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_ChequeNo") as ASPxTextBox;
            ASPxDateEdit chqDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_ChqDt") as ASPxDateEdit;
            ASPxTextBox pic = this.ASPxGridView1.FindEditFormTemplateControl("txt_Pic") as ASPxTextBox;

            ASPxSpinEdit docAmt = this.ASPxGridView1.FindEditFormTemplateControl("spin_DocAmt") as ASPxSpinEdit;
            C2.XAArReceipt inv = Manager.ORManager.GetObject(typeof(XAArReceipt), SafeValue.SafeInt(oidCtr.Text, 0)) as XAArReceipt;
            if (null == inv)// first insert invoice
            {

                string invN = C2Setup.GetNextNo("RE", "AR-RECEIPT", docDate.Date);//C2Setup.GetNextNo("AR-RECEIPT");
                inv = new C2.XAArReceipt();
                inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
                inv.DocNo = invN;
                inv.DocType = docType.Value.ToString();
                inv.DocType1 = docType1.Value.ToString();
                inv.DocDate = docDate.Date;
                inv.ChqDate = chqDt.Date;
                inv.Remark = remark.Text;
                inv.DocCurrency = docCurr.Text.ToString();
                inv.DocExRate = SafeValue.SafeDecimal(exRate.Value, 1);
                if (inv.DocExRate <= 0)
                    inv.DocExRate = 1;
                inv.AcCode = acCode.Text;
                inv.BankName = bankName.Text;
                inv.ChqNo = chqNo.Text;
                inv.ExportInd = "N";

                if (inv.DocType1.ToLower() == "refund")
                    inv.AcSource = "CR";
                else
                    inv.AcSource = "DB";

                inv.DocAmt = SafeValue.SafeDecimal(docAmt.Value, 0);
                inv.LocAmt = inv.DocAmt * inv.DocExRate;
                inv.CancelDate = new DateTime(1900, 1, 1);
                inv.CancelInd = "N";
                inv.BankRec = "N";
                inv.BankDate = new DateTime(1900, 1, 1);
                inv.OtherPartyName = otherPartyName.Text;
                string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);
                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
                inv.Pic = pic.Text;
                try
                {
                    inv.GenerateInd = "N";
                    inv.CreateBy = HttpContext.Current.User.Identity.Name;
                    inv.CreateDateTime = DateTime.Now;
                    inv.UpdateBy = HttpContext.Current.User.Identity.Name;
                    inv.UpdateDateTime = DateTime.Now;
                    inv.PostBy = "";
                    inv.PostDateTime = new DateTime(1900, 1, 1);
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);
                    //inv.DocNo = inv.SequenceId.ToString();
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(inv);
                     C2Setup.SetNextNo("", "AR-RECEIPT", invN, inv.DocDate);
					//C2Setup.SetNextNo("AR-RECEIPT", invN);
                }
                catch
                {
                }
            }
            else
            {
                inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
                inv.DocDate = docDate.Date;
                inv.ChqDate = chqDt.Date;
                inv.Remark = remark.Text;
                inv.DocCurrency = docCurr.Text.ToString();
                inv.DocExRate = SafeValue.SafeDecimal(exRate.Value, 1);
                if (inv.DocExRate <= 0)
                    inv.DocExRate = 1;
                inv.AcCode = acCode.Text;
                inv.BankName = bankName.Text;
                inv.ChqNo = chqNo.Text;
                inv.DocAmt = SafeValue.SafeDecimal(docAmt.Value, 0);
                inv.LocAmt = inv.DocAmt * inv.DocExRate;
                string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);
                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);

                inv.OtherPartyName = otherPartyName.Text;
                inv.Pic = pic.Text;
                try
                {
                    inv.UpdateBy = HttpContext.Current.User.Identity.Name;
                    inv.UpdateDateTime = DateTime.Now;
                    Manager.ORManager.StartTracking(inv, InitialState.Updated);
                    Manager.ORManager.PersistChanges(inv);
                }
                catch
                { }

            }
            Session["RecEditWhere"] = "SequenceId=" + inv.SequenceId;
            this.dsArReceipt.FilterExpression = Session["RecEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
            #endregion
        }
    }


    #endregion

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
            ASPxComboBox docType1 = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType1") as ASPxComboBox;
            docType1.ReadOnly = true;
            docType1.BackColor = docN.BackColor;
        }
    }

    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArReceiptDet));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsArReceiptDet.FilterExpression = "RepId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["RepLineNo"] = 0;
        e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["AcSource"] = "CR";
        e.NewValues["DocId"] = 0;
        e.NewValues["DocNo"] = "";
        e.NewValues["DocDate"] = new DateTime(1900, 1, 1);
        e.NewValues["DocType"] = "RE";

        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        if (docType.Text == "RE")
        {
            e.NewValues["AcSource"] = "CR";
            e.NewValues["DocType"] = "RE";
        }
        else
        {
            e.NewValues["AcSource"] = "DB";
            e.NewValues["DocType"] = "PC";
        }

    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql_detCnt = "select count(DocNo) from XAArReceiptDet where RepId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["RepLineNo"] = lineNo;
        e.NewValues["RepId"] = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        e.NewValues["RepNo"] = docN.Text;
        e.NewValues["RepType"] = "RE";
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;

        e.NewValues["LocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;
        e.NewValues["LocAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DocAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
		e.NewValues["Dim1"] = S.Text(e.NewValues["Dim1"]);
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string sql_detCnt = "select ExportInd from XAArReceipt where SequenceId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        string exportInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql_detCnt), "N");
        if (exportInd == "Y")
        {
            throw new Exception("Have Posted, can't delete item");
        }
    }
    protected void grid_InvDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
    }
    protected void grid_InvDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        string invId = e.NewValues["DocId"].ToString();
        string docType = e.NewValues["DocType"].ToString();
        UpdateMaster(invId,docType);
    }
    protected void grid_InvDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
       // ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        string invId = e.Values["DocId"].ToString();
        string docType = e.Values["DocType"].ToString();
        UpdateMaster(invId,docType);
    }
    private void UpdateMaster(string docId, string docType)
    {
        if (docType == "IV" || docType == "DN" || docType == "CN")
        {
            string sql = "select SUM(docAmt) from XAArReceiptDet where DocId='" + docId + "' and DocType='" + docType + "'";
            decimal docAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);

            sql = "select SUM(docAmt) from XAApPaymentDet where DocId='" + docId + "' and DocType='" + docType + "'";
            docAmt += SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = string.Format("Update XAArInvoice set BalanceAmt=DocAmt-'{0}' where SequenceId='{1}'", docAmt, docId);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
        else if (docType == "PL" || docType == "SD"||docType=="SC")
        {
            string sql = "select SUM(docAmt) from XAArReceiptDet where DocId='" + docId + "' and DocType='" + docType + "'";
            decimal docAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = "select SUM(docAmt) from XAApPaymentDet where DocId='" + docId + "' and DocType='" + docType + "'";
            docAmt += SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = string.Format("Update XAApPayable set BalanceAmt=DocAmt-'{0}' where SequenceId='{1}'", docAmt, docId);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }
    #endregion

    #region popub
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] Code = new object[grid.VisibleRowCount];
        object[] Des = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            Code[i] = grid.GetRowValues(i, "Code");
            Des[i] = grid.GetRowValues(i, "AcDesc");
        }
        e.Properties["cpCode"] = Code;
        e.Properties["cpDes"] = Des;
    }
    #endregion
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string filter = e.Parameters;
            ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

            //ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;

        if (filter == "P")
        {
            #region Post
            string sql = @"SELECT AcYear, AcPeriod, DocType, DocNo, DocDate, DocCurrency, DocExRate, PartyTo, OtherPartyName, DocAmt, LocAmt, AcCode, AcSource, ChqNo, ChqDate, ExportInd, 
                      BankName, Remark FROM  XAArReceipt";
            sql += " WHERE SequenceId='" + oidCtr.Text + "'";
            DataTable dt = Helper.Sql.List(sql);
            int acYear = 0;
            int acPeriod = 0;
            string docN = "";
            string docType = "";
            string acSource = "";
            string acCode = "";
            decimal locAmt = 0;
            decimal docAmt = 0;
            decimal exRate = 0;
            string currency = "";
            DateTime docDt = DateTime.Today;
            string remarks = "";
            string partyTo = "";
            string otherPartyName = "";
            string chqNo = "";
            if (dt.Rows.Count == 1)
            {
                acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
                acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
                acSource = dt.Rows[0]["AcSource"].ToString();
                acCode = dt.Rows[0]["AcCode"].ToString();
                docN = dt.Rows[0]["DocNo"].ToString();
                docType = dt.Rows[0]["DocType"].ToString();
                locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
                docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
                exRate = SafeValue.SafeDecimal(dt.Rows[0]["DocExRate"].ToString(), 0);
                currency = dt.Rows[0]["DocCurrency"].ToString();
                docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
                // partyId = dt.Rows[0][""].ToString();
                remarks = dt.Rows[0]["Remark"].ToString();
                partyTo = dt.Rows[0]["PartyTo"].ToString();
                otherPartyName = dt.Rows[0]["OtherPartyName"].ToString();
                chqNo = dt.Rows[0]["ChqNo"].ToString();
            }
            else
            {
                e.Result = "Can't find the ArReceipt!";
                return;
            }
            string sqlDet = string.Format("select count(SequenceId) from XAArReceiptDet where RepId='{0}'", oidCtr.Text);
            int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
            if (detCnt == 0)
            {
                e.Result = "No Detail, Can't Post";
                return;
            }
            sqlDet = string.Format("select max(docDate) from XAArReceiptDet where RepId='{0}'", oidCtr.Text);
            DateTime maxLineDocDate = SafeValue.SafeDate(Manager.ORManager.ExecuteScalar(sqlDet), new DateTime(1900, 1, 1));
            if (maxLineDocDate > new DateTime(2000, 1, 1) && maxLineDocDate > docDt)
            {
                e.Result = "The bill's Date can't be greater than receipt date.";
                return;
            }

            //check account period
            if (acYear < 1 || acPeriod < 1)
            {
                e.Result = "Account year or Period Invalid!";
                return;
            }
            string sql1 = "select CloseInd from XXAccPeriod where Year='" + acYear + "' and Period ='" + acPeriod + "'";
            string closeInd = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql1), "");
            if (closeInd == "")
            {
                e.Result = "Can't find this account period!";
                return;
            }
            else if (closeInd == "Y")
            {
                e.Result = "The account period is closed!";
                return;
            }
            //mast.amt det.amt is match
            sql = string.Format("select sum(locamt) from XAArReceiptDet where RepId='{0}' and AcSource='CR'", oidCtr.Text);
            decimal amt_detCr = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
            sql = string.Format("select sum(locamt) from XAArReceiptDet where RepId='{0}' and AcSource='DB'", oidCtr.Text);
            decimal amt_detDb = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(sql), 0);
            if (locAmt != amt_detCr - amt_detDb)
            {
                e.Result = "Amount can't match, can't post";
                return;
            }
            sql = "select count(*) from XAArReceiptDet where AcCode='' and RepId='" + oidCtr.Text + "'";
            detCnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (detCnt > 0)
            {
                e.Result = "Some Item's Accode is blank, pls check";
                return;
            }
            //delete old post data
            sql = string.Format("SELECT SequenceId from XAGlEntry WHERE DocNo='{0}' and DocType='{1}'", docN, docType);
            int glOldOid = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql), 0);
            if (glOldOid > 0)
            {
                DeleteGl(glOldOid);
            }

            //Insert into gl entry
            int glOid = 0;
            try
            {
                C2.XAGlEntry gl = new XAGlEntry();
                gl.AcPeriod = acPeriod;
                gl.AcYear = acYear;
                gl.ArApInd = "AR";
                gl.DocType = docType;
                gl.DocDate = docDt;
                gl.DocNo = docN;
                gl.CrAmt = docAmt;
                gl.DbAmt = docAmt;
                gl.CurrencyCrAmt = locAmt;
                gl.CurrencyDbAmt = locAmt;
                gl.CurrencyId = currency;
                gl.EntryDate = DateTime.Now;
                gl.ExRate = exRate;
                gl.PostDate = DateTime.Now;
                gl.PostInd = "N";
                gl.Remark = remarks;
                gl.UserId = HttpContext.Current.User.Identity.Name;
                gl.CancelInd = "N";
                gl.CancelDate = new DateTime(1900, 1, 1);
                gl.PartyTo = partyTo;
                gl.OtherPartyName = otherPartyName;
                gl.ChqNo = chqNo;
                gl.SupplierBillNo = "";
                gl.SupplierBillDate = new DateTime(1900, 1, 1);
                Manager.ORManager.StartTracking(gl, InitialState.Inserted);
                Manager.ORManager.PersistChanges(gl);
                glOid = gl.SequenceId;

                //insert Detail
                OPathQuery query = new OPathQuery(typeof(XAArReceiptDet), "RepId='" + oidCtr.Text + "'");
                ObjectSet set = Manager.ORManager.GetObjectSet(query);
                int index = 1;
                XAGlEntryDet det1 = new XAGlEntryDet();

                det1.AcCode = acCode;
                det1.ArApInd = "AR";
                det1.AcPeriod = acPeriod;
                det1.AcSource = acSource;
                det1.AcYear = acYear;
                det1.CrAmt = 0;
                det1.CurrencyCrAmt = 0;
                det1.DbAmt = docAmt;
                det1.CurrencyDbAmt = locAmt;
                det1.CurrencyId = currency;
                det1.DocNo = docN;
                det1.DocType = docType;
                det1.ExRate = exRate;
                det1.GlLineNo = index;
                det1.GlNo = gl.SequenceId;
                det1.Remark = remarks;


                Manager.ORManager.StartTracking(det1, InitialState.Inserted);
                Manager.ORManager.PersistChanges(det1);
                for (int i = 0; i < set.Count; i++)
                {
                    try
                    {
                        index++;
                        XAArReceiptDet invDet = set[i] as XAArReceiptDet;
                        XAGlEntryDet det = new XAGlEntryDet();

                        det.AcCode = invDet.AcCode;
                        det.ArApInd = "AR";
                        det.AcPeriod = acPeriod;
                        det.AcSource = invDet.AcSource;
                        det.AcYear = acYear;
                        if (invDet.AcSource == "DB")
                        {
                            det.DbAmt = invDet.DocAmt;
                            det.CurrencyDbAmt = invDet.LocAmt;
                            det.CrAmt = 0;
                            det.CurrencyCrAmt = 0;
                        }
                        else// if (det.AcSource == "CR")
                        {
                            det.CrAmt = invDet.DocAmt;
                            det.CurrencyCrAmt = invDet.LocAmt;
                            det.DbAmt = 0;
                            det.CurrencyDbAmt = 0;
                        }
                        det.CurrencyId = invDet.Currency;

                        det.DocNo = docN;
                        det.DocType = docType;
                        det.ExRate = invDet.ExRate;
                        det.GlLineNo = index;
                        det.GlNo = gl.SequenceId;
                        det.Remark = invDet.Remark1;


                        Manager.ORManager.StartTracking(det, InitialState.Inserted);
                        Manager.ORManager.PersistChanges(det);
                    }
                    catch
                    {
                        e.Result = "Posting Error, Please repost!";
                        DeleteGl(glOid);
                    }
                }
                UpdateArInv(oidCtr.Text);
                e.Result = "Post completely!";
            }
            catch
            {
                e.Result = "Posting Error, Please repost!";
                DeleteGl(glOid);
            }
            #endregion
        }
        else if (filter == "DD")
        {
            #region delete receipt

            try
            {
                string sql = "SELECT SequenceId,DocId, DocType FROM XAArReceiptDet where RepId ='" + oidCtr.Text + "'";
                DataTable tab = Helper.Sql.List(sql);
                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    try
                    {
                        string sequenceId = tab.Rows[i]["SequenceId"].ToString();
                        string docId = tab.Rows[i]["DocId"].ToString();
                        string docType = tab.Rows[i]["DocType"].ToString();
                        string sql_det = "delete from XAArReceiptDet where SequenceId='" + sequenceId + "'";
                        if (C2.Manager.ORManager.ExecuteCommand(sql_det) > 0)
                        {
                            UpdateMaster(docId, docType);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }
        else if (filter == "GainLoss")
        {
            #region GAIN LOSS
           ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
            string sql = string.Format("select (select LocAmt from XaArReceipt where sequenceId='{0}')-iSNULL(sum(case when AcSource='CR' then -LocAmt else LocAmt end),0) As LocAmt from xaarreceiptdet where repid='{0}'", oidCtr.Text);
            if (docType.Text == "RE")
                sql = string.Format("select (select LocAmt from XaArReceipt where sequenceId='{0}')-iSNULL(sum(case when AcSource='DB' then -LocAmt else LocAmt end),0) As LocAmt from xaarreceiptdet where repid='{0}'", oidCtr.Text);
            decimal balAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            

			if (balAmt > 0)
            {
                string gainAccCode = System.Configuration.ConfigurationManager.AppSettings["GainLoseAcCode"];
                XAArReceiptDet det = new XAArReceiptDet();
                det.AcCode = gainAccCode;
                det.AcSource = "CR";
                det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.DocAmt = balAmt;
                det.DocDate = new DateTime(1900, 1, 1);
                det.DocId = 0;
                det.DocNo = "";
                det.DocType = "RE";
                det.ExRate = 1;
                det.LocAmt = balAmt;
                det.Remark1 = "GAIN AND LOSS";
                det.Remark2 = "";
                det.Remark3 = "";
                det.RepId = SafeValue.SafeInt(oidCtr.Text, 0);
                ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
                det.RepNo = docN.Text;
                string sql_detCnt = "select count(DocNo) from XAArReceiptDet where RepId='" + oidCtr.Text + "'";
                int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
                det.RepLineNo = lineNo;
                det.RepType = "RE";
                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(det);

            }
            else if (balAmt < 0)
            {

                string gainAccCode = System.Configuration.ConfigurationManager.AppSettings["GainLoseAcCode"];
                XAArReceiptDet det = new XAArReceiptDet();
                det.AcCode = gainAccCode;
                det.AcSource = "DB";
                det.Currency = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                det.DocAmt = -balAmt;
                det.DocDate = new DateTime(1900, 1, 1);
                det.DocId = 0;
                det.DocNo = "";
                det.DocType = "RE";
                det.ExRate = 1;
                det.LocAmt = -balAmt;
                det.Remark1 = "GAIN AND LOSS";
                det.Remark2 = "";
                det.Remark3 = "";
                det.RepId = SafeValue.SafeInt(oidCtr.Text, 0);
                ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
                det.RepNo = docN.Text;
                string sql_detCnt = "select count(DocNo) from XAArReceiptDet where RepId='" + oidCtr.Text + "'";
                int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
                det.RepLineNo = lineNo;
                det.RepType = "RE";
                C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(det);
            } 
            #endregion
        }
        else if (filter == "Generate")
        {
            string invN = C2Setup.GetNextNo("AR-RECEIPT");
            string sql = string.Format("Update XAArReceipt set DocNo='{1}',GenerateInd='Y' where SequenceId='{0}'", oidCtr.Text, invN);
            if (ConnectSql.ExecuteSql(sql) > -1)
                C2Setup.SetNextNo("AR-RECEIPT", invN);
            e.Result = invN;
        }
    }
    private void DeleteGl(int glOid)
    {
        string sql_delete = "delete from XAGlEntry where SequenceId= '" + glOid + "'";
        int m = Manager.ORManager.ExecuteCommand(sql_delete);
        sql_delete = "delete from XAGlEntryDet where GlNo= '" + glOid + "'";
        m = Manager.ORManager.ExecuteCommand(sql_delete);
    }
    private void UpdateArInv(string docId)
    {
        string sql_invoice = "update XAArReceipt set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
}
