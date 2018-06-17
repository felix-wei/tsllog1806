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
        e.NewValues["MastType"] = "SI";
        e.NewValues["AcSource"] = "DB";
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["Term"] = "CASH";

        if (Request.QueryString["JobType"] != null && Request.QueryString["RefN"] != null && Request.QueryString["JobN"] != null)
        {
            e.NewValues["MastType"] = Request.QueryString["JobType"].ToString();
            e.NewValues["MastRefNo"] = Request.QueryString["RefN"].ToString();
            string houseNo= Request.QueryString["JobN"].ToString();
            e.NewValues["JobRefNo"] =houseNo;
            if (houseNo.Length > 1)
            {
                string sql = "SELECT CustomerID FROM  SeaImport WHERE (JobNo= '"+houseNo+"')";
                if(Request.QueryString["JobType"].ToString()=="SE")
                    sql = "SELECT CustomerID FROM  SeaExport WHERE (JobNo= '" + houseNo + "')";
                e.NewValues["PartyTo"] = SafeValue.SafeString(Manager.ORManager.ExecuteScalar(sql), "");

                e.NewValues["PartyName"] = EzshipHelper.GetPartyName(e.NewValues["PartyTo"]);
                if (Request.QueryString["JobType"].ToString() == "SI")
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

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        SaveAndUpdate();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        int oid = SafeValue.SafeInt(this.ASPxGridView1.GetRowValues(this.ASPxGridView1.EditingRowVisibleIndex, new string[] { "SequenceId" }),0);
        if (oid > 0)
        {
            string sqlExportInd = EzshipHelper.GetCloseIndByOid("IV", oid.ToString());
            if (sqlExportInd == "N")
            {
                ASPxGridView grd = this.ASPxGridView1.FindEditFormTemplateControl("grid_InvDet") as ASPxGridView;
                grd.AddNewRow();
            }
        }
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


        ASPxTextBox mastRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
        ASPxTextBox jobRefNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
        ASPxTextBox jobTypeCtr = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocCate") as ASPxTextBox;

        string invN = docN.Text;
        C2.XAArInvoice inv = Manager.ORManager.GetObject(typeof(XAArInvoice), SafeValue.SafeInt(invNCtr.Text, 0)) as XAArInvoice;
        if (inv == null)// first insert invoice
        {
            string counterType = "AR-IV";
            if (docType.Value.ToString() == "DN")
                counterType = "AR-DN";

            inv = new XAArInvoice();
            invN = C2Setup.GetNextNo(counterType);
            inv.PartyTo = SafeValue.SafeString(partyTo.Value, "");
            inv.DocType = docType.Value.ToString();
            inv.DocNo = invN.ToString();
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);

            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);

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

            inv.MastType = jobTypeCtr.Text;
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
                C2Setup.SetNextNo(counterType, invN);
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

            inv.MastType = jobTypeCtr.Text;
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
    #endregion
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox invNCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;

        ASPxGridView grd = this.ASPxGridView1.FindEditFormTemplateControl("grid_InvDet") as ASPxGridView;
        int lineNo = 1;
        #region update detail
        int start = 0;
        int end = 10000;
        for (int i = start; i < end; i++)
        {
            ASPxTextBox lineId = grd.FindRowTemplateControl(i, "txt_det_Oid") as ASPxTextBox;
            if (lineId != null)
            {
                try
                {

                    ASPxTextBox txtCheck = grd.FindRowTemplateControl(i, "txtCheck") as ASPxTextBox;
                    if (txtCheck.Text == "Y")
                    {
                        //delete
                        string sql_det = string.Format("delete from XaArInvoiceDet where SequenceId='{0}'", lineId.Text);
                        C2.Manager.ORManager.ExecuteCommand(sql_det);
                    }
                    else
                    {
                        ASPxTextBox lineChgCode = grd.FindRowTemplateControl(i, "txt_det_ChgCode") as ASPxTextBox;
                        ASPxTextBox lineAcCode = grd.FindRowTemplateControl(i, "txt_det_AcCode") as ASPxTextBox;
                        ASPxTextBox lineDes1 = grd.FindRowTemplateControl(i, "txt_det_Des1") as ASPxTextBox;
                        ASPxSpinEdit linePrice = grd.FindRowTemplateControl(i, "spin_det_Price") as ASPxSpinEdit;

                        ASPxTextBox lineCurrency = grd.FindRowTemplateControl(i, "txt_det_Currency") as ASPxTextBox;
                        ASPxSpinEdit lineGst = grd.FindRowTemplateControl(i, "spin_det_GstP") as ASPxSpinEdit;
                        ASPxComboBox lineAcSource = grd.FindRowTemplateControl(i, "txt_AcSource1") as ASPxComboBox;

                        ASPxTextBox lineDes2 = grd.FindRowTemplateControl(i, "txt_det_Remarks2") as ASPxTextBox;
                        ASPxSpinEdit lineQty = grd.FindRowTemplateControl(i, "spin_det_Qty") as ASPxSpinEdit;
                        ASPxSpinEdit lineExRate = grd.FindRowTemplateControl(i, "spin_det_ExRate") as ASPxSpinEdit;
                        ASPxTextBox lineGstType = grd.FindRowTemplateControl(i, "txt_det_GstType") as ASPxTextBox;

                        ASPxTextBox lineDes3 = grd.FindRowTemplateControl(i, "txt_det_Remakrs3") as ASPxTextBox;
                        ASPxTextBox lineUnit = grd.FindRowTemplateControl(i, "txt_det_Unit") as ASPxTextBox;

                        ASPxTextBox refNo = grd.FindRowTemplateControl(i, "txt_MastRefNo") as ASPxTextBox;
                        ASPxTextBox jobNo = grd.FindRowTemplateControl(i, "txt_JobRefNo") as ASPxTextBox;
                        ASPxTextBox mastType = grd.FindRowTemplateControl(i, "txt_mastType") as ASPxTextBox;

                        string currency = lineCurrency.Text;
                        string gstType = lineGstType.Text;
                        decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                        if (currency != "SGD")
                        {
                            gstType = "Z";
                            gst = 0;
                        }
                        decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                        if (exRate == 0)
                            exRate = 1;
                        decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                        decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                        decimal amt = SafeValue.ChinaRound(qty * price, 2);
                        decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                        string sql_update = string.Format(@"update XaArInvoiceDet set [DocLineNo] = '{1}'
                          ,[AcCode] = '{2}',[AcSource] = '{3}',[ChgCode] = '{4}'
                          ,[ChgDes1] =  '{5}',[ChgDes2] =  '{6}',[ChgDes3] =  '{7}',[GstType] =  '{8}'
                          ,[Qty] =  '{9}',[Price] =  '{10}',[Unit] =  '{11}',[Currency] = '{12}',[ExRate] =  '{13}'
                          ,[Gst] =  '{14}',[GstAmt] =  '{15}',[DocAmt] =  '{16}',[LocAmt] =  '{17}',MastRefNo='{18}',JobRefNo='{19}',MastType='{20}' where sequenceid='{0}'", lineId.Text, lineNo,
                          lineAcCode.Text, lineAcSource.Text, lineChgCode.Text,
                          lineDes1.Text, lineDes2.Text, lineDes3.Text, gstType,
                          qty, price, lineUnit.Text, currency, exRate,
                          gst, gstAmt, docAmt, locAmt,refNo.Text,jobNo.Text,mastType.Text);
                        C2.Manager.ORManager.ExecuteCommand(sql_update);
                        lineNo++;
                    }
                }
                catch
                { }
            }
        }
        #endregion
        #region inert detail
        bool insertDet = false;
        try
        {
            ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
            ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;

            ASPxTextBox lineChgCode_1 = grd.FindEditFormTemplateControl("txt_det_ChgCode") as ASPxTextBox;
            ASPxTextBox lineAcCode1 = grd.FindEditFormTemplateControl("txt_det_AcCode") as ASPxTextBox;
            if (lineChgCode_1.Text.Length > 0 && lineAcCode1.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit") as ASPxTextBox;


                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode1.Text, lineAcSource.Text,
                        lineChgCode_1.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt,refNo.Text,jobNo.Text,mastType.Text);

                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            ASPxTextBox lineChgCode_2 = grd.FindEditFormTemplateControl("txt_det_ChgCode_2") as ASPxTextBox;
            ASPxTextBox lineAcCode2 = grd.FindEditFormTemplateControl("txt_det_AcCode_2") as ASPxTextBox;

            if (lineChgCode_2.Text.Length > 0 && lineAcCode2.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_2") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_2") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_2") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_2") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_2") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_2") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_2") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_2") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_2") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_2") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_2") as ASPxTextBox;

                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_2") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_2") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_2") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode2.Text, lineAcSource.Text,
                        lineChgCode_2.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //3

            ASPxTextBox lineChgCode_3 = grd.FindEditFormTemplateControl("txt_det_ChgCode_3") as ASPxTextBox;
            ASPxTextBox lineAcCode3 = grd.FindEditFormTemplateControl("txt_det_AcCode_3") as ASPxTextBox;

            if (lineChgCode_3.Text.Length > 0 && lineAcCode3.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_3") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_3") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_3") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_3") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_3") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_3") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_3") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_3") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_3") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_3") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_3") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_3") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_3") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_3") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode3.Text, lineAcSource.Text,
                        lineChgCode_3.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);

                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //4

            ASPxTextBox lineChgCode_4 = grd.FindEditFormTemplateControl("txt_det_ChgCode_4") as ASPxTextBox;
            ASPxTextBox lineAcCode4 = grd.FindEditFormTemplateControl("txt_det_AcCode_4") as ASPxTextBox;

            if (lineChgCode_4.Text.Length > 0 && lineAcCode4.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_4") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_4") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_4") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_4") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_4") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_4") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_4") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_4") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_4") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_4") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_4") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_4") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_4") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_4") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode4.Text, lineAcSource.Text,
                        lineChgCode_4.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //5

            ASPxTextBox lineChgCode_5 = grd.FindEditFormTemplateControl("txt_det_ChgCode_5") as ASPxTextBox;
            ASPxTextBox lineAcCode5 = grd.FindEditFormTemplateControl("txt_det_AcCode_5") as ASPxTextBox;

            if (lineChgCode_5.Text.Length > 0 && lineAcCode5.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_5") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_5") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_5") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_5") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_5") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_5") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_5") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_5") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_5") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_5") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_5") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_5") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_5") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_5") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode5.Text, lineAcSource.Text,
                        lineChgCode_5.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //6

            ASPxTextBox lineChgCode_6 = grd.FindEditFormTemplateControl("txt_det_ChgCode_6") as ASPxTextBox;
            ASPxTextBox lineAcCode6 = grd.FindEditFormTemplateControl("txt_det_AcCode_6") as ASPxTextBox;

            if (lineChgCode_6.Text.Length > 0 && lineAcCode6.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_6") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_6") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_6") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_6") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_6") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_6") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_6") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_6") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_6") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_6") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_6") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_6") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_6") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_6") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode6.Text, lineAcSource.Text,
                        lineChgCode_6.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //7

            ASPxTextBox lineChgCode_7 = grd.FindEditFormTemplateControl("txt_det_ChgCode_7") as ASPxTextBox;
            ASPxTextBox lineAcCode7 = grd.FindEditFormTemplateControl("txt_det_AcCode_7") as ASPxTextBox;

            if (lineChgCode_7.Text.Length > 0 && lineAcCode7.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_7") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_7") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_7") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_7") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_7") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_7") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_7") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_7") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_7") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_7") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_7") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_7") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_7") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_7") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode7.Text, lineAcSource.Text,
                        lineChgCode_7.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //8

            ASPxTextBox lineChgCode_8 = grd.FindEditFormTemplateControl("txt_det_ChgCode_8") as ASPxTextBox;
            ASPxTextBox lineAcCode8 = grd.FindEditFormTemplateControl("txt_det_AcCode_8") as ASPxTextBox;

            if (lineChgCode_8.Text.Length > 0 && lineAcCode8.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_8") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_8") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_8") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_8") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_8") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_8") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_8") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_8") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_8") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_8") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_8") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_8") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_8") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_8") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode8.Text, lineAcSource.Text,
                        lineChgCode_8.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //9

            ASPxTextBox lineChgCode_9 = grd.FindEditFormTemplateControl("txt_det_ChgCode_9") as ASPxTextBox;
            ASPxTextBox lineAcCode9 = grd.FindEditFormTemplateControl("txt_det_AcCode_9") as ASPxTextBox;

            if (lineChgCode_9.Text.Length > 0 && lineAcCode9.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_9") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_9") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_9") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_9") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_9") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_9") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_9") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_9") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_9") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_9") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_9") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_9") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_9") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_9") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode9.Text, lineAcSource.Text,
                        lineChgCode_9.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //10

            ASPxTextBox lineChgCode_10 = grd.FindEditFormTemplateControl("txt_det_ChgCode_10") as ASPxTextBox;
            ASPxTextBox lineAcCode10 = grd.FindEditFormTemplateControl("txt_det_AcCode_10") as ASPxTextBox;

            if (lineChgCode_10.Text.Length > 0 && lineAcCode10.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_10") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_10") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_10") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_10") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_10") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_10") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_10") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_10") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_10") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_10") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_10") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_10") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_10") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_10") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode10.Text, lineAcSource.Text,
                        lineChgCode_10.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }

            //11

            ASPxTextBox lineChgCode_11 = grd.FindEditFormTemplateControl("txt_det_ChgCode_11") as ASPxTextBox;
            ASPxTextBox lineAcCode11 = grd.FindEditFormTemplateControl("txt_det_AcCode_11") as ASPxTextBox;

            if (lineChgCode_11.Text.Length > 0 && lineAcCode11.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_11") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_11") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_11") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_11") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_11") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_11") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_11") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_11") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_11") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_11") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_11") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_11") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_11") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_11") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode11.Text, lineAcSource.Text,
                        lineChgCode_11.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
            //12

            ASPxTextBox lineChgCode_12 = grd.FindEditFormTemplateControl("txt_det_ChgCode_12") as ASPxTextBox;
            ASPxTextBox lineAcCode12 = grd.FindEditFormTemplateControl("txt_det_AcCode_12") as ASPxTextBox;

            if (lineChgCode_12.Text.Length > 0 && lineAcCode12.Text.Length > 0)
            {
                ASPxTextBox lineDes1 = grd.FindEditFormTemplateControl("txt_det_Des1_12") as ASPxTextBox;
                ASPxSpinEdit linePrice = grd.FindEditFormTemplateControl("spin_det_Price_12") as ASPxSpinEdit;

                ASPxTextBox lineCurrency = grd.FindEditFormTemplateControl("txt_det_Currency_12") as ASPxTextBox;
                ASPxSpinEdit lineGst = grd.FindEditFormTemplateControl("spin_det_GstP_12") as ASPxSpinEdit;
                ASPxTextBox lineAcSource = grd.FindEditFormTemplateControl("txt_AcSource_12") as ASPxTextBox;

                ASPxTextBox lineDes2 = grd.FindEditFormTemplateControl("txt_det_Remarks2_12") as ASPxTextBox;
                ASPxSpinEdit lineQty = grd.FindEditFormTemplateControl("spin_det_Qty_12") as ASPxSpinEdit;
                ASPxSpinEdit lineExRate = grd.FindEditFormTemplateControl("spin_det_ExRate_12") as ASPxSpinEdit;
                ASPxTextBox lineGstType = grd.FindEditFormTemplateControl("txt_det_GstType_12") as ASPxTextBox;

                ASPxTextBox lineDes3 = grd.FindEditFormTemplateControl("txt_det_Remakrs3_12") as ASPxTextBox;
                ASPxTextBox lineUnit = grd.FindEditFormTemplateControl("txt_det_Unit_12") as ASPxTextBox;
                ASPxTextBox refNo = grd.FindEditFormTemplateControl("txt_MastRefNo_12") as ASPxTextBox;
                ASPxTextBox jobNo = grd.FindEditFormTemplateControl("txt_JobRefNo_12") as ASPxTextBox;
                ASPxTextBox mastType = grd.FindEditFormTemplateControl("txt_mastType_12") as ASPxTextBox;
                string currency = lineCurrency.Text;
                string gstType = lineGstType.Text;
                decimal gst = SafeValue.SafeDecimal(lineGst.Value, 0);
                if (currency != "SGD")
                {
                    gstType = "Z";
                    gst = 0;
                }
                decimal exRate = SafeValue.SafeDecimal(lineExRate.Value, 1);
                if (exRate == 0)
                    exRate = 1;
                decimal qty = SafeValue.SafeDecimal(lineQty.Value, 0);
                decimal price = SafeValue.SafeDecimal(linePrice.Value, 0);
                decimal amt = SafeValue.ChinaRound(qty * price, 2);
                decimal gstAmt = SafeValue.ChinaRound(amt * gst, 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exRate, 2);

                string sql_insert = string.Format(@"INSERT INTO [XAArInvoiceDet]
                        ([DocId],[DocNo],[DocType],[DocLineNo],[AcCode],[AcSource]
                        ,[ChgCode],[ChgDes1],[ChgDes2],[ChgDes3]
                        ,[GstType],[Qty],[Price],[Unit],[Currency]
                        ,[ExRate],[Gst],[GstAmt],[DocAmt],[LocAmt],[MastRefNo],[JobRefNo],[MastType])
                        VALUES('{0}','{1}','{2}','{3}','{4}','{5}',
                        '{6}','{7}','{8}','{9}',
                        '{10}','{11}','{12}','{13}','{14}',
                        '{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')",
                        invNCtr.Text, docN.Text, docType.Text, lineNo, lineAcCode12.Text, lineAcSource.Text,
                        lineChgCode_12.Text, lineDes1.Text, lineDes2.Text, lineDes3.Text,
                        gstType, qty, price, lineUnit.Text, currency,
                        exRate, gst, gstAmt, docAmt, locAmt, refNo.Text, jobNo.Text, mastType.Text);
                C2.Manager.ORManager.ExecuteCommand(sql_insert);
                lineNo++;
                insertDet = true;
            }
        }
        catch { }
        #endregion
        UpdateMaster(SafeValue.SafeInt(invNCtr.Text, 0));
        if (insertDet)
            e.Result = "INSERT";//INSERT DETAIL
        else
            e.Result = "UPDATE";
    }


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
        //ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        e.NewValues["Currency"] = this.ASPxGridView1.GetRowValues(0, new string[] { "CurrencyId" });
       // e.NewValues["Currency"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["GstAmt"] = 0;
        e.NewValues["DocAmt"] = 0;
        e.NewValues["LocAmt"] = 0;
        e.NewValues["Qty"] = 1;
        e.NewValues["Price"] = 0;
        e.NewValues["Gst"] = 0;
        e.NewValues["GstType"] = "Z";
        e.NewValues["AcSource"] = "CR";
        e.NewValues["MastType"] = this.ASPxGridView1.GetRowValues(0, new string[] { "MastType" });
        e.NewValues["MastRefNo"] = this.ASPxGridView1.GetRowValues(0, new string[] { "MastRefNo" });
        e.NewValues["JobRefNo"] = this.ASPxGridView1.GetRowValues(0, new string[] { "JobRefNo" });
        e.NewValues["SplitType"] = "SET";
    }

    protected void grid_InvDet_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        ASPxGridView grd = sender as ASPxGridView;

        ASPxTextBox chgCode = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_ChgCode") as ASPxTextBox;
        ASPxTextBox codeDes = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_Des1") as ASPxTextBox;
        ASPxTextBox unit = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_Unit") as ASPxTextBox;
        ASPxTextBox gstType = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_GstType") as ASPxTextBox;
        ASPxSpinEdit gstP = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_GstP") as ASPxSpinEdit;
        ASPxTextBox acCode = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_AcCode") as ASPxTextBox;
        chgCode.ClientInstanceName = "chgCode_" + e.VisibleIndex;
        codeDes.ClientInstanceName = "codeDes_" + e.VisibleIndex;
        unit.ClientInstanceName = "unit_" + e.VisibleIndex;
        gstType.ClientInstanceName = "gstType_" + e.VisibleIndex;
        gstP.ClientInstanceName = "gstP_" + e.VisibleIndex;
        acCode.ClientInstanceName = "acCode_" + e.VisibleIndex;
        ASPxButton pick = grd.FindRowTemplateControl(e.VisibleIndex, "btn_ChgCode_Pick") as ASPxButton;
        pick.ClientSideEvents.Click = "function(s, e) {" + string.Format("PopupChgCode({0}, {1}, {2}, {3}, {4}, {5})", chgCode.ClientInstanceName, codeDes.ClientInstanceName, unit.ClientInstanceName, gstType.ClientInstanceName, gstP.ClientInstanceName, acCode.ClientInstanceName) + "}";

        ASPxSpinEdit qty = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_Qty") as ASPxSpinEdit;
        ASPxSpinEdit price = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_Price") as ASPxSpinEdit;
        ASPxSpinEdit exRate = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_ExRate") as ASPxSpinEdit;
        ASPxSpinEdit gstAmt = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_GstAmt") as ASPxSpinEdit;
        ASPxSpinEdit docAmt = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_DocAmt") as ASPxSpinEdit;
        ASPxSpinEdit locAmt = grd.FindRowTemplateControl(e.VisibleIndex, "spin_det_LocAmt") as ASPxSpinEdit;
        qty.ClientInstanceName = "qty_" + e.VisibleIndex;
        price.ClientInstanceName = "price_" + e.VisibleIndex;
        exRate.ClientInstanceName = "exRate_" + e.VisibleIndex;
        gstAmt.ClientInstanceName = "gstAmt_" + e.VisibleIndex;
        docAmt.ClientInstanceName = "docAmt_" + e.VisibleIndex;
        locAmt.ClientInstanceName = "locAmt_" + e.VisibleIndex;

        qty.ClientSideEvents.ValueChanged = "function(s, e) {" + string.Format("PutAmt1({0}, {1}, {2}, {3}, {4}, {5},{6})", exRate.ClientInstanceName, qty.ClientInstanceName, price.ClientInstanceName, gstP.ClientInstanceName, gstAmt.ClientInstanceName, docAmt.ClientInstanceName, locAmt.ClientInstanceName) + "}";
        price.ClientSideEvents.ValueChanged = "function(s, e) {" + string.Format("PutAmt1({0}, {1}, {2}, {3}, {4}, {5},{6})", exRate.ClientInstanceName, qty.ClientInstanceName, price.ClientInstanceName, gstP.ClientInstanceName, gstAmt.ClientInstanceName, docAmt.ClientInstanceName, locAmt.ClientInstanceName) + "}";
        exRate.ClientSideEvents.ValueChanged = "function(s, e) {" + string.Format("PutAmt1({0}, {1}, {2}, {3}, {4}, {5},{6})", exRate.ClientInstanceName, qty.ClientInstanceName, price.ClientInstanceName, gstP.ClientInstanceName, gstAmt.ClientInstanceName, docAmt.ClientInstanceName, locAmt.ClientInstanceName) + "}";
        gstP.ClientSideEvents.ValueChanged = "function(s, e) {" + string.Format("PutAmt1({0}, {1}, {2}, {3}, {4}, {5},{6})", exRate.ClientInstanceName, qty.ClientInstanceName, price.ClientInstanceName, gstP.ClientInstanceName, gstAmt.ClientInstanceName, docAmt.ClientInstanceName, locAmt.ClientInstanceName) + "}";

        ASPxTextBox currency = grd.FindRowTemplateControl(e.VisibleIndex, "txt_det_Currency") as ASPxTextBox;
        currency.ClientInstanceName = "currency_" + e.VisibleIndex;
        ASPxButton pick1 = grd.FindRowTemplateControl(e.VisibleIndex, "btn_Currency_Pick") as ASPxButton;
        pick1.ClientSideEvents.Click = "function(s, e) {" + string.Format("PopupCurrency({0}, null)", currency.ClientInstanceName, exRate.ClientInstanceName) + "}";


        ASPxTextBox txtCB =
(ASPxTextBox)grd.FindRowTemplateControl(e.VisibleIndex, "txtCheck");
        txtCB.ClientInstanceName = string.Format("{0}_{1}", "txtCheck", e.KeyValue);
        //txtCB.Text = txtCB.ClientID;

    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        string sql_detCnt = "select count(DocId) from XAArInvoiceDet where DocId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["CostingId"] = "";
        e.NewValues["DocId"] = SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0);
        e.NewValues["DocLineNo"] = lineNo;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        e.NewValues["DocNo"] = docN.Text;
        e.NewValues["DocType"] = docType.Text;
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
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
            e.NewValues["SplitType"] = "SET";
        }
    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (!e.NewValues["Currency"].Equals("SGD"))
        {
            e.NewValues["GstType"] = "Z";
            e.NewValues["Gst"] = new decimal(0);
        }
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
            e.NewValues["SplitType"] = "SET";
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
            docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
            locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"],0);
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
