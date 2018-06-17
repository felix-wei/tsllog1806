using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_PayrollEdit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today.AddDays(7);
            Session["Payroll"] = null;
            this.dsPayroll.FilterExpression = "1=0";
        }
        if (Session["Payroll"] != null)
        {
            this.dsPayroll.FilterExpression = Session["Payroll"].ToString();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value,"");
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (id.Length > 0)
        {
            where = String.Format("Person='{0}'", id);
        }
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format("ToDate >= '{0}' and FromDate <= '{1}'", dateFrom, dateTo));
        }
        else
        {
            where = "1=1";
        }
        this.dsPayroll.FilterExpression = where;
        Session["Payroll"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Payroll", true);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    #region Payroll
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPayroll));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = 0 ;
        e.NewValues["StatusCode"] = "Draft";
        DateTime dt = DateTime.Now;
        DateTime start = new DateTime(dt.Year, dt.Month, 1);
        DateTime end = start.AddMonths(1).AddDays(-1);
        e.NewValues["FromDate"] = start;
        e.NewValues["ToDate"] = end;
        e.NewValues["CreateUser"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDate"] = DateTime.Today;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {
            string s = e.Parameters;
            if (s == "Cancle")
            {
                this.ASPxGridView1.CancelEdit();
                return;
            }
            ASPxTextBox Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            ASPxComboBox personId = ASPxGridView1.FindEditFormTemplateControl("cmb_Person") as ASPxComboBox;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPayroll), "Id='" + Id.Text + "'");
            HrPayroll payroll = C2.Manager.ORManager.GetObject(query) as HrPayroll;

            bool action = false;

            if (SafeValue.SafeString(personId.Value,"0") == "0")
            {
                throw new Exception("Name not be null!!!");
                return;
            }
            if (payroll == null)
            {
                action = true;
                payroll = new HrPayroll();
            }

            payroll.Person = SafeValue.SafeInt(personId.Value,0);
            ASPxDateEdit fromDate = ASPxGridView1.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
            payroll.FromDate = fromDate.Date;
            ASPxDateEdit toDate = ASPxGridView1.FindEditFormTemplateControl("txt_ToDate") as ASPxDateEdit;
            payroll.ToDate = toDate.Date;
            ASPxTextBox term = ASPxGridView1.FindEditFormTemplateControl("txt_Term") as ASPxTextBox;
            payroll.Term = term.Text;
            ASPxTextBox pic = ASPxGridView1.FindEditFormTemplateControl("txt_Pic") as ASPxTextBox;
            payroll.Pic = pic.Text;
            ASPxComboBox status = ASPxGridView1.FindEditFormTemplateControl("cmb_StatusCode") as ASPxComboBox;
            payroll.StatusCode = status.Text;
            ASPxMemo remark = ASPxGridView1.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            payroll.Remark = remark.Text;

            if (action)
            {
                payroll.CreateBy = HttpContext.Current.User.Identity.Name;
                payroll.CreateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(payroll, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(payroll);
            }
            else
            {
                payroll.UpdateBy = HttpContext.Current.User.Identity.Name;
                payroll.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(payroll, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(payroll);
            }

            Session["Payroll"] = "Id=" + payroll.Id;
            this.dsPayroll.FilterExpression = Session["Payroll"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {


    }
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Det
    protected void grid_PayrollDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPayrollDet));
    }
    protected void grid_PayrollDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        this.dsPayrollDet.FilterExpression = "PayrollId='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_PayrollDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = (decimal)0;
    }
    protected void grid_PayrollDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ChgCode"], "") == "")
            throw new Exception("ChgCode not be null !!!");
        ASPxTextBox id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        e.NewValues["PayrollId"] = SafeValue.SafeInt(id.Text, 0);
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
    }
    protected void grid_PayrollDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"], "");
        if (e.NewValues["ChgCode"] == "")
            throw new Exception("ChgCode not be null !!!");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
        e.NewValues["Pic"] = SafeValue.SafeString(e.NewValues["Pic"], "");
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"], 0);
    }
    protected void grid_PayrollDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_PayrollDet_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        UpdateMaster(SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0));
    }
    protected void grid_PayrollDet_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        UpdateMaster(SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0));
    }
    protected void grid_PayrollDet_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        UpdateMaster(SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0));
    }
    private void UpdateMaster(int mastId)
    {
        string sql = string.Format("Update Hr_Payroll set Amt=(select sum(Amt) from Hr_PayrollDet where PayrollId='{0}') where Id='{0}'", mastId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    #endregion
}
