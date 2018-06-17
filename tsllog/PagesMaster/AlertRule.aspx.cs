using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class PagesMaster_AlertRule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
        btn_search_Click(null, null);
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        string where = "1=1";
        if (txt_Code.Text.Length > 0) {
            where = "Subject like '%" + txt_Code.Text + "%' or AlertTo like '%" + txt_Code.Text + "%' or AlertBody like '%" + txt_Code.Text + "%'";
        }
        this.dsSysAlertRule.FilterExpression = where;
    }
    protected void grid_Init(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        if (grid != null)
        {
            grid.ForceDataRowType(typeof(C2.SysAlertRule));
        }
    }
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string code = SafeValue.SafeString(e.NewValues["Code"]);
        int n= SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format(@"select count(*) from sys_alert_rule where Code='{0}'",code)), 0);
        if(n>0){
            throw new Exception("Pls keyin the Code again");
        }
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"], "");
        e.NewValues["MasterId"] = SafeValue.SafeString(e.NewValues["MasterId"], "");
        e.NewValues["AlertColumns"] = SafeValue.SafeString(e.NewValues["AlertColumns"], "");
        e.NewValues["AlertSql"] = SafeValue.SafeString(e.NewValues["AlertSql"], "");
        e.NewValues["AlertTo"] = SafeValue.SafeString(e.NewValues["AlertTo"], "");
        e.NewValues["AlertCc"] = SafeValue.SafeString(e.NewValues["AlertCc"], "");
        e.NewValues["AlertBcc"] = SafeValue.SafeString(e.NewValues["AlertBcc"], "");
        e.NewValues["AlertMobile"] = SafeValue.SafeString(e.NewValues["AlertMobile"]);
        e.NewValues["AlertBody"] = SafeValue.SafeString(e.NewValues["AlertBody"]);
        e.NewValues["AlertSubject"] = SafeValue.SafeString(e.NewValues["AlertSubject"], "");
        e.NewValues["AlertSms"] = SafeValue.SafeString(e.NewValues["AlertSms"]);

    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["Code"] = SafeValue.SafeString(e.NewValues["Code"], "");
        e.NewValues["MasterId"] = SafeValue.SafeString(e.NewValues["MasterId"], "");
        e.NewValues["AlertColumns"] = SafeValue.SafeString(e.NewValues["AlertColumns"], "");
        e.NewValues["AlertSql"] =e.NewValues["AlertSql"].ToString();
        e.NewValues["AlertTo"] = SafeValue.SafeString(e.NewValues["AlertTo"], "");
        e.NewValues["AlertCc"] = SafeValue.SafeString(e.NewValues["AlertCc"], "");
        e.NewValues["AlertBcc"] = SafeValue.SafeString(e.NewValues["AlertBcc"], "");
        e.NewValues["AlertMobile"] = SafeValue.SafeString(e.NewValues["AlertMobile"]);
        e.NewValues["AlertBody"] = SafeValue.SafeString(e.NewValues["AlertBody"]);
        e.NewValues["AlertSubject"] = SafeValue.SafeString(e.NewValues["AlertSubject"], "");
        e.NewValues["AlertSms"] = SafeValue.SafeString(e.NewValues["AlertSms"]);
        e.NewValues["SubjectPosition"] = SafeValue.SafeString(e.NewValues["SubjectPosition"]);
        e.NewValues["BodyPosition"] = SafeValue.SafeString(e.NewValues["BodyPosition"]);
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Code"] = "";
        e.NewValues["MasterId"] = "";
        e.NewValues["AlertColumns"] = "";
        e.NewValues["AlertSql"] = "";
        e.NewValues["AlertTo"] = "";
        e.NewValues["AlertCc"] = "";
        e.NewValues["AlertBcc"] = "";
        e.NewValues["AlertMobile"] = "";
        e.NewValues["AlertBody"] = "";
        e.NewValues["AlertSubject"] = "";
        e.NewValues["AlertSms"] = "";
    }
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        e.Values["Id"] = SafeValue.SafeString(e.Values["Id"]);
    }
    protected void grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
       
    }
    protected void btn_saveExcel_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
        gridExport.WriteXlsToResponse("AlertRule_" + DateTime.Now.ToString("yyyyMMdd_HHmmsss"), true);
    }
    protected void grid_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
    {

    }

    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (this.grid.EditingRowVisibleIndex > -1)
        {
            ASPxTextBox code = this.grid.FindEditRowCellTemplateControl(null, "txt") as ASPxTextBox;
            if (code != null)
            {
                code.ReadOnly = true;
                code.Border.BorderWidth = 0;
            }
        }
    }
}