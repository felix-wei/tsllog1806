using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class Modules_Hr_Master_AllPerson : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtSchStatus.Text = "All";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["AllPersonWhere"] = null;
            this.dsPerson.FilterExpression = "1=0";
        }
        if (Session["AllPersonWhere"] != null)
        {
            this.dsPerson.FilterExpression = Session["AllPersonWhere"].ToString();
        }
        btn_Sch_Click(null, null);
    }
    //protected void txtSchStatus_ValueChanged(object sender, EventArgs e)
    //{
    //    string type = SafeValue.SafeString(txtSchStatus.Value, "");
    //    this.dsSchPerson.FilterExpression = GetWhere("Id>0", string.Format("Status='{0}'", type));
    //}
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = SafeValue.SafeString(txtSchName.Text, "");
        string type = SafeValue.SafeString(txtSchStatus.Value, "");
        string where = "Id>0";
        if (name.Length > 0)
        {
            where = GetWhere(where, string.Format("NAME LIKE '{0}'", name.Replace("'", "''")));
        }
        if (type.Length > 0)
            where = GetWhere(where, string.Format("Status='{0}'", type));
        this.dsPerson.FilterExpression = where;
        Session["AllPersonWhere"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("All Personel", true);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPerson));
        }
    }
    protected void grid_Load(object sender, EventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
        }
        if (txtSchStatus.Text.ToLower() == "candidate")
        {
            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
        }
        

        //else if (txtSchStatus.Text.ToLower() == "resignation")
        //{
        //    btn_Add.ClientVisible = false;
        //}
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxPageControl pageControl1 = this.grid.FindEditFormTemplateControl("pageControl1") as ASPxPageControl;
            int id = SafeValue.SafeInt(grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Id" }), 0);
            string oldStatus = SafeValue.SafeString(grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "Status" }));
            Panel divPersonDet = pageControl.FindControl("divPersonDet") as Panel;

            if (oldStatus.ToLower() == "candidate")
            {
                divPersonDet.Visible = false;
                pageControl.TabPages[1].ClientVisible = false;
                pageControl.TabPages[2].ClientVisible = false;
                pageControl.TabPages[3].ClientVisible = false;
                pageControl.TabPages[4].ClientVisible = false;
                pageControl.TabPages[5].ClientVisible = false;
                pageControl1.TabPages[1].ClientVisible = false;
                pageControl1.TabPages[2].ClientVisible = false;
                pageControl1.TabPages[3].ClientVisible = false;
                pageControl1.TabPages[4].ClientVisible = false;
                pageControl1.TabPages[5].ClientVisible = false;
                pageControl1.TabPages[6].ClientVisible = false;
                pageControl1.TabPages[7].ClientVisible = false;
            }
        }
    }

    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string s = e.Parameters;
        if (s == "Save")
        {
            AddOrUpdate();
        }
        else if (s == "Cancle")
            this.grid.CancelEdit();

    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["InterviewId"] = "";
        e.NewValues["RecruitId"] = "";
        e.NewValues["Gender"] = "Male";
        e.NewValues["Status"] = "CANDIDATE";
        e.NewValues["EmploymentType"] = "FullTime";
        e.NewValues["NationalServiceStatus"] = "NotAffected";
        e.NewValues["EmploySource"] = "Advertisement";
        e.NewValues["DrivingType"] = "No";
        e.NewValues["HealthStatus"] = "Normal";
        e.NewValues["OverTimeStatus"] = "Yes";
        e.NewValues["RelativeWorking"] = "No";
        e.NewValues["TerminateStatus"] = "No";
        e.NewValues["CheckReferenceStatus"] = "No";
        //ASPxComboBox status = grid.FindEditFormTemplateControl("cmb_Type") as ASPxComboBox;
        //status.Items.Remove(status.Items[2]);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);

    }
    protected void AddOrUpdate()
    {
        try
        {

            ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
            ASPxTextBox txt_Id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
            ASPxTextBox txt_Name = grid.FindEditFormTemplateControl("txt_Name") as ASPxTextBox;
            string Id = SafeValue.SafeString(txt_Id.Text, "");
            string name = txt_Name.Text;
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPerson), "Id='" + Id + "'");
            HrPerson person = C2.Manager.ORManager.GetObject(query) as HrPerson;
            bool action = false;

            if (SafeValue.SafeString(txt_Name.Text,"") == "")
            {
                throw new Exception("Name not be null!!!");
                return;
            }
            if (person == null || person.Id == 0)
            {
                action = true;
                person = new HrPerson();
            }
            person.Name = name;
            ASPxComboBox status = grid.FindEditFormTemplateControl("cmb_Type") as ASPxComboBox;
            person.Status = status.Text;
            ASPxTextBox interviewId = this.grid.FindEditFormTemplateControl("txt_InterviewId") as ASPxTextBox;
            person.InterviewId = SafeValue.SafeString(interviewId.Text, "");
            ASPxTextBox recruitId = this.grid.FindEditFormTemplateControl("txt_RecruitId") as ASPxTextBox;
            person.RecruitId = SafeValue.SafeString(recruitId.Text, "");
            ASPxComboBox gender = pageControl.FindControl("cbo_Gender") as ASPxComboBox;
            person.Gender = gender.Text;
            ASPxTextBox icno = pageControl.FindControl("txtIcNo") as ASPxTextBox;
            person.IcNo = icno.Text;
            ASPxDateEdit birthday = pageControl.FindControl("date_Birthday") as ASPxDateEdit;
            person.BirthDay = birthday.Date;
            ASPxButtonEdit country = pageControl.FindControl("txt_Country") as ASPxButtonEdit;
            person.Country = country.Text;
            ASPxTextBox race = pageControl.FindControl("txt_Race") as ASPxTextBox;
            person.Race = race.Text;
            ASPxTextBox religion = pageControl.FindControl("txt_Religion") as ASPxTextBox;
            person.Religion = religion.Text;
            ASPxCheckBox married = pageControl.FindControl("ckb_Married") as ASPxCheckBox;
            if (married.Checked)
                person.Married = "Y";
            else
                person.Married = "N";
            ASPxTextBox telephone = pageControl.FindControl("txt_Telephone") as ASPxTextBox;
            person.Telephone = telephone.Text;
            ASPxTextBox email = pageControl.FindControl("txt_Email") as ASPxTextBox;
            person.Email = email.Text;
            ASPxComboBox hrGroup = pageControl.FindControl("cmb_HrGroup") as ASPxComboBox;
            person.HrGroup = hrGroup.Text;
            ASPxComboBox department = pageControl.FindControl("cmb_Department") as ASPxComboBox;
            person.Department = department.Text;
            ASPxComboBox hrRole = pageControl.FindControl("cmb_Role") as ASPxComboBox;
            person.HrRole = hrRole.Text;
            ASPxMemo address = pageControl.FindControl("memo_Address") as ASPxMemo;
            person.Address = address.Text;
            ASPxMemo remark = pageControl.FindControl("memo_Remark") as ASPxMemo;
            person.Remark = remark.Text;
            ASPxMemo remark1 = pageControl.FindControl("memo_Profile") as ASPxMemo;
            person.Remark1 = remark1.Text;
            ASPxMemo remark2 = pageControl.FindControl("memo_Work") as ASPxMemo;
            person.Remark2 = remark2.Text;
            ASPxMemo remark3 = pageControl.FindControl("memo_Education") as ASPxMemo;
            person.Remark3 = remark3.Text;
            ASPxMemo remark4 = pageControl.FindControl("memo_Family") as ASPxMemo;
            person.Remark4 = remark4.Text;
            ASPxMemo remark5 = pageControl.FindControl("memo_Emergency") as ASPxMemo;
            person.Remark5 = remark5.Text;
            ASPxComboBox PassType = pageControl.FindControl("cmb_PassType") as ASPxComboBox;
            person.PassType = PassType.Text;
            ASPxTextBox PassNo = pageControl.FindControl("txt_PassNo") as ASPxTextBox;
            person.PassNo = PassNo.Text;
            ASPxTextBox Dialect = pageControl.FindControl("txt_Dialect") as ASPxTextBox;
            person.Dialect = Dialect.Text;
            ASPxTextBox LanguageWrittenRemark = pageControl.FindControl("txt_LanguageWrittenRemark") as ASPxTextBox;
            person.LanguageWrittenRemark = LanguageWrittenRemark.Text;
            ASPxTextBox LanguageSpokenRemark = pageControl.FindControl("txt_LanguageSpokenRemark") as ASPxTextBox;
            person.LanguageSpokenRemark = LanguageSpokenRemark.Text;
            ASPxComboBox NationalServiceStatus = pageControl.FindControl("cmb_NationalServiceStatus") as ASPxComboBox;
            person.NationalServiceStatus = NationalServiceStatus.Text;
            ASPxTextBox NationalServiceRemark = pageControl.FindControl("txt_NationalServiceRemark") as ASPxTextBox;
            person.NationalServiceRemark = NationalServiceRemark.Text;
            ASPxComboBox EmploySource = pageControl.FindControl("cmb_EmploySource") as ASPxComboBox;
            person.EmploySource = EmploySource.Text;
            ASPxTextBox EmploySourceRemark = pageControl.FindControl("cmb_EmploySourceRemark") as ASPxTextBox;
            person.EmploySourceRemark = EmploySourceRemark.Text;
            ASPxComboBox DrivingType = pageControl.FindControl("cmb_DrivingType") as ASPxComboBox;
            person.DrivingType = DrivingType.Text;
            ASPxTextBox DrivingRemark = pageControl.FindControl("txt_DrivingRemark") as ASPxTextBox;
            person.DrivingRemark = DrivingRemark.Text;
            ASPxComboBox HealthStatus = pageControl.FindControl("cmb_HealthStatus") as ASPxComboBox;
            person.HealthStatus = HealthStatus.Text;
            ASPxTextBox HealthRemark = pageControl.FindControl("txt_HealthRemark") as ASPxTextBox;
            person.HealthRemark = HealthRemark.Text;
            ASPxComboBox OverTimeStatus = pageControl.FindControl("cmb_OverTimeStatus") as ASPxComboBox;
            person.OverTimeStatus = OverTimeStatus.Text;
            ASPxTextBox OverTimeRemark = pageControl.FindControl("txt_OverTimeRemark") as ASPxTextBox;
            person.OverTimeRemark = OverTimeRemark.Text;
            ASPxComboBox RelativeWorkingStatus = pageControl.FindControl("cmb_RelativeWorkingStatus") as ASPxComboBox;
            person.RelativeWorkingStatus = RelativeWorkingStatus.Text;
            ASPxTextBox RelativeWorkingRemark = pageControl.FindControl("txt_RelativeWorkingRemark") as ASPxTextBox;
            person.RelativeWorkingRemark = RelativeWorkingRemark.Text;
            ASPxComboBox TerminateStatus = pageControl.FindControl("cmb_TerminateStatus") as ASPxComboBox;
            person.TerminateStatus = TerminateStatus.Text;
            ASPxTextBox TerminateRemark = pageControl.FindControl("txt_TerminateRemark") as ASPxTextBox;
            person.TerminateRemark = TerminateRemark.Text;
            ASPxComboBox CheckReferenceStatus = pageControl.FindControl("cmb_CheckReferenceStatus") as ASPxComboBox;
            person.CheckReferenceStatus = CheckReferenceStatus.Text;
            ASPxTextBox CheckReferenceRemark = pageControl.FindControl("txt_CheckReferenceRemark") as ASPxTextBox;
            person.CheckReferenceRemark = CheckReferenceRemark.Text;
            ASPxComboBox EmploymentType = pageControl.FindControl("cmb_EmploymentType") as ASPxComboBox;
            person.EmploymentType = EmploymentType.Text;
            ASPxComboBox SalaryPaymode = pageControl.FindControl("cmb_SalaryPaymode") as ASPxComboBox;
            person.SalaryPaymode = SalaryPaymode.Text;
            ASPxSpinEdit HoursPerDay = pageControl.FindControl("spin_HoursPerDay") as ASPxSpinEdit;
            person.HoursPerDay = SafeValue.SafeInt(HoursPerDay.Text, 0);
            ASPxSpinEdit HoursPerWeek = pageControl.FindControl("spin_HoursPerWeek") as ASPxSpinEdit;
            person.HoursPerWeek = SafeValue.SafeInt(HoursPerWeek.Text, 0);
            ASPxSpinEdit HoursPerMonth = pageControl.FindControl("spin_HoursPerMonth") as ASPxSpinEdit;
            person.HoursPerMonth = SafeValue.SafeInt(HoursPerMonth.Text, 0);
            ASPxSpinEdit DaysPerWeek = pageControl.FindControl("spin_DaysPerWeek") as ASPxSpinEdit;
            person.DaysPerWeek = SafeValue.SafeInt(DaysPerWeek.Text, 0);
            ASPxSpinEdit DaysPerMonth = pageControl.FindControl("spin_DaysPerMonth") as ASPxSpinEdit;
            person.DaysPerMonth = SafeValue.SafeInt(DaysPerMonth.Text, 0);
            ASPxDateEdit ProbationFromDate = pageControl.FindControl("date_ProbationFromDate") as ASPxDateEdit;
            person.ProbationFromDate = SafeValue.SafeDate(ProbationFromDate.Value, new DateTime(1753, 1, 1));
            ASPxDateEdit ProbationToDate = pageControl.FindControl("date_ProbationToDate") as ASPxDateEdit;
            person.ProbationToDate = SafeValue.SafeDate(ProbationToDate.Value, new DateTime(1753, 1, 1));

            ASPxComboBox pr = pageControl.FindControl("cmb_PRInd") as ASPxComboBox;
            person.PRInd = pr.Text;
            ASPxSpinEdit prYear = pageControl.FindControl("PR_Year") as ASPxSpinEdit;
            person.PRYear = SafeValue.SafeInt(prYear.Text, 0);

            if (action)
            {
                person.CreateBy = HttpContext.Current.User.Identity.Name;
                person.CreateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(person, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(person);
            }
            else
            {
                person.UpdateBy = HttpContext.Current.User.Identity.Name;
                person.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(person, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(person);
            }

            Session["AllPersonWhere"] = "Id='" + person.Id + "'";
            this.dsPerson.FilterExpression = Session["AllPersonWhere"].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
            else
            {
                Session["AllPersonWhere"] = "Id>0";
                this.dsPerson.FilterExpression = Session["AllPersonWhere"].ToString();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }


    #region EducationHistory
    protected void gridPersonDet4_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonDet4));
    }
    protected void gridPersonDet4_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet4.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet4_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SchoolYear"] = 0;
        e.NewValues["Status"] = "Completed";
    }
    protected void gridPersonDet4_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["SchoolName"]).Length < 1)
            throw new Exception("SchoolName not be null !!!");

        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], new DateTime(1753, 1, 1));
        e.NewValues["DateTo"] = SafeValue.SafeDate(e.NewValues["DateTo"], new DateTime(1753, 1, 1));

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet4_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["SchoolName"]).Length < 1)
            throw new Exception("SchoolName not be null !!!");
        if (SafeValue.SafeInt(e.NewValues["SchoolYear"], 0) == 0)
            throw new Exception("Pls keyin SchoolYear!");
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], new DateTime(1753, 1, 1));
        e.NewValues["DateTo"] = SafeValue.SafeDate(e.NewValues["DateTo"], new DateTime(1753, 1, 1));
        e.NewValues["HighestLevel"] = SafeValue.SafeString(e.NewValues["HighestLevel"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet4_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Employment History
    protected void gridPersonDet5_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonDet5));
    }
    protected void gridPersonDet5_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet5.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet5_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void gridPersonDet5_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["EmployerName"]).Length < 1)
            throw new Exception("EmployerName not be null !!!");
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], new DateTime(1753, 1, 1));
        e.NewValues["DateTo"] = SafeValue.SafeDate(e.NewValues["DateTo"], new DateTime(1753, 1, 1));

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet5_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["EmployerName"]).Length < 1)
            throw new Exception("EmployerName not be null !!!");
        e.NewValues["DateFrom"] = SafeValue.SafeDate(e.NewValues["DateFrom"], new DateTime(1753, 1, 1));
        e.NewValues["DateTo"] = SafeValue.SafeDate(e.NewValues["DateTo"], new DateTime(1753, 1, 1));
        e.NewValues["Salary"] = SafeValue.SafeDecimal(e.NewValues["Salary"]);
        e.NewValues["Allowance"] = SafeValue.SafeString(e.NewValues["Allowance"]);
        e.NewValues["LeaveStatus"] = SafeValue.SafeString(e.NewValues["LeaveStatus"]);
        e.NewValues["ReasonForLeaving"] = SafeValue.SafeString(e.NewValues["ReasonForLeaving"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet5_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Family Member
    protected void gridPersonDet6_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonDet6));
    }
    protected void gridPersonDet6_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet6.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet6_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void gridPersonDet6_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Name"]).Length < 1)
            throw new Exception("Name not be null !!!");
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);
        e.NewValues["Age"] = SafeValue.SafeInt(e.NewValues["Age"], 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet6_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["Name"]).Length < 1)
            throw new Exception("Name not be null !!!");
        e.NewValues["Relationship"] = SafeValue.SafeString(e.NewValues["Relationship"]);
        e.NewValues["Occupation"] = SafeValue.SafeString(e.NewValues["Occupation"]);
        e.NewValues["Age"] = SafeValue.SafeInt(e.NewValues["Age"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
    }
    protected void gridPersonDet6_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region PersonDet
    protected void gridPersonDet_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPersonDet1));
        }
    }
    protected void gridPersonDet_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void gridPersonDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["BeginDate"] = SafeValue.SafeDate(e.NewValues["BeginDate"], new DateTime(1753, 1, 1));
        e.NewValues["ResignDate"] = SafeValue.SafeDate(e.NewValues["ResignDate"], new DateTime(1753, 1, 1));
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Contact
    protected void gridPersonDet2_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonDet2));
    }
    protected void gridPersonDet2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet2.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet2_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void gridPersonDet2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ContactName"], "") == "")
            throw new Exception("Name not be null !!!");
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ContactName"], "") == "")
            throw new Exception("Name not be null !!!");
        e.NewValues["Gender"] = SafeValue.SafeString(e.NewValues["Gender"]);
        e.NewValues["Dob"] = SafeValue.SafeDate(e.NewValues["Dob"], new DateTime(1753, 01, 01));
        e.NewValues["Phone"] = SafeValue.SafeString(e.NewValues["Phone"]);
        e.NewValues["Mobile"] = SafeValue.SafeString(e.NewValues["Mobile"]);
        e.NewValues["Email"] = SafeValue.SafeString(e.NewValues["Email"]);
        e.NewValues["RelationShip"] = SafeValue.SafeString(e.NewValues["RelationShip"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Bank Account
    protected void gridPersonDet3_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrPersonDet3));
    }
    protected void gridPersonDet3_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonDet3.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void gridPersonDet3_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxGridView grd = sender as ASPxGridView;
            ASPxCheckBox isPayroll = grd.FindEditFormTemplateControl("ckb_IsPayroll") as ASPxCheckBox;
            bool oldIsPayroll = SafeValue.SafeBool(grid.GetRowValues(this.grid.EditingRowVisibleIndex, new string[] { "IsPayroll" }), false);
            if (isPayroll != null)
            {
                if (oldIsPayroll)
                    isPayroll.Checked = true;
                else
                    isPayroll.Checked = false;
            }
        }
    }
    protected void gridPersonDet3_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

    }
    protected void gridPersonDet3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        ASPxGridView grd = sender as ASPxGridView;
        ASPxCheckBox isPayroll = grd.FindControl("ckb_IsPayroll") as ASPxCheckBox;
        if (isPayroll.Checked)
            e.NewValues["IsPayroll"] = true;
        else
            e.NewValues["IsPayroll"] = false;

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["BankCode"] = SafeValue.SafeString(e.NewValues["BankCode"]);
        e.NewValues["BankName"] = SafeValue.SafeString(e.NewValues["BankName"]);
        e.NewValues["BranchCode"] = SafeValue.SafeString(e.NewValues["BranchCode"]);
        e.NewValues["SwiftCode"] = SafeValue.SafeString(e.NewValues["SwiftCode"]);
        e.NewValues["AccNo"] = SafeValue.SafeString(e.NewValues["AccNo"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        ASPxGridView grd = sender as ASPxGridView;
        ASPxCheckBox isPayroll = grd.FindEditFormTemplateControl("ckb_IsPayroll") as ASPxCheckBox;
        if (isPayroll.Checked)
            e.NewValues["IsPayroll"] = true;
        else
            e.NewValues["IsPayroll"] = false;

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void gridPersonDet3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Contract
    protected void grid_Contract_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrContract));
        }
    }
    protected void grid_Contract_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsContract.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_Contract_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Date"] = DateTime.Today;
    }
    protected void grid_Contract_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Contract_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["No"] = SafeValue.SafeString(e.NewValues["No"]);
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);
        e.NewValues["Remark3"] = SafeValue.SafeString(e.NewValues["Remark3"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Contract_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Recruitment/InterView
    protected void grid_RecItv_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        string recruitId = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select RecruitId from Hr_Person where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'"),"");
        string interviewId = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar("select InterviewId from Hr_Person where Id='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'"), "");
        string sql = string.Format(@"select 'Recruit' AS BelongTo,Id,convert(nvarchar(10),Date,103) AS Date,Pic,Department,StatusCode,Remark1,Remark2 AS Remark from Hr_Recruitment WHERE CHARINDEX('<'+CAST(Id AS NVARCHAR(10))+'>','{0}')>0 UNION ALL select 'Interview' AS BelongTo,Id,convert(nvarchar(10),Date,103) AS Date,Pic,Department,StatusCode,Remark1,Remark from Hr_Interview where  CHARINDEX('<'+CAST(Id AS NVARCHAR(10))+'>','{1}')>0  ORDER BY Date", recruitId, interviewId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        grd.DataSource = tab;
        grd.DataBind();
    }
    protected void grid_RecItv_BeforePerformDataSelect(object sender, EventArgs e)
    {
    }
    #endregion

    #region OT/Expense
    protected void grid_Trans_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPersonTran));
        }
    }
    protected void grid_Trans_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsTrans.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_Trans_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl1") as ASPxPageControl;
        ASPxTextBox type = pageControl.FindControl("txtTransType") as ASPxTextBox;
        e.NewValues["Type"] = type.Text;
        e.NewValues["Amt"] = 0;
        e.NewValues["Date1"] = DateTime.Today;
        e.NewValues["Date2"] = DateTime.Today;
        e.NewValues["Time1"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        e.NewValues["Time2"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
    }
    protected void grid_Trans_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Trans_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string a = dsPerson.FilterExpression;
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], DateTime.Now);
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], DateTime.Now);
        e.NewValues["Hrs"] = SafeValue.SafeString(e.NewValues["Hrs"]);
        e.NewValues["Pic"] = SafeValue.SafeInt(e.NewValues["Pic"],0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Trans_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Log
    protected void grid_Log_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPersonLog));
        }
    }
    protected void grid_Log_DataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsPersonLog.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), 0) + "'";
    }
    protected void grid_Log_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = id.Text;
        e.NewValues["LogDate"] = DateTime.Now;
        e.NewValues["LogTime"] = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

    }
    protected void grid_Log_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Log_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["LogDate"] = SafeValue.SafeDate(e.NewValues["LogDate"], new DateTime(1753, 1, 1));
        e.NewValues["LogTime"] = SafeValue.SafeString(e.NewValues["LogTime"], "");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["UpdateBy"] = EzshipHelper.GetUserName();
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Log_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Comment
    protected void grid_Comment_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrPersonComment));
        }
    }
    protected void grid_Comment_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsComment.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_Comment_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Date"] = DateTime.Today;
    }
    protected void grid_Comment_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Comment_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Manager"] = SafeValue.SafeString(e.NewValues["Manager"]);
        e.NewValues["Rating"] = SafeValue.SafeString(e.NewValues["Rating"]);
        e.NewValues["Status"] = SafeValue.SafeString(e.NewValues["Status"]);
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["Remark1"] = SafeValue.SafeString(e.NewValues["Remark1"]);
        e.NewValues["Remark2"] = SafeValue.SafeString(e.NewValues["Remark2"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Comment_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Payroll Setup
    protected void grid_Quote_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrQuote));
        }
    }
    protected void grid_Quote_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsQuote.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_Quote_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Amt"] = (decimal)0;
    }
    protected void grid_Quote_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"]).Length < 1)
            throw new Exception("PayItem not be null !!!");
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Quote_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["PayItem"]).Length < 1)
            throw new Exception("PayItem not be null !!!");
        e.NewValues["Amt"] = SafeValue.SafeDecimal(e.NewValues["Amt"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Quote_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region Attachment
    protected void grd_Photo_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsJobPhoto.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grd_Photo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grd_Photo_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrAttachment));
        }
    }
    #endregion

    #region Leave Record
    protected void grid_Leave_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrLeave));
    }
    protected void grid_Leave_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsLeave.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_Leave_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ApplyDateTime"] = DateTime.Now;
        e.NewValues["ApproveStatus"] = "Draft";
        e.NewValues["Date1"] = DateTime.Today;
        e.NewValues["Time1"] = "AM";
        e.NewValues["Time2"] = "";
        e.NewValues["Days"] = 0;
    }
    protected void grid_Leave_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Leave_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1753, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1753, 1, 1));
        e.NewValues["Time1"] = SafeValue.SafeString(e.NewValues["Time1"]);
        e.NewValues["Time2"] = SafeValue.SafeString(e.NewValues["Time2"]);
        e.NewValues["Days"] = SafeValue.SafeString(e.NewValues["Days"], "0");
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["ApplyDateTime"] = SafeValue.SafeDate(e.NewValues["ApplyDateTime"], new DateTime(1753, 1, 1));
        e.NewValues["ApproveBy"] = SafeValue.SafeInt(e.NewValues["ApproveBy"], 0);
        e.NewValues["ApproveDate"] = SafeValue.SafeDate(e.NewValues["ApproveDate"], new DateTime(1753, 1, 1));
        e.NewValues["ApproveTime"] = SafeValue.SafeString(e.NewValues["ApproveTime"]);
        e.NewValues["ApproveStatus"] = SafeValue.SafeString(e.NewValues["ApproveStatus"]);
        e.NewValues["ApproveRemark"] = SafeValue.SafeString(e.NewValues["ApproveRemark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_Leave_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
    
    #region Leave Template
    
    protected void grid_LeaveTmp_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
            grd.ForceDataRowType(typeof(C2.HrLeaveTmp));
    }
    protected void grid_LeaveTmp_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsLeaveTmp.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_LeaveTmp_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Year"] = DateTime.Now.Year;
        e.NewValues["Days"] = 0;
    }
    protected void grid_LeaveTmp_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_LeaveTmp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Year"] = SafeValue.SafeString(e.NewValues["Year"]);
        e.NewValues["Days"] = SafeValue.SafeInt(e.NewValues["Days"], 0);
        e.NewValues["LeaveType"] = SafeValue.SafeString(e.NewValues["LeaveType"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_LeaveTmp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion

    #region CPF
    protected void grid_CPF_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.HrCpf));
        }
    }
    protected void grid_CPF_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView grd = (sender as ASPxGridView) as ASPxGridView;
        this.dsCPF.FilterExpression = "Person='" + SafeValue.SafeInt(grd.GetMasterRowKeyValue(), -1) + "'";
    }
    protected void grid_CPF_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Year"] = (decimal)0;
        e.NewValues["FromDate"] = DateTime.Today;
        e.NewValues["Cpf1"] = 0;
        e.NewValues["Cpf2"] = 0;
    }
    protected void grid_CPF_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_CPF_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        e.NewValues["FromDate"] = SafeValue.SafeDate(e.NewValues["FromDate"], new DateTime(1753, 1, 1));
        e.NewValues["ToDate"] = SafeValue.SafeDate(e.NewValues["ToDate"], new DateTime(1753, 1, 1));

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void grid_CPF_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    #endregion
}