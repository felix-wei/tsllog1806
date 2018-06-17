using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using C2;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

public partial class PagesHr_Job_Crews : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_to.Date = DateTime.Today;
            txt_PayDate.Date = DateTime.Today;
        }
    }
    private void Bind()
    {
        string where = "";
        if (txt_from.Value != null)
        {
            where = "CONVERT(varchar(100), JobTime, 23) between '" + txt_from.Date.ToString("yyyy-MM-dd") + "' and '" + txt_from.Date.ToString("yyyy-MM-dd") + "' and Status='Pay' group by Name,JobTime";
        }
        this.dsCrews.FilterExpression = where;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
        else
        {
            string where = "";
            if (btn_Name.Text != "")
            {
                where = "and Name='" + btn_Name.Text + "'";
            }
            where += "CONVERT(varchar(100), JobTime, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "' and Status='Pay'";
            this.dsCrews.FilterExpression = where;
        }

    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobCrews));
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["PayDate"] = this.txt_PayDate.Date;
        e.NewValues["Name"] = "";
        e.NewValues["Code"] = "";
        e.NewValues["JobTime"] = this.txt_from.Date;
        e.NewValues["WorkHour"] = 0;
        e.NewValues["OtHour"] = 0;
        e.NewValues["Amount1"] = 0;
        e.NewValues["Status"] = "Pay";
        int pay = SafeValue.SafeInt(System.Configuration.ConfigurationManager.AppSettings["OTPayHour"], 0);
        e.NewValues["Amount6"] = pay;
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string name = SafeValue.SafeString(e.Values["Remark"]);
        DateTime date = SafeValue.SafeDate(e.Values["JobTime"],DateTime.Today);
        string sql = string.Format(@"update JobCrews set IsPay='N' where Remark='{0}' and CONVERT(varchar(100), JobTime, 23)='{1}'", name, date.ToString("yyyy-MM-dd"));
        int res = C2.Manager.ORManager.ExecuteCommand(sql);
        //if (res > 0)
        //{
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
        //}
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["PayDate"] = SafeValue.SafeDate(e.NewValues["PayDate"],DateTime.Now);

        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["JobTime"] = SafeValue.SafeDate(e.NewValues["JobTime"], DateTime.Now);
        e.NewValues["WorkHour"] = SafeValue.SafeDecimal(e.NewValues["WorkHour"]);
        e.NewValues["Status"] = "Pay";
        e.NewValues["IsPay"] = "Y";
        string name = SafeValue.SafeString(e.NewValues["Name"]);
        string code = SafeValue.SafeString(e.NewValues["Code"]);
        string refNo = SafeValue.SafeString(e.NewValues["RefNo"]);
        DateTime date = SafeValue.SafeDate(e.NewValues["JobTime"], DateTime.Today);
        //UpdateCrewLine(name,code,refNo,date);
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        decimal amount1 = SafeValue.SafeDecimal(e.NewValues["Amount1"]);
        decimal amount2 = SafeValue.SafeDecimal(e.NewValues["Amount2"]);
        decimal amount3 = SafeValue.SafeDecimal(e.NewValues["Amount3"]);
        decimal amount4 = SafeValue.SafeDecimal(e.NewValues["Amount4"]);
        decimal amount5 = SafeValue.SafeDecimal(e.NewValues["Amount5"]);
        decimal amount6 = SafeValue.SafeDecimal(e.NewValues["Amount6"]);
        decimal amount7 = SafeValue.SafeDecimal(e.NewValues["Amount7"]);
        decimal otHour = SafeValue.SafeDecimal(e.NewValues["OtHour"]);
        amount4 = SafeValue.ChinaRound(otHour * amount6, 2);
        amount7 = SafeValue.ChinaRound(amount1 + amount2 + amount3 + amount4 - amount5, 2);
        e.NewValues["Amount1"] = SafeValue.SafeDecimal(amount1);
        e.NewValues["Amount2"] = SafeValue.SafeDecimal(amount2);
        e.NewValues["Amount3"] = SafeValue.SafeDecimal(amount3);
        e.NewValues["Amount4"] = SafeValue.SafeDecimal(amount4);
        e.NewValues["Amount5"] = SafeValue.SafeDecimal(amount5);
        e.NewValues["Amount6"] = SafeValue.SafeDecimal(amount6);
        e.NewValues["Amount7"] = SafeValue.SafeDecimal(amount7);
        ASPxButtonEdit btn_Name = grid.FindEditRowCellTemplateControl(null, "btn_Name") as ASPxButtonEdit;
        e.NewValues["Name"] =  SafeValue.SafeString(btn_Name.Text);
        //e.NewValues["Remark"] = SafeValue.SafeString(txt_Remark.Text);
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["PayDate"] = SafeValue.SafeDate(e.NewValues["PayDate"], DateTime.Now);
        e.NewValues["Name"] = SafeValue.SafeString(e.NewValues["Name"]);
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"]);
        e.NewValues["JobTime"] = SafeValue.SafeDate(e.NewValues["JobTime"], DateTime.Now);
        e.NewValues["WorkHour"] = SafeValue.SafeDecimal(e.NewValues["WorkHour"]);
        e.NewValues["OtHour"] = SafeValue.SafeDecimal(e.NewValues["OtHour"]);
        string name = SafeValue.SafeString(e.NewValues["Name"]);
        string code = SafeValue.SafeString(e.NewValues["Code"]);
        string refNo = SafeValue.SafeString(e.NewValues["RefNo"]);
        DateTime date = SafeValue.SafeDate(e.NewValues["JobTime"], DateTime.Today);
        UpdateCrewLine(name, code, refNo, date);
        e.NewValues["IsPay"] = "Y";
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        decimal amount1 = SafeValue.SafeDecimal(e.NewValues["Amount1"]);
        decimal amount2 = SafeValue.SafeDecimal(e.NewValues["Amount2"]);
        decimal amount3 = SafeValue.SafeDecimal(e.NewValues["Amount3"]);
        decimal amount4 = SafeValue.SafeDecimal(e.NewValues["Amount4"]);
        decimal amount5 = SafeValue.SafeDecimal(e.NewValues["Amount5"]);
        decimal amount6 = SafeValue.SafeDecimal(e.NewValues["Amount6"]);
        decimal amount7 = SafeValue.SafeDecimal(e.NewValues["Amount7"]);
        decimal otHour = SafeValue.SafeDecimal(e.NewValues["OtHour"]);
        amount4=SafeValue.ChinaRound(otHour*amount6,2);
        amount7 = SafeValue.ChinaRound(amount1 + amount2 + amount3 + amount4 - amount5, 2);
        e.NewValues["Amount1"] = SafeValue.SafeDecimal(amount1);
        e.NewValues["Amount2"] = SafeValue.SafeDecimal(amount2);
        e.NewValues["Amount3"] = SafeValue.SafeDecimal(amount3);
        e.NewValues["Amount4"] = SafeValue.SafeDecimal(amount4);
        e.NewValues["Amount5"] = SafeValue.SafeDecimal(amount5);
        e.NewValues["Amount6"] = SafeValue.SafeDecimal(amount6);
        e.NewValues["Amount7"] = SafeValue.SafeDecimal(amount7);
        ASPxButtonEdit btn_Name = grid.FindEditRowCellTemplateControl(null, "btn_Name") as ASPxButtonEdit;
        //ASPxButtonEdit btn_Name = grid.FindEditRowCellTemplateControl(null, "btn_Name") as ASPxButtonEdit;
        e.NewValues["Name"] =  SafeValue.SafeString(btn_Name.Text);
        //e.NewValues["Remark"] = SafeValue.SafeString(txt_Remark.Text); 
		//D.Text("Select top 1 Remark4 from Hr_Person where Name='"+S.Text(btn_Name.Text) + "'";
    }
    private void UpdateCrewLine(string name, string code, string refNo, DateTime date)
    {
        string sql = string.Format(@"select JobNo from JobSchedule where RefNo='{0}'", refNo);
        string schNo = "";
        DataTable tab = ConnectSql.GetTab(sql);
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            schNo = SafeValue.SafeString(tab.Rows[i]["JobNo"]);
            sql = string.Format(@"select Id from JobCrews where Name='{0}' and Code='{1}' and RefNo='{2}' and CONVERT(varchar(100), JobTime, 23)='{3}'", name, code, schNo, date.ToString("yyyy-MM-dd"));
            int id = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql),0);
            if(id>0){
                sql = string.Format(@"update JobCrews set IsPay='Y' where Id={0}", id);
                ConnectSql.ExecuteScalar(sql);
            }
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string fromdate = "";
        string todate = "";
        string where = "";
        if (txt_from.Value!="")
        {
            fromdate = txt_from.Date.ToString("yyyy-MM-dd");
            todate = this.txt_from.Date.ToString("yyyy-MM-dd");
            where = "CONVERT(varchar(100), JobTime, 23) between '" + fromdate + "' and '" + todate + "' and Status='Pay'";
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_to.Date;
        }
        if (btn_Name.Text != "" && chk_All.Checked == false)
        {
            where = GetWhere(where, " Name='" + btn_Name.Text + "'");
        }
        this.dsCrews.FilterExpression = where ;

    }
    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] keyValues = new object[grid.VisibleRowCount];
        object[] name = new object[grid.VisibleRowCount];
        object[] code = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            keyValues[i] = grid.GetRowValues(i, "Id");
            name[i] = grid.GetRowValues(i, "Name");
            code[i] = grid.GetRowValues(i, "IcNo");
        }
        e.Properties["cpName"] = name;
        e.Properties["cpCode"] = code;
        e.Properties["cpKeyValues"] = keyValues;
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string s = e.Parameters;
        if(s=="Add"){
            string date = txt_from.Date.ToString("yyyy-MM-dd");
            string to = txt_to.Date.ToString("yyyy-MM-dd");
            string sql = string.Format(@"select RefNo,JobDate,JobNo from JobSchedule where CONVERT(VARCHAR(10),JobDate,120) between '{0}' and '{1}'",date,to);
            DataTable tab_job = ConnectSql.GetTab(sql);
            int res = 0;
            string payDate = txt_PayDate.Date.ToString("yyyy-MM-dd");
            if (tab_job.Rows.Count > 0)
            {
                string refNoStr = "";
                for (int m = 0; m < tab_job.Rows.Count; m++)
                {
                    DateTime jobDate = SafeValue.SafeDate(tab_job.Rows[m]["JobDate"], DateTime.Today);
                    string jobNo = SafeValue.SafeString(tab_job.Rows[m]["JobNo"]);
                    string refNo = SafeValue.SafeString(tab_job.Rows[m]["RefNo"]);
                    sql = string.Format(@"select * from JobCrews where RefNo='{0}' and HrRole='Casual' and IsPay='N'", jobNo);
                    DataTable tab = ConnectSql.GetTab(sql);
                    for (int i = 0; i < tab.Rows.Count; i++)
                    {
                        int id = SafeValue.SafeInt(tab.Rows[i]["Id"],0);
                        string code = SafeValue.SafeString(tab.Rows[i]["Code"]);
                        string name = SafeValue.SafeString(tab.Rows[i]["Name"]);
                        string tel = SafeValue.SafeString(tab.Rows[i]["Tel"]);
                        string hrRole = SafeValue.SafeString(tab.Rows[i]["HrRole"]);
                        string remark = SafeValue.SafeString(tab.Rows[i]["Remark"]);
                        sql = string.Format(@"select count(*) from JobCrews where Remark='{0}' and HrRole='Casual' and Code='{2}' and CONVERT(varchar(100), JobTime, 23)='{1}' and Status='Pay'", remark, jobDate.ToString("yyyy-MM-dd"), code);
                        int cnt=SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql),0);

                            sql = string.Format(@"select HrGroup from Hr_Person where Name='{0}'",name);
                            string type=SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                            if (type == "DAY")
                            {
                                if (cnt == 0)
                                {
                                    refNoStr = GetJobNo(name, jobDate.ToString("yyyy-MM-dd"));
                                    sql = string.Format(@"select (case when isnull(Amount1,0)=0 then 40 else Amount1 end) as Amount1,
(case when isnull(Amount2,0)=0 then 20 else Amount2 end) as Amount2,
(case when isnull(Amount3,0)=0 then 5 else Amount3 end) as Amount3 from Hr_Person where Name='{0}' and IcNo='{1}'", name, code);
                                    DataTable tab_amt = ConnectSql.GetTab(sql);
                                    decimal amt1 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount1"]);
                                    decimal amt2 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount2"]);
                                    decimal amt3 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount3"]);
                                    sql = string.Format(@"insert into JobCrews(RefNo,Code,Name,Tel,Status,JobTime,PayDate,IsPay,Amount1,Amount2,Amount3,Amount4,Amount5,Amount6,Amount7,Remark,HrRole) values('{0}','{1}','{2}','{3}','Pay','{4}','{5}','Y',{7},{8},0,0,0,{6},{9},'{10}','{11}')", refNoStr, code, name, tel, jobDate, payDate, amt3, amt1, amt2, (amt1 + amt2), remark, hrRole);
                                    res = Manager.ORManager.ExecuteCommand(sql);
                                    if (res > 0)
                                    {
                                        sql = string.Format(@"update JobCrews set IsPay='Y' where Id={0}", id);
                                        Manager.ORManager.ExecuteCommand(sql);
                                        e.Result = "Success";
                                    }
                                }
                                else
                                {
                                    sql = string.Format(@"update JobCrews set IsPay='Y' where Id={0}", id);
                                    Manager.ORManager.ExecuteCommand(sql);
                                }
                            }
                            if(type=="JOB"){

                                sql = string.Format(@"select RefNo from JobSchedule where JobNo='{0}'", jobNo);
                                refNoStr = SafeValue.SafeString(ConnectSql.ExecuteScalar(sql));
                                sql = string.Format(@"select (case when isnull(Amount1,0)=0 then 40 else Amount1 end) as Amount1,
(case when isnull(Amount2,0)=0 then 20 else Amount2 end) as Amount2,
(case when isnull(Amount3,0)=0 then 5 else Amount3 end) as Amount3 from Hr_Person where Name='{0}' and IcNo='{1}'", name, code);
                                DataTable tab_amt = ConnectSql.GetTab(sql);
                                decimal amt1 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount1"]);
                                decimal amt2 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount2"]);
                                decimal amt3 = SafeValue.SafeDecimal(tab_amt.Rows[0]["Amount3"]);
                                sql = string.Format(@"insert into JobCrews(RefNo,Code,Name,Tel,Status,JobTime,PayDate,IsPay,Amount1,Amount2,Amount3,Amount4,Amount5,Amount6,Amount7,Remark,HrRole) values('{0}','{1}','{2}','{3}','Pay','{4}','{5}','Y',{7},{8},0,0,0,{6},{9},'{10}','{11}')", refNoStr, code, name, tel, jobDate, payDate, amt3, amt1, amt2, (amt1 + amt2), remark, hrRole);
                                res = Manager.ORManager.ExecuteCommand(sql);
                                if (res > 0)
                                {
                                    sql = string.Format(@"update JobCrews set IsPay='Y' where Id={0}", id);
                                    Manager.ORManager.ExecuteCommand(sql);
                                    e.Result = "Success";
                                }

                            }

                        


                    }
                }
            }
            if (tab_job.Rows.Count==0)
            {
                e.Result = "Fail";
            }
        }
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }

    public string GetJobNo(string name,string date)
    {
        string sql = string.Format(@"select RefNo from JobCrews where Name='{0}' and CONVERT(varchar(100), JobTime, 23)='{1}' and HrRole='Casual'", name, date);
        DataTable tab_jobNo =ConnectSql.GetTab(sql);
        string refNoStr = "";
        string res = "";
        for (int i = 0; i < tab_jobNo.Rows.Count;i++ )
        {
            string jobNo = SafeValue.SafeString(tab_jobNo.Rows[i]["RefNo"]);
            sql =string.Format(@"select RefNo from JobSchedule where JobNo='{0}'",jobNo);
            string refNo= SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql));
            if (tab_jobNo.Rows.Count - i>1)
            {
                refNoStr +=refNo+ ",";
            }
            else
            {
                res = refNo;
            }
            
        }
        return refNoStr+res;
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = "";
        string todate = "";
        string where = "";
        if (txt_from.Value != "")
        {
            fromdate = txt_from.Date.ToString("yyyy-MM-dd");
            todate = this.txt_from.Date.ToString("yyyy-MM-dd");
            where = "CONVERT(varchar(100), JobTime, 23) between '" + fromdate + "' and '" + todate + "' and Status='Pay'";
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_to.Date;
        }
        if (btn_Name.Text != "" && chk_All.Checked == false)
        {
            where = GetWhere(where, " Name='" + btn_Name.Text + "'");
        }
        this.dsCrews.FilterExpression = where;
        gridExport.WriteXlsToResponse("CrewPayment", true);
    }
}