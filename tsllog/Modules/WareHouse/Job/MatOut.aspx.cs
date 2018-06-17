using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using C2;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxEditors;
using System.Data;
public partial class WareHouse_Job_MatOut : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_end.Date = DateTime.Today;
            string d = Request.QueryString["d"] ?? "";
            if (d != "")
            {
                this.txt_from.Date = DateTime.Parse(d);
                this.txt_end.Date = DateTime.Parse(d);
            }
            btn_search_Click(null, null);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn_search_Click(null, null);
        }
        else
        {
//            string sql = string.Format(@"select distinct job.JobDate,Note2,job.JobNo,DoType,job.RefNo,job.MoveDate,job.WorkStatus,job.Value4,job.Value3 from Materials m inner join JobSchedule job on m.RefNo=job.JobNo where (m.DoType='OUT' or ISNULL(DoType,'')='')
//");

           string where = "(CONVERT(varchar(100), JobDate, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "')";
            //sql += where;
            //DataTable tab = ConnectSql.GetTab(sql);
            //this.grid.DataSource = tab;
            //this.grid.DataBind();
           this.dsJobSchedule.FilterExpression = where + "and (WorkStatus<>'Cancel' and WorkStatus<>'Unsuccess')";
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string where = "";
        //string sql = string.Format(@"select job.JobDate,Note2,job.JobNo,DoType,job.RefNo,job.MoveDate from Materials m inner join JobSchedule job on m.RefNo=job.JobNo where (m.DoType='OUT' or ISNULL(DoType,'')='')
//");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " JobDate>= '" + dateFrom + "' and JobDate<= '" + dateTo + "'");
        }
        if (where.Length > 0)
        {
            where = GetWhere(where, " and (JobType='Local Move' or JobType='Office Move' or JobType='Outbound' or JobType='Inbound' or JobType='Storage')");
        }
        if (where.Length > 0)
        {
            where += " Order by JobNo";
            //sql += where;
        }
        //DataTable tab = ConnectSql.GetTab(sql);
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
        dsJobSchedule.FilterExpression = where;
        //btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("JobSchedule", true);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {

        string where = "";
        //string sql = string.Format(@"select * from  JobSchedule ");
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(0).ToString("yyyy-MM-dd");
            txt_date.Date = txt_from.Date;
            txt_date2.Date = txt_end.Date;
        }
        if (dateFrom.Length > 0)
        {
            where = GetWhere(where, " JobDate>= '" + dateFrom + " 00:00' and JobDate<= '" + dateTo + " 23:59' ");
        }
        if (where.Length > 0)
        {
            where += " and (WorkStatus<>'Cancel' and WorkStatus<>'Unsuccess') and len(RefNo)>0 ";
        }
        //throw new Exception(sql);
        dsJobSchedule.FilterExpression = where;
        //DataTable tab = ConnectSql.GetTab(sql);
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {
        string id = "";
        string sql = "";
        string s = e.Parameters;
    }

    public string GetRequisitionedNew(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemNew = SafeValue.SafeInt(dt.Rows[i]["RequisitionNew"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);
            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");

            if (itemNew > 0)
            {
                result += itemNew + code + "(N)" + "<br />";
            }
        }
        return result;
    }
    public string GetRequisitionedUsed(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemUsed = SafeValue.SafeInt(dt.Rows[i]["RequisitionUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");
            if (itemUsed > 0)
            {
                result += itemUsed + code + "(U)" + "<br />";
            }
        }
        return result;
    }
    public string GetAdditionalNew(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemNew = SafeValue.SafeInt(dt.Rows[i]["AdditionalNew"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);
            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");

            if (itemNew > 0)
            {
                result += itemNew + code + "(N)" + "<br />";
            }
        }
        return result;
    }
    public string GetAdditionalUsed(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemUsed = SafeValue.SafeInt(dt.Rows[i]["AdditionalUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");
            if (itemUsed > 0)
            {
                result += itemUsed + code + "(U)" + "<br />";
            }
        }
        return result;
    }
    public string GetReturnedNew(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemNew = SafeValue.SafeInt(dt.Rows[i]["ReturnedNew"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);
            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");

            if (itemNew > 0)
            {
                result += itemNew + code + "(N)" + "<br />";
            }
        }
        return result;
    }
    public string GetReturnedUsed(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemUsed = SafeValue.SafeInt(dt.Rows[i]["ReturnedUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");
            if (itemUsed > 0)
            {
                result += itemUsed + code + "(U)" + "<br />";
            }
        }
        return result;
    }
    public string GetTotalNew(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalNew>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemNew = SafeValue.SafeInt(dt.Rows[i]["TotalNew"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);
            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");

            if (itemNew > 0)
            {
                result += itemNew + code + "(N)" + "<br />";
            }
        }
        return result;
    }
    public string GetTotalUsed(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}' and TotalUsed>0", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int itemUsed = SafeValue.SafeInt(dt.Rows[i]["TotalUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");
            if (itemUsed > 0)
            {
                result += itemUsed + code + "(U)" + "<br />";
            }
        }
        return result;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grd = sender as ASPxGridView;
        if (grd != null)
        {
            grd.ForceDataRowType(typeof(C2.JobSchedule));
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["JobType"] = "OUT";
        e.NewValues["OriginAdd"] = SafeValue.SafeString(e.NewValues["OriginAdd"]);
        e.NewValues["DestinationAdd"] = SafeValue.SafeString(e.NewValues["DestinationAdd"]);
        e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
        e.NewValues["Contact"] = SafeValue.SafeString(e.NewValues["Contact"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["Value1"] = SafeValue.SafeString(e.NewValues["Value1"]);
        e.NewValues["Value2"] = SafeValue.SafeString(e.NewValues["Value2"]);
        e.NewValues["Value3"] = SafeValue.SafeString(e.NewValues["Value3"]);
        //e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
        e.NewValues["JobDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["PackRmk"] = SafeValue.SafeString(e.NewValues["PackRmk"]);
        e.NewValues["MoveRmk"] = SafeValue.SafeString(e.NewValues["MoveRmk"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["VolumneRmk"] = SafeValue.SafeString(e.NewValues["VolumneRmk"]);
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        e.NewValues["Value4"] = SafeValue.SafeString(e.NewValues["Value4"]);
        e.NewValues["Value5"] = SafeValue.SafeString(e.NewValues["Value5"]);
        e.NewValues["DateTime1"] = SafeValue.SafeDate(e.NewValues["DateTime1"], DateTime.Now);
        e.NewValues["DateTime2"] = SafeValue.SafeDate(e.NewValues["DateTime2"], DateTime.Now);
        e.NewValues["DateTime3"] = SafeValue.SafeDate(e.NewValues["DateTime3"], DateTime.Now);
        e.NewValues["DateTime4"] = SafeValue.SafeDate(e.NewValues["DateTime4"], DateTime.Now);
        e.NewValues["DateTime5"] = SafeValue.SafeDate(e.NewValues["DateTime5"], DateTime.Now);
        DateTime jobDate = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        ASPxTimeEdit date_Time = grid.FindEditRowCellTemplateControl(null, "date_Time") as ASPxTimeEdit;
        string jobTime = SafeValue.SafeDateStr(e.NewValues["JobDate"]);
        DateTime moveDate = DateTime.Parse(jobDate.ToString("yyyy-MM-dd") + " " + date_Time.Text);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(moveDate, DateTime.Now);
        string refNo = SafeValue.SafeString(e.NewValues["RefNo"]);
        e.NewValues["RefNo"] = refNo;
        C2Setup.SetNextNo("", "Materials-OUT", refNo, DateTime.Now);
        e.NewValues["WorkStatus"] = "Cancel";
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {

        DateTime dt = DateTime.Now;
        string refNo = C2Setup.GetNextSchNo("", "Materials-OUT", dt);
        e.NewValues["RefNo"] = refNo;
        e.NewValues["JobType"] = "OUT";
        e.NewValues["JobDate"] = dt;
        e.NewValues["WorkStatus"] = "Cancel";
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["WorkStatus"] = "Cancel";
        e.NewValues["RefNo"] = SafeValue.SafeString(e.NewValues["RefNo"]);
        e.NewValues["OriginAdd"] = SafeValue.SafeString(e.NewValues["OriginAdd"]);
        e.NewValues["DestinationAdd"] = SafeValue.SafeString(e.NewValues["DestinationAdd"]);
        e.NewValues["CustomerName"] = SafeValue.SafeString(e.NewValues["CustomerName"]);
        e.NewValues["Contact"] = SafeValue.SafeString(e.NewValues["Contact"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["Value1"] = SafeValue.SafeString(e.NewValues["Value1"]);
        e.NewValues["Value2"] = SafeValue.SafeString(e.NewValues["Value2"]);
        e.NewValues["Value3"] = SafeValue.SafeString(e.NewValues["Value3"]);
        //e.NewValues["WorkStatus"] = SafeValue.SafeString(e.NewValues["WorkStatus"]);
        e.NewValues["JobDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        e.NewValues["PackRmk"] = SafeValue.SafeString(e.NewValues["PackRmk"]);
        e.NewValues["MoveRmk"] = SafeValue.SafeString(e.NewValues["MoveRmk"]);
        e.NewValues["TruckNo"] = SafeValue.SafeString(e.NewValues["TruckNo"]);
        e.NewValues["VolumneRmk"] = SafeValue.SafeString(e.NewValues["VolumneRmk"]);
        e.NewValues["Note1"] = SafeValue.SafeString(e.NewValues["Note1"]);
        e.NewValues["Note2"] = SafeValue.SafeString(e.NewValues["Note2"]);
        e.NewValues["Value4"] = SafeValue.SafeString(e.NewValues["Value4"]);
        e.NewValues["Value5"] = SafeValue.SafeString(e.NewValues["Value5"]);
        e.NewValues["DateTime1"] = SafeValue.SafeDate(e.NewValues["DateTime1"], DateTime.Now);
        e.NewValues["DateTime2"] = SafeValue.SafeDate(e.NewValues["DateTime2"], DateTime.Now);
        e.NewValues["DateTime3"] = SafeValue.SafeDate(e.NewValues["DateTime3"], DateTime.Now);
        e.NewValues["DateTime4"] = SafeValue.SafeDate(e.NewValues["DateTime4"], DateTime.Now);
        e.NewValues["DateTime5"] = SafeValue.SafeDate(e.NewValues["DateTime5"], DateTime.Now);
        DateTime jobDate = SafeValue.SafeDate(e.NewValues["JobDate"], DateTime.Now);
        ASPxTimeEdit date_Time = grid.FindEditRowCellTemplateControl(null, "date_Time") as ASPxTimeEdit;
        string jobTime = SafeValue.SafeDateStr(e.NewValues["JobDate"]);
        DateTime moveDate = DateTime.Parse(jobDate.ToString("yyyy-MM-dd") + " " + date_Time.Text);
        e.NewValues["MoveDate"] = SafeValue.SafeDate(moveDate, DateTime.Now);
        //e.NewValues["MoveDate"] = SafeValue.SafeDate(date_Time.Value, DateTime.Now);
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.Caption == "Status")
        {
            string closeInd = SafeValue.SafeString(this.grid.GetRowValues(e.VisibleIndex, "Value1"));
            if (closeInd == "Billed")
            {
                e.Cell.BackColor = System.Drawing.Color.Orange;
            }
            if (closeInd == "Paid")
            {
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
            if (closeInd == "Cancel")
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
            }
        }
    }
}