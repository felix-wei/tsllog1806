using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format("select Id from Hr_Person where name='{0}'", user);
        int person = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        if (!IsPostBack)
        {
            Session["TaskWhere"] = null;
            if(user.ToUpper()=="ADMIN")
                this.dsTask.FilterExpression = "Status in ('In Progress','Pending')";
            else
                this.dsTask.FilterExpression = "Status in ('In Progress','Pending') and Person='" + person + "'";
        }
        BindData();
    }
    private void BindData()
    {
        //Task DataBind
        string person = HttpContext.Current.User.Identity.Name;
        string sql = @"select Id,(select name from Hr_Person where Id=T.Person) AS Person,Date,Time,RefNo,Status,Remark from Hr_Task AS T ";
        DataTable tab_Task = ConnectSql.GetTab(sql);
        this.Grid_Task.DataSource = tab_Task;
        this.Grid_Task.DataBind();

        //BOD DataBind
        sql = @" select Id,Name,Department,Email,Telephone,Birthday from Hr_Person where Status='Employee' and Id>0 and DATEDIFF(dd, GETDATE(), DATEADD(yy, DATEDIFF(yy, birthday, GETDATE())+1,birthday))>=0 and DATEDIFF(dd, GETDATE(), DATEADD(yy, DATEDIFF(yy, birthday, GETDATE())+1,birthday))<15 or (DATEDIFF(dd, GETDATE(), DATEADD(yy, DATEDIFF(yy, birthday, GETDATE()),birthday))>=0 AND DATEDIFF(dd, GETDATE(), DATEADD(yy, DATEDIFF(yy, birthday, GETDATE()),birthday))<15)";
        DataTable tab_BOD = ConnectSql.GetTab(sql);
        this.Grid_BOD.DataSource = tab_BOD;
        this.Grid_BOD.DataBind();

        //Interview DataBind
        sql = @"select Id,'Interview' as Name,Date,Department,(select name from Hr_Person where Id=T.Pic) as PIC from Hr_Interview as T where StatusCode='USE'";
        DataTable tab_Interview = ConnectSql.GetTab(sql);
        this.Grid_Interview.DataSource = tab_Interview;
        this.Grid_Interview.DataBind();

        //News DataBind
        sql = "select Id,Title,Note from Hr_PersonNews WHERE GETDATE()<ExpireDateTime";
        DataTable tab_News = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        this.rpt_News.DataSource = tab_News;
        this.rpt_News.DataBind();
    }
    protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            //string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "Status"));
            //if (closeInd == "InActive")
            //{
            //    e.Row.BackColor = System.Drawing.Color.LightBlue;
            //}
        }
    }
    protected void grid_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        int index = SafeValue.SafeInt(e.Parameters,-1);
        //string partyId = SafeValue.SafeString((this.grid.GetDataRow(index))["PartyId"]);
        //if (partyId.Length > 0)
        //{
        //    string sql = string.Format("update xxparty set Status=(case when Status='USE' then 'InActive' when Status='InActive' then 'USE' else Status end) where PartyId='{0}'", partyId); 
        //    ConnectSql.ExecuteSql(sql);
        //}
    }
    protected void grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {
        //if (e.VisibleIndex < 0) return;
        //if (e.DataColumn.FieldName == "WarningQty")
        //{
        //    DataRow row = this.grid.GetDataRow(e.VisibleIndex);
        //    int cnt = SafeValue.SafeInt(row["Cnt"], 0);
        //    if (SafeValue.SafeInt(e.CellValue, 0) < cnt)
        //        e.Cell.BackColor = System.Drawing.Color.LightPink;
        //}
        //if (e.DataColumn.FieldName == "WarningAmt")
        //{
        //    DataRow row = this.grid.GetDataRow(e.VisibleIndex);
        //    decimal cnt = SafeValue.SafeDecimal(row["Amt"], 0);
        //    if (SafeValue.SafeDecimal(e.CellValue, 0) < cnt)
        //        e.Cell.BackColor = System.Drawing.Color.LightPink;
        //}
        //if (e.DataColumn.FieldName == "BlockQty")
        //{
        //    DataRow row = this.grid.GetDataRow(e.VisibleIndex);
        //    int cnt = SafeValue.SafeInt(row["Cnt"], 0);
        //    if (SafeValue.SafeInt(e.CellValue, 0) < cnt)
        //        e.Cell.BackColor = System.Drawing.Color.LightPink;
        //}
        //if (e.DataColumn.FieldName == "BlockAmt")
        //{
        //    DataRow row = this.grid.GetDataRow(e.VisibleIndex);
        //    decimal cnt = SafeValue.SafeDecimal(row["Amt"], 0);
        //    if (SafeValue.SafeDecimal(e.CellValue, 0) < cnt)
        //        e.Cell.BackColor = System.Drawing.Color.LightPink;
        //}
    }
    protected void btn_LogOn_Click(object sender, EventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format("select Id from Hr_Person where name='{0}'", user);
        int person = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("insert into Hr_PersonLog (Person,LogDate,LogTime,[Status]) VALUES('{0}','{1}',{2},'{3}')", person, DateTime.Today, DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString(), "Begin");
        if (user.ToUpper() == "ADMIN")
            return;
        else
            C2.Manager.ORManager.ExecuteCommand(sql);
    }
    protected void btn_LogOff_Click(object sender, EventArgs e)
    {
        string user = HttpContext.Current.User.Identity.Name;
        string sql = string.Format("select Id from Hr_Person where name='{0}'", user);
        int person = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        sql = string.Format("insert into Hr_PersonLog (Person,LogDate,LogTime,[Status]) VALUES('{0}','{1}',{2},'{3}')", person, DateTime.Today, DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString(), "End");
        if (user.ToUpper() == "ADMIN")
            return;
        else
            C2.Manager.ORManager.ExecuteCommand(sql);
    }
}