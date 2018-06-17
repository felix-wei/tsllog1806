using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Modules_Hr_Job_Interview : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtSchDpm.Text = "";//"51856";// "55788";
            this.Date_From.Date = DateTime.Today.AddDays(-15);
            this.Date_To.Date = DateTime.Today.AddDays(8);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Interview"] = null;
            this.dsInterview.FilterExpression = "1=0";
        }
        if (Session["Interview"] != null)
        {
            this.dsInterview.FilterExpression = Session["Interview"].ToString();
        }
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string dpm = SafeValue.SafeString(txtSchDpm.Value, "");
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (dpm.Length > 0)
            where = String.Format("Department like '{0}%'", dpm);
        else
        {
            where = "1=1";
        }
        if (Date_From.Value != null && Date_To.Value != null)
        {
            dateFrom = Date_From.Date.ToString("yyyy-MM-dd");
            dateTo = Date_To.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format(" Date >= '{0}' and Date <= '{1}'", dateFrom, dateTo));
            //string.Format(" ref.Eta >= '{0}' and ref.Eta <= '{1}'", dateFrom, dateTo));
        }
        this.dsInterview.FilterExpression = where;
        Session["Interview"] = where;
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void ASPxGridView1_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrInterview));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["Date"] = DateTime.Today ;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

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
    protected void SaveAndUpdate()
    {
        try
        {

            ASPxTextBox txt_Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
            string Id = SafeValue.SafeString(txt_Id.Text, "");
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrInterview), "Id='" + Id + "'");
            HrInterview itv = C2.Manager.ORManager.GetObject(query) as HrInterview;
            bool action = false;

            if (itv == null)
            {
                action = true;
                itv = new HrInterview();
            }

            ASPxComboBox department = ASPxGridView1.FindEditFormTemplateControl("cmb_Department") as ASPxComboBox;
            itv.Department = department.Text;
            ASPxTextBox interviewPic = ASPxGridView1.FindEditFormTemplateControl("txt_InterviewPic") as ASPxTextBox;
            itv.Pic = interviewPic.Text;
            ASPxDateEdit interviewDay = ASPxGridView1.FindEditFormTemplateControl("date_InterviewDay") as ASPxDateEdit;
            itv.Date = interviewDay.Date;
            ASPxMemo remark = ASPxGridView1.FindEditFormTemplateControl("memo_InterviewRemark") as ASPxMemo;
            itv.Remark = remark.Text;
            ASPxMemo remark1 = ASPxGridView1.FindEditFormTemplateControl("memo_InterviewJoiner") as ASPxMemo;
            itv.Remark1 = remark1.Text;
            ASPxComboBox status = ASPxGridView1.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            itv.StatusCode = status.Text;

            if (action)
            {
                itv.CreateBy = HttpContext.Current.User.Identity.Name;
                itv.CreateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(itv, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(itv);
            }
            else
            {
                itv.UpdateBy = HttpContext.Current.User.Identity.Name;
                itv.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(itv, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(itv);
            }
            Session["Interview"] = "Id='" + itv.Id + "'";
            this.dsInterview.FilterExpression = Session["Interview"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #region Det
    protected void gridInterviewDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPerson));
    }
    protected void gridInterviewDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPerson.FilterExpression = string.Format("CHARINDEX('<'+cast({0} as nvarchar(10))+'>',InterviewId)>0", SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void gridInterviewDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        e.NewValues["InterviewId"] = SafeValue.SafeString("<" + id.Text + ">", "");
        e.NewValues["RecruitId"] = "";
        e.NewValues["Gender"] = "Male";
    }

    protected void gridInterviewDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox name = grd.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
        if (SafeValue.SafeString(name.Text).Length < 1)
        {
            throw new Exception("Name not be null !!!");
            return;
        }
        e.NewValues["Status"] = "CANDIDATE";
        e.NewValues["InterviewId"] = SafeValue.SafeString(e.NewValues["InterviewId"], "");
        e.NewValues["RecruitId"] = SafeValue.SafeString(e.NewValues["RecruitId"], "");

        ASPxCheckBox married = grd.FindEditFormTemplateControl("ckb_Married") as ASPxCheckBox;
        if (married.Checked)
            e.NewValues["Married"] = "Y";
        else
            e.NewValues["Married"] = "N";

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    protected void gridInterviewDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox name = grd.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
        if (SafeValue.SafeString(name.Text).Length < 1)
        {
            throw new Exception("Name not be null !!!");
            return;
        }

        ASPxRadioButton married = grd.FindEditFormTemplateControl("rbt_Married") as ASPxRadioButton;
        if (married.Checked)
            e.NewValues["Married"] = "Y";
        else
            e.NewValues["Married"] = "N";

        e.NewValues["InterviewId"] = SafeValue.SafeString(e.NewValues["InterviewId"], "");
        e.NewValues["RecruitId"] = SafeValue.SafeString(e.NewValues["RecruitId"], "");

        e.NewValues["Gender"] = SafeValue.SafeString(e.NewValues["Gender"]);
        e.NewValues["IcNo"] = SafeValue.SafeString(e.NewValues["IcNo"]);
        e.NewValues["BirthDay"] = SafeValue.SafeDate(e.NewValues["BirthDay"], new DateTime(1753, 1, 1));
        e.NewValues["Country"] = SafeValue.SafeString(e.NewValues["Country"]);
        e.NewValues["Race"] = SafeValue.SafeString(e.NewValues["Race"]);
        e.NewValues["Religion"] = SafeValue.SafeString(e.NewValues["Religion"]);
        e.NewValues["Telephone"] = SafeValue.SafeString(e.NewValues["Telephone"]);
        e.NewValues["Email"] = SafeValue.SafeString(e.NewValues["Email"]);
        e.NewValues["Address"] = SafeValue.SafeString(e.NewValues["Address"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);
        e.NewValues["Remark3"] = SafeValue.SafeString(e.NewValues["Remark3"]);
        e.NewValues["Remark4"] = SafeValue.SafeString(e.NewValues["Remark4"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }

    protected void gridInterviewDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string mastId = SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0");
        string sql = string.Format("UPDATE Hr_Person SET InterviewId=REPLACE(InterviewId,'<{0}>','') WHERE Id='{1}'", mastId, SafeValue.SafeString(e.Values["Id"]));
        int res = C2.Manager.ORManager.ExecuteCommand(sql);
        if (res < 1)
            throw new Exception("error!pls try again!");

        this.dsPerson.FilterExpression = string.Format("CHARINDEX('<'+cast({0} as nvarchar(10))+'>',InterviewId)>0", mastId);

        e.Cancel = true;
    }

    #endregion

}
