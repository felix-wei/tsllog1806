using System;
using System.Collections.Generic;
using System.Web;
using C2;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Data;

public partial class PagesHr_Job_LeaveRecord : BasePage
{
    protected void Page_Init(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today.AddDays(30);
            Session["LeaveRecord"] = null;
            this.dsLeaveRecord.FilterExpression = "1=0";
        }
        if (Session["LeaveRecord"] != null)
        {
            this.dsLeaveRecord.FilterExpression = Session["LeaveRecord"].ToString();
        }
        btn_Sch_Click(null, null);
    }
    protected void btn_Sch_Click(object sender, EventArgs e)
    {
        string id = SafeValue.SafeString(txtSchName.Value,"");
        string where = "1=1";
        string dateFrom = "";
        string dateTo = "";
        if (id.Length > 0)
            where = String.Format("Person='{0}'", id);
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.ToString("yyyy-MM-dd");
            where = GetWhere(where, string.Format(" ((Date2 >= '{0}' and Date1 <= '{1}') or (Date1>='{0}' and date1<='{1}' and Year(Date2)=1753))", dateFrom, dateTo));
        }
        this.dsLeaveRecord.FilterExpression = where;
        Session["LeaveRecord"] = where;
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse("LeaveRecord", true);
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
            grd.ForceDataRowType(typeof(C2.HrLeave));
    }
    protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format("select Id from Hr_Person where name='{0}'", user);
        int userId = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        e.NewValues["Person"] = userId;
        e.NewValues["ApplyDateTime"] = DateTime.Now;
        e.NewValues["ApproveStatus"] = "Draft";
        e.NewValues["Date1"] = DateTime.Today;
        e.NewValues["Time1"] = "AM";
        e.NewValues["Time2"] = "";
        e.NewValues["Days"] = 0;
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (SafeValue.SafeInt(e.NewValues["Person"], 0) == 0)
            throw new Exception("Pls select Person!");
        e.NewValues["CreateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (SafeValue.SafeInt(e.NewValues["Person"],0) == 0)
            throw new Exception("Pls select Person!");
        e.NewValues["Person"] = SafeValue.SafeInt(e.NewValues["Person"], 0);
        e.NewValues["Date1"] = SafeValue.SafeDate(e.NewValues["Date1"], new DateTime(1753, 1, 1));
        e.NewValues["Date2"] = SafeValue.SafeDate(e.NewValues["Date2"], new DateTime(1753, 1, 1));
        e.NewValues["Time1"] = SafeValue.SafeString(e.NewValues["Time1"]);
        e.NewValues["Time2"] = SafeValue.SafeString(e.NewValues["Time2"]);
        e.NewValues["Days"] = SafeValue.SafeString(e.NewValues["Days"], "0");
        e.NewValues["ApplyDateTime"] = SafeValue.SafeDate(e.NewValues["ApplyDateTime"], new DateTime(1753, 1, 1));
        e.NewValues["ApproveBy"] = SafeValue.SafeInt(e.NewValues["ApproveBy"], 0);
        e.NewValues["ApproveDate"] = SafeValue.SafeDate(e.NewValues["ApproveDate"], new DateTime(1753, 1, 1));
        e.NewValues["ApproveTime"] = SafeValue.SafeString(e.NewValues["ApproveTime"]);
        e.NewValues["ApproveStatus"] = SafeValue.SafeString(e.NewValues["ApproveStatus"]);
        e.NewValues["Remark"] = SafeValue.SafeString(e.NewValues["Remark"]);
        e.NewValues["ApproveRemark"] = SafeValue.SafeString(e.NewValues["ApproveRemark"]);

        e.NewValues["UpdateBy"] = HttpContext.Current.User.Identity.Name;
        e.NewValues["UpdateDateTime"] = DateTime.Now;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
}
