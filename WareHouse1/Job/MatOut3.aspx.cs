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

public partial class WareHouse_Job_MatOut3 : System.Web.UI.Page
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
            string where = "";
            where += "CONVERT(varchar(100), JobDate, 23) between '" + txt_date.Date.ToString("yyyy-MM-dd")
               + "' and '" + txt_date2.Date.ToString("yyyy-MM-dd") + "'";
            this.dsJobSchedule.FilterExpression = where + "and JobType='OUT3'";
        }
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
            where += " and JobType='OUT3' ";
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

    public string GetMaterials(string jobNo)
    {
        string sql = string.Format(@"select * from Materials where RefNo='{0}'", jobNo);
        DataTable dt = ConnectSql.GetTab(sql);
        if (dt.Rows.Count == 0)
        {
            return "NO MAT";
        }
        string result = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int totalNew = SafeValue.SafeInt(dt.Rows[i]["TotalNew"], 0);
            int totalUsed = SafeValue.SafeInt(dt.Rows[i]["TotalUsed"], 0);
            string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);

            string des = SafeValue.SafeString(dt.Rows[i]["Description"]);
            //string code = SafeValue.SafeString(dt.Rows[i]["Code"]);
            string code = D.Text("select top 1 code from materials where description='" + des + "' and refno='JO0001010001'");

            if (totalNew > 0)
            {
                result += totalNew + code + "(N)" + "<br />";
            }
            if (totalUsed > 0)
            {
                result += totalUsed + code + "(U)" + "<br />";
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
        e.NewValues["JobType"] = "OUT3";
        e.NewValues["Value1"] = "Pending";
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
        C2Setup.SetNextNo("", "Materials-OUT3", refNo, DateTime.Now);
        e.NewValues["WorkStatus"] = "Cancel";
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        DateTime dt = DateTime.Now;
        string refNo = C2Setup.GetNextSchNo("", "Materials-OUT3", dt);
        e.NewValues["RefNo"] = refNo;
        e.NewValues["JobType"] = "OUT3";
        e.NewValues["JobDate"] = dt;
        e.NewValues["Value1"] = "Pending";
        e.NewValues["WorkStatus"] = "Cancel";
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
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
		e.NewValues["WorkStatus"] = "Cancel";
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