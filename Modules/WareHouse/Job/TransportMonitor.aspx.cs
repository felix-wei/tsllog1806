using C2;
using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_WareHouse_Job_TransportMonitor : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today.AddDays(-30);
            this.txt_end.Date = DateTime.Today;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = " UseTransport='Yes' ";
        string sql = "select *,dbo.fun_GetPartyName(PartyId) as PartyName from Wh_DO d where ";
        string dateFrom = "";
        string dateTo = "";
        string dateRef = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (date_RefDate.Value != null)
        {
            dateRef = date_RefDate.Date.ToString("yyyy-MM-dd");
        }
        if (this.txt_CustId.Text.Length > 0)
        {
            where = GetWhere(where, "PartyId='" + this.txt_CustId.Text.Trim() + "'");
        }
        if (this.txt_CustomerRef.Text.Length > 0)
        {
            where = GetWhere(where, "CustomerReference='" + this.txt_CustomerRef.Text.Trim() + "'");
        }
        if (dateRef.Length > 0)
        {
            where = GetWhere(where, "CustomerDate='" + dateRef + "'");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
        {
            where = GetWhere(where, " DoDate >= '" + dateFrom + "' and DoDate < '" + dateTo + "'");
        }

        if (where.Length > 0)
        {
            sql += where + "  order by d.DoNo";
        }
        //throw new Exception(sql);
        //DataTable tab = ConnectSql.GetTab(sql);
        //this.grid.DataSource = tab;
        //this.grid.DataBind();
        dsIssue.FilterExpression = where;
        dsIssue.DataBind();
    }
    private string GetWhere(string where, string s)
    {
        if (where.Length > 0)
            where += " and " + s;
        else
            where = s;
        return where;
    }
    public string ShowColor(string status)
    {
        string color = "";
        if (status == "New")
        {
            color = "orange";
        }
        if (status == "Scheduled")
        {
            color = "orange";
        }
        if (status == "InTransit")
        {
            color = "green";
        }
        if (status == "Completed")
        {
            color = "blue";
        }
        if (status == "Canceled")
        {
            color = "gray";
        }
        return color;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //e.NewValues["PartyName"] = SafeValue.SafeString(e.NewValues["PartyName"]);
        //e.NewValues["TransportName"] = SafeValue.SafeString(e.NewValues["TransportName"]);
        //e.NewValues["TransportStatus"] = SafeValue.SafeString(e.NewValues["TransportStatus"]);
        //e.NewValues["DriverName"] = SafeValue.SafeString(e.NewValues["DriverName"]);
        //e.NewValues["DriverIC"] = SafeValue.SafeString(e.NewValues["DriverIC"]);
        //e.NewValues["DriverTel"] = SafeValue.SafeString(e.NewValues["DriverTel"]);
        //e.NewValues["TptJobNo"] = SafeValue.SafeString(e.NewValues["TptJobNo"]);
        //e.NewValues["TransportStart"] = SafeValue.SafeDate(e.NewValues["TransportStart"],DateTime.Today);
        //e.NewValues["Remarks"] = SafeValue.SafeString(e.NewValues["Remarks"]);

        string userId = HttpContext.Current.User.Identity.Name;
        ASPxTextBox txt_Id = grid.FindEditRowCellTemplateControl(null, "txt_Id") as ASPxTextBox;
        Wilson.ORMapper.OPathQuery query = new Wilson.ORMapper.OPathQuery(typeof(WhDo), "Id='" + txt_Id.Text + "'");
        WhDo whDo = C2.Manager.ORManager.GetObject(query) as WhDo;

        ASPxComboBox txt_TransportStatus = grid.FindEditRowCellTemplateControl(null, "txt_TransportStatus") as ASPxComboBox;
        ASPxTextBox txt_DriveName = grid.FindEditRowCellTemplateControl(null, "txt_DriveName") as ASPxTextBox;
        ASPxTextBox txt_DriverIC = grid.FindEditRowCellTemplateControl(null, "txt_DriverIC") as ASPxTextBox;
        ASPxTextBox txt_DriverTel = grid.FindEditRowCellTemplateControl(null, "txt_DriverTel") as ASPxTextBox;
        ASPxTextBox txt_VechicleNo = grid.FindEditRowCellTemplateControl(null, "txt_VechicleNo") as ASPxTextBox;
        ASPxTextBox txt_TptJobNo = grid.FindEditRowCellTemplateControl(null, "txt_TptJobNo") as ASPxTextBox;
        ASPxDateEdit date_TransportStart = grid.FindEditRowCellTemplateControl(null, "date_TransportStart") as ASPxDateEdit;
        ASPxMemo meno_Remark = grid.FindEditRowCellTemplateControl(null, "meno_Remark") as ASPxMemo;

        whDo.TransportStatus = SafeValue.SafeString(txt_TransportStatus.Text);
        whDo.DriverName = SafeValue.SafeString(txt_DriveName.Text);
        whDo.DriverIC = SafeValue.SafeString(txt_DriverIC.Text);
        whDo.DriverTel = SafeValue.SafeString(txt_DriverTel.Text);
        whDo.VehicleNo = SafeValue.SafeString(txt_VechicleNo.Text);
        whDo.TptJobNo = SafeValue.SafeString(txt_TptJobNo.Text);
        whDo.TransportStart = SafeValue.SafeDate(txt_TransportStatus.Text,DateTime.Today);
        whDo.Remarks = SafeValue.SafeString(meno_Remark.Text);
        whDo.UpdateBy = userId;
        whDo.UpdateDateTime = DateTime.Now;

        Manager.ORManager.StartTracking(whDo, Wilson.ORMapper.InitialState.Updated);
        Manager.ORManager.PersistChanges(whDo);

        btn_search_Click(null, null);
    }
}