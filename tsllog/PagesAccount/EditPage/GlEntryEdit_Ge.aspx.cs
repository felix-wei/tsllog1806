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

public partial class Account_GlEntryEdit_Ge : BasePage
{
    protected void page_Init(object sender, EventArgs e)
    {
        // this.txt_refNo.Text = "280674";
        if (!IsPostBack)
        {
            Session["GlEntryWhere"] = null;
            if (Request.QueryString["no"] != null && Request.QueryString["type"] != null && Request.QueryString["no"].ToString() != "0")
            {
                this.txtSchNo.Text = Request.QueryString["no"].ToString();
                Session["GlEntryWhere"] = "DocNo='" +Request.QueryString["no"] + "' and DocType='GE'";
            }
            else if (Request.QueryString["no"] != null && Request.QueryString["no"].ToString() == "0")
            {
                if (Session["GlEntryWhere"] == null)
                {
                    this.ASPxGridView1.AddNewRow();
                }
            }
            else
                this.dsGlEntry.FilterExpression = "1=0";
        }
        if (Session["GlEntryWhere"] != null)
        {
            this.dsGlEntry.FilterExpression = Session["GlEntryWhere"].ToString();
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
            grd.ForceDataRowType(typeof(C2.XAGlEntry));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string[] currentPeriod = EzshipHelper.GetAccPeriod(DateTime.Today);
        string acYear = currentPeriod[1];
        string acPeriod = currentPeriod[0];

        e.NewValues["DocNo"] = 0;
        e.NewValues["AcYear"] = acYear;
        e.NewValues["AcPeriod"] = acPeriod;
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = new decimal(1.0);
        e.NewValues["PostInd"] = "N";
        e.NewValues["PostDate"] = new DateTime(1900, 1, 1);
        e.NewValues["ArApInd"] = "GL";
        e.NewValues["DocType"] = "GE";
        e.NewValues["CurrencyDbAmt"] = 0;
        e.NewValues["CurrencyCrAmt"] = 0;
        e.NewValues["DocDate"] = DateTime.Today;

    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        //ASPxTextBox docNo = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox arApInd = this.ASPxGridView1.FindEditFormTemplateControl("cbo_ArAPInd") as ASPxComboBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        //ASPxTextBox acYear = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcYear") as ASPxTextBox;
        //ASPxTextBox acPeriod = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcPeriod") as ASPxTextBox;
        ASPxTextBox docCurr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Currency") as ASPxTextBox;
        ASPxDateEdit docDate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocDt") as ASPxDateEdit;
        ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
        ASPxMemo rmk = this.ASPxGridView1.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;

        C2.XAGlEntry inv = Manager.ORManager.GetObject(typeof(XAGlEntry), SafeValue.SafeInt(oidCtr.Text, 0)) as XAGlEntry;
        bool isNew = false;
        if (inv == null)// first insert invoice
        {
            inv = new XAGlEntry();
            inv.ArApInd = arApInd.Text;
            //inv.DocNo = C2Setup.GetNextNo("GLENTRY");
			inv.DocNo = C2Setup.GetNextNo("", "GLENTRY", docDate.Date);

            inv.DocType = docType.Text;
            inv.CancelDate = new DateTime(1900, 1, 1);
            inv.CancelInd = "N";
            inv.CrAmt = 0;
            inv.CurrencyCrAmt = 0;
            inv.CurrencyDbAmt = 0;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.DbAmt = 0;
            inv.PostInd = "N";
            inv.Remark = rmk.Text;
            inv.UserId = HttpContext.Current.User.Identity.Name;
            inv.PartyTo = "";
            inv.OtherPartyName = "";
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);
            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
            try
            {
                inv.DocDate = DateTime.Now;
                inv.PostDate = DateTime.Now;
                inv.EntryDate = DateTime.Now;
                C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                C2.Manager.ORManager.PersistChanges(inv);
                isNew = true;
				C2Setup.SetNextNo("", "GLENTRY", inv.DocNo, inv.DocDate);

            }
            catch
            {
            }
        }
        else
        {
            //inv.ArApInd = arApInd.Text;
            inv.CurrencyId = docCurr.Text.ToString();
            inv.ExRate = SafeValue.SafeDecimal(exRate.Value, 1);
            if (inv.ExRate <= 0)
                inv.ExRate = 1;
            inv.Remark = rmk.Text;
            inv.DocDate = docDate.Date;
            string[] currentPeriod = EzshipHelper.GetAccPeriod(docDate.Date);
            inv.AcYear = SafeValue.SafeInt(currentPeriod[1], docDate.Date.Year);
            inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], docDate.Date.Month);
            try
            {
                Manager.ORManager.PersistChanges(inv);
                Manager.ORManager.StartTracking(inv, InitialState.Updated);
                string sql = string.Format("Update XAGlEntryDet set AcYear='{0}',AcPeriod='{1}' where GlNo='{2}'",inv.AcYear,inv.AcPeriod,inv.SequenceId);
                Manager.ORManager.ExecuteCommand(sql);
            }
            catch
            {
            }

        }
        Session["GlEntryWhere"] = "SequenceId=" + inv.SequenceId;
        this.dsGlEntry.FilterExpression = Session["GlEntryWhere"].ToString();
        if (isNew)
        {
            C2Setup.SetNextNo("GLENTRY",inv.DocNo);
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
    }
    #endregion

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
    }

    #region invoice det
    protected void grid_InvDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.XAGlEntryDet));
    }
    protected void grid_InvDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsGlEntryDet.FilterExpression = "GlNo='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_InvDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["DocLineNo"] = 0;
        e.NewValues["CurrencyId"] = System.Configuration.ConfigurationManager.AppSettings["Currency"];
        e.NewValues["ExRate"] = 1.0;
        e.NewValues["CrAmt"] = 0;
        e.NewValues["DbAmt"] = 0;
        e.NewValues["CurrencyCrAmt"] = 0;
        e.NewValues["CurrencyDbAmt"] = 0;
    }
    protected void grid_InvDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        ASPxTextBox docN = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocNo") as ASPxTextBox;
        ASPxComboBox docType = this.ASPxGridView1.FindEditFormTemplateControl("cbo_DocType") as ASPxComboBox;
        ASPxComboBox arApInd = this.ASPxGridView1.FindEditFormTemplateControl("cbo_ArAPInd") as ASPxComboBox;
        ASPxTextBox acYear = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcYear") as ASPxTextBox;
        ASPxTextBox acPeriod = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcPeriod") as ASPxTextBox;

        string sql_detCnt = "select count(*) from XAGlEntryDet where GlNo ='" + oidCtr.Text + "'";
        int lineNo = SafeValue.SafeInt(Manager.ORManager.ExecuteScalar(sql_detCnt), 0) + 1;
        e.NewValues["GlNo"] = SafeValue.SafeInt(oidCtr.Text,0);
        e.NewValues["DocNo"] = docN.Text;
        e.NewValues["DocType"] = docType.Text;
        e.NewValues["ArApInd"] = arApInd.Text;
        e.NewValues["AcYear"] = SafeValue.SafeInt(acYear.Text,2010);
        e.NewValues["AcPeriod"] = SafeValue.SafeInt(acPeriod.Text, 1);

        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;

        e.NewValues["GlLineNo"] = lineNo;
        e.NewValues["CurrencyCrAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CrAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["CurrencyDbAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DbAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        if (SafeValue.SafeDecimal(e.NewValues["CurrencyCrAmt"], 0) > 0)
            e.NewValues["AcSource"] = "CR";
        else
            e.NewValues["AcSource"] = "DB";


    }
    protected void grid_InvDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeDecimal(e.NewValues["ExRate"], 1) == 0)
            e.NewValues["ExRate"] = 1;
        e.NewValues["CurrencyCrAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["CrAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        e.NewValues["CurrencyDbAmt"] = SafeValue.ChinaRound(SafeValue.SafeDecimal(e.NewValues["DbAmt"], 0) * SafeValue.SafeDecimal(e.NewValues["ExRate"], 1), 2);
        if (SafeValue.SafeDecimal(e.NewValues["CurrencyCrAmt"], 0) > 0)
            e.NewValues["AcSource"] = "CR";
        else
            e.NewValues["AcSource"] = "DB";
        ASPxTextBox acYear = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcYear") as ASPxTextBox;
        ASPxTextBox acPeriod = this.ASPxGridView1.FindEditFormTemplateControl("txt_AcPeriod") as ASPxTextBox;
        e.NewValues["AcYear"] = SafeValue.SafeInt(acYear.Text, 2010);
        e.NewValues["AcPeriod"] = SafeValue.SafeInt(acPeriod.Text, 1);

    }
    protected void grid_InvDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["SequenceId"] = SafeValue.SafeString(e.Values["SequenceId"]);
    }
    protected void grid_InvDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_InvDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    protected void grid_InvDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxTextBox oidCtr = this.ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        UpdateMaster(oidCtr.Text);
    }
    private void UpdateMaster(string glId)
    {
        string sql = "select SUM(CrAmt) CrAmt,sum(DbAmt) DbAmt,sum(CurrencyCrAmt) CurrencyCrAmt,sum(CurrencyDbAmt) CurrencyDbAmt from XAGlEntryDet where GLNo='" + glId + "'";
        DataTable tab = Helper.Sql.List(sql);
        if (tab.Rows.Count > 0)
        {
            decimal CrAmt = SafeValue.SafeDecimal(tab.Rows[0]["CrAmt"], 0);
            decimal DbAmt = SafeValue.SafeDecimal(tab.Rows[0]["DbAmt"], 0);
            decimal CurrencyCrAmt = SafeValue.SafeDecimal(tab.Rows[0]["CurrencyCrAmt"], 0);
            decimal CurrencyDbAmt = SafeValue.SafeDecimal(tab.Rows[0]["CurrencyDbAmt"], 0);
            ASPxSpinEdit exRate = this.ASPxGridView1.FindEditFormTemplateControl("txt_DocExRate") as ASPxSpinEdit;
            sql = string.Format("Update XAGlEntry set CrAmt='{0}',DbAmt='{1}',CurrencyCrAmt='{2}',CurrencyDbAmt='{3}' where sequenceId='{4}'", CrAmt, DbAmt, CurrencyCrAmt, CurrencyDbAmt, glId);
            C2.Manager.ORManager.ExecuteCommand(sql);
        }
    }
    #endregion

    #region popub
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] Code = new object[grid.VisibleRowCount];
        object[] AcSource = new object[grid.VisibleRowCount];
        object[] Des = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            Code[i] = grid.GetRowValues(i, "Code");
            AcSource[i] = grid.GetRowValues(i, "AcDorc");
            Des[i] = grid.GetRowValues(i, "AcDesc");
        }
        e.Properties["cpCode"] = Code;
        e.Properties["cpSource"] = AcSource;
        e.Properties["cpDes"] = Des;
    }
    #endregion
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Delete")
        {
            ASPxGridView grd = sender as ASPxGridView;
            ASPxTextBox oid = grd.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            string sql = string.Format("Delete from XAGlEntryDet where GlNo='{0}'", oid.Text);
            int res = Manager.ORManager.ExecuteCommand(sql);
            if (res > -1)
            {
                sql = string.Format("Delete from XAGlEntry where SequenceId='{0}'", oid.Text);
                res = Manager.ORManager.ExecuteCommand(sql);
                if (res > -1)
                {
                    e.Result = "CS";//cancel success
                }
                else
                {
                    e.Result = "CF";
                }
            }
            else
            {
            }
        }
    }

}
