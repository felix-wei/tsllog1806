using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.IO;
using Aspose.Cells;


public partial class Modules_Hr_Job_PayrollRecordYear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int day = DateTime.Today.Day;
            if (day < 20)
            {
                this.txt_from.Date = DateTime.Today.AddMonths(-12).AddDays(-DateTime.Today.Day + 1);
                this.txt_end.Date = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
                Session["Payroll"] = null;
                this.dsPayroll.FilterExpression = "1=0";
            }
            else
            {
                this.txt_from.Date = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
                this.txt_end.Date = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.AddMonths(1).Day);
                Session["Payroll"] = null;
                this.dsPayroll.FilterExpression = "1=0";
            }
        }
        if (Session["Payroll"] != null)
        {
            this.dsPayroll.FilterExpression = Session["Payroll"].ToString();
        }
		
		
		// runn once
		
        //btn_Sch_Click(null, null);
		if((Request.QueryString["type"] ?? "") == "CPF")
			CalcCpf();
    }
	
	
	public void CalcCpf()
	{
		DataTable dt = D.List("select * from hr_Payroll where FromDate >= '2018-02-01'");
		string dbg = "";
		for(int r=0; r<dt.Rows.Count; r++)
		{
			dbg = "";
		   DataRow dr_pay=dt.Rows[r];
			string pay_id = S.Text(dr_pay["Id"]);
			
			DataTable dt_line = D.List("select * from hr_payrolldet where payrollid=" + pay_id);
			DataTable dt_emp = D.List(string.Format("select * from hr_person where id='{0}'",dr_pay["Person"]));

			
			
		   DataRow dr_emp=dt_emp.Rows[0];
			DateTime date_pay = S.Date(dr_emp["Date3"]);
			DateTime date_from = S.Date(dr_pay["FromDate"]);

				decimal total1 = 0;
				decimal total2 = 0;
				decimal unpaid = 0;
				decimal reim = 0;
				decimal cpf1 = 0;
				decimal cpf2 = 0;
				decimal nett = 0;
			DataTable dte = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('AB','AL','AO','AR','AX','AY','AZ') order by ProcessType,Code",pay_id));
			for(int j=0; j<dte.Rows.Count; j++)
			{
				DataRow dre = dte.Rows[j];
				total1 += S.Dec(dre["Amount"]);
				
				if(S.Text(dre["Code"]) ==  "PR25")
					reim += S.Dec(dre["Amount"]);
			}
				
			
			
			
				DataTable dtd;
				string pass = S.Text(dr_emp["PassType"]);
				string race  = S.Text(dr_emp["Race"]);
				if(pass=="NRIC" && race=="CHINESE")
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR20','PR21') order by ProcessType,Code",pay_id));
				else if(pass=="NRIC" && race=="MALAY")
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR20') order by ProcessType,Code",pay_id));
				else if(pass=="NRIC" && race=="TAMIL")
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
				else if(pass=="NRIC" && race=="HINDI")
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
				else if(pass=="NRIC" && race=="INDIAN")
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR21') order by ProcessType,Code",pay_id));
				else	
					dtd = D.List(string.Format("select p.Code, p.Description, isnull(d.Amt,0) as Amount, d.Description as Remark from hr_payitem p left join hr_payrolldet d on p.code=d.chgcode and d.payrollid={0} where p.ProcessType in ('DD','DU','DZ') and p.Code not in ('PR19','PR20','PR21') order by ProcessType,Code",pay_id));
				//dbg += pay_id + "/" + j.ToString() + ",";
				for(int i=0; i<dtd.Rows.Count; i++)
				{
					DataRow drd = dtd.Rows[i];
					total2 += S.Dec(drd["Amount"]);
					dbg += "," + S.Dec(drd["Amount"]).ToString();
					if(S.Text(drd["Code"]) == "PR16" || S.Text(drd["Code"]) == "PR25")
						unpaid += S.Dec(drd["Amount"]);
					
				}




decimal rate1 = 0;
		decimal rate2 = 0;
		DateTime tod = S.Date(dr_pay["FromDate"]);
		DateTime dob = S.Date(dr_emp["BirthDay"]);
		string _cpf = S.Text(dr_emp["IsCPF"]);
		DataTable dt_cpf = D.List("select top 1 * from hr_rate where RateType='"+_cpf+"'");
		if(dt_cpf.Rows.Count > 0) {
		DataRow dr_cpf = dt_cpf.Rows[0];
		   
            TimeSpan actualAge = tod.Subtract(dob);
			int age = actualAge.Days / 365;
			
			rate1 = S.Dec(dr_cpf["EmployerRate"]);
			rate2 = S.Dec(dr_cpf["EmployeeRate"]);
			if(age > 55 && age <= 60) {
			 rate1 = S.Dec(dr_cpf["EmployerRate55"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate55"]);
			}
			if(age > 60 && age <= 65) {
			 rate1 = S.Dec(dr_cpf["EmployerRate60"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate60"]);
			}
			if(age > 65) {
			 rate1 = S.Dec(dr_cpf["EmployerRate65"]);
			 rate2 = S.Dec(dr_cpf["EmployeeRate65"]);
			}
		}	
				
			if(	dr_emp["Name"].ToString().ToUpper() == "TEO ANG THO1")
			{
				rate1 = S.Dec("0.09");
				rate2 = 0;
			}
			
		
			decimal cpf_base = total1 - unpaid - reim;  //total1-unpaid - reim
			if(cpf_base > 6000)
				cpf_base = 6000;

			if(pass != "NRIC")
				{
				cpf_base = 0;
				rate1 = 0;
				rate2 = 0;
				}
				
				
				
			if(	dr_emp["Name"].ToString().ToUpper() == "CHEW CHAI CHIN 11 ")
			{
				cpf_base = 350;
				rate1 = S.Dec("0.04");
				rate2 = 0;
			}

				
			string ymd = string.Format("{0:yyMMdd}",tod);
			string ym = string.Format("{0:yyMM}",tod);


			if(	dr_emp["Name"].ToString().ToUpper().Trim() == "LU ZHIJIE" && ym == "1712")
			{
				rate1 = S.Dec("0.09");
				rate2 = S.Dec("0.075");
			}

			if(S.Int(ym) > 1704) {
			cpf1 = Math.Round(rate1 * (cpf_base),0);
			decimal cpf1a = Math.Round(rate1 * (cpf_base),2);
			if(cpf1a > cpf1)
				cpf1 = cpf1 + 1;
			cpf2 = Math.Round(rate2 * (cpf_base),0);
			decimal cpf2a = Math.Round(rate2 * (cpf_base),2);
			if(cpf2a < cpf2 )
				cpf2=cpf2-1;
			} else {
			cpf1 = Math.Round(rate1 * (cpf_base),2);
			cpf2 = Math.Round(rate2 * (cpf_base),2);

			}
			
			//throw new Exception(rate1.ToString() + " / " + rate2.ToString() + " / " + cpf_base.ToString() + " / " + cpf1.ToString() );
			
			
			// new way
			decimal new_cpf_add = 0;
			if(cpf_base < 750)
				rate2 = S.Dec("0.1");
			if(cpf_base < 500)
				rate2 = 0;
			if(cpf_base < 50)
				rate2 = 0;
			//if(cpf_base >= 500 && cpf_base < 750)
			//	new_cpf_add = S.Dec("0.6");
			//if(cpf_base >= 50 && cpf_base < 500)
			//	new_cpf_add = S.Dec("0.6");
				
			
			if(	dr_emp["Name"].ToString().ToUpper().Trim() == "LU ZHIJIE" && ym == "1712")
			{
				new_cpf_add = 0;
				rate2 = S.Dec("0.075");
			}
	
				
			decimal new_cpf0 = 0;
			new_cpf0 = Math.Round(cpf_base * (rate1+rate2), 0);
			if(cpf_base==450)
				new_cpf0 = 77;
			//throw new Exception(new_cpf0.ToString() + "/" + (rate1+rate2).ToString() + cpf_base.ToString());
			decimal new_cpf2 = Math.Round(cpf_base * (rate2), 0);
			//throw new Exception(string.Format("{0}/{1}/{2}",cpf_base, new_cpf0, new_cpf2));
			
			if(new_cpf2 > (cpf_base * rate2))
				new_cpf2 = new_cpf2 - 1;
			decimal new_cpf1 = new_cpf0 - new_cpf2;
			
			cpf1 = new_cpf1 + new_cpf_add;
			cpf2 = new_cpf2 + new_cpf_add;
			
			

			if(	dr_emp["Name"].ToString().ToUpper() == "LAY TSE SHEN 11")
			{
				//cpf_base = 350;
				cpf1 = 89;
				cpf2 = 15;
				//rate1 = S.Dec("0.04");
				//rate2 = 0;
			}

			if(	dr_emp["Name"].ToString().ToUpper() == "TAN CHOON LAN" && string.Format("{0:dd/MM/yyyy}",dr_pay["FromDate"])=="01/11/2017")
			{
				//cpf_base = 350;
				cpf1 = 92;
				cpf2 = 23;
				//rate1 = S.Dec("0.04");
				//rate2 = 0;
			}

			
			nett = total1 - cpf2 - total2;
			if(cpf1 <0  ||  cpf2 < 0)
			{
				throw new Exception(string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}",pay_id,cpf1,cpf2,total1,total2,dtd.Rows.Count, dbg));
			}
 
	D.Exec(string.Format("update hr_payroll set cpf1='{1}', cpf1amt='{2}',cpf2='{3}',cpf2amt='{4}',total1='{5}',total2='{6}',cpf0='{7}' where Id='{0}'",pay_id,
			rate1, cpf1, rate2, cpf2, total1, total2, cpf_base));




				
			
		}
	}
	
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value, "");
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (id.Length > 0)
        {
            where = GetWhere(where, string.Format("p.Id='{0}'", id));
        }
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format(" r.FromDate >= '{0}' and r.FromDate <= '{1}'", dateFrom, dateTo));
        }
        string sql = string.Format(@"
		
		select t.*,mast.Id from Hr_Payroll mast left join (select sum(Amt) Amt,Person,max(Pic) Pic,max(Term) Term,max(Remark) Remark,'{0}' FromDate,'{1}'ToDate from Hr_Payroll group by Person) as t on t.Person=mast.Person "
		
		,dateFrom,dateTo);

		/*
		ID Type
ID No
Name
Nationalty
Female
Sate of Brith
Position
Address Type
Address 
Post Code
Country
Date of Commercement
Date of cesstion
Bank 
		*/
		
		sql = string.Format(@"
		SELECT     p.Id,   p.PassType, p.IcNo as PassNo, p.Name, p.Country, p.Gender, p.BirthDay, p.HrRole, 'L' AS AddressType, p.Address, '' AS Postal, 'SINGAPORE' AS Country2, p.HrRole AS Position,
                             (SELECT        TOP (1) BeginDate
                               FROM            Hr_PersonDet1
                               WHERE        (Person = p.Id)) AS DateStart,
                             (SELECT        TOP (1) ResignDate
                               FROM            Hr_PersonDet1 AS Hr_PersonDet1_1
                               WHERE        (Person = p.Id)) AS DateEnd,
                             (SELECT        TOP (1) BankName
                               FROM            Hr_PersonDet3
                               WHERE        (Person = p.Id)) AS BankName,
                             (SELECT        TOP (1) AccNo
                               FROM            Hr_PersonDet3 AS Hr_PersonDet3_1
                               WHERE        (Person = p.Id)) AS BankAccNo, SUM(r.Fee1) AS Fee1, SUM(r.Fee2) AS Fee2, SUM(r.Fee3) AS Fee3, SUM(-r.Fee4) AS Fee4, 
							   SUM(-r.Fee5) AS Fee5, SUM(r.Fee6) AS Fee6, SUM(r.Fee7) AS Fee7
FROM   vw_salary r,  Hr_Person  p Where r.Person = p.Id and {0}
GROUP BY p.Id, p.PassType, p.IcNo, p.Name, p.Country, p.Gender, p.BirthDay, p.HrRole, p.Address
ORDER BY p.Name ",
		where
		 );

 // throw new Exception(sql);
        DataTable dt = ConnectSql.GetTab(sql);
        this.ASPxGridView1.DataSource = dt;
        this.ASPxGridView1.DataBind();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        btn_Sch_Click(null, null);
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
        e.NewValues["Amt"] = 0;
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

            if (SafeValue.SafeString(personId.Value, "0") == "0")
            {
                throw new Exception("Name not be null!!!");
                return;
            }
            if (payroll == null)
            {
                action = true;
                payroll = new HrPayroll();
            }

            payroll.Person = SafeValue.SafeInt(personId.Value, 0);
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
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        string pars = e.Parameters;
        string[] ar = pars.Split('_');
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("C"))
            {
                string[] arrIds = ar[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string strList = "";
                for (int i = 0; i < arrIds.Length; i++)
                {
                    if (i < arrIds.Length - 1)
                    {
                        strList += "'" + arrIds[i] + "'" + ",";
                    }
                    else
                    {
                        strList += "'" + arrIds[i] + "'";
                    }
                }
                e.Result = "";
            }
            if (ar[0].Equals("I"))
            {
                string[] arrIds = ar[1].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string strList = "";
                for (int i = 0; i < arrIds.Length; i++)
                {
                    if (i < arrIds.Length - 1)
                    {
                        strList += "'" + arrIds[i] + "'" + ",";
                    }
                    else
                    {
                        strList += "'" + arrIds[i] + "'";
                    }
                }
                e.Result = "";
            }
        }
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
        e.NewValues["Before"] = (decimal)0;
    }
	
	/*
	
ID Type
ID No
Name
Nationalty
Female
Sate of Brith
Position
Address Type
Address 
Post Code
Country
Date of Commercement
Date of cesstion
Bank 

gross salary
bonus
employee cpf
cdac + sinda
mbmf
director
	*/
    protected void grid_PayrollDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ChgCode"], "") == "")
            throw new Exception("ChgCode not be null !!!");
        ASPxTextBox id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        e.NewValues["PayrollId"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["Before"] = SafeValue.SafeDecimal(e.NewValues["Before"], 0);
        e.NewValues["SignNo"] = SafeValue.SafeString(e.NewValues["SignNo"], "");
        if (SafeValue.SafeString(e.NewValues["SignNo"], "") != "")
        {
            if (SafeValue.SafeString(e.NewValues["SignNo"], "") == "+")
            {
                e.NewValues["Amt"] = SafeValue.SafeDecimal("+" + e.NewValues["Amt"], 0);
            }
            if (SafeValue.SafeString(e.NewValues["SignNo"], "") == "-")
            {
                e.NewValues["Amt"] = SafeValue.SafeDecimal("-" + e.NewValues["Amt"], 0);
            }
        }
    }
    protected void grid_PayrollDet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ChgCode"] = SafeValue.SafeString(e.NewValues["ChgCode"], "");
        if (e.NewValues["ChgCode"] == "")
            throw new Exception("ChgCode not be null !!!");
        e.NewValues["SignNo"] = SafeValue.SafeString(e.NewValues["SignNo"], "");
        e.NewValues["Description"] = SafeValue.SafeString(e.NewValues["Description"], "");
        e.NewValues["Pic"] = SafeValue.SafeString(e.NewValues["Pic"], "");

        if (SafeValue.SafeString(e.NewValues["SignNo"], "") != "")
        {
            if (SafeValue.SafeString(e.NewValues["SignNo"], "") == "+")
            {
                e.NewValues["Amt"] = SafeValue.SafeDecimal("+" + e.NewValues["Amt"], 0);
            }
            if (SafeValue.SafeString(e.NewValues["SignNo"], "") == "-")
            {
                e.NewValues["Amt"] = SafeValue.SafeDecimal("-" + e.NewValues["Amt"], 0);
            }
        }
        if (SafeValue.SafeDecimal(e.NewValues["Amt"], 0) != SafeValue.SafeDecimal(e.OldValues["Amt"], 0))
        {
            e.NewValues["Before"] = SafeValue.SafeDecimal(e.OldValues["Amt"], 0);
        }

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
    protected void cmb_StatusCode_CustomJSProperties(object sender, DevExpress.Web.ASPxClasses.CustomJSPropertiesEventArgs e)
    {
        ASPxComboBox cmb_Status = ASPxGridView1.FindEditFormTemplateControl("cmb_StatusCode") as ASPxComboBox;
        ASPxTextBox txt_Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        string Id = SafeValue.SafeString(txt_Id.Text);
        if (Id != "")
        {
            string sql = string.Format(@"select StatusCode from Hr_Payroll where Id={0}", Id);
            string status = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
            if (status == "Draft")
            {
                cmb_Status.Text = "Draft";
            }
            if (status == "Confirm")
            {
                cmb_Status.Text = "Confirm";
            }
            if (status == "Cancel")
            {
                cmb_Status.Text = "Cancel";
            }
        }

    }
	
	 protected void ASPxButton6_Click(object sender, EventArgs e)
    {
        string name = txtSchId.Text;
        string dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
        string dateEnd = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        string temp = cpf_download(name, dateFrom, dateEnd);
        Response.Write("<script>window.open('" + temp + "');</script>");
    }
	
	
	public string cpf_download(string name, string DateFrom, string DateTo)
	{
        string where = "";
        if (name.Length > 0)
        {
            where = String.Format("Person='{0}'", name);
        }
        where = GetWhere(where, string.Format(" r.FromDate >= '{0}' and r.FromDate <= '{1}'", DateFrom, DateTo));

		 string sql = string.Format(@"select p.Id,p.Name,p.IcNo , sum( pl.Amt ) as Amt
		from Hr_Payroll as pl
		left outer join Hr_Person as p on pl.Person=p.Id
		where "+where + " group by p.Id, p.Name, p.IcNo");
		
		sql = string.Format(@"
		SELECT     p.Id,   p.PassType, p.IcNo as PassNo, p.Name, p.Country, p.Gender, p.BirthDay, p.HrRole, 'L' AS AddressType, p.Address, '' AS Postal, 'SINGAPORE' AS Country2, p.HrRole AS Position,
                             (SELECT        TOP (1) BeginDate
                               FROM            Hr_PersonDet1
                               WHERE        (Person = p.Id)) AS DateStart,
                             (SELECT        TOP (1) ResignDate
                               FROM            Hr_PersonDet1 AS Hr_PersonDet1_1
                               WHERE        (Person = p.Id)) AS DateEnd,
                             (SELECT        TOP (1) BankName
                               FROM            Hr_PersonDet3
                               WHERE        (Person = p.Id)) AS BankName,
                             (SELECT        TOP (1) AccNo
                               FROM            Hr_PersonDet3 AS Hr_PersonDet3_1
                               WHERE        (Person = p.Id)) AS BankAccNo, SUM(r.Fee1) AS Fee1, SUM(r.Fee2) AS Fee2, SUM(r.Fee3) AS Fee3, SUM(-r.Fee4) AS Fee4, SUM(-r.Fee5) AS Fee5, SUM(r.Fee6) AS Fee6
FROM   vw_salary r,  Hr_Person  p Where r.Person = p.Id and {0}
GROUP BY p.Id, p.PassType, p.IcNo, p.Name, p.Country, p.Gender, p.BirthDay, p.HrRole, p.Address
ORDER BY p.Name ",
		where
		 );
		
		
		
		DataTable dt = D.List(sql);


		string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.Parent.FullName;
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + "";
        string to_file = Path.Combine(rootPath, "files", "Hr", "iras_" + fileName + ".txt");

		
		
		using (System.IO.StreamWriter file = 
            new System.IO.StreamWriter(to_file, true))
        {
            string line0 = string.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}",
				"0","6",DateTime.Today.Year - 1, "08", 
				"7", space("201309403M",12), 
				space("TAY SOON LAI",30), space("DIRECTOR",30), space("TSL LOGISTICS PTE LTD",60), space("67772724",20), space("alan@tsllogistics.sg",60),
				"O",
				space(DateTime.Today.ToString("yyyyMMdd"),8),
				space("  ",30),
				space("IR8A",10),
				space("  ",30)
				);
			
			file.WriteLine(line0);
			
			decimal fee1 = 0;
			decimal fee2 = 0;
			decimal fee3 = 0;
			decimal fee4 = 0;
			decimal fee5 = 0;
			decimal fee6 = 0;
			decimal fee7 = 0;
			decimal fee8 = 0;
			decimal fee9 = 0;
			decimal fee10 = 0;
			decimal fee11 = 0;
			decimal fee12 = 0;
			
			for(int i=0;i< dt.Rows.Count; i++)
			{
				DataRow dr= dt.Rows[i];
				
				decimal fee1_ = S.Dec(dr["Fee1"]);
				decimal fee2_ = S.Dec(dr["Fee2"]);
				decimal fee3_ = S.Dec(dr["Fee3"]);
				decimal fee4_ = S.Dec(dr["Fee4"]);
				decimal fee5_ = S.Dec(dr["Fee5"]);
				decimal fee6_ = S.Dec(dr["Fee6"]);

				DateTime dat1 = S.Date(dr["DateStart"]);
				DateTime dat2 = S.Date(dr["DateEnd"]);
				DateTime dat3 = S.Date(dr["DateStart"]);
				DateTime dat4 = S.Date(dr["DateEnd"]);
				
            string line1 = string.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}",
				"1",S.Text(dr["PassType"]) == "NRIC" ? "1" : "2",
				space(S.Text(dr["PassNo"]),12),
				space(S.Text(dr["Name"]),40), space("",40),
				"L", 
				space(S.Text(dr["Address"]).Replace("\r","").Replace("\n",""), 60),
				space("", 6),
				space("", 3),
				space("", 3),
				space(S.Text(dr["Gender"]).Substring(0,1), 1),
				space(string.Format("{0:yyyyMMdd}",dr["BirthDay"]), 8),
				space((fee1+fee2+fee3).ToString("0.00"), 12),
				space(dat1.ToString("yyyyMMdd"), 8),				space(dat2.ToString("yyyyMMdd"), 8),
				space(fee5.ToString("0"), 5),space(fee4.ToString("0"), 5), space(fee6.ToString("0"), 7), space("0", 60),
				space(fee1.ToString("0"), 9), space(fee2.ToString("0"), 9), space(fee3.ToString("0"), 9), space("0", 9),space("0", 9),
				space("0", 9), space((fee1+fee2+fee3).ToString("0"), 12), " ","N"," "," ", " " ,space(" ",8),
		
				" "," "," ",space("0",9),space(" ",8),space(" ",8)," ",
				space("0", 9),space("0", 9),space("0", 9),space("0", 9),
				
				space("0", 9),space("0", 9),space("0", 9),space("0", 9),
				space("0", 9),space("0", 9),space("0", 9),space("0", 9),
				space("0", 9),space("0", 9),space("0", 9),space("0", 9),space("0", 9),
				space(S.Text(dr["HrRole"]), 30), 
				space(dat3.ToString("yyyyMMdd"), 8),				space(dat4.ToString("yyyyMMdd"), 8),
				space(dat2.ToString("yyyyMMdd"), 8),				space(dat2.ToString("yyyyMMdd"), 8),
				space("",60),space("",60),space("1",1),space("",8),space("",393),space("",50)
				);
				// 51 - 
				fee1 += S.Dec(dr["Fee1"]);
				fee2 += S.Dec(dr["Fee2"]);
				fee3 += S.Dec(dr["Fee3"]);
				fee4 += S.Dec(dr["Fee4"]);
				fee5 += S.Dec(dr["Fee5"]);
				fee6 += S.Dec(dr["Fee6"]);
				fee7 += 0;
				fee8 += 0;
				fee9 += 0;
				fee10 += 0;
				fee11 += 0;
				fee12 += 0;

				file.WriteLine(line1);
			}
			
            string line2 = string.Format(@"{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
				"2",space(dt.Rows.Count.ToString(),6),
				space(Math.Ceiling(fee1+fee2+fee3).ToString("0"), 12),
				space(Math.Ceiling(fee1).ToString("0"), 12),
				space(Math.Ceiling(fee2).ToString("0"), 12),
				space(Math.Ceiling(fee3).ToString("0"), 12),
				space("0", 12),
				space("0", 12),
				space("0", 12),
				space(Math.Ceiling(fee1+fee2+fee3).ToString("0"), 12),
				space(Math.Ceiling(fee4).ToString("0"), 12),
				space(Math.Ceiling(fee6).ToString("0"), 12),
				space("0", 12),
				space(Math.Ceiling(fee5).ToString("0"), 12),
				space("  ",1049)
				);
			
			file.WriteLine(line2);
			
        }
		
		string context = "../../../files/Hr/iras_" + fileName + ".txt";
        return context;
	}
	
	public string space(string str, int len)
	{
		//string str1 = str + "                                                                                                                                                                                                                                                                                                                          ";
		//return str.Substring(0,len);
		return str.PadRight(len);
	}

    public string cpf_download2(string name, string DateFrom, string DateTo)
    {

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.Parent.FullName;
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + "";
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "iras_" + fileName + ".csv");

        string where = "";
        if (name.Length > 0)
        {
            where = String.Format("Person='{0}'", name);
        }
        where = GetWhere(where, string.Format("ToDate >= '{0}' and FromDate <= '{1}'", DateFrom, DateTo));
        
        string sql = string.Format(@"select p.Id,p.Name,p.IcNo 
from Hr_Payroll as pl
left outer join Hr_Person as p on pl.Person=p.Id
where "+where);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        string DataList = Common.DataTableToJson(dt);

        Aspose.Cells.License license = new Aspose.Cells.License();
        license.SetLicense(HttpContext.Current.Server.MapPath("~/Aspose.lic"));
        Workbook wb = new Workbook();
        //wb.Open(file);
        Worksheet ws = wb.Worksheets[0];
        ws.Cells[0, 0].PutValue("#");
        ws.Cells[0, 1].PutValue("Name");
        ws.Cells[0, 2].PutValue("IcNo");
        int baseRow = 1;
        int i = 0;
        for (; i < dt.Rows.Count;)
        {
            ws.Cells[baseRow + i, 0].PutValue(i+1);
            ws.Cells[baseRow + i, 1].PutValue(dt.Rows[i]["Name"]);
            ws.Cells[baseRow + i, 2].PutValue(dt.Rows[i]["IcNo"]);

            i++;
        }
        wb.Save(to_file);


        string context = "../../../files/Excel_DailyTrips/iras_" + fileName + ".csv";
        return context;
    }

    protected void btn_SaveCPF_Click(object sender, EventArgs e)
    {

    }


}