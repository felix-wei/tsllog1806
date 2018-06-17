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


public partial class Modules_Hr_Job_PayrollRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int day = DateTime.Today.Day;
            if (day < 20)
            {
                this.txt_from.Date = DateTime.Today.AddMonths(-1).AddDays(-DateTime.Today.Day + 1);
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
        //btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {

        string id = SafeValue.SafeString(txtSchName.Value, "");
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
    protected void ASPxGridView1_CustomDataCallback1(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        ASPxTextBox Id = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
        string s = e.Parameters;
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
            string sql = string.Format(@"select Person,sum(Amt) as TotalAmt,max(Remark) as Remark from Hr_Quote group by Person");
            DataTable tab = ConnectSql.GetTab(sql);
            int fromYear = txt_from.Date.Year;
            int fromMonth = txt_from.Date.Month;
            string name = HttpContext.Current.User.Identity.Name;

            int toYear = txt_end.Date.Year;
            int toMonth = txt_end.Date.Month;
            bool result = false;
            string from = txt_from.Date.ToString("yyyy-MM-dd");
            string to = txt_end.Date.ToString("yyyy-MM-dd");

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

                        string sql_pay_to = string.Format(@"select count(*) from Hr_Payroll where Person={0} and (FromDate>='{1}' and ToDate<='{2}')", person, firstDayOfToMonth.ToString("yyyy-MM-dd"), lastDayOfToMonth.ToString("yyyy-MM-dd"));
                        int cnt_to = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_pay_to), 0);
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

                            sql = string.Format(@"select * from Hr_Quote where Person={0}", person);
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
                            #endregion
                            result = true;
                        }
                        else if (cnt_to == 0)
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

                            sql = string.Format(@"select * from Hr_Quote where Person={0}", person);
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
                            #endregion
                            result = true;

                        }
                        else
                        {
                            result = false;
                            e.Result = "Had Created Payroll!No Need Again!";
                        }
                    }
                }
                if (result)
                {
                    e.Result = "Success!";
                }
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
        string temp = invoice_download_excel(name, dateFrom, dateEnd);
        Response.Write("<script>window.open('" + temp + "');</script>");
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