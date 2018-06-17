using DevExpress.Web.ASPxEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagesTpt_Local_DispatchPlanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txt_from.Date = DateTime.Today;
            this.txt_end.Date = DateTime.Today;
            btn_Search_Click(null, null);
        }
        //OnLoad("");
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string where = "";
        string dateFrom = "";
        string dateTo = "";
        if (txt_from.Value != null && txt_end.Value != null)
        {
            dateFrom = txt_from.Date.ToString("yyyy-MM-dd");
            dateTo = txt_end.Date.AddDays(1).ToString("yyyy-MM-dd");
        }
        if (dateFrom.Length > 0 && dateTo.Length > 0)
            where = string.Format(" and BkgDate >= '{0}' and BkgDate < '{1}'", dateFrom, dateTo);
        string driver = this.btn_DriverCode.Text;
        if (driver.Length > 0)
            where = string.Format(" and Driver = '{0}'", btn_DriverCode.Text);


        string sql = string.Format(@"select Id,JobNo,JobType,BkgDate,BkgTime,TptDate,TptTime,JobProgress,TptType,Driver,VehicleNo,TripCode,Wt,M3,Qty,PickFrm1,DeliveryTo1 from tpt_job where JobProgress<>'Canceled' and len(isnull(driver,''))>0 and StatusCode<>'CNL' {0}", where);
        string jobType = SafeValue.SafeString(Request.QueryString["typ"]);
        if (jobType.Length > 0)
            sql += string.Format(" and JobType='{0}'",jobType);

        DataTable dt = ConnectSql.GetTab(sql);
        grid_Transport.DataSource = dt;
        grid_Transport.DataBind();

    }
    protected void grid_Transport_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
    {
        if (e.Parameters == "OK")
        {
            int update_error_rowcount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    string id = list[i].Id;
                    string driver = list[i].diver;
                    string vehicle = list[i].vehicle;
                    string sql = string.Format(@"update tpt_job set Driver='{1}',VehicleNo='{2}' where Id='{0}'", id,driver,vehicle);
                    ConnectSql.ExecuteSql(sql);
                }
                catch { }
            }
            if (update_error_rowcount > 0)
            {
                e.Result = update_error_rowcount + " Rows: Status is existing";
            }
        }
    }

    List<Record> list = new List<Record>();
    internal class Record
    {
        public string diver = "";
        public string vehicle = "";
        public string Id = "";

        public Record(string _Id,string _DriverCode, string _TowheadCode)
        {
            Id = _Id;
            diver = _DriverCode;
            vehicle = _TowheadCode;
        }

    }
    private void OnLoad(string t)
    {
        int start = 0;// this.ASPxGridView1.PageIndex * ASPxGridView1.SettingsPager.PageSize;
        int end = 10000;// (ASPxGridView1.PageIndex + 1) * ASPxGridView1.SettingsPager.PageSize;
        for (int i = start; i < end; i++)
        {
            ASPxCheckBox isPay = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns[0], "ack_IsPay") as ASPxCheckBox;
            ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
            ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
            ASPxTextBox Id = this.grid_Transport.FindRowCellTemplateControl(i, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "lb_Id") as ASPxTextBox;

            if (Id != null && driver != null & towhead != null && isPay != null && isPay.Checked)
            {
                list.Add(new Record(Id.Text, driver.Text, towhead.Text));
            }
            else if (Id == null)
                break;
        }
    }

    protected void grid_Transport_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data) return;

        ASPxButtonEdit driver = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["Driver"], "btn_DriverCode") as ASPxButtonEdit;
        if (driver != null)
        {
            driver.ClientInstanceName = driver.ClientInstanceName + e.VisibleIndex;
            ASPxButtonEdit towhead = this.grid_Transport.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.ASPxGridView.GridViewDataColumn)this.grid_Transport.Columns["VehicleNo"], "btn_vehicle") as ASPxButtonEdit;
            towhead.ClientInstanceName = towhead.ClientInstanceName + e.VisibleIndex;
            driver.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_Driver({0},null,{1},null);", driver.ClientInstanceName, towhead.ClientInstanceName) + "}";
            towhead.ClientSideEvents.ButtonClick = "function(s,e){" + string.Format("PopupCTM_MasterData({0},null,'Towhead');", towhead.ClientInstanceName) + "}";
        }
    }
}