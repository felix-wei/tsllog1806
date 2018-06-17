using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;
using System.IO;
using Aspose.Cells;

public partial class Modules_Hr_Job_PayrollEdit : BasePage
{
    protected void Page_init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int day = DateTime.Today.Day;
            if (Request.QueryString["no"]!=null)
            {
                string no = SafeValue.SafeString(Request.QueryString["no"]);
                if (no != "NEW")
                {
                    Session["PayrollEdit"] = "Id=" + no;
                    this.dsPayroll.FilterExpression = Session["PayrollEdit"].ToString();
                }
                else
                {
                    Session["PayrollEdit"] = null;
                    ASPxGridView1.AddNewRow();
                }
            }
        }
        if (Session["PayrollEdit"] != null)
        {
            this.dsPayroll.FilterExpression = Session["PayrollEdit"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
        // 
    }
    protected void Page_Load(object sender, EventArgs e) {

    }
	
	public string GetDob(string id)
	{
		string ret = "";
		DateTime dob = S.Date(D.One("select top 1 BirthDay from hr_person where id='"+id+"'"));
		TimeSpan ts = DateTime.Today.Subtract(dob);
		ret = string.Format("<b>{0:dd/MM/yyyy} (Age: {1})</b>",dob, ts.Days / 365);
		return ret;
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
            ASPxComboBox autoInd = ASPxGridView1.FindEditFormTemplateControl("cmb_AutoInd") as ASPxComboBox;
            payroll.AutoInd = autoInd.Text;
            ASPxMemo remark = ASPxGridView1.FindEditFormTemplateControl("txt_Remark") as ASPxMemo;
            payroll.Remark = remark.Text;
			
            ASPxSpinEdit spin_cpf1 = ASPxGridView1.FindEditFormTemplateControl("spin_cpf1") as ASPxSpinEdit;
            ASPxSpinEdit spin_cpf2 = ASPxGridView1.FindEditFormTemplateControl("spin_cpf2") as ASPxSpinEdit;
            ASPxSpinEdit spin_cpf1amt = ASPxGridView1.FindEditFormTemplateControl("spin_cpf1amt") as ASPxSpinEdit;
            ASPxSpinEdit spin_cpf2amt = ASPxGridView1.FindEditFormTemplateControl("spin_cpf2amt") as ASPxSpinEdit;
			
			payroll.Cpf1 = S.Dec(spin_cpf1.Value);
			payroll.Cpf2 = S.Dec(spin_cpf2.Value);
			payroll.Cpf1Amt = S.Dec(spin_cpf1amt.Value);
			payroll.Cpf2Amt = S.Dec(spin_cpf2amt.Value);
			
			

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

			CalcCpf(payroll.Id, payroll.AutoInd);
			DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("payrolledit.aspx?no=" + payroll.Id.ToString());
			
            Session["PayrollEdit"] = "Id=" + payroll.Id;
            this.dsPayroll.FilterExpression = Session["PayrollEdit"].ToString();
            if (this.ASPxGridView1.GetRow(0) != null)
                this.ASPxGridView1.StartEdit(0);
        }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }
	
	
	
	public void CalcCpf(int payid, string autoInd)
	{
		DataTable dt = D.List("select * from hr_Payroll where Id=" + payid.ToString());
		for(int r=0; r<dt.Rows.Count; r++)
		{
		   DataRow dr_pay=dt.Rows[r];
			string pay_id = S.Text(payid);
			
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
				for(int i=0; i<dtd.Rows.Count; i++)
				{
					DataRow drd = dtd.Rows[i];
					total2 += S.Dec(drd["Amount"]);
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

 
			if(autoInd == "Yes")
			{
	D.Exec(string.Format("update hr_payroll set cpf1='{1}', cpf1amt='{2}',cpf2='{3}',cpf2amt='{4}',total1='{5}',total2='{6}',cpf0='{7}' where Id='{0}'",pay_id,
			rate1, cpf1, rate2, cpf2, total1, total2, cpf_base));

			
			}
			else
			{			
				D.Exec(string.Format("update hr_payroll set  total1='{1}',total2='{2}',cpf0='{3}' where Id='{0}'",pay_id,
						 total1, total2, cpf_base));
			}


				
			}
		
	}
	
	
	
	
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {


    }
    protected void ASPxGridView1_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        string s = e.Parameters;
        string[] ar = s.Split('_');
        ASPxDateEdit txt_FromDate = ASPxGridView1.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxDateEdit txt_ToDate = ASPxGridView1.FindEditFormTemplateControl("txt_ToDate") as ASPxDateEdit;
        if (ar.Length >= 2)
        {
            if (ar[0].Equals("Printline"))
            {
                int rowIndex = SafeValue.SafeInt(ar[1], -1);
                ASPxLabel lbl_Id = this.ASPxGridView1.FindRowCellTemplateControl(rowIndex, null, "lbl_Id") as ASPxLabel;
                string sql = string.Format(@"select FromDate,ToDate,Person from Hr_Payroll where Id={0}",SafeValue.SafeInt(lbl_Id.Text,0));
                DataTable dt = ConnectSql.GetTab(sql);
                string Person = "";
                DateTime FromDate = DateTime.Now;
                DateTime ToDate = DateTime.Now;
                if (dt.Rows.Count > 0)
                {
                    Person = SafeValue.SafeString(dt.Rows[0]["Person"]);
                    FromDate = SafeValue.SafeDate(dt.Rows[0]["FromDate"],DateTime.Now);
                    ToDate = SafeValue.SafeDate(dt.Rows[0]["ToDate"], DateTime.Now);
                }
                e.Result =FromDate+","+ToDate+","+lbl_Id.Text+","+Person;
            }
        }
        else
        {
            if (s == "Confirm")
            {
                string update_sql = string.Format(@"update Hr_Payroll set StatusCode='Confirm' where StatusCode='Draft'");
                ConnectSql.ExecuteSql(update_sql);

                e.Result = "Success!";
            }
            if (s == "UnConfirm")
            {
                string update_sql = string.Format(@"update Hr_Payroll set StatusCode='Draft' where Id={0}", SafeValue.SafeInt(Id.Text, 0));
                ConnectSql.ExecuteSql(update_sql);

                e.Result = "Success!";
            }
            if (s == "UnCancel")
            {
                string update_sql = string.Format(@"update Hr_Payroll set StatusCode='Draft' where Id={0}", SafeValue.SafeInt(Id.Text, 0));
                ConnectSql.ExecuteSql(update_sql);

                e.Result = "Success!";
            }

            if (s == "Payroll")
            {
                string value = "Action Error! Pls check again";
                string sql = string.Format(@"select Person,sum(Amt) as TotalAmt,max(Remark) as Remark from Hr_Quote group by Person");
                DataTable tab = ConnectSql.GetTab(sql);
                int fromYear = txt_FromDate.Date.Year;
                int fromMonth = txt_FromDate.Date.Month;
                string name = HttpContext.Current.User.Identity.Name;

                int toYear = txt_ToDate.Date.Year;
                int toMonth = txt_ToDate.Date.Month;
                bool result = false;
                string from = txt_FromDate.Date.ToString("yyyy-MM-dd");
                string to = txt_ToDate.Date.ToString("yyyy-MM-dd");

                int month = toMonth - fromMonth;

                DateTime firstDayOfFromMonth = new DateTime(fromYear, fromMonth, 1);
                DateTime lastDayOfFromMonth = new DateTime(fromYear, fromMonth, DateTime.DaysInMonth(fromYear, fromMonth));

                DateTime firstDayOfToMonth = new DateTime(toYear, toMonth, 1);
                DateTime lastDayOfToMonth = new DateTime(toYear, toMonth, DateTime.DaysInMonth(toYear, toMonth));
                if (month < 2)
                {
                    for (int a = 0; a <= month; a++)
                    {
                        for (int i = 0; i < tab.Rows.Count; i++)
                        {
                            int person = SafeValue.SafeInt(tab.Rows[i]["Person"], 0);
                            string sql_pay = string.Format(@"select count(*) from Hr_Payroll where Person={0} and (FromDate>='{1}' and ToDate<='{2}')", person, firstDayOfFromMonth.ToString("yyyy-MM-dd"), lastDayOfFromMonth.ToString("yyyy-MM-dd"));
                            int cnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_pay), 0);
                            if (cnt == 0)
                            {
                                #region From Date
                                decimal amt = SafeValue.SafeDecimal(tab.Rows[i]["TotalAmt"]);
                                string remark = SafeValue.SafeString(tab.Rows[i]["Remark"]);

                                HrPayroll payroll = new HrPayroll();
                                payroll.Person = person;
                                payroll.FromDate = firstDayOfFromMonth;
                                payroll.ToDate = lastDayOfFromMonth;
                                payroll.StatusCode = "Draft";
                                payroll.Term = "";
                                payroll.Remark = "";
                                payroll.Pic = "";
                                payroll.CreateBy = name;
                                payroll.CreateDateTime = DateTime.Now;
                                payroll.Amt = amt;

                                Manager.ORManager.StartTracking(payroll, Wilson.ORMapper.InitialState.Inserted);
                                Manager.ORManager.PersistChanges(payroll);

                                sql = string.Format(@"select * from Hr_Quote where Person={0} and IsCal='NO'", person);
                                DataTable tabDet = ConnectSql.GetTab(sql);
                                for (int j = 0; j < tabDet.Rows.Count; j++)
                                {
                                    string code = SafeValue.SafeString(tabDet.Rows[j]["PayItem"]);
                                    string des = SafeValue.SafeString(tabDet.Rows[j]["Remark"]);
                                    decimal payamt = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);
                                    decimal before = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);
                                    sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payroll.Id, code, des, payamt, name);

                                    ConnectSql.ExecuteSql(sql);
                                }
                                auto_calculate(person, payroll.Id);
                                //InsertLeave(payroll.Id, person, from, to, name);
                                C2.HrPayroll.UpdateMaster(payroll.Id);
                                #endregion
                                result = true;
                            }
                            string sql_pay_to = string.Format(@"select count(*) from Hr_Payroll where Person={0} and (FromDate>='{1}' and ToDate<='{2}')", person, firstDayOfToMonth.ToString("yyyy-MM-dd"), lastDayOfToMonth.ToString("yyyy-MM-dd"));
                            int cnt_to = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_pay_to), 0);
                            if (cnt_to == 0)
                            {
                                #region To Date
                                decimal amt = SafeValue.SafeDecimal(tab.Rows[i]["TotalAmt"]);
                                string remark = SafeValue.SafeString(tab.Rows[i]["Remark"]);

                                HrPayroll payroll = new HrPayroll();
                                payroll.Person = person;
                                payroll.FromDate = firstDayOfToMonth;
                                payroll.ToDate = lastDayOfToMonth;
                                payroll.StatusCode = "Draft";
                                payroll.Term = "";
                                payroll.Remark = "";
                                payroll.Pic = "";
                                payroll.CreateBy = name;
                                payroll.CreateDateTime = DateTime.Now;
                                payroll.Amt = amt;

                                Manager.ORManager.StartTracking(payroll, Wilson.ORMapper.InitialState.Inserted);
                                Manager.ORManager.PersistChanges(payroll);

                                sql = string.Format(@"select * from Hr_Quote where Person={0} and IsCal='NO'", person);
                                DataTable tabDet = ConnectSql.GetTab(sql);
                                for (int j = 0; j < tabDet.Rows.Count; j++)
                                {
                                    string code = SafeValue.SafeString(tabDet.Rows[j]["PayItem"]);
                                    string des = SafeValue.SafeString(tabDet.Rows[j]["Remark"]);
                                    decimal payamt = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);
                                    decimal before = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);
                                    sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payroll.Id, code, des, payamt, name);

                                    ConnectSql.ExecuteSql(sql);
                                }
                                auto_calculate(person, payroll.Id);
                                InsertLeave(payroll.Id, person, from, to, name);
                                C2.HrPayroll.UpdateMaster(payroll.Id);
                                #endregion
                                result = true;

                            }
                        }
                    }
                    if (result)
                    {
                        value = "Success!";
                    }
                }
                e.Result = value;
            }
        }
    }

    private void InsertLeave(int id, int person, string date1, string date2, string name)
    {

        string sql = string.Format(@"select Remark,LeaveType,(select Amt from Hr_Quote where PayItem='Leave') as Amt from Hr_Leave  where ApproveStatus='Approve' and Person={0} and (Date1>='{1}' and Date2<='{2}')", person, date1, date2);
        DataTable tabDet = ConnectSql.GetTab(sql);
        for (int j = 0; j < tabDet.Rows.Count; j++)
        {
            string code = "Leave";
            string des = SafeValue.SafeString(tabDet.Rows[j]["Remark"]);
            decimal payamt = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);
            decimal before = SafeValue.SafeDecimal(tabDet.Rows[j]["Amt"]);

            sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", id, code, des, payamt, name);

            ConnectSql.ExecuteSql(sql);
        }
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        DeletePayrollDet(SafeValue.SafeInt(e.Values["Id"], 0));
    }
    private void DeletePayrollDet(int id)
    {
        string sql = string.Format(@"delete from Hr_PayrollDet where PayrollId={0}", id);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void auto_calculate(int person, int payrollId)
    {
        ASPxDateEdit txt_FromDate = ASPxGridView1.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxDateEdit txt_ToDate = ASPxGridView1.FindEditFormTemplateControl("txt_ToDate") as ASPxDateEdit;
        string name = HttpContext.Current.User.Identity.Name;
        bool action = false;
        decimal salary = 0;
        DateTime now = DateTime.Now;
        string sql = string.Format(@"select Person,PayItem,Amt,Remark,IsCal from Hr_Quote where Person={0}", person);
        DataTable dt = ConnectSql_mb.GetDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(HrPerson), "Id='" + person + "'");
            HrPerson hr = C2.Manager.ORManager.GetObject(query) as HrPerson;
            if (hr != null)
            {
                DateTime birthday = hr.BirthDay;
                int age = now.Year - birthday.Year;
                if (now.Month < birthday.Month || (now.Month == birthday.Month && now.Day < birthday.Day))
                    age--;

                DataRow row = dt.Rows[i];
                string department = hr.Department;
                string payItem = SafeValue.SafeString(row["PayItem"]);
                string isCal = SafeValue.SafeString(row["IsCal"]);
                string remark = SafeValue.SafeString(row["Remark"]);
                string sql_item = string.Format(@"select Description from Hr_PayItem where (Code='{0}' or Description like '%{0}%')", payItem);
                string des = SafeValue.SafeString(ConnectSql_mb.ExecuteScalar(sql));
                if (des.ToLower().Contains("basic"))
                {
                    #region Salary
                    //if (isCal == "NO")
                    //{
                    decimal amt = Get_Incentive(hr.Name);
                    if (amt > 0)
                        salary = SafeValue.SafeDecimal(row["Amt"]) + amt;
                    else
                    {
                        amt = Get_MonthlyVC(person);
                        salary = SafeValue.SafeDecimal(row["Amt"]) + amt;
                    }
                    //}
                    if (isCal == "YES")
                    {
                        DateTime beginDate = txt_FromDate.Date;
                        DateTime resignDate = txt_ToDate.Date;

                        DateTime beginDate1 = txt_FromDate.Date;
                        DateTime resignDate1 = txt_ToDate.Date;

                        sql = string.Format(@"select top 1 BeginDate,ResignDate from Hr_PersonDet1 where Person={0} and month(ResignDate)={1} order by Id", person, txt_ToDate.Date.Month);
                        DataTable dt_det1 = ConnectSql_mb.GetDataTable(sql);
                        if (dt_det1.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt_det1.Rows.Count; j++)
                            {
                                beginDate = SafeValue.SafeDate(dt_det1.Rows[j]["BeginDate"], now);
                                resignDate = SafeValue.SafeDate(dt_det1.Rows[j]["ResignDate"], now);
                                beginDate1 = beginDate.AddDays(1 - beginDate.Day);//这个月的第一天
                                resignDate1 = resignDate.AddDays(1 - resignDate.Day).AddMonths(1).AddDays(-1);//这个月的最后一天
                            }
                        }
                        else
                        {
                            beginDate1 = beginDate.AddDays(1 - beginDate.Day);//这个月的第一天
                            resignDate1 = resignDate.AddDays(1 - resignDate.Day).AddMonths(1).AddDays(-1);//这个月的最后一天
                        }
                        sql = string.Format(@"select WorkDays from Hr_MastData where Type='Department' and Code='{0}'", department);
                        decimal workDays = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
                        TimeSpan span = resignDate - beginDate;
                        TimeSpan span1 = resignDate1 - beginDate1;
                        int days = Get_TotalWorkDays(beginDate, resignDate, workDays) + 1;
                        int days1 = Get_TotalWorkDays(beginDate1, resignDate1, workDays) + 1;


                        decimal dayRate = SafeValue.ChinaRound(salary / days1, 2);

                        salary = SafeValue.ChinaRound(dayRate * days, 2);
                        sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payrollId, payItem, remark, salary, name);

                        ConnectSql.ExecuteSql(sql);
                    }
                    action = true;
                    #endregion
                }
                if (action)
                {
                    #region CPF
                    if (payItem == "EmployeeCPF" && isCal == "YES")
                    {
                        decimal amt = 0;
                        string sql_rate = string.Format(@"select Rate from Hr_Rate where (Age<{0} and Age1>{0}) and PayItem='{1}'", age, payItem);
                        DataTable dt_rate = ConnectSql_mb.GetDataTable(sql_rate);
                        if (dt_rate.Rows.Count > 0)
                        {
                            amt = -SafeValue.ChinaRound(salary * (SafeValue.SafeDecimal(dt_rate.Rows[0]["Rate"]) / 100), 2);
                        }
                        sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payrollId, payItem, remark, amt, name);

                        ConnectSql.ExecuteSql(sql);
                    }
                    if (payItem == "EmployerCPF" && isCal == "YES")
                    {
                        decimal amt = 0;
                        string sql_rate = string.Format(@"select Rate from Hr_Rate where (Age<{0} and Age1>{0}) and PayItem='{1}'", age, payItem);
                        DataTable dt_rate = ConnectSql_mb.GetDataTable(sql_rate);
                        if (dt_rate.Rows.Count > 0)
                        {
                            amt = SafeValue.ChinaRound(salary * (SafeValue.SafeDecimal(dt_rate.Rows[0]["Rate"]) / 100), 2);
                        }
                        sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payrollId, payItem, remark, amt, name);

                        ConnectSql.ExecuteSql(sql);
                    }
                    #endregion
                    #region Monthy Variable Component
                    if (payItem == "MonthlyVC" && isCal == "YES")
                    {
                        decimal amt = Get_Incentive(hr.Name);
                        sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payrollId, payItem, remark, amt, name);
                        ConnectSql.ExecuteSql(sql);
                    }
                    #endregion
                    #region SDL
                    if (payItem == "SDL" && isCal == "YES")
                    {
                        decimal amt = SafeValue.ChinaRound(salary * SafeValue.SafeDecimal(0.0025), 2);
                        if (amt > SafeValue.SafeDecimal(11.25))
                        {
                            amt = SafeValue.SafeDecimal(11.25);
                        }
                        sql = string.Format(@"insert into Hr_PayrollDet(PayrollId,ChgCode,Description,Amt,CreateBy,CreateDateTime,Before) values({0},'{1}','{2}',{3},'{4}',getdate(),{3})", payrollId, payItem, remark, amt, name);
                        ConnectSql.ExecuteSql(sql);
                    }
                    #endregion

                }
            }
        }

    }
    private int Get_TotalWorkDays(DateTime date1, DateTime date2, decimal workDays)
    {
        int days = 0;
        TimeSpan span = date2 - date1;
        DateTime now = DateTime.Now;
        DateTime date3 = DateTime.Today;//公休起始时间
        DateTime date4 = DateTime.Today;//公休结束时间
        string sql = string.Format(@"select * from holidays where FromMonth={0}", date1.Month);
        DataTable tab = ConnectSql_mb.GetDataTable(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            int year = now.Year;
            string fromMonth = SafeValue.SafeString(tab.Rows[i]["FromMonth"]);
            if (fromMonth.Length == 1)
                fromMonth = "0" + fromMonth;
            string fromDay = SafeValue.SafeString(tab.Rows[i]["FromDay"]);
            if (fromDay.Length == 1)
                fromDay = "0" + fromDay;
            string toMonth = SafeValue.SafeString(tab.Rows[i]["ToMonth"]);
            if (toMonth.Length == 1)
                toMonth = "0" + toMonth;
            string toDay = SafeValue.SafeString(tab.Rows[i]["ToDay"]);
            if (toDay.Length == 1)
                toDay = "0" + toDay;
            string strFrom = SafeValue.SafeString(year + fromMonth + fromDay);
            string strTo = SafeValue.SafeString(year + toMonth + toDay);
            date3 = new DateTime(SafeValue.SafeInt(year, 0), SafeValue.SafeInt(fromMonth, 0), SafeValue.SafeInt(fromDay, 0));
            date4 = new DateTime(SafeValue.SafeInt(year, 0), SafeValue.SafeInt(toMonth, 0), SafeValue.SafeInt(toDay, 0));
        }
        int intDiffer = span.Days;//相差天数的int值
        for (int i = 0; i < intDiffer; i++)//从dt1开始一天天加，判断临时的日期值是不是星期六或星期天，如果既不是星期六，也不是星期天，而且也不在dt3和dt4之间，则该天为工作日，intReturn加1 
        {
            DateTime dtTemp = date1.Date.AddDays(i);
            if (workDays == 5)
            {
                if ((dtTemp.DayOfWeek != System.DayOfWeek.Sunday) && (dtTemp.DayOfWeek != System.DayOfWeek.Saturday))
                {
                    days++;
                    // if ((dtTemp.Date < date3.Date) || (dtTemp.Date > date4.Date))
                    // {
                    //     days++;
                    // }
                }
            }
            else
            {
                if ((dtTemp.DayOfWeek != System.DayOfWeek.Sunday))
                {
                    //if ((dtTemp.Date < date3.Date) || (dtTemp.Date > date4.Date))
                    //{
                    days++;
                    //}
                }
            }
        }
        return days;
    }
    private decimal Get_Incentive(string name)
    {
        ASPxDateEdit txt_FromDate = ASPxGridView1.FindEditFormTemplateControl("txt_FromDate") as ASPxDateEdit;
        ASPxDateEdit txt_ToDate = ASPxGridView1.FindEditFormTemplateControl("txt_ToDate") as ASPxDateEdit;
        string dateFrom = txt_FromDate.Date.ToString("yyyy-MM-dd");
        string dateTo = txt_ToDate.Date.ToString("yyyy-MM-dd");
        string sql = string.Format(@"with pri as (select * from ctm_MastData where [Type]='tripcode'),tb1 as (
select det2.JobNo,det2.ContainerNo,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Trip') as Incentive1,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='OverTime') as Incentive2,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='Standby') as Incentive3,
(select isnull(sum(isnull(Qty*Price,0)),0) from job_cost where TripNo=det2.Id and LineType='DP' and ChgCode='PSA') as Incentive4
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on job.jobNo=det2.JobNo
left outer join CTM_Driver as dri on dri.Code=det2.DriverCode ");
        string sql_part1 = string.Format(@")
select *,isnull(Incentive1,0)+isnull(Incentive2,0)+isnull(Incentive3,0)+isnull(Incentive4,0) as TotalIncentive from tb1");
        string where = string.Format(@" where det2.Statuscode='C' and DATEDIFF(d,det2.ToDate,'{0}')<=0 and DATEDIFF(d,det2.ToDate,'{1}')>=0", dateFrom, dateTo);
        where += string.Format(@" and det2.DriverCode='{0}'", name);
        DataTable dt_incentive = ConnectSql.GetTab(sql + where + sql_part1);
        decimal amt = 0;
        for (int j = 0; j < dt_incentive.Rows.Count; j++)
        {
            amt += SafeValue.SafeDecimal(dt_incentive.Rows[0]["TotalIncentive"]);
        }
        return amt;
    }
    private decimal Get_MonthlyVC(int person)
    {
        string sql = string.Format(@"select Amt from Hr_Quote where Person={0} and PayItem='MonthlyVC'", person);
        decimal amt = SafeValue.SafeDecimal(ConnectSql_mb.ExecuteScalar(sql));
        return amt;
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
    protected void grid_PayrollDet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeString(e.NewValues["ChgCode"], "") == "")
            throw new Exception("ChgCode not be null !!!");
        ASPxTextBox id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        e.NewValues["PayrollId"] = SafeValue.SafeInt(id.Text, 0);

        e.NewValues["Before"] = SafeValue.SafeDecimal(e.NewValues["Before"], 0);
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

    public string invoice_download_excel(string name, string DateFrom, string DateTo)
    {

        string rootPath = Directory.GetParent(HttpContext.Current.Server.MapPath("")).Parent.Parent.FullName;
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + "";
        string to_file = Path.Combine(rootPath, "files", "Excel_DailyTrips", "cpf_" + fileName + ".csv");

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


        string context = "../../../files/Excel_DailyTrips/cpf_" + fileName + ".csv";
        return context;
    }
}
