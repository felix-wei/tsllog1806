using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesContTrucking_Daily_DriverDaily : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["DailyDriver"] = null;
            date_search.Value = DateTime.Now;
            btn_search_Click(null, null);
        }
        if (Session["DailyDriver"] != null)
            this.dsTransport.FilterExpression = Session["DailyDriver"].ToString();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "";
        if (date_search.Date < new DateTime(1900, 1, 1))
        {
            date_search.Date = DateTime.Now;
        }
        where = string.Format(@"datediff(d,Date,'{0}')=0", date_search.Date);
        if (where != "")
        {
            Session["DailyDriver"] = where+string.Format(" AND {0}={0}", DateTime.Now.ToString("HHmmss"));
            this.dsTransport.FilterExpression = where;
        }
    }
    protected void grid_Transport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string result = check_save(SafeValue.SafeString(e.NewValues["Driver"], ""), SafeValue.SafeString(e.NewValues["Towhead"], ""), SafeValue.SafeString(e.NewValues["Date"], ""), "0");
        if (result.Length > 0)
        {
            throw new Exception(result);
        }
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Towhead"] = SafeValue.SafeString(e.NewValues["Towhead"], "");
        e.NewValues["IsActive"] = SafeValue.SafeString(e.NewValues["IsActive"], "Y");
        e.NewValues["TeamNo"] = SafeValue.SafeString(e.NewValues["TeamNo"]);
    }
    protected void grid_Transport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }

    private string check_save(string Driver, string Towhead, string date, string Id)
    {
        //if (Driver.Trim().Length == 0)
        //{
        //    return "Driver is request";
        //}
        if (date.Trim().Length == 0)
        {
            return "Date is request";
        }
        string sql = string.Format(@"select count(*) from CTM_DriverLog where Driver='{0}' and datediff(d,Date,'{1}')=0 and Id<>'{2}'", Driver, date, Id);
        int result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 1);
        if (result > 0)
        {
            return "For this Date, Driver is Existing";
        }
        sql = string.Format(@"select count(*) from CTM_Driver where Code='{0}'", Driver);
        result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
        if (result < 1)
        {
            return "Driver does not exist";
        }
        sql = string.Format(@"select count(*) from ref_vehicle where VehicleCode='{0}' and VehicleType='Towhead'", Towhead);
        result = SafeValue.SafeInt(ConnectSql.GetTab(sql).Rows[0][0], 0);
        if (result < 1)
        {
            return "Towhead does not exist";
        }
        return "";
    }

    protected void grid_Transport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxLabel lb_id = this.grid_Transport.FindEditFormTemplateControl("lb_id") as ASPxLabel;
        string result = check_save(SafeValue.SafeString(e.NewValues["Driver"], ""), SafeValue.SafeString(e.NewValues["Towhead"], ""), SafeValue.SafeString(e.NewValues["Date"], ""), lb_id.Text);
        if (result.Length > 0)
        {
            throw new Exception(result);
        }
        e.NewValues["Driver"] = SafeValue.SafeString(e.NewValues["Driver"], "");
        e.NewValues["Date"] = SafeValue.SafeDate(e.NewValues["Date"], DateTime.Now);
        e.NewValues["Towhead"] = SafeValue.SafeString(e.NewValues["Towhead"], "");
        e.NewValues["IsActive"] = SafeValue.SafeString(e.NewValues["IsActive"], "Y");
        e.NewValues["TeamNo"] = SafeValue.SafeString(e.NewValues["TeamNo"]);
    }
    protected void grid_Transport_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["IsActive"] = "Y";
        e.NewValues["Date"] = this.date_search.Date;
    }
    protected void grid_Transport_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.CtmDriverLog));
        }
    }

    protected void gridPopCont_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        object[] contNs = new object[grid.VisibleRowCount];
        object[] contTypes = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            contNs[i] = grid.GetRowValues(i, "Code");
            contTypes[i] = grid.GetRowValues(i, "TowheaderCode");
        }
        e.Properties["cpContType"] = contTypes;
        e.Properties["cpContN"] = contNs;
    }
    protected void grid_Transport_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        ASPxDropDownEdit d = grid.FindEditFormTemplateControl("DropDownEdit") as ASPxDropDownEdit;
        ASPxGridView gvlist = d.FindControl("gridPopCont") as ASPxGridView;
        string sql = string.Format(@"select Code,Name,TowheaderCode From CTM_Driver where StatusCode='Active'");
        gvlist.DataSource = C2.Manager.ORManager.GetDataSet(sql);
        gvlist.DataBind();
    }
    protected void btn_CreateAll_Click(object sender, EventArgs e)
    {
        if (this.date_search.Date > new DateTime(2010, 1, 1))
        {
            string sql = string.Format(@"insert into CTM_DriverLog (date,driver,towhead,IsActive,TeamNo) (select '{0}',Code,TowheaderCode,'Y',TeamNo from CTM_Driver where Code not in(select Driver from CTM_DriverLog where DATEDIFF(d,'{0}',Date)=0) and StatusCode='Active')", this.date_search.Date);
            int result = SafeValue.SafeInt(ConnectSql.ExecuteSql(sql), 0);
            btn_search_Click(null, null);
        }

    }
}