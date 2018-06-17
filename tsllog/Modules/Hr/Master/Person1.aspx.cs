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

public partial class Modules_Hr_Master_Person1 : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = SafeValue.SafeString(Request.QueryString["type"]).ToUpper();
            if (type.Length > 0)
            {
                this.txt_Status.Text = type;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PersonWhere" + txt_Status.Text] = null;
            this.dsPerson.FilterExpression = "1=0";
        }
        if (Session["PersonWhere" + txt_Status.Text] != null)
        {
            this.dsPerson.FilterExpression = Session["PersonWhere" + txt_Status.Text].ToString();
        }
        Session["SchPersonWhere" + txt_Status.Text] = "Id>0 ";
        this.dsSchPerson.FilterExpression = Session["SchPersonWhere" + txt_Status.Text].ToString();
    }
	
	public string GetExpiry(DateTime dt, int ds)
	{
		string dts = string.Format("{0:dd/MMM/yyyy}",dt);
		if(dts=="01/Jan/0001" || dts=="01/Jan/1753" || dts=="01/Jan/1900")
			return "";
		TimeSpan ts= dt.Subtract(DateTime.Today);
		if(ts.Days < 1)
		{
			return "<span style='background:red;font-weight:bold;padding:2px;'>"+dts+"</span>";
		}
		if(ts.Days < ds)
		{
			return "<span style='background:orange;font-weight:bold;padding:2px;'>"+dts+"</span>";
		}
		return dts;
	}
	
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string name = this.txt_keyword.Text.Trim();
        string where = "";
        if (name.Length > 0)
        {
            where = string.Format("Id>0  and (REMARK4 LIKE '%{0}%' OR NAME LIKE '%{0}%')", name.Replace("'", "''"));
        }
        else
        {
            where = "Id>0 ";
        }
        this.dsPerson.FilterExpression = where;
        Session["PersonWhere" + txt_Status.Text] = where;
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
        if (txt_Status.Text.ToLower() == "candidate")
        {
            grid.Columns[4].Visible = false;
            grid.Columns[5].Visible = false;
        }
        else if (txt_Status.Text.ToLower() == "resignation")
        {
            btn_Add.ClientVisible = false;
        }
    }
    protected void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        Panel divStatus1 = pageControl.FindControl("divStatus1") as Panel;
        Panel divStatus2 = pageControl.FindControl("divStatus2") as Panel;
        Panel divPersonDet = pageControl.FindControl("divPersonDet") as Panel;
        if (txt_Status.Text.ToLower() == "candidate")
        {
            divStatus1.Visible = false;
            divStatus2.Visible = false;
            divPersonDet.Visible = false;
            pageControl.TabPages[1].ClientVisible = false;
            pageControl.TabPages[3].ClientVisible = false;
            pageControl.TabPages[4].ClientVisible = false;
            pageControl.TabPages[5].ClientVisible = false;
            pageControl.TabPages[6].ClientVisible = false;
            pageControl.TabPages[7].ClientVisible = false;
            pageControl.TabPages[8].ClientVisible = false;

        }

        if (this.grid.EditingRowVisibleIndex > -1)
        {

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
        else if (s == "Cancel")
            this.grid.CancelEdit();

    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["InterviewId"] = "";
        e.NewValues["RecruitId"] = "";
        e.NewValues["Gender"] = "Male";
        e.NewValues["Status"] = txt_Status.Text;
        e.NewValues["HrRole"] = "Casual";
        e.NewValues["HrGroup"] = "DAY";
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
			ASPxTextBox txtIcNo = pageControl.FindControl("txtIcNo") as ASPxTextBox;
            string Id = SafeValue.SafeString(txt_Id.Text, "");
            string name = txt_Name.Text;
			//throw new Exception(name);
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPerson), "Id='" + Id + "'");
            HrPerson person = C2.Manager.ORManager.GetObject(query) as HrPerson;
            bool action = false;

            if (SafeValue.SafeString(txt_Name.Text,"") == "")
            {
                throw new Exception("Name not be null!!!");
                return;
            }
            //if (SafeValue.SafeString(txtIcNo.Text,"") == "")
            //{
            //    throw new Exception("Ic No not be null!!!");
            //    return;
            //}
			string sql=string.Format(@"select count(*) from Hr_Person where IcNo='{0}' and Id<>{1}",SafeValue.SafeString(txtIcNo.Text,""),SafeValue.SafeString(txt_Id.Text,"0"));
			//string sql=string.Format(@"select count(*) from Hr_Person where Id={0}",SafeValue.SafeString(txt_Id.Text,""),SafeValue.SafeString(txt_Name.Text,""));
			//throw new Exception(sql);
			int cnt=SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);
			if(cnt>0){
			   throw new Exception("Please enter another value of the Ic No again!");
               return;
			}
            if (person == null || person.Id == 0)
            {
                action = true;
                person = new HrPerson();
            }
			//throw new Exception(name);
            person.Name = name;
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

            ASPxTextBox ss1 = pageControl.FindControl("txt_s1") as ASPxTextBox;
            person.Status1 = S.Text(ss1.Text);
            ASPxTextBox ss2 = pageControl.FindControl("txt_s2") as ASPxTextBox;
            person.Status2 = S.Text(ss2.Text);
						
            ASPxDateEdit dd1 = pageControl.FindControl("date_d1") as ASPxDateEdit;
            person.Date1 = S.Date(dd1.Date);
            ASPxDateEdit dd2 = pageControl.FindControl("date_d2") as ASPxDateEdit;
            person.Date2 = S.Date(dd2.Date);


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
            ASPxSpinEdit spin_Amount1 = pageControl.FindControl("spin_Amount1") as ASPxSpinEdit;
            person.Amount1 = SafeValue.SafeDecimal(spin_Amount1.Value);
            ASPxSpinEdit spin_Amount2 = pageControl.FindControl("spin_Amount2") as ASPxSpinEdit;
            person.Amount2 = SafeValue.SafeDecimal(spin_Amount2.Value);
            ASPxSpinEdit spin_Amount3 = pageControl.FindControl("spin_Amount3") as ASPxSpinEdit;
            person.Amount3 = SafeValue.SafeDecimal(spin_Amount3.Value);
			//throw new Exception(person.Name);
            if (action)
            {
                person.Status = txt_Status.Text;
                person.CreateBy = HttpContext.Current.User.Identity.Name;
                person.CreateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(person, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(person);
            }
            else
            {
                ASPxComboBox newStatus = grid.FindEditFormTemplateControl("cmb_Type") as ASPxComboBox;
                person.Status = newStatus.Text;
                string oldStatus=SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select Status from Hr_Person where Id={0}",Id)));
                if (oldStatus != SafeValue.SafeString(newStatus.Text))
                    this.grid.CancelEdit();
                    //this.Response.Write("<script>if(confirm('Confirm change this'+txt_Status.GetText()+' To'+cmb_Type.GetText()+'?')){alert('Sucess!');};</script>"); 
                person.UpdateBy = HttpContext.Current.User.Identity.Name;
                person.UpdateDateTime = DateTime.Now;
                Manager.ORManager.StartTracking(person, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(person);
            }
            Session["PersonWhere" + txt_Status.Text] = "Id='" + person.Id + "' and Status='" + txt_Status.Text + "'";
            this.dsPerson.FilterExpression = Session["PersonWhere" + txt_Status.Text].ToString();
            if (this.grid.GetRow(0) != null)
                this.grid.StartEdit(0);
            else
            {
                Session["PersonWhere" + txt_Status.Text] = "Id>0 and Status='" + txt_Status.Text + "'";
                this.dsPerson.FilterExpression = Session["PersonWhere" + txt_Status.Text].ToString();
            }
        }
        catch (Exception ex) { throw new Exception(ex.Message + ex.StackTrace); }
    }

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
        e.NewValues["Dob"] = SafeValue.SafeString(e.NewValues["Dob"]);
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
        ASPxPageControl pageControl = this.grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
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
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], DateTime.Now);
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], DateTime.Now);
        e.NewValues["Hrs"] = SafeValue.SafeString(e.NewValues["Hrs"]);
        e.NewValues["Pic"] = SafeValue.SafeString(e.NewValues["Pic"]);
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

    #region Quote
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
        ASPxTextBox id = this.grid.FindEditFormTemplateControl("txt_Id") as ASPxTextBox;
        e.NewValues["Person"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_Quote_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["PayItem"] = SafeValue.SafeString(e.NewValues["PayItem"]);
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
        e.NewValues["Days"] = SafeValue.SafeInt(e.NewValues["Days"], 0);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);

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

    protected void btn_export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("EmployeeData", true);
    }
}