using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevExpress.Web.ASPxGridView;
using System.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;

public partial class PagesHr_Job_PayrollProcessList : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today.AddDays(7);
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "ProcessModule='HRS' and ProcessType='Payroll'";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format("ProcessDateTime >= '{0}' and ProcessDateTime <= '{1}'", dateFrom, dateTo));
        }
        Session["PayrollProcessWhere"] = where;
        this.dsPayrollProcess.FilterExpression = Session["PayrollProcessWhere"].ToString();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("PayrollProcess", true);
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        //string sql = string.Format("INSERT INTO dbo.Ref_Process ( ProcessModule, ProcessType, ProcessStatus, ProcessBy , ProcessDateTime)VALUES  ('HRS', 'Payroll', 'Open', '{0}', '{1}')", HttpContext.Current.User.Identity.Name, DateTime.Now);
        //C2.Manager.ORManager.ExecuteCommand(sql);
        //btn_search_Click(null, null);
    }
    protected void grid_PayrollProcess_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.RefProcess));
        }
    }
    protected void grid_PayrollProcess_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ProcessDateTime"] = DateTime.Now;
        e.NewValues["ProcessStatus"] = "Open";
    }
    protected void grid_PayrollProcess_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["ProcessModule"] = "HRS";
        e.NewValues["ProcessType"] = "Payroll";
        e.NewValues["ProcessDateTime"] = SafeValue.SafeDate(e.NewValues["ProcessDateTime"], DateTime.Now);

        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void grid_PayrollProcess_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["ProcessStatus"] = SafeValue.SafeString(e.NewValues["ProcessStatus"], "Open");
        e.NewValues["ProcessRemark"] = SafeValue.SafeString(e.NewValues["ProcessRemark"]);
    }
    protected void btn_Process_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Process_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string sql = "";
            string User = HttpContext.Current.User.Identity.Name;
            int id = SafeValue.SafeInt(e.CommandArgument, 0);
            DateTime processDate = new DateTime(1753, 1, 1);
            if (id > 0)
            {
                sql = string.Format("select ProcessDateTime from Ref_Process where Id='{0}'", id);
                processDate = SafeValue.SafeDate(C2.Manager.ORManager.ExecuteScalar(sql), new DateTime(1753, 1, 1));
            }
            if (processDate.Year != 1753) 
            {
                sql = "select count(id) from Hr_Person where Status='EMPLOYEE' and Id>0";
                int total = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);

                sql = string.Format(@"
insert into Hr_Payroll(Person, FromDate, ToDate, StatusCode) 
select Id,CAST('{0}' AS DATETIME)-DAY(CAST('{0}' AS DATETIME))+1,DATEADD(MONTH, 1, CAST('{0}' AS DATETIME) - DAY(CAST('{0}' AS DATETIME)) + 1) - 1,'Draft'
from Hr_Person 
WHERE Status='EMPLOYEE' AND Id>0", processDate);
                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = string.Format(@"insert into Hr_PayrollDet(PayrollId, ChgCode, Amt) 
select PR.Id,QT.PayItem,QT.Amt
from Hr_Person PS 
LEFT OUTER JOIN dbo.Hr_Quote QT ON QT.Person=PS.Id or QT.Person='0'
LEFT OUTER JOIN dbo.Hr_Payroll PR ON PS.Id=PR.Person AND YEAR(PR.FromDate)=YEAR('{0}') AND MONTH(PR.FromDate)=MONTH('{0}') 
where PS.Status='EMPLOYEE' AND PS.Id>0", processDate);
                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = string.Format(@"
update Hr_Payroll SET Amt=det.Amt 
from (select SUM(Amt) AS Amt,PayrollId as id from Hr_PayrollDet group by PayrollId) as det 
where Hr_Payroll.Id=det.id AND YEAR(Hr_Payroll.FromDate)=YEAR('{0}') AND MONTH(Hr_Payroll.FromDate)=MONTH('{0}')", processDate);
                C2.Manager.ORManager.ExecuteCommand(sql);

                sql = string.Format("update Ref_Process set ProcessStatus='Closed',ProcessBy='{0}' where Id='{1}'", User, id);
                C2.Manager.ORManager.ExecuteCommand(sql);

                string script = string.Format("<script type='text/javascript' >alert('Success generates {0} employees payroll');</script>", total);
                Response.Clear();
                Response.Write(script);

                btn_search_Click(null, null);
            }
        }
        catch (Exception)
        {
            //throw;
            string script = string.Format("<script type='text/javascript' >alert('Process failed ,please refresh and try again!');</script>");
            Response.Clear();
            Response.Write(script);
        }
    }
}