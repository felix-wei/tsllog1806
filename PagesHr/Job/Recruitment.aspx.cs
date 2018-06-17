using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_Recruitment : BasePage
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
            Session["Recruitment"] = null;
            this.dsRecruitment.FilterExpression = "1=0";
        }
        if (Session["Recruitment"] != null)
        {
            this.dsRecruitment.FilterExpression = Session["Recruitment"].ToString();
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
        this.dsRecruitment.FilterExpression = where;
        Session["Recruitment"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("Recruitment", true);
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
            grd.ForceDataRowType(typeof(C2.HrRecruitment));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["StatusCode"] = "USE";
        e.NewValues["Date"] = DateTime.Today;
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
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrRecruitment), "Id='" + Id + "'");
            HrRecruitment recruit = C2.Manager.ORManager.GetObject(query) as HrRecruitment;
            bool action = false;

            if (recruit == null)
            {
                action = true;
                recruit = new HrRecruitment();
            }

            ASPxComboBox department = ASPxGridView1.FindEditFormTemplateControl("cmb_Department") as ASPxComboBox;
            recruit.Department = department.Text;
            ASPxComboBox recruitPic = ASPxGridView1.FindEditFormTemplateControl("txt_RecruitPic") as ASPxComboBox;
            recruit.Pic = SafeValue.SafeInt(recruitPic.Value, 0);
            ASPxDateEdit recruitDay = ASPxGridView1.FindEditFormTemplateControl("date_RecruitDay") as ASPxDateEdit;
            recruit.Date = recruitDay.Date;
            ASPxMemo remark1 = ASPxGridView1.FindEditFormTemplateControl("memo_RecruitWork") as ASPxMemo;
            recruit.Remark1 = remark1.Text;
            ASPxMemo remark2 = ASPxGridView1.FindEditFormTemplateControl("memo_RecruitSalary") as ASPxMemo;
            recruit.Remark2 = remark2.Text;
            ASPxComboBox status = ASPxGridView1.FindEditFormTemplateControl("cmb_Status") as ASPxComboBox;
            recruit.StatusCode = status.Text;

            if (action)
            {
                recruit.CreateBy = HttpContext.Current.User.Identity.Name;
                recruit.CreateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(recruit, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(recruit);
            }
            else
            {
                recruit.UpdateBy = HttpContext.Current.User.Identity.Name;
                recruit.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(recruit, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(recruit);
            }
            Session["Recruitment"] = "Id='" + recruit.Id + "'";
            this.dsRecruitment.FilterExpression = Session["Recruitment"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);

        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #region Det
    protected void gridRecruitmentDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPerson));
    }
    protected void gridRecruitmentDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPerson.FilterExpression = string.Format("CHARINDEX('<'+cast({0} as nvarchar(10))+'>',RecruitId)>0", SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0"));
    }
    protected void gridRecruitmentDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        e.NewValues["RecruitId"] = SafeValue.SafeString("<"+id.Text+">", "");
        e.NewValues["InterviewId"] = "";
        e.NewValues["Gender"] = "Femal";
    }

    protected void gridRecruitmentDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
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
    }

    protected void gridRecruitmentDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        ASPxTextBox name = grd.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
        if (SafeValue.SafeString(name.Text).Length < 1)
        {
            throw new Exception("Name not be null !!!");
            return;
        }

        ASPxCheckBox married = grd.FindEditFormTemplateControl("ckb_Married") as ASPxCheckBox;
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
    }

    protected void gridRecruitmentDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string mastId = SafeValue.SafeString(grd.GetMasterRowKeyValue(), "0");
        string sql = string.Format("UPDATE Hr_Person SET RecruitId=REPLACE(RecruitId,'<{0}>','') WHERE Id='{1}'", mastId, SafeValue.SafeString(e.Values["Id"]));
        int res = C2.Manager.ORManager.ExecuteCommand(sql);
        if (res < 1)
            throw new Exception("error!pls try again!");

        this.dsPerson.FilterExpression = string.Format("CHARINDEX('<'+cast({0} as nvarchar(10))+'>',RecruitId)>0", mastId);

        e.Cancel = true;
    }

    #endregion

}
