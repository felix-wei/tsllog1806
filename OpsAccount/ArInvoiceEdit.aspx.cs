﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Wilson.ORMapper;
using System.IO;
using System.Xml;

public partial class Account_ArInvoiceEdit : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            //this.txtSchNo.Focus();
            this.form1.Focus();
            Session["SeaIvEditWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["SeaIvEditWhere"] = "DocType='IV' and DocNo='" + Request.QueryString["no"]+"'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["SeaIvEditWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsArInvoice.FilterExpression = "1=0";
        }
        if (Session["SeaIvEditWhere"] != null)
        {
            this.dsArInvoice.FilterExpression = Session["SeaIvEditWhere"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region invoice
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArInvoice));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["AcYear"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["DocDate"] = DateTime.Today;
        e.NewValues["DocDueDate"] = DateTime.Today;
        e.NewValues["DocType"] = "IV";
        e.NewValues["AcSource"] = "DB";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";


        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            string refNo = Request.QueryString["RefN"].ToString();
            string houseNo = Request.QueryString["JobN"].ToString();
            string jobType = Request.QueryString["JobType"].ToString();
            e.NewValues["MastRefNo"] = refNo;
            e.NewValues["JobRefNo"] = houseNo;
            e.NewValues["MastType"] = jobType;
            string sql = "SELECT ClientId as Cust FROM  CTM_Job WHERE (JobNo= '" + refNo + "')";
            //if (jobType == "TPT")
            //{
                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());
            //}
            if (jobType == "TPT")
            {
                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());
            }
            if (houseNo.Length > 1)
            {
                if (jobType == "SI")
                    sql = "SELECT CustomerID FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "SE")
                    sql = "SELECT CustomerID FROM  SeaExport WHERE (JobNo= '" + houseNo + "')";
                if (jobType == "AI" || jobType == "AE" || jobType == "ACT")
                    sql = "SELECT CustomerID FROM  air_job WHERE (JobNo= '" + houseNo + "')";

                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");
                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                e.NewValues["Term"] = EzshipHelper.GetTerm(e.NewValues["PartyTo"].ToString());

                if (jobType == "SI")
                {
                    sql = "SELECT CollectCurrency, CollectExRate FROM  SeaImport WHERE (JobNo= '" + houseNo + "')";
                    DataTable tab_cur = Manager.ORManager.GetDataSet(sql).Tables[0];
                    if (tab_cur.Rows.Count == 1)
                    {
                        e.NewValues["CurrencyId"] = tab_cur.Rows[0][0].ToString();
                        e.NewValues["ExRate"] = SafeValue.SafeDecimal(tab_cur.Rows[0][1], 0);
                    }
                }

            }
        }
    }
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string filter = e.Parameters;
        string[] _filter = filter.Split(',');
        string p = _filter[0];
        string s = _filter[1];
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        //get informations from arinvoice
        if (p.ToUpper() == "P")
        {
            SaveBill();
            #region Post
            string sql = @"SELECT  AcYear, AcPeriod, AcCode, AcSource, DocType, DocNo, DocDate, DocDueDate, PartyTo, MastType, CurrencyId, ExRate, 
                      Term, Description, DocAmt, LocAmt
FROM XAArInvoice";
            sql += " WHERE SequenceId='" + docId.Text + "'";
            DataTable dt = Manager.ORManager.GetDataSet(sql).Tables[0];
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
            if (dt.Rows.Count == 1)
            {
                acYear = SafeValue.SafeInt(dt.Rows[0]["AcYear"], 0);
                acPeriod = SafeValue.SafeInt(dt.Rows[0]["AcPeriod"], 0);
                acSource = dt.Rows[0]["AcSource"].ToString();
                acCode = dt.Rows[0]["AcCode"].ToString();
                docN = dt.Rows[0]["DocNo"].ToString();
                docType = dt.Rows[0]["DocType"].ToString();
                docAmt = SafeValue.SafeDecimal(dt.Rows[0]["DocAmt"].ToString(), 0);
                locAmt = SafeValue.SafeDecimal(dt.Rows[0]["LocAmt"].ToString(), 0);
                exRate = SafeValue.SafeDecimal(dt.Rows[0]["ExRate"].ToString(), 0);
                currency = dt.Rows[0]["CurrencyId"].ToString();
                docDt = SafeValue.SafeDate(dt.Rows[0]["DocDate"], new DateTime(1900, 1, 1));
                remarks = dt.Rows[0]["Description"].ToString();
                partyTo = dt.Rows[0]["PartyTo"].ToString();
            }
            else
            {
                e.Result = "Can't find the Ar Invoice!";
                return;
            }
            string sqlDet = string.Format("select count(SequenceId) from XAArInvoiceDet where DocId='{0}'", docId.Text);
            int detCnt = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sqlDet), 0);
            if (detCnt == 0)
            {
                e.Result = "No Detail, Can't Post";
                return;
            }

            //check account period
            if (acYear < 2000 || acPeriod < 1)
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
            sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='CR' and DocId='" + docId.Text + "'";
            decimal amt_det = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            sql = "select SUM(LocAmt) from XAArInvoiceDet where AcSource='DB' and DocId='" + docId.Text + "'";
            amt_det -= SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
            if (docAmt != amt_det)
            {
                e.Result = "Loc Amount can't match, can't post,Please first resave it,";
                return;
            }
            sql = "select count(LocAmt) from XAArInvoiceDet where AcCode='' and DocId='" + docId.Text + "'";
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
                //gl.GlNo = GetNo("GLENTRY");
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
                gl.SupplierBillNo = "";
                gl.SupplierBillDate = new DateTime(1900, 1, 1);
                Manager.ORManager.StartTracking(gl, InitialState.Inserted);
                Manager.ORManager.PersistChanges(gl);
                glOid = gl.SequenceId;
                // SetNo(gl.GlNo, "GLENTRY");
                //insert Detail
                OPathQuery query = new OPathQuery(typeof(XAArInvoiceDet), "DocId='" + docId.Text + "'");
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
                det1.GlNo = glOid;//gl.GlNo;
                det1.Remark = remarks;


                Manager.ORManager.StartTracking(det1, InitialState.Inserted);
                Manager.ORManager.PersistChanges(det1);
                decimal gstCrAmt = 0;
                decimal gstDbAmt = 0;
                string gstAcc = SafeValue.SafeString(Manager.ORManager.ExecuteScalar("SELECT AcCode FROM XXGstAccount where GstSrc='AR'"), "2033");
                for (int i = 0; i < set.Count; i++)
                {
                    try
                    {
                        index++;
                        XAArInvoiceDet invDet = set[i] as XAArInvoiceDet;
                        XAGlEntryDet det = new XAGlEntryDet();
                        if (invDet.AcCode == gstAcc)
                        {
                            if (invDet.AcSource == "CR")
                                gstCrAmt += invDet.LocAmt;
                            else
                                gstDbAmt += invDet.LocAmt;
                        }
                        else
                        {

                            det.AcCode = invDet.AcCode;
                            det.ArApInd = "AR";
                            det.AcPeriod = acPeriod;
                            det.AcSource = invDet.AcSource;
                            det.AcYear = acYear;
                            if (det.AcSource == "CR")
                            {
                                det.CrAmt = invDet.LocAmt - invDet.GstAmt;
                                det.CurrencyCrAmt = SafeValue.ChinaRound(det.CrAmt * exRate, 2);
                                det.DbAmt = 0;
                                det.CurrencyDbAmt = 0;
                                gstCrAmt += invDet.GstAmt;
                            }
                            else
                            {
                                det.DbAmt = invDet.LocAmt - invDet.GstAmt;
                                det.CurrencyDbAmt = SafeValue.ChinaRound(det.DbAmt * exRate, 2);
                                det.CrAmt = 0;
                                det.CurrencyCrAmt = 0;
                                gstDbAmt += invDet.GstAmt;
                            }
                            det.CurrencyId = invDet.Currency;
                            det.DocNo = docN;
                            det.DocType = docType;
                            det.ExRate = invDet.ExRate;
                            det.GlLineNo = index;
                            det.GlNo = gl.SequenceId;
                            det.Remark = invDet.ChgCode;

                            Manager.ORManager.StartTracking(det, InitialState.Inserted);
                            Manager.ORManager.PersistChanges(det);
                        }
                    }
                    catch
                    {
                        e.Result = "Posting Error, Please repost!";
                        DeleteGl(glOid);
                    }
                }
                if (gstCrAmt - gstDbAmt != 0)
                {
                    XAGlEntryDet det = new XAGlEntryDet();

                    det.AcCode = gstAcc;
                    det.ArApInd = "AR";
                    det.AcPeriod = acPeriod;
                    det.AcSource = "CR";
                    det.AcYear = acYear;
                    det.CrAmt = gstCrAmt - gstDbAmt;
                    det.CurrencyCrAmt = gstCrAmt - gstDbAmt;
                    det.DbAmt = 0;
                    det.CurrencyDbAmt = 0;
                    det.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                    det.DocNo = docN;
                    det.DocType = docType;
                    det.ExRate = 1;
                    det.GlLineNo = index + 1;
                    det.GlNo = gl.SequenceId;
                    det.Remark = "GST";
                    Manager.ORManager.StartTracking(det, InitialState.Inserted);
                    Manager.ORManager.PersistChanges(det);
                }
                UpdateArInv(docId.Text);
                EzshipLog.Log(docN, "", docType, "Post");
                e.Result = "Post completely!";
            }
            catch
            {
                e.Result = "Posting Error, Please repost!";
                DeleteGl(glOid);
            }
            #endregion
        }
    }
    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        SaveAndUpdate();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.ASPxGridView1.EditingRowVisibleIndex > -1)
        {
            string oid = SafeValue.SafeString(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "SequenceId" }));
            if (oid.Length > 0)
            {
                ASPxDateEdit DocDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
                DocDt.BackColor = System.Drawing.Color.FromArgb(255, 240, 240, 240);
                //DocDt.ReadOnly = true;
            }
        }
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        SaveAndUpdate();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        SaveAndUpdate();
        e.Cancel = true;
    }
    private void SaveAndUpdate()
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxMemo remarks1 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
        ASPxDateEdit dueDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DueDt") as ASPxDateEdit;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;
        ASPxComboBox acSource = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcSource") as ASPxComboBox;
        ASPxTextBox specialNote = this.ASPxGridView1.FindEditFormTemplateControl("txt_SpecialNote") as ASPxTextBox;

        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxTextBox jobType = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastType") as ASPxTextBox;

        string invN = docN.Text;
        C2.XAArInvoice inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invNCtr.Text, 0)) as XAArInvoice;
        if (inv == null)// first insert invoice
        {
            string counterType = "AR-IV";
            if (docType.Value.ToString() == "DN")
                counterType = "AR-DN";

            inv = new XAArInvoice();
            invN = C2Setup.GetNextNo("", counterType, docDate.Date);
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.DocType = docType.Value.ToString();
            inv.DocNo = invN.ToString();
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
            inv.Term = termId.Text;

            //
            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            if (inv.AcCode == "")
            {
                throw new Exception("Please frist set account code!");
            }
            inv.AcSource = acSource.Value.ToString();
            inv.SpecialNote = specialNote.Text;

            inv.MastType = jobType.Text;
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;

            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("",counterType, invN,inv.DocDate);
            }
            catch
            {
            }
			DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("ArInvoiceEdit.aspx?no=" + inv.DocNo);
 
        }
        else
        {
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");

            inv.Term = termId.Text;
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);

            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            inv.AcSource = acSource.Text;
            inv.SpecialNote = specialNote.Text;

            inv.MastType =jobType.Text;
            inv.MastRefNo = mastRefNCtr.Text;
            inv.JobRefNo = jobRefNCtr.Text;
            try
            {
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                Manager.ORManager.PersistChanges(inv);
                UpdateMaster(inv.SequenceId);
            }
            catch
            {
            }

        }
        Session["SeaIvEditWhere"] = "SequenceId=" + inv.SequenceId;
        this.dsArInvoice.FilterExpression = Session["SeaIvEditWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
    }
    private void SaveBill()
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        ASPxComboBox partyTo = this.ASPxGridView1.FindEditFormTemplateControl("cmb_PartyTo") as ASPxComboBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxMemo remarks1 = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remarks1") as ASPxMemo;
        ASPxComboBox termId = this.ASPxGridView1.FindEditFormTemplateControl("txt_TermId") as ASPxComboBox;
        ASPxDateEdit dueDt = this.ASPxGridView1.FindEditFormTemplateControl("txt_DueDt") as ASPxDateEdit;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxTextBox acCode = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcCode") as ASPxTextBox;
        ASPxComboBox acSource = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcSource") as ASPxComboBox;

        string invN = docN.Text;
        C2.XAArInvoice inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invNCtr.Text, 0)) as XAArInvoice;
        if (inv == null)// first insert invoice
        {
            string counterType = "AR-IV";
            if (docType.Value.ToString() == "DN")
                counterType = "AR-DN";

            inv = new XAArInvoice();
            inv.DocType = docType.Value.ToString();
            inv.DocDate = docDate.Date;
            invN = C2Setup.GetNextNo(inv.DocType, counterType, inv.DocDate);
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.DocNo = invN.ToString();
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
            inv.Term = termId.Text;
            //
            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;

            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId); ;
            inv.AcSource = acSource.Value.ToString();

            inv.MastRefNo = "0";
            inv.JobRefNo = "0";
            inv.MastType = "";
            inv.ExportInd = "N";
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.EntryDate = DateTime.Now;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            try
            {
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
            }
            catch
            {
            }
        }
        else
        {
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");

            inv.Term = termId.Text;
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);

            int dueDay = SafeValue.SafeInt(termId.Text.ToUpper().Replace("DAYS", "").Trim(), 0);
            inv.DocDueDate = inv.DocDate.AddDays(dueDay);//SafeValue.SafeDate(dueDt.Text, DateTime.Now);
            inv.Description = remarks1.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;

            inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
            inv.AcSource = acSource.Text;

            try
            {
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                Manager.ORManager.PersistChanges(inv);
            }
            catch
            {
            }
            UpdateMaster(inv.SequenceId);
        }
        Session["InvoiceEditWhere"] = "SequenceId=" + inv.SequenceId;
        this.dsArInvoice.FilterExpression = Session["InvoiceEditWhere"].ToString();
        if (this.ASPxGridView1.GetRow(0) != null)
            this.ASPxGridView1.StartEdit(0);
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
        string sql_invoice = "update XAArInvoice set ExportInd='Y' where SequenceId='" + docId + "'";
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
    #endregion



    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAArInvoiceDet));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        this.dsArInvoiceDet.FilterExpression = "DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocLineNo"] = 0;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        e.NewValues["Currency"] = docCurr.Text;
        //e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["AcSource"] = "CR";
        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxTextBox mastType = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastType") as ASPxTextBox;
        e.NewValues["MastType"] = mastType.Text;
        e.NewValues["MastRefNo"] = mastRefNCtr.Text;
        //e.NewValues["JobRefNo"] = jobRefNCtr.Text;
        e.NewValues["SplitType"] = "Set";
        e.NewValues["OtherAmt"] = 0;


        if (mastRefNCtr.Text.Length > 1 && jobRefNCtr.Text.Length > 1)
        {
            string sql = string.Format("SELECT round(case when Weight/1000>Volume then Weight/1000 else  Volume end,3) FROM SeaImport where RefNo='{0}' and JobNo='{1}'", mastRefNCtr.Text, jobRefNCtr.Text);
            if (mastType.Text == "SE")
                sql = string.Format("SELECT round(case when Weight/1000>Volume then Weight/1000 else  Volume end,3) FROM SeaExport where RefNo='{0}' and JobNo='{1}'", mastRefNCtr.Text, jobRefNCtr.Text);
            if (mastType.Text == "AI" || mastType.Text == "AE" || mastType.Text == "ACT")
                sql = string.Format("SELECT round(case when Weight/1000>Volume then Weight/1000 else  Volume end,3) FROM air_job where RefNo='{0}' and JobNo='{1}'", mastRefNCtr.Text, jobRefNCtr.Text);
            
            e.NewValues["Qty"] = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 1);
        }
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql_detCnt = "select count(DocId) from XAArInvoiceDet where DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
       // e.NewValues["CostingId"] = "";
        e.NewValues["DocId"] = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        e.NewValues["DocLineNo"] = lineNo;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        e.NewValues["DocNo"] = docN.Text;
        e.NewValues["DocType"] = docType.Text;
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;
        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        if (SafeValue.SafeString(e.NewValues["JobRefNo"]).Length > 1)
        {
            e.NewValues["SplitType"] = "Set";
        }
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;

        decimal amt = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["Qty"], 0) * SafeValue.SafeDecimal(e.NewValues["Price"], 0), 2);
        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(e.NewValues["Gst"], 0)), 2);
        decimal docAmt = amt + gstAmt;
        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["GstAmt"] = gstAmt;
        e.NewValues["DocAmt"] = docAmt;
        e.NewValues["LocAmt"] = locAmt;
        if (SafeValue.SafeString(e.NewValues["JobRefNo"]).Length > 1)
        {
            e.NewValues["SplitType"] = "Set";
        }
    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_InvDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
    }
    protected void grid_InvDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
    }
    protected void grid_InvDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox docId = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(SafeValue.SafeInt(docId.Text, 0));
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

    #region invoice sql
    private string GetNo(string noType)
    {
        string sql = "select Counter from XXSetup where Category='" + noType + "'";
        int cnt = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0) + 1;

        return cnt.ToString();

    }
    private void SetNo(string no, string noType)
    {
        string sql = string.Format("update XXSetup set counter='{0}' where category='{1}'", no, noType);
        int res = C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion
}
